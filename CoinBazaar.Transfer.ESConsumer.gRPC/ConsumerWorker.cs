namespace CoinBazaar.Transfer.ESConsumer.gRPC
{
    using System;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using CoinBazaar.Infrastructure.Camunda;
    using CoinBazaar.Infrastructure.EventBus;
    using CoinBazaar.Infrastructure.Mongo.Data;
    using CoinBazaar.Transfer.Domain;

    using EventStore.Client;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using MongoDB.Driver;

    public class ConsumerWorker : BackgroundService
    {
        private readonly ILogger<ConsumerWorker> _logger;
        private readonly EventStorePersistentSubscriptionsClient _eventStorePersistentSubscription;
        private readonly EventStoreOptions _eventStoreOptions;
        private readonly BPMContext _bpmContext;
        private readonly IBPMNRepository _bpmnRepository;
        private readonly IEventRepository _eventRepository;

        public ConsumerWorker(
            ILogger<ConsumerWorker> logger,
            EventStorePersistentSubscriptionsClient eventStorePersistentSubscription,
            EventStoreOptions eventStoreOptions,
            BPMContext bpmContext,
            IBPMNRepository bpmnRepository,
            IEventRepository eventRepository)
        {
            _logger = logger;
            _eventStorePersistentSubscription = eventStorePersistentSubscription;
            _eventStoreOptions = eventStoreOptions;
            _bpmContext = bpmContext;
            _bpmnRepository = bpmnRepository;
            _eventRepository = eventRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            try
            {
                await _eventStorePersistentSubscription.CreateAsync(
                    $"$ce-{_eventStoreOptions.AggregateStream}",
                    _eventStoreOptions.PersistentSubscriptionGroup,
                    new PersistentSubscriptionSettings(startFrom: StreamPosition.End, resolveLinkTos: true),
                    cancellationToken: stoppingToken);
            }
            catch (InvalidOperationException ex)
            {
                if (!ex.Message.Contains("AlreadyExist"))
                {
                    throw;
                }
            }


            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            await _eventStorePersistentSubscription.SubscribeAsync(
                $"$ce-{_eventStoreOptions.AggregateStream}",
                _eventStoreOptions.PersistentSubscriptionGroup,
                (_, evt, _, _) => HandleEvent(evt),
                cancellationToken: stoppingToken);
        }

        private async Task HandleEvent(ResolvedEvent @event)
        {
            if (@event.Event.EventType == "$metadata")
            {
                return;
            }

            var metadata = JsonSerializer.Deserialize<ESMetadata>(Encoding.UTF8.GetString(@event.Event.Metadata.Span));

            if (metadata?.ProcessStarter == true)
            {
                //event pointed to process starter.
                var processStarterEvent = JsonSerializer.Deserialize<ProcessStarterEvent>(Encoding.UTF8.GetString(@event.Event.Data.Span));

                var filter = Builders<Process>.Filter.Eq(x => x.ProcessId, processStarterEvent.ProcessId);

                var process = (await _bpmContext.Processes.FindAsync(filter)).FirstOrDefault();

                if (process != null)
                {
                    _logger.LogWarning($"Idempotent Process Exception. Process started with same Id. Process Id: {processStarterEvent.ProcessId}");
                    return;
                }

                process = new Process()
                {
                    ProcessId = processStarterEvent.ProcessId,
                    ProcessName = processStarterEvent.ProcessName,
                    CreationDate = DateTime.UtcNow
                };

                await _bpmContext.Processes.InsertOneAsync(process);

                _bpmnRepository.StartProcessInstance(processStarterEvent.ProcessName, processStarterEvent.ProcessParameters);
            }

            //Redirect to aggregateRoot for apply all events.

            if (metadata != null && metadata.StreamId != default)
            {
                var events = await _eventRepository.GetAllEvents(metadata.StreamId);

                var manager = new Infrastructure.Aggregates.AggregateManager<TransferAggregateRoot>();

                var aggregateRoot = manager.ApplyAll(metadata.StreamId, events);

                //Read db için aggregate kullanýlacak

                //return Task.CompletedTask;
            }
        }
    }
}

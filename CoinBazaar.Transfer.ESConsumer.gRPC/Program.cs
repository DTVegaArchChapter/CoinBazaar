namespace CoinBazaar.Transfer.ESConsumer.gRPC
{
    using CoinBazaar.Infrastructure.Camunda;
    using CoinBazaar.Infrastructure.EventBus;
    using CoinBazaar.Infrastructure.Mongo;
    using CoinBazaar.Infrastructure.Mongo.Data;
    using CoinBazaar.Infrastructure.Mongo.Data.Transfer;
    using EventStore.Client;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;

                    var eventStoreOptions = configuration.GetSection("EventStore").Get<EventStoreOptions>();
                    services.AddSingleton(eventStoreOptions);

                    services.AddSingleton(new EventStoreClient(EventStoreClientSettings.Create(eventStoreOptions.ConnectionString)));
                    services.AddSingleton<IEventStoreDbClient>(s => new EventStoreDbClient(s.GetRequiredService<EventStoreClient>()));
                    services.AddSingleton<IEventSourceRepository>(s => new EventSourceRepository(s.GetRequiredService<IEventStoreDbClient>()));

                    services.AddAutoMapper(typeof(Program));

                    var mongoConfig = new MongoServerConfig();
                    configuration.Bind(mongoConfig);

                    var bpmContext = new BPMContext(mongoConfig.MongoBPMDB);
                    var transferContext = new TransferStateModelContext(mongoConfig.MongoTransferDB);

                    services.AddSingleton(bpmContext);
                    services.AddSingleton(transferContext);

                    services.AddEventStorePersistentSubscriptionsClient(eventStoreOptions.ConnectionString);

                    services.AddSingleton<IBPMNRepository, CamundaRepository>(_ => new CamundaRepository(configuration.GetValue<string>("Camunda:EngineUrl")));

                    services.AddHostedService<ConsumerWorker>();
                });
    }
}

using CoinBazaar.Infrastructure.Camunda;
using CoinBazaar.Infrastructure.EventBus;
using CoinBazaar.Infrastructure.Mongo;
using CoinBazaar.Infrastructure.Mongo.Data;
using EventStore.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
//using System.Net.Http;

namespace CoinBazaar.Transfer.ESConsumer.gRPC
{
    using System.Net.Http;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;

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
                    IConfiguration configuration = hostContext.Configuration;

                    EventStoreOptions options = configuration.GetSection("EventStore").Get<EventStoreOptions>();
                    services.AddSingleton(options);

                    services.AddSingleton<IEventRepository, EventRepository>(
                        eventRepository => new EventRepository(
                            eventRepository.GetService<EventStoreClient>(),
                            configuration.GetValue<string>("EventStore:AggregateStream")));

                    var mongoConfig = new MongoServerConfig();
                    configuration.Bind(mongoConfig);

                    var bpmContext = new BPMContext(mongoConfig.MongoDB);

                    services.AddSingleton(bpmContext);
                    services.AddEventStorePersistentSubscriptionsClient(configuration.GetValue<string>("EventStore:ConnectionString"));

                    services.AddSingleton<IBPMNRepository, CamundaRepository>(
                        camundaRepository =>
                            new CamundaRepository(configuration.GetValue<string>("Camunda:EngineUrl")));

                    services.AddHostedService<ConsumerWorker>();
                });
    }
}

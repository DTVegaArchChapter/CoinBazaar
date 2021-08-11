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

                    EventStoreOptions eventStoreOptions = configuration.GetSection("EventStore").Get<EventStoreOptions>();

                    EventStoreOptions options = configuration.GetSection("EventStore").Get<EventStoreOptions>();
                    services.AddSingleton(options);

                    var eventStoreConnection = EventStoreClientSettings.Create(
                        connectionString: configuration.GetValue<string>("EventStore:ConnectionStringESDB"));

                    var eventStoreClient = new EventStoreClient(eventStoreConnection);

                    services.AddSingleton(eventStoreClient);

                    services.AddSingleton<IEventRepository, EventRepository>(eventRepository =>
                    new EventRepository(
                        eventRepository.GetService<EventStoreClient>(),
                        configuration.GetValue<string>("EventStore:AggregateStream"))
                    );

                    var mongoConfig = new MongoServerConfig();
                    configuration.Bind(mongoConfig);

                    var bpmContext = new BPMContext(mongoConfig.MongoDB);

                    services.AddSingleton(bpmContext);
                    services.AddEventStorePersistentSubscriptionsClient(new Uri(configuration.GetValue<string>("EventStore:ConnectionString")),
                        () =>
                        new System.Net.Http.HttpClientHandler
                        {
                            ServerCertificateCustomValidationCallback =
                                (message, certificate2, x509Chain, sslPolicyErrors) => true // ignore https
                        }
                    );

                    services.AddSingleton<IBPMNRepository, CamundaRepository>(camundaRepository =>
                    new CamundaRepository(configuration.GetValue<string>("Camunda:EngineUrl"))
                    );
                    //var eventStoreClient = new EventStorePersistentSubscriptionsClient(new EventStoreClientSettings
                    //{

                    //    CreateHttpMessageHandler = () =>
                    //        new HttpClientHandler
                    //        {
                    //            ServerCertificateCustomValidationCallback =
                    //                (message, certificate2, x509Chain, sslPolicyErrors) => true // ignore https
                    //        },
                    //    ConnectivitySettings = new EventStoreClientConnectivitySettings
                    //    {
                    //        Address = new Uri(configuration.GetValue<string>("EventStore:ConnectionString"))
                    //    }
                    //});

                    //services.AddSingleton(eventStoreClient);

                    services.AddHostedService<ConsumerWorker>();
                });
    }
}

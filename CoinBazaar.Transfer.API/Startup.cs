using Autofac;
using Autofac.Extensions.DependencyInjection;
using CoinBazaar.Infrastructure.Aggregates;
using CoinBazaar.Infrastructure.EventBus;
using CoinBazaar.Transfer.Application.CommandHandlers;
using CoinBazaar.Transfer.Application.Infrastructure.AutofacModules;
using EventStore.Client;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace CoinBazaar.Transfer.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoinBazaar.Transfer.API", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(TransferCommandHandler));

            var eventStoreConnection = EventStoreClientSettings.Create(
                connectionString: Configuration.GetValue<string>("EventStore:ConnectionString"));

            var eventStoreClient = new EventStoreClient(eventStoreConnection);

            services.AddSingleton(eventStoreClient);
            
            services.AddScoped<IEventRepository, EventRepository>(eventRepository => 
            new EventRepository(
                eventRepository.GetService<EventStoreClient>(), 
                Configuration.GetValue<string>("EventStore:AggregateStream"))
            );

            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoinBazaar.Transfer.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

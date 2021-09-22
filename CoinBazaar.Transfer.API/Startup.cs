namespace CoinBazaar.Transfer.API
{
    using Autofac;
    using Autofac.Extensions.DependencyInjection;

    using CoinBazaar.Infrastructure.EventBus;
    using CoinBazaar.Infrastructure.Mongo;
    using CoinBazaar.Infrastructure.Mongo.Data.Transfer;
    using CoinBazaar.Transfer.Application.CommandHandlers;
    using CoinBazaar.Transfer.Application.Infrastructure.AutofacModules;

    using EventStore.Client;

    using MediatR;
    using Microsoft.AspNetCore.Authentication.Certificate;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Serializers;

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

            services.AddSingleton(new EventStoreClient(EventStoreClientSettings.Create(Configuration.GetValue<string>("EventStore:ConnectionString"))));
            services.AddSingleton<IEventStoreDbClient>(s => new EventStoreDbClient(s.GetRequiredService<EventStoreClient>()));
            services.AddSingleton<IEventSourceRepository>(s => new EventSourceRepository(s.GetRequiredService<IEventStoreDbClient>()));

            var container = new ContainerBuilder();
            container.Populate(services);

            var mongoConfig = new MongoServerConfig();
            Configuration.Bind(mongoConfig);

            var transferContext = new TransferStateModelContext(mongoConfig.MongoTransferDB);
            services.AddSingleton(transferContext);

            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]));

            services.AddCors(policy =>
            {
                policy.AddPolicy("CorsPolicy", opt => opt
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services.AddAuthentication( CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoinBazaar.Transfer.API v1"));
                app.UseDeveloperExceptionPage();
                app.UseCors("CorsPolicy");
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
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

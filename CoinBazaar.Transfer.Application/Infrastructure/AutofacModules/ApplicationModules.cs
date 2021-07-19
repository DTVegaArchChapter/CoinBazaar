using Autofac;
using CoinBazaar.Transfer.Application.CommandHandlers;
using System.Reflection;

namespace CoinBazaar.Transfer.Application.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {

        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {

            //builder.Register(c => new TransferQueries(QueriesConnectionString))
            //    .As<ITransferQueries>()
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<TransferRepository>()
            //    .As<ITransferRepository>()
            //    .InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(TransferCommandHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}

using CatchMe.Infrastructure.Abstract;
using CatchMe.Infrastructure.Concrete;
using Ninject.Modules;

namespace CatchMe.Infrastructure
{
    public class InfrastructureModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IConfigurationService>().To<ConfigurationService>();
            Bind<IEmailService>().To<EmailService>();
            Bind<IClientSideLogger>().To<Logger>().WithConstructorArgument("loggerSide", "ClientSide");
            Bind<IServerSideLogger>().To<Logger>().WithConstructorArgument("loggerSide", "ServerSide");            
        }
    }
}

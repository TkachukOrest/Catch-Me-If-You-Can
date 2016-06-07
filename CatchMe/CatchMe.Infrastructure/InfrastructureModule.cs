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
            Bind<IWebApiRequestService>().To<HttpClientApiRequestService>();
            Bind<ILogger>().To<Logger>().Named("ClientSideLogger").WithConstructorArgument("appenderName", "ClientSide");
            Bind<ILogger>().To<Logger>().Named("ServerSideLogger").WithConstructorArgument("appenderName", "ServerSide");            
        }
    }
}

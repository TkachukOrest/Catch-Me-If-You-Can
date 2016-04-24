using System.Collections.Generic;
using System.Web.Http;
using CatchMe.Infrastructure;
using CatchMe.Repositories;
using CatchMe.Security;
using CatchMe.SecurityService.App_Start;
using CommonServiceLocator.NinjectAdapter.Unofficial;
using Microsoft.Owin;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(CatchMe.SecurityService.Startup))]

namespace CatchMe.SecurityService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {                        
            var startup = new Security.Startup();
            startup.ConfigureAuth(app);

            var httpConfig = ConfigureHttp();
            var kernel = ConfigureNinject(app, httpConfig);                       
        }

        public HttpConfiguration ConfigureHttp()
        {
            var httpConfig = new HttpConfiguration(); 

            httpConfig.EnableCors();            
            WebApiConfig.Register(httpConfig);

            return httpConfig;
        }

        public IKernel ConfigureNinject(IAppBuilder app, HttpConfiguration httpConfig)
        {                       
            var kernel = CreateKernel(app);

            app.UseNinjectMiddleware(() => kernel);
            app.UseNinjectWebApi(httpConfig);

            return kernel;
        }

        public IKernel CreateKernel(IAppBuilder app)
        {
            var kernel = new StandardKernel();

            var modules = new List<INinjectModule>
            {
                new RepositoryModule(),
                new InfrastructureModule(),
                new SecurityModule(),
                new SecurityServiceModule()
            };            

            kernel.Bind<IAppBuilder>().ToConstant(app);
            kernel.Load(modules);

            var ninject = new NinjectServiceLocator(kernel);
            ServiceLocator.SetLocatorProvider(() => ninject);

            return kernel;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Web;
using CatchMe.Infrastructure;
using CatchMe.MapService.GoogleMapService;
using CatchMe.Repositories.EF;
using CatchMe.WebUI;
using CommonServiceLocator.NinjectAdapter.Unofficial;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace CatchMe.WebUI
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
                
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
                
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                RegisterServiceLocator(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }
        
        private static void RegisterServices(IKernel kernel)
        {
            var modules = new List<INinjectModule>
            {              
                new EFRepositoryModule(),
                new InfrastructureModule(),
                new GoogleMapServiceModule(),
                new WebUIModule()
            };            

            kernel.Load(modules);                                 
        }

        private static void RegisterServiceLocator(IKernel kernel)
        {
            var ninjectServiceLocator = new NinjectServiceLocator(kernel);
            ServiceLocator.SetLocatorProvider(() => ninjectServiceLocator);
        }  
    }
}

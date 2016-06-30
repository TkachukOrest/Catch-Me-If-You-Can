using CatchMe.Repositories.Abstract;
using CatchMe.Repositories.EF.Abstract;
using CatchMe.Repositories.EF.Concrete;
using CatchMe.SecurityService.Code;
using Ninject.Modules;
using Ninject.Web.Common;

namespace CatchMe.SecurityService
{
    public class SecurityServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepositorySettings>().To<RepositorySettings>();
            Rebind<ICatchMeContext>().To<CatchMeContext>().InRequestScope();
        }
    }
}
using System;
using CatchMe.Repositories.Abstract;
using CatchMe.SecurityService.Code;
using Ninject.Modules;

namespace CatchMe.SecurityService
{
    public class SecurityServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepositorySettings>().To<RepositorySettings>();
        }
    }
}
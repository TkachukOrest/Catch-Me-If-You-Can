using CatchMe.Repositories.Abstract;
using CatchMe.Repositories.EF.Abstract;
using CatchMe.Repositories.EF.Concrete;
using Ninject.Modules;

namespace CatchMe.Repositories.EF
{
    public class EFRepositoryModule : NinjectModule
    {         
        public override void Load()
        {
            Bind<ICatchMeContext>().To<CatchMeContext>();
            Bind<ITripRepository>().To<TripRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IRoleRepository>().To<RoleRepository>();
        }
    }
}

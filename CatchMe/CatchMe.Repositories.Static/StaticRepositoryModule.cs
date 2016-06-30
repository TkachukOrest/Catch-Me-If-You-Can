using CatchMe.Repositories.Abstract;
using CatchMe.Repositories.Static.Concrete;
using Ninject.Modules;

namespace CatchMe.Repositories.Static
{
    public class SqlRepositoryModule : NinjectModule
    {
        public override void Load()
        {            
            Bind<ITripRepository>().To<StaticTripRepository>();
            Bind<IUserRepository>().To<StaticUserRepository>();
        }
    }
}

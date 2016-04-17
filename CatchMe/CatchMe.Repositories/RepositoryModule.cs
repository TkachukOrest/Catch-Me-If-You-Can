using CatchMe.Repositories.Abstract;
using CatchMe.Repositories.Sql;
using CatchMe.Repositories.Static;
using Ninject.Modules;

namespace CatchMe.Repositories
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRoleRepository>().To<RoleRepository>();
            Bind<ITripRepository>().To<StaticTripRepository>();
            Bind<IUserRepository>().To<UserRepository>();
        }
    }
}

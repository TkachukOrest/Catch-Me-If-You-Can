using CatchMe.Repositories.Abstract;
using CatchMe.Repositories.Sql.Concrete;
using Ninject.Modules;

namespace CatchMe.Repositories.Sql
{
    public class SqlRepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRoleRepository>().To<RoleRepository>();
            Bind<ITripRepository>().To<TripRepository>();
            Bind<IUserRepository>().To<UserRepository>();
        }
    }
}

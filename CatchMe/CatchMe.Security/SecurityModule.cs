using System.Web;
using CatchMe.Security.Abstract;
using CatchMe.Security.Concrete;
using CatchMe.Security.Configurations;
using CatchMe.Security.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ninject.Modules;

namespace CatchMe.Security
{
    public class SecurityModule : NinjectModule
    {
        public override void Load()
        {            
            Bind<IRoleStorageService<IdentityRole>>().To<RoleStorageService>();
            Bind<IUserStorageService<IdentityUser>>().To<UserStorageService>();
            
            Bind<IAuthenticationManager>().ToMethod( context => HttpContext.Current.GetOwinContext().Authentication);
            Bind<IdentityUserManager>().ToMethod(context => HttpContext.Current.GetOwinContext().GetUserManager<IdentityUserManager>());

            Bind<IUserStore<IdentityUser>>().To<UserStorageService>();
            Bind<UserManager<IdentityUser>>().ToMethod(context => HttpContext.Current.GetOwinContext().GetUserManager<IdentityUserManager>());            
        }
    }
}

using System.Web;
using CatchMe.Security.Abstract;
using CatchMe.Security.Concrete;
using CatchMe.Security.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using Owin;

namespace CatchMe.Security
{
    public class SecurityModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRoleStorageService<IdentityRole>>().To<RoleStorageService>();
            Bind<IUserStorageService<IdentityUser>>().To<UserStorageService>();

            Bind<IAuthenticationManager>().ToMethod(x => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            Bind<IdentityUserManager>().ToMethod(x => HttpContext.Current.GetOwinContext().GetUserManager<IdentityUserManager>()).InRequestScope();
            Bind<IUserStore<IdentityUser, int>>().To<UserStorageService>().InRequestScope();
            Bind<ISecureDataFormat<AuthenticationTicket>>().To<SecureDataFormat<AuthenticationTicket>>();
            Bind<IDataSerializer<AuthenticationTicket>>().To<TicketSerializer>();
            Bind<ITextEncoder>().To<Base64UrlTextEncoder>();
            Bind<IDataProtector>().ToMethod(x => x.Kernel.Get<IAppBuilder>().GetDataProtectionProvider().Create("ASP.NET Identity"));
        }
    }
}

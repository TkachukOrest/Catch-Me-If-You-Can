using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(CatchMe.WebUI.Startup))]
namespace CatchMe.WebUI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}

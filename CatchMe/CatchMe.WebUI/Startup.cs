using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CatchMe.WebUI.Startup))]

namespace CatchMe.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
using System;
using System.Web.Http;
using CatchMe.SecurityService.App_Start;

namespace CatchMe.SecurityService
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
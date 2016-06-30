using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using CatchMe.WebUI.Infrastructure.ErrorHandling;
using Microsoft.Practices.ServiceLocation;

namespace CatchMe.WebUI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {            
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{action}/{id}", 
                new { id = RouteParameter.Optional });

            config.Formatters.Remove(config.Formatters.XmlFormatter);            
        }
    }
}

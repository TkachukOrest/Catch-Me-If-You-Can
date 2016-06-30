using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Optimization;
using CatchMe.WebUI.Infrastructure.ErrorHandling;
using Microsoft.Practices.ServiceLocation;

namespace CatchMe.WebUI
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var error = Server.GetLastError();

            if (!(error is HttpException))
            {
                this.HandleError(error);
            }
        }

        protected void Application_EndRequest()
        {
            if (Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                this.HandleError(Response.StatusCode);
            }
            else
            {
                var error = Server.GetLastError();
                if (error is HttpException)
                {
                    this.HandleError(error);
                }
            }
        }

        protected void HandleError(Exception error)
        {
            ServiceLocator.Current.GetInstance<IErrorHandler>()
                    .HandleError(new HttpContextWrapper(HttpContext.Current), error);
        }

        protected void HandleError(int statusCode)
        {
            ServiceLocator.Current.GetInstance<IErrorHandler>()
                    .HandleError(new HttpContextWrapper(HttpContext.Current), statusCode);
        }
    }
}
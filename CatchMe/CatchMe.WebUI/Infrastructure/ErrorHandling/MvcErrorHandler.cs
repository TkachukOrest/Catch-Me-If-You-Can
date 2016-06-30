using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CatchMe.Infrastructure.Abstract;
using Newtonsoft.Json.Linq;
using Ninject;

namespace CatchMe.WebUI.Infrastructure.ErrorHandling
{
    public class MvcErrorHandler : IErrorHandler
    {
        private readonly ILogger _logger;
        private readonly Type _errorControllerType;

        public MvcErrorHandler([Named("ServerSideLogger")]ILogger logger, Type errorControllerType)
        {
            _logger = logger;
            _errorControllerType = errorControllerType;
        }

        public void HandleError(HttpContextBase context, Exception ex)
        {
            this.LogApplicationError(ex);            
            this.ClearHttpContext(context, GetStatusCode(ex));

            if (this.IsAjaxRequest(context.Request))
            {
                this.ReturnErrorJson(context, ex);
                return;
            }

            this.RedirectToErrorView(context, GetStatusCode(ex));
        }

        public void HandleError(HttpContextBase context, int statusCode)
        {
            this.ClearHttpContext(context, statusCode);

            if (this.IsAjaxRequest(context.Request)) { return; }

            this.RedirectToErrorView(context, statusCode);
        }

        private void LogApplicationError(Exception ex)
        {
            _logger.LogError(ex);
        }

        private void ClearHttpContext(HttpContextBase context, int statusCode)
        {
            context.Response.StatusCode = statusCode;
            context.ClearError();
            context.Response.Clear();
            context.Response.TrySkipIisCustomErrors = true;
        }

        private void RedirectToErrorView(HttpContextBase context, int statusCode)
        {
            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = GetRedirectActionName(statusCode);

            var errorController = (IController)Activator.CreateInstance(_errorControllerType);

            errorController.Execute(new RequestContext(context, routeData));
        }

        private void ReturnErrorJson(HttpContextBase context, Exception ex)
        {
            dynamic errorJson = new JObject();
            errorJson.success = false;
            errorJson.message = ex.Message;

            context.Response.ContentType = "application/json";
            context.Response.Write(errorJson.ToString());
        }

        private int GetStatusCode(Exception ex)
        {
            return (ex as HttpException)?.GetHttpCode() ?? 500;
        }

        private string GetRedirectActionName(int statusCode)
        {
            var actionName = string.Empty;

            switch (statusCode)
            {
                case 401:
                    actionName = "AccessDenied";
                    break;

                case 404:
                    actionName = "NotFound";
                    break;

                default:
                    actionName = "ServerError";
                    break;
            }

            return actionName;
        }

        private bool IsAjaxRequest(HttpRequestBase request)
        {
            return request.Headers["X-Requested-With"] != null && request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
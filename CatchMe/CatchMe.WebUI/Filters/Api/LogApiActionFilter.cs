using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using CatchMe.Infrastructure.Abstract;
using CatchMe.Infrastructure.Extensions;
using Ninject;

namespace CatchMe.WebUI.Filters.Api
{
    public class LogApiActionFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;

        public LogApiActionFilter([Named("ServerSideLogger")]ILogger logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            var sb = new StringBuilder("API Request:\n");
            sb.Append("URL: ").AppendLine(actionContext.Request.RequestUri.ToString());

            if (actionContext.ActionArguments.Any())
            {
                sb.Append("Parameters: ").AppendLine(GetRequestParams(actionContext.ActionArguments));
            }

            if (actionContext.ModelState.Any())
            {
                sb.Append("Model State: ").AppendLine(GetRequestParams(actionContext.ModelState.ToDictionary(x => x.Key, y => (object)y.Value)));
            }

            _logger.LogInfo(sb.ToString());
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);

            if (actionExecutedContext.Exception != null)
            {
                _logger.LogError(string.Format("An API request {0} returned with an exception.", actionExecutedContext.Request.RequestUri), actionExecutedContext.Exception);
                return;
            }

            var sb = new StringBuilder("API Response:\n");
            sb.Append("Status: ").AppendLine(actionExecutedContext.Response.StatusCode.ToString());

            if (actionExecutedContext.ActionContext.ActionArguments.Any())
            {
                sb.Append("Parameters: ").AppendLine(GetRequestParams(actionExecutedContext.ActionContext.ActionArguments));
            }

            if (actionExecutedContext.ActionContext.ModelState.Any())
            {
                sb.Append("Model State: ").AppendLine(GetRequestParams(actionExecutedContext.ActionContext.ModelState.ToDictionary(x => x.Key, y => (object)y.Value)));
            }

            _logger.LogInfo(sb.ToString());
        }

        private string GetRequestParams(IDictionary<string, object> dictionary)
        {
            IEnumerable<string> result = dictionary.Select(x => string.Format("{0}: {1};", x.Key, (x.Value ?? "null").ToJson()));
            return string.Join(", ", result);
        }
    }
}
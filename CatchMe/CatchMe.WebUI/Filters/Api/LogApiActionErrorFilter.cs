using System.Web.Http.Filters;
using CatchMe.Infrastructure.Abstract;
using Ninject;

namespace CatchMe.WebUI.Filters.Api
{
    public class LogApiActionErrorAttribute : FilterAttribute { }

    public class LogApiActionErrorFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public LogApiActionErrorFilter([Named("ServerSideLogger")]ILogger logger)
        {
            _logger = logger;
        }

        public override void OnException(HttpActionExecutedContext context)
        {
            _logger.LogError(context.Exception.ToString());

            base.OnException(context);
        }
    }
}

using System.Web.Http;
using CatchMe.Infrastructure.Abstract;
using CatchMe.WebUI.Models;
using Ninject;

namespace CatchMe.WebUI.Controllers.Api
{
    public class LogController : ApiController
    {
        private readonly ILogger _clientSideLogger;

        public LogController([Named("ClientSideLogger")]ILogger logger)
        {
            _clientSideLogger = logger;
        }

        [HttpPost]
        public void LogError(ClientExceptionBindingModel[] errors)
        {
            foreach (var error in errors)
            {
                _clientSideLogger.LogError("An error has been occured: \nUser: {0} \nUrl: {1} \nMessage: {2}, \nTime: {3}, \nStackTrace: {4}",
                    error.User,
                    error.Url,
                    error.Message,
                    error.Time,
                    error.StackTrace);
            }
        }
    }
}
using System.Web.Http;
using CatchMe.Infrastructure.Abstract;
using CatchMe.WebUI.Models;

namespace CatchMe.WebUI.Controllers.Api
{
    public class LogController : ApiController
    {
        private readonly IClientSideLogger _clientSideLogger;

        public LogController(IClientSideLogger logger)
        {
            _clientSideLogger = logger;
        }

        [HttpPost]
        public void LogError(ClientException[] errors)
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
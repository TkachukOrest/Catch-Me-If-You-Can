using System;

namespace CatchMe.Infrastructure.Exceptions
{
    public class WebApiFailedExceptionException: Exception
    {
        public string WebApiUrl { get; protected set; }

        public WebApiFailedExceptionException(string webApiUrl, string message) : base(message)
        {
            WebApiUrl = webApiUrl;
        }
    }
}

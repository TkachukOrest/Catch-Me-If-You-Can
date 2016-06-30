using System;
using System.Web;

namespace CatchMe.WebUI.Infrastructure.ErrorHandling
{
    public interface IErrorHandler
    {
        void HandleError(HttpContextBase context, Exception ex);

        void HandleError(HttpContextBase context, int statusCode);
    }
}
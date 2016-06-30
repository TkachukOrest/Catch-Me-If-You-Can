using System.Web.Mvc;

namespace CatchMe.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult ServerError()
        {
            return View("ServerError");
        }

        [HttpGet]        
        public ActionResult NotFound()
        {
            return View("NotFound");
        }

        [HttpGet]
        public ActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
    }
}
using System.Configuration;
using System.Web.Mvc;

namespace CatchMe.WebUI.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.GoogleMapsApiDeveloperKey = ConfigurationManager.AppSettings["GoogleMapsApiDeveloperKey"];

            return View();
        }
    }
}
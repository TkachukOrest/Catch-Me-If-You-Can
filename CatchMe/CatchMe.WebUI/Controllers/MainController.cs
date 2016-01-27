using System.Web.Mvc;

namespace CatchMe.WebUI.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }        
    }
}
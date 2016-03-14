using System.Web.Mvc;
using CatchMe.MapService;


namespace CatchMe.WebUI.Controllers
{
    public class MainController : Controller
    {
        private readonly IMapService _mapService;        

        public MainController(IMapService mapService)
        {
            _mapService = mapService;
        }

        public ActionResult Index()
        {
            ViewBag.MapsApiSource = _mapService.GetApiUrl();

            return View();
        }
    }
}
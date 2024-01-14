using Microsoft.AspNetCore.Mvc;

namespace MapTrackingWithSignalR.Web.Controllers
{
    public class MapController : Controller
    {
        private readonly IConfiguration _configuration;

        public MapController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.ApiBaseUrl = _configuration["ApiBaseUrl"];

            return View();
        }
    }
}

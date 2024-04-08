namespace TruckManagerSoftware.Areas.Unauthorized.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using static Common.DataConstants.DataConstants.Unauthorized;

    [Area(UnauthorizedAreaName)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

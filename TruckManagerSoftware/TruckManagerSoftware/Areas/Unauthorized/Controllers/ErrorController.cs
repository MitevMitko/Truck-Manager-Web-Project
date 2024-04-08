namespace TruckManagerSoftware.Areas.Unauthorized.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using static Common.DataConstants.DataConstants.Unauthorized;

    [Area(UnauthorizedAreaName)]
    public class ErrorController : Controller
    {
        public IActionResult NotFound404()
        {
            return View();
        }

        public IActionResult BadRequest500()
        {
            return View();
        }
    }
}

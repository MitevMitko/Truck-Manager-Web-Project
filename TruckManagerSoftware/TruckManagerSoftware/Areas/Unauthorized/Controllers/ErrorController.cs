namespace TruckManagerSoftware.Areas.Unauthorized.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area("Unauthorized")]
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

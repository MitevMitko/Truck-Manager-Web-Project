namespace TruckManagerSoftware.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Home;

    using static Common.DataConstants.DataConstants.Admin;

    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BadRequest500(string errorMessage)
        {
            return View(new ErrorInfoViewModel()
            {
                MessageInfo = errorMessage
            });
        }

        public IActionResult NotFound404()
        {
            return View();
        }
    }
}

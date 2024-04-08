namespace TruckManagerSoftware.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Engine;
    using Core.Services.Contract;

    using static Common.DataConstants.DataConstants.User;
    using static Common.Messages.Messages.Common;

    [Area(UserAreaName)]
    [Authorize(Roles = UserRoleName)]
    public class EngineController : Controller
    {
        private readonly IEngineService engineService;

        public EngineController(IEngineService engineService)
        {
            this.engineService = engineService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Service which returns
                // All engines from the database
                ICollection<EngineInfoViewModel> serviceModel = await engineService.GetAllEnginesInfo();

                return View(serviceModel);
            }
            catch (Exception)
            {
                TempData["ExceptionMessage"] = SomethingWentWrongMessage;

                return RedirectToAction("Index", "Home");
            }
        }
    }
}

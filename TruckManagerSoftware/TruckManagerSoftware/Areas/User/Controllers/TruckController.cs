namespace TruckManagerSoftware.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Truck;
    using Core.Services.Contract;

    using static Common.Messages.Messages.Common;

    [Area("User")]
    [Authorize]
    public class TruckController : Controller
    {
        private readonly ITruckService truckService;

        public TruckController(ITruckService truckService)
        {
            this.truckService = truckService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Service which returns
                // All trucks from the database
                ICollection<TruckInfoViewModel> serviceModel = await truckService.GetAllTrucksInfo();

                return View(serviceModel);
            }
            catch (Exception)
            {
                TempData["ExceptionMessage"] = SomethingWentWrongMessage;

                return View("Index", "Home");
            }
        }
    }
}

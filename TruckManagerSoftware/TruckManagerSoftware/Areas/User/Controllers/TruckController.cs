namespace TruckManagerSoftware.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Truck;
    using Core.Services.Contract;

    using static Common.DataConstants.DataConstants.User;
    using static Common.Messages.Messages.Common;

    [Area(UserAreaName)]
    [Authorize(Roles = UserRoleName)]
    public class TruckController : Controller
    {
        private readonly ITruckService truckService;

        public TruckController(ITruckService truckService)
        {
            this.truckService = truckService;
        }

        [HttpGet]
        public IActionResult All()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Service which returns
                // All trucks from the database
                ICollection<TruckInfoViewModel> serviceModel = await truckService.GetAllTrucksInfo();

                return Json(serviceModel);
            }
            catch (Exception)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = SomethingWentWrongMessage });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAdditionalInfoById(Guid id)
        {
            try
            {
                // Get additional truck info
                // By id from the database
                TruckAdditionalInfoViewModel serviceModel = await truckService.GetAdditionalTruckInfoById(id);

                return View(serviceModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ChooseTruckOrder(Guid id)
        {
            try
            {
                // Service which returns
                // Additional info for
                // Truck with property Id == id
                TruckAdditionalInfoViewModel serviceModel = await truckService.GetAdditionalTruckInfoById(id);

                return View(serviceModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }
    }
}

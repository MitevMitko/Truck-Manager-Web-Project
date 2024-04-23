namespace TruckManagerSoftware.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Garage;
    using Core.Models.Trailer;
    using Core.Models.Truck;
    using Core.Services.Contract;

    using static Common.DataConstants.DataConstants.User;
    using static Common.Messages.Messages.Common;
    using static Common.Messages.Messages.Garage;

    [Area(UserAreaName)]
    [Authorize(Roles = UserRoleName)]
    public class GarageController : Controller
    {
        private readonly IGarageService garageService;

        private readonly ITrailerService trailerService;

        private readonly ITruckService truckService;

        public GarageController(IGarageService garageService,
            ITrailerService trailerService,
            ITruckService truckService)
        {
            this.garageService = garageService;
            this.trailerService = trailerService;
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
                // All garages from the database
                ICollection<GarageInfoViewModel> serviceModel = await garageService.GetAllGaragesInfo();

                return Json(serviceModel);
            }
            catch (Exception)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = SomethingWentWrongMessage });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GarageTrucksTrailers(Guid id)
        {
            try
            {
                // Service which returns
                // Garage with property Id == id
                GarageInfoViewModel serviceModel = await garageService.GetGarageInfoById(id);

                return View(serviceModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetGarageTrucksTrailers(Guid id)
        {
            try
            {
                ICollection<GarageTruckTrailerInfoViewModel> serviceModel = await garageService.GetGarageTrucksTrailersInfo(id);

                return Json(serviceModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAdditionalGarageTruckInfoById(Guid id)
        {
            try
            {
                // Get additional garage's truck info
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
        public async Task<IActionResult> GetAdditionalGarageTrailerInfoById(Guid id)
        {
            try
            {
                // Get additional garage's trailer info
                // By id from the database
                TrailerAdditionalInfoViewModel serviceModel = await trailerService.GetAdditionalTrailerInfoById(id);

                return View(serviceModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddGarageTruckToUser(Guid id, Guid truckId)
        {
            try
            {
                // Service which adds truck
                // With property Id == truckId
                // To user with property Id == id
                await garageService.AddGarageTruckToUser(id, truckId);

                TempData["Message"] = GarageTruckSuccessfullyAddedToUserMessage;

                return RedirectToAction("GetById", "User", new { id = id });
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> RemoveGarageTruckFromUser(Guid id, Guid truckId)
        {
            try
            {
                // Service which removes truck
                // With property Id == truckId
                // From the user with property Id == id
                await garageService.RemoveGarageTruckFromUser(id, truckId);

                TempData["Message"] = GarageTruckSuccessfullyRemovedFromUserMessage;

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }
    }
}

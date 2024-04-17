namespace TruckManagerSoftware.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Garage;
    using Core.Models.Trailer;
    using Core.Models.Truck;
    using Core.Services.Contract;

    using static Common.DataConstants.DataConstants.Admin;
    using static Common.Messages.Messages.Common;
    using static Common.Messages.Messages.Garage;

    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
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
        public IActionResult Add()
        {
            // Return add garage view model
            return View(new AddGarageViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddGarageViewModel model)
        {
            // If the entered data
            // Is not valid
            // Return the model
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Service which is adding
                // Garage to the database
                await garageService.AddGarage(model);

                TempData["Message"] = GarageSuccessfullyCreatedMessage;

                return RedirectToAction("All", "Garage");
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                // Get garage by id
                // From the database
                GarageInfoViewModel garageInfo = await garageService.GetGarageInfoById(id);

                // Return edit garage view model
                return View(new EditGarageViewModel()
                {
                    Id = garageInfo.Id,
                    Country = garageInfo.Country,
                    City = garageInfo.City,
                    Size = garageInfo.Size
                });
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditGarageViewModel model)
        {
            // If the entered data
            // Is not valid
            // Return the model
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Service which is editing
                // The garage with Id == model.Id
                await garageService.EditGarage(model);

                TempData["Message"] = GarageSuccessfullyEditedMessage;

                return RedirectToAction("All", "Garage");
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                // Service which is removing
                // Garage from the database
                await garageService.RemoveGarage(id);

                TempData["Message"] = GarageSuccessfullyRemovedMessage;

                return RedirectToAction("All", "Garage");
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
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
        public async Task<IActionResult> GarageTrucks(Guid id)
        {
            try
            {
                // Service which returns
                // garage with property Id == id
                GarageInfoViewModel garageInfo = await garageService.GetGarageInfoById(id);

                return View(garageInfo);
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetGarageTrucks(Guid id)
        {
            try
            {
                ICollection<GarageTruckInfoViewModel> serviceModel = await garageService.GetGarageTrucksInfo(id);

                return Json(serviceModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GarageTrailers(Guid id)
        {
            try
            {
                // Service which returns
                // garage with property Id == id
                GarageInfoViewModel garageInfo = await garageService.GetGarageInfoById(id);

                return View(garageInfo);
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetGarageTrailers(Guid id)
        {
            try
            {
                ICollection<GarageTrailerInfoViewModel> serviceModel = await garageService.GetGarageTrailersInfo(id);

                return Json(serviceModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
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
        public async Task<IActionResult> ChooseTruckTrailer(Guid id)
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
        public async Task<IActionResult> GetGarageTrailersWithoutTruckId(Guid id)
        {
            try
            {
                // Service which returns
                // trailers with property
                // TruckId == null
                // And property GarageId == id
                ICollection<TrailerInfoViewModel> serviceModel = await trailerService.GetAllTrailersInfoByGarageIdWithoutTruckId(id);

                return Json(serviceModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddGarageTruckToGarageTrailer(Guid id, Guid trailerId)
        {
            try
            {
                // Service which adds trailer
                // With property Id == trailerId
                // To truck with property Id == id
                await garageService.AddGarageTruckToGarageTrailer(id, trailerId);

                TempData["Message"] = GarageTrailerSuccessfullyAddedToGarageTruckMessage;

                return RedirectToAction("All", "Garage");
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> RemoveGarageTruckFromGarageTrailer(Guid id, Guid trailerId)
        {
            try
            {
                // Service which removes trailer
                // With property Id == trailerId
                // From truck with property Id == id
                await garageService.RemoveGarageTruckFromGarageTrailer(id, trailerId);

                TempData["Message"] = GarageTrailerSuccessfullyRemovedFromGarageTruckMessage;

                return RedirectToAction("All", "Garage" );
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }
    }
}

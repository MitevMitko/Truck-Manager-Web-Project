namespace TruckManagerSoftware.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Engine;
    using Core.Models.Garage;
    using Core.Models.Transmission;
    using Core.Models.Truck;
    using Core.Services.Contract;

    using static Common.DataConstants.DataConstants.Admin;
    using static Common.Messages.Messages.Common;
    using static Common.Messages.Messages.Truck;

    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class TruckController : Controller
    {
        private readonly IEngineService engineService;

        private readonly IGarageService garageService;

        private readonly ITransmissionService transmissionService;

        private readonly ITruckService truckService;

        public TruckController(IEngineService engineService,
            IGarageService garageService,
            ITransmissionService transmissionService,
            ITruckService truckService)
        {
            this.engineService = engineService;
            this.garageService = garageService;
            this.transmissionService = transmissionService;
            this.truckService = truckService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                // Service which returns
                // All garages with free space
                // For trucks from the database
                ICollection<GarageInfoViewModel> garagesWithFreeSpaceForTrucks = await garageService.GetAllGaragesInfoWithFreeSpaceForTrucks();

                // Service which returns
                // All engines from the database
                ICollection<EngineInfoViewModel> engines = await engineService.GetAllEnginesInfo();

                // Service which returns
                // All transmissions from the database
                ICollection<TransmissionInfoViewModel> transmissions = await transmissionService.GetAllTransmissionsInfo();

                // Return add truck view model
                return View(new AddTruckViewModel()
                {
                    Garages = garagesWithFreeSpaceForTrucks,
                    Engines = engines,
                    Transmissions = transmissions
                });
            }
            catch (Exception)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = SomethingWentWrongMessage });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTruckViewModel model)
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
                // Truck to the database
                await truckService.AddTruck(model);

                TempData["Message"] = TruckSuccessfullyCreatedMessage;

                return RedirectToAction("All", "Truck");
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
                // Get truck by id
                // From the database
                TruckInfoViewModel truckInfo = await truckService.GetTruckInfoById(id);

                // Service which returns
                // All garages with free space
                // For trucks from the database
                ICollection<GarageInfoViewModel> garagesWithFreeSpaceForTrucks = await garageService.GetAllGaragesInfoWithFreeSpaceForTrucks();

                // Service which returns
                // All engines from the database
                ICollection<EngineInfoViewModel> engines = await engineService.GetAllEnginesInfo();

                // Service which returns
                // All transmissions from the database
                ICollection<TransmissionInfoViewModel> transmissions = await transmissionService.GetAllTransmissionsInfo();

                // Return edit truck view model
                return View(new EditTruckViewModel()
                {
                    Id = truckInfo.Id,
                    Brand = truckInfo.Brand,
                    Series = truckInfo.Series,
                    DrivenDistance = truckInfo.DrivenDistance,
                    GarageId = truckInfo.GarageId,
                    EngineId = truckInfo.EngineId,
                    TransmissionId = truckInfo.TransmissionId,
                    Garages = garagesWithFreeSpaceForTrucks,
                    Engines = engines,
                    Transmissions = transmissions
                });
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTruckViewModel model)
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
                // The truck with Id == model.Id
                await truckService.EditTruck(model);

                TempData["Message"] = TruckSuccessfullyEditedMessage;

                return RedirectToAction("All", "Truck");
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
                // Truck from the database
                await truckService.RemoveTruck(id);

                TempData["Message"] = TruckSuccessfullyRemovedMessage;

                return RedirectToAction("All", "Truck");
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
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                // Get truck by id
                // From the database
                TruckInfoViewModel serviceModel = await truckService.GetTruckInfoById(id);

                return View(serviceModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
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
    }
}

namespace TruckManagerSoftware.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Garage;
    using Core.Services.Contract;

    using static Common.DataConstants.DataConstants.Admin;
    using static Common.Messages.Messages.Common;
    using static Common.Messages.Messages.Garage;

    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class GarageController : Controller
    {
        private readonly IGarageService garageService;

        public GarageController(IGarageService garageService)
        {
            this.garageService = garageService;
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

                return RedirectToAction("GetAll", "Garage");
            }
            catch (Exception ex)
            {
                TempData["ExceptionMessage"] = ex.Message;

                return View(model);
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
                TempData["ExceptionMessage"] = ex.Message;

                return RedirectToAction("GetAll", "Garage");
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

                return RedirectToAction("GetAll", "Garage");
            }
            catch (Exception ex)
            {
                TempData["ExceptionMessage"] = ex.Message;

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                // Service which is removing
                // Garage from the database
                await garageService.RemoveGarage(id);

                TempData["Message"] = GarageSuccessfullyRemovedMessage;

                return RedirectToAction("GetAll", "Garage");
            }
            catch (Exception ex)
            {
                TempData["ExceptionMessage"] = ex.Message;

                return RedirectToAction("GetAll", "Garage");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Service which returns
                // All garages from the database
                ICollection<GarageInfoViewModel> serviceModel = await garageService.GetAllGaragesInfo();

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

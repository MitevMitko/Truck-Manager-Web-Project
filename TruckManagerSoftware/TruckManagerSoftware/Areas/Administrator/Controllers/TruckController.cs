namespace TruckManagerSoftware.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Truck;
    using Core.Services.Contract;

    using static Common.DataConstants.DataConstants.Admin;
    using static Common.Messages.Messages.Common;
    using static Common.Messages.Messages.Truck;

    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class TruckController : Controller
    {
        private readonly ITruckService truckService;

        public TruckController(ITruckService truckService)
        {
            this.truckService = truckService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            // Return add truck view model
            return View(new AddTruckViewModel());
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
                TempData["ExceptionMessage"] = ex.Message;

                return View(model);
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

                // Return edit truck view model
                return View(new EditTruckViewModel()
                {
                    Id = truckInfo.Id,
                    Brand = truckInfo.Brand,
                    Series = truckInfo.Series,
                    DrivenDistance = truckInfo.DrivenDistance
                });
            }
            catch (Exception ex)
            {
                TempData["ExceptionMessage"] = ex.Message;

                return RedirectToAction("All", "Truck");
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
                TempData["ExceptionMessage"] = ex.Message;

                return View(model);
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
                TempData["ExceptionMessage"] = ex.Message;

                return RedirectToAction("All", "Truck");
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
                TempData["ExceptionMessage"] = SomethingWentWrongMessage;

                return View("Index", "Home");
            }
        }
    }
}

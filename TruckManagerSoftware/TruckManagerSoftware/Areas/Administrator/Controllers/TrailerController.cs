namespace TruckManagerSoftware.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Trailer;
    using Core.Services.Contract;

    using static Common.DataConstants.DataConstants.Admin;
    using static Common.Messages.Messages.Common;
    using static Common.Messages.Messages.Trailer;
    using TruckManagerSoftware.Core.Models.Garage;
    using TruckManagerSoftware.Core.Services.Implementation;

    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class TrailerController : Controller
    {
        private readonly IGarageService garageService;

        private readonly ITrailerService trailerService;

        public TrailerController(IGarageService garageService, ITrailerService trailerService)
        {
            this.garageService = garageService;
            this.trailerService = trailerService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // Service which returns
            // All garages with free space
            // For trailers from the database
            ICollection<GarageInfoViewModel> garagesWithFreeSpaceForTrailers = await garageService.GetAllGaragesInfoWithFreeSpaceForTrailers();

            // Return add trailer view model
            return View(new AddTrailerViewModel()
            {
                Garages = garagesWithFreeSpaceForTrailers
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTrailerViewModel model)
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
                // Trailer to the database
                await trailerService.AddTrailer(model);

                TempData["Message"] = TrailerSuccessfullyCreatedMessage;

                return RedirectToAction("All", "Trailer");
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
                // Get trailer by id
                // From the database
                TrailerInfoViewModel trailerInfo = await trailerService.GetTrailerInfoById(id);

                // Service which returns
                // All garages with free space
                // For trailers from the database
                ICollection<GarageInfoViewModel> garagesWithFreeSpaceForTrailers = await garageService.GetAllGaragesInfoWithFreeSpaceForTrailers();

                // Return edit trailer view model
                return View(new EditTrailerViewModel()
                {
                    Id = trailerInfo.Id,
                    Title = trailerInfo.Title,
                    Series = trailerInfo.Series,
                    TrailerType = trailerInfo.TrailerType,
                    BodyType = trailerInfo.BodyType,
                    TareWeight = trailerInfo.TareWeight,
                    AxleCount = trailerInfo.AxleCount,
                    TotalLength = trailerInfo.TotalLength,
                    CargoTypes = trailerInfo.CargoTypes,
                    GarageId = trailerInfo.GarageId,
                    Garages = garagesWithFreeSpaceForTrailers
                });
            }
            catch (Exception ex)
            {
                TempData["ExceptionMessage"] = ex.Message;

                return RedirectToAction("All", "Trailer");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTrailerViewModel model)
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
                // The trailer with Id == model.Id
                await trailerService.EditTrailer(model);

                TempData["Message"] = TrailerSuccessfullyEditedMessage;

                return RedirectToAction("All", "Trailer");
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
                // Trailer from the database
                await trailerService.RemoveTrailer(id);

                TempData["Message"] = TrailerSuccessfullyRemovedMessage;

                return RedirectToAction("All", "Trailer");
            }
            catch (Exception ex)
            {
                TempData["ExceptionMessage"] = ex.Message;

                return RedirectToAction("All", "Trailer");
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
                // All trailers from the database
                ICollection<TrailerInfoViewModel> serviceModel = await trailerService.GetAllTrailersInfo();

                return Json(serviceModel);
            }
            catch (Exception)
            {
                TempData["ExceptionMessage"] = SomethingWentWrongMessage;

                return RedirectToAction("Index", "Home");
            }
        }
    }
}

namespace TruckManagerSoftware.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Trailer;
    using Core.Services.Contract;

    using static Common.Messages.Messages.Common;
    using static Common.Messages.Messages.Trailer;

    [Area("Admin")]
    [Authorize]
    public class TrailerController : Controller
    {
        private readonly ITrailerService trailerService;

        public TrailerController(ITrailerService trailerService)
        {
            this.trailerService = trailerService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            // Return add trailer view model
            return View(new AddTrailerViewModel());
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

                return RedirectToAction("GetAll", "Trailer");
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
                    CargoTypes = trailerInfo.CargoTypes
                });
            }
            catch (Exception ex)
            {
                TempData["ExceptionMessage"] = ex.Message;

                return RedirectToAction("GetAll", "Trailer");
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

                return RedirectToAction("GetAll", "Trailer");
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
                // Trailer from the database
                await trailerService.RemoveTrailer(id);

                TempData["Message"] = TrailerSuccessfullyRemovedMessage;

                return RedirectToAction("GetAll", "Trailer");
            }
            catch (Exception ex)
            {
                TempData["ExceptionMessage"] = ex.Message;

                return RedirectToAction("GetAll", "Trailer");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Service which returns
                // All trailers from the database
                ICollection<TrailerInfoViewModel> serviceModel = await trailerService.GetAllTrailersInfo();

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

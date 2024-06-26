﻿namespace TruckManagerSoftware.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Transmission;
    using Core.Services.Contract;

    using static Common.DataConstants.DataConstants.Admin;
    using static Common.Messages.Messages.Common;
    using static Common.Messages.Messages.Transmission;

    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class TransmissionController : Controller
    {
        private readonly ITransmissionService transmissionService;

        public TransmissionController(ITransmissionService transmissionService)
        {
            this.transmissionService = transmissionService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            // Return add transmission view model
            return View(new AddTransmissionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTransmissionViewModel model)
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
                // Transmission to the database
                await transmissionService.AddTransmission(model);

                TempData["Message"] = TransmissionSuccessfullyCreatedMessage;

                return RedirectToAction("All", "Transmission");
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
                // Get the transmission by id
                // From the database
                TransmissionInfoViewModel transmissionInfo = await transmissionService.GetTransmissionInfoById(id);

                // Return edit transmission view model
                return View(new EditTransmissionViewModel()
                {
                    Id = transmissionInfo.Id,
                    Title = transmissionInfo.Title,
                    GearsCount = transmissionInfo.GearsCount,
                    Retarder = transmissionInfo.Retarder == "Available" ? true : false
                });
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTransmissionViewModel model)
        {
            // If the entered data
            // Is not valid
            // Return model
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Service which is editing
                // The transmission with Id == model.Id
                await transmissionService.EditTransmission(model);

                TempData["Message"] = TransmissionSuccessfullyEditedMessage;

                return RedirectToAction("All", "Transmission");
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
                // Transmission from the database
                await transmissionService.RemoveTransmission(id);

                TempData["Message"] = TransmissionSuccessfullyRemovedMessage;

                return RedirectToAction("All", "Transmission");
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
                // All transmissions from the database
                ICollection<TransmissionInfoViewModel> serviceModel = await transmissionService.GetAllTransmissionsInfo();

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
                // Service which returns
                // Info for transmission
                // With Id == id
                TransmissionInfoViewModel serviceModel = await transmissionService.GetTransmissionInfoById(id);

                return View(serviceModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }
    }
}

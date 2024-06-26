﻿namespace TruckManagerSoftware.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Engine;
    using Core.Services.Contract;

    using static Common.DataConstants.DataConstants.Admin;
    using static Common.Messages.Messages.Common;
    using static Common.Messages.Messages.Engine;

    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class EngineController : Controller
    {
        private readonly IEngineService engineService;

        public EngineController(IEngineService engineService)
        {
            this.engineService = engineService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            // Return add engine view model
            return View(new AddEngineViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEngineViewModel model)
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
                // Engine to the database
                await engineService.AddEngine(model);

                TempData["Message"] = EngineSuccessfullyCreatedMessage;

                return RedirectToAction("All", "Engine");
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
                // Get engine by id
                // From the database
                EngineInfoViewModel engineInfo = await engineService.GetEngineInfoById(id);

                // Return edit engine view model
                return View(new EditEngineViewModel()
                {
                    Id = engineInfo.Id,
                    Title = engineInfo.Title,
                    PowerHp = engineInfo.PowerHp,
                    PowerKw = engineInfo.PowerKw,
                    TorqueNm = engineInfo.TorqueNm
                });
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEngineViewModel model)
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
                // The engine with Id == model.Id
                await engineService.EditEngine(model);

                TempData["Message"] = EngineSuccessfullyEditedMessage;

                return RedirectToAction("All", "Engine");
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
                // Engine from the database
                await engineService.RemoveEngine(id);

                TempData["Message"] = EngineSuccessfullyRemovedMessage;

                return RedirectToAction("All", "Engine");
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
                // All engines from the database
                ICollection<EngineInfoViewModel> serviceModel = await engineService.GetAllEnginesInfo();

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
                // Info for engine
                // With Id == id
                EngineInfoViewModel serviceModel = await engineService.GetEngineInfoById(id);

                return View(serviceModel);
            }
            catch (Exception)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = SomethingWentWrongMessage });
            }
        }
    }
}

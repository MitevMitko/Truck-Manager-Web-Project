namespace TruckManagerSoftware.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.BankContact;
    using Core.Services.Contract;

    using static Common.DataConstants.DataConstants.Admin;
    using static Common.Messages.Messages.Common;
    using static Common.Messages.Messages.BankContact;

    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class BankContactController : Controller
    {
        private readonly IBankContactService bankContactService;

        public BankContactController(IBankContactService bankContactService)
        {
            this.bankContactService = bankContactService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            // Return add bank contact view model
            return View(new AddBankContactViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBankContactViewModel model)
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
                // Bank contact to the database
                await bankContactService.AddBankContact(model);

                TempData["Message"] = BankContactSuccessfullyCreatedMessage;

                return RedirectToAction("All", "BankContact");
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
                // Get bank contact by id
                // From the database
                BankContactInfoViewModel bankContactInfo = await bankContactService.GetBankContactInfoById(id);

                // Return edit bank contact view model
                return View(new EditBankContactViewModel
                {
                    Id = bankContactInfo.Id,
                    Name = bankContactInfo.Name,
                    Email = bankContactInfo.Email,
                    PhoneNumber = bankContactInfo.PhoneNumber
                });
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBankContactViewModel model)
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
                // The bank contact with Id == model.Id
                await bankContactService.EditBankContact(model);

                TempData["Message"] = BankContactSuccessfullyEditedMessage;

                return RedirectToAction("All", "BankContact");
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
                // Bank contact from the database
                await bankContactService.RemoveBankContact(id);

                TempData["Message"] = BankContactSuccessfullyRemovedMessage;

                return RedirectToAction("All", "BankContact");
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
                // All bank contacts from the database
                ICollection<BankContactInfoViewModel> serviceModel = await bankContactService.GetAllBankContactsInfo();

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
                // Info about the bank contact
                // With Id == id
                BankContactInfoViewModel serviceModel = await bankContactService.GetBankContactInfoById(id);

                return View(serviceModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }
    }
}

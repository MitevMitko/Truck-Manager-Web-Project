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

                return RedirectToAction("GetAll", "BankContact");
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
                TempData["ExceptionMessage"] = ex.Message;

                return RedirectToAction("GetAll", "BankContact");
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

                return RedirectToAction("GetAll", "BankContact");
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
                // Bank contact from the database
                await bankContactService.RemoveBankContact(id);

                TempData["Message"] = BankContactSuccessfullyRemovedMessage;

                return RedirectToAction("GetAll", "BankContact");
            }
            catch (Exception ex)
            {
                TempData["ExceptionMessage"] = ex.Message;

                return RedirectToAction("GetAll", "BankContact");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Service which returns
                // All bank contacts from the database
                ICollection<BankContactInfoViewModel> serviceModel = await bankContactService.GetAllBankContactsInfo();

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

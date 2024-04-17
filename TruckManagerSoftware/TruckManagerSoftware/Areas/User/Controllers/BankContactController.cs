namespace TruckManagerSoftware.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.BankContact;
    using Core.Services.Contract;

    using static Common.DataConstants.DataConstants.User;
    using static Common.Messages.Messages.Common;

    [Area(UserAreaName)]
    [Authorize(Roles = UserRoleName)]
    public class BankContactController : Controller
    {
        private readonly IBankContactService bankContactService;

        public BankContactController(IBankContactService bankContactService)
        {
            this.bankContactService = bankContactService;
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
                // All bank contacts
                // From the database
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

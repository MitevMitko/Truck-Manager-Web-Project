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

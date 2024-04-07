namespace TruckManagerSoftware.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Transmission;
    using Core.Services.Contract;

    using static Common.Messages.Messages.Common;

    [Area("User")]
    [Authorize]
    public class TransmissionController : Controller
    {
        private readonly ITransmissionService transmissionService;

        public TransmissionController(ITransmissionService transmissionService)
        {
            this.transmissionService = transmissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Service which returns
                // All transmissions from the database
                ICollection<TransmissionInfoViewModel> serviceModel = await transmissionService.GetAllTransmissionsInfo();

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

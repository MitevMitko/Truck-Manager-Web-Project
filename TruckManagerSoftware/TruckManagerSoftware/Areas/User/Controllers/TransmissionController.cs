namespace TruckManagerSoftware.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Transmission;
    using Core.Services.Contract;

    using static Common.DataConstants.DataConstants.User;
    using static Common.Messages.Messages.Common;

    [Area(UserAreaName)]
    [Authorize(Roles = UserRoleName)]
    public class TransmissionController : Controller
    {
        private readonly ITransmissionService transmissionService;

        public TransmissionController(ITransmissionService transmissionService)
        {
            this.transmissionService = transmissionService;
        }

        [HttpGet]
        public ActionResult All()
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

namespace TruckManagerSoftware.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Order;
    using Core.Services.Contract;

    using static Common.DataConstants.DataConstants.User;
    using static Common.Messages.Messages.Common;

    [Area(UserAreaName)]
    [Authorize(Roles = UserRoleName)]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
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
                // All orders from the database
                ICollection<OrderInfoViewModel> serviceModel = await orderService.GetAllOrdersInfo();

                return Json(serviceModel);
            }
            catch (Exception)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = SomethingWentWrongMessage });
            }
        }

        [HttpGet]
        public IActionResult GetOrdersWithoutTruckId()
        {
            try
            {
                ICollection<OrderInfoViewModel> serviceModel = orderService.GetOrdersInfoWithoutTruckId();

                return Json(serviceModel);
            }
            catch (Exception)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = SomethingWentWrongMessage });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAdditionalInfoById(Guid id)
        {
            try
            {
                OrderAdditionalInfoViewModel serviceModel = await orderService.GetAdditionalOrderInfoById(id);

                return View(serviceModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddOrderToTruck(Guid id, Guid orderId)
        {
            try
            {
                await orderService.AddOrderToTruck(id, orderId);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> FinishOrder(Guid id)
        {
            try
            {
                await orderService.FinishOrder(id);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }
    }
}

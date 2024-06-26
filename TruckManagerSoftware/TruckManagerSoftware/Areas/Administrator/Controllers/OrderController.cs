﻿namespace TruckManagerSoftware.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Order;
    using Core.Services.Contract;

    using static Common.DataConstants.DataConstants.Admin;
    using static Common.Messages.Messages.Common;
    using static Common.Messages.Messages.Order;

    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            // Return add order view model
            return View(new AddOrderViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddOrderViewModel model)
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
                // Garage to the database
                await orderService.AddOrder(model);

                TempData["Message"] = OrderSuccessfullyCreatedMessage;

                return RedirectToAction("All", "Order");
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
                // Get order by id
                // From the database
                OrderInfoViewModel orderInfo = await orderService.GetOrderInfoById(id);

                // Return edit order view model
                return View(new EditOrderViewModel()
                {
                    Id = orderInfo.Id,
                    Cargo = orderInfo.Cargo,
                    CargoWeight = orderInfo.CargoWeight,
                    StartPoint = orderInfo.StartPoint,
                    EndPoint = orderInfo.EndPoint,
                    DeliveryType = orderInfo.DeliveryType,
                    TripDistance = orderInfo.TripDistance,
                    TripTime = orderInfo.TripTime,
                    DeliveryPrice = orderInfo.DeliveryPrice
                });
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditOrderViewModel model)
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
                // The order with Id == model.Id
                await orderService.EditOrder(model);

                TempData["Message"] = OrderSuccessfullyEditedMessage;

                return RedirectToAction("All", "Order");
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
                // Order from the database
                await orderService.RemoveOrder(id);

                TempData["Message"] = OrderSuccessfullyRemovedMessage;

                return RedirectToAction("All", "Order");
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
    }
}

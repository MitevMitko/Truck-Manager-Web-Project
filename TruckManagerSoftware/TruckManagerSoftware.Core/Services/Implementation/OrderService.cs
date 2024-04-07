namespace TruckManagerSoftware.Core.Services.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Contract;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;
    using Models.Order;

    using static Common.DataConstants.DataConstants.Order;
    using static Common.Messages.Messages.Order;

    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddOrder(AddOrderViewModel model)
        {
            if (!Regex.IsMatch(model.TripTime, TripTimeRegExExpression))
            {
                throw new ArgumentException(OrderTripTimePropertyIsNotMatch);
            }

            // Create new order
            Order order = new Order()
            {
                Cargo = model.Cargo,
                CargoWeight = model.CargoWeight,
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                DeliveryType = model.DeliveryType,
                TripDistance = model.TripDistance,
                TripTime = model.TripTime,
                DeliveryPrice = model.DeliveryPrice
            };

            // Add the created order to the database
            await unitOfWork.Order.Add(order);

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task EditOrder(EditOrderViewModel model)
        {
            // Get the order by id
            // From the database
            Order order = await unitOfWork.Order.GetById(model.Id);

            // If order does not exist
            // Throw argument exception
            if (order == null)
            {
                throw new ArgumentException(OrderNotExistMessage);
            }

            if (!Regex.IsMatch(model.TripTime, TripTimeRegExExpression))
            {
                throw new ArgumentException(OrderTripTimePropertyIsNotMatch);
            }

            // Assign the edited data
            // To the order
            order.Cargo = model.Cargo;
            order.CargoWeight = model.CargoWeight;
            order.StartPoint = model.StartPoint;
            order.EndPoint = model.EndPoint;
            order.DeliveryType = model.DeliveryType;
            order.TripDistance = model.TripDistance;
            order.TripTime = model.TripTime;
            order.DeliveryPrice = model.DeliveryPrice;

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task<ICollection<OrderInfoViewModel>> GetAllOrdersInfo()
        {
            // Create collection of order view model
            ICollection<OrderInfoViewModel> model = new List<OrderInfoViewModel>();

            // Get all orders
            // From the database
            IEnumerable<Order> orders = await unitOfWork.Order.GetAll();

            // Assign the data from the orders
            // From the database to the order view model
            // And then add the order view model
            // To the collection of order view model
            foreach (Order order in orders)
            {
                OrderInfoViewModel orderInfo = new OrderInfoViewModel()
                {
                    Id = order.Id,
                    Cargo = order.Cargo,
                    CargoWeight = order.CargoWeight,
                    StartPoint = order.StartPoint,
                    EndPoint = order.EndPoint,
                    DeliveryType = order.DeliveryType,
                    TripDistance = order.TripDistance,
                    TripTime = order.TripTime,
                    DeliveryPrice = order.DeliveryPrice
                };

                model.Add(orderInfo);
            }

            return model;
        }

        public async Task<OrderInfoViewModel> GetOrderInfoById(Guid id)
        {
            // Get the order by id
            // From the database
            Order order = await unitOfWork.Order.GetById(id);

            // If order does not exist
            // Throw argument exception
            if (order == null)
            {
                throw new ArgumentException(OrderNotExistMessage);
            }

            // Create order view model
            // Assign the data from the order
            // And return the order view model
            return new OrderInfoViewModel
            {
                Id = order.Id,
                Cargo = order.Cargo,
                CargoWeight = order.CargoWeight,
                StartPoint = order.StartPoint,
                EndPoint = order.EndPoint,
                DeliveryType = order.DeliveryType,
                TripDistance = order.TripDistance,
                TripTime = order.TripTime,
                DeliveryPrice = order.DeliveryPrice
            };
        }

        public async Task RemoveOrder(Guid id)
        {
            // Get the order by id
            // From the database
            Order order = await unitOfWork.Order.GetById(id);

            // If order does not exist
            // Throw argument exception
            if (order == null)
            {
                throw new ArgumentException(OrderNotExistMessage);
            }

            // Remove the order
            // From the database
            unitOfWork.Order.Remove(order);

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }
    }
}

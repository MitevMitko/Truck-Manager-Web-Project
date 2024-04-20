namespace TruckManagerSoftware.Core.Services.Implementation
{
    using Microsoft.AspNetCore.Identity;

    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Contract;
    using Models.Order;
    using Models.Truck;
    using Models.User;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;

    using static Common.DataConstants.DataConstants.Order;
    using static Common.DataConstants.DataConstants.User;
    using static Common.Messages.Messages.Order;
    using static Common.Messages.Messages.Truck;
    using static Common.Messages.Messages.User;

    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly UserManager<User> userManager;

        public OrderService(IUnitOfWork unitOfWork,
            UserManager<User> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
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

        public ICollection<OrderInfoViewModel> GetOrdersInfoWithoutTruckId()
        {
            // Create collection of order view model
            ICollection<OrderInfoViewModel> model = new List<OrderInfoViewModel>();

            // Get all orders
            // Without with property
            // TruckId == null
            // From the database
            ICollection<Order> orders = unitOfWork.Order.Find(o => o.TruckId == null).ToList();

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

        public async Task<OrderAdditionalInfoViewModel> GetAdditionalOrderInfoById(Guid id)
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

            // Create OrderAdditionalInfoViewModel
            // Assign the data from the order
            OrderAdditionalInfoViewModel orderAdditionalInfo = new OrderAdditionalInfoViewModel()
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

            // If the order's property called TruckId has value
            // Get the order's truck from the database
            // If the order's property TruckId does not have value
            // Return null
            if (order.TruckId.HasValue)
            {
                // Get the order's truck
                // With Id == order.TruckId
                // From the database
                Truck orderTruck = await unitOfWork.Truck.GetById(order.TruckId.Value);

                // Assign the data from orderTruck
                // To TruckInfoViewModel
                orderAdditionalInfo.TruckInfo = new TruckInfoViewModel()
                {
                    Id = orderTruck.Id,
                    Brand = orderTruck.Brand,
                    Series = orderTruck.Series
                };
            }
            else
            {
                orderAdditionalInfo.TruckInfo = null;
            }

            // If the order's property called UserId has value
            // Get the order's user from the database
            // If the order's property UserId does not have value
            // Return null
            if (order.UserId.HasValue)
            {
                // Get the order's user
                // From the database
                User orderUser = await userManager.FindByIdAsync(order.UserId.ToString());

                // Assign the data from the orderUser
                // To UserInfoViewModel
                orderAdditionalInfo.UserInfo = new UserInfoViewModel()
                {
                    Id = orderUser.Id,
                    UserName = orderUser.UserName
                };
            }
            else
            {
                orderAdditionalInfo.UserInfo = null;
            }

            return orderAdditionalInfo;
        }

        public async Task FinishOrder(Guid id)
        {
            // Get order with
            // Id == id from the database
            Order order = await unitOfWork.Order.GetById(id);

            // If order does not exist
            // Throw argument exception
            if (order == null)
            {
                throw new ArgumentException(OrderNotExistMessage);
            }

            // If the order's property called TruckId has value
            // Get the order's truck from the database
            // If the order's property TruckId does not have value
            // Throw argument exception
            if (order.TruckId.HasValue)
            {
                // Get truck with
                // Id == order.TruckId
                // From the database
                Truck truck = await unitOfWork.Truck.GetById(order.TruckId.Value);

                truck.OrderId = null;
            }
            else
            {
                throw new ArgumentException(TruckNotExistMessage);
            }

            // If the order's property called UserId has value
            // Get the order's user from the database
            // If the order's property UserId does not have value
            // Throw argument exception
            if (order.UserId.HasValue)
            {
                // Get user with
                // Id == order.UserId
                // From the database
                User user = await userManager.FindByIdAsync(order.UserId.ToString());

                user.OrderId = null;
                user.Status = StatusValueWhenRegisterUser;
            }
            else
            {
                throw new ArgumentException(UserNotExistMessage);
            }

            order.TruckId = null;
            order.UserId = null;

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task AddOrderToTruck(Guid id, Guid orderId)
        {
            // Get truck with property
            // Id == id from the database
            Truck truck = await unitOfWork.Truck.GetById(id);

            // If the truck
            // Does not exist
            // Throw argument exception
            if (truck == null)
            {
                throw new ArgumentException(TruckNotExistMessage);
            }

            // Get order with property
            // Id == id from the database
            Order order = await unitOfWork.Order.GetById(orderId);

            // If the order
            // Does not exist
            // Throw argument exception
            if (order == null)
            {
                throw new ArgumentException(OrderNotExistMessage);
            }

            // Get user with property
            // Id == truck.UserId
            // From the database
            User user = await userManager.FindByIdAsync(truck.UserId.ToString());

            // If the user
            // Does not exist
            // Throw argument exception
            if (user == null)
            {
                throw new ArgumentException(UserNotExistMessage);
            }

            // Assign data from order
            // To truck.OrderId
            truck.OrderId = order.Id;

            // Assign data from truck
            // To order.TruckId
            // And order.UserId
            order.TruckId = truck.Id;
            order.UserId = truck.UserId;

            // Assign data from order
            // To user.OrderId
            user.OrderId = order.Id;
            user.Status = StatusValueWhenAddingOrderToTruck;

            // Save changes
            await unitOfWork.CompleteAsync();
        }
    }
}

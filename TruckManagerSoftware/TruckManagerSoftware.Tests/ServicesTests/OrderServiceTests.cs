namespace TruckManagerSoftware.Tests.ServicesTests
{
    using Microsoft.AspNetCore.Identity;
    using Moq;

    using Core.Models.Order;
    using Core.Models.Truck;
    using Core.Models.User;
    using Core.Services.Implementation;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;

    using static Common.Messages.Messages.Order;
    using static Common.Messages.Messages.Truck;
    using static Common.Messages.Messages.User;

    public class OrderServiceTests
    {
        private readonly OrderService orderService;

        private readonly Mock<IUnitOfWork> unitOfWorkMock;

        private readonly Mock<UserManager<User>> userManagerMock;

        public OrderServiceTests()
        {
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
            this.userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            this.orderService = new OrderService(
                unitOfWorkMock.Object,
                userManagerMock.Object);
        }

        // EditOrder
        [Test]
        public Task EditOrder_ShouldThrowArgumentException_WhenOrderDoesNotExist()
        {
            // Arrange
            Order order = null!;

            EditOrderViewModel model = new EditOrderViewModel();

            unitOfWorkMock.Setup(x => x.Order.GetById(model.Id)).ReturnsAsync(order);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await orderService.EditOrder(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task EditOrder_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenOrderDoesNotExist()
        {
            // Arrange
            Order order = null!;

            EditOrderViewModel model = new EditOrderViewModel();

            unitOfWorkMock.Setup(x => x.Order.GetById(model.Id)).ReturnsAsync(order);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await orderService.EditOrder(model));

            Assert.That(argumentException.Message, Is.EqualTo(OrderNotExistMessage));

            return Task.CompletedTask;
        }

        // GetAllOrdersInfo
        [Test]
        public async Task GetAllOrdersInfo_ShouldReturnICollectionOfOrderInfoViewModel()
        {
            // Arrange
            Order scaniaOrder = new Order
            {
                Id = Guid.NewGuid(),
                Cargo = "Wood",
                CargoWeight = 13,
                StartPoint = "Ruse",
                EndPoint = "Pleven",
                DeliveryType = "important",
                TripDistance = 300,
                TripTime = "3h 30m",
                DeliveryPrice = 1500
            };

            Order dafOrder = new Order
            {
                Id = Guid.NewGuid(),
                Cargo = "Plastic",
                CargoWeight = 10,
                StartPoint = "Varna",
                EndPoint = "Lukovit",
                DeliveryType = "urgent",
                TripDistance = 500,
                TripTime = "5h 30m",
                DeliveryPrice = 3500
            };

            IEnumerable<Order> orders = new List<Order>
            {
                scaniaOrder,
                dafOrder
            };

            unitOfWorkMock.Setup(x => x.Order.GetAll()).ReturnsAsync(orders);

            // Act
            ICollection<OrderInfoViewModel> serviceModel = await orderService.GetAllOrdersInfo();

            // Assert
            Assert.IsInstanceOf<ICollection<OrderInfoViewModel>>(serviceModel);
            Assert.IsNotNull(serviceModel);
            Assert.That(serviceModel.Count, Is.EqualTo(orders.Count()));

            int cnt = 0;

            foreach (OrderInfoViewModel orderInfo in serviceModel)
            {
                if (cnt == 0)
                {
                    Assert.IsInstanceOf<OrderInfoViewModel>(orderInfo);
                    Assert.That(orderInfo.Id, Is.EqualTo(scaniaOrder.Id));
                    Assert.That(orderInfo.Cargo, Is.EqualTo(scaniaOrder.Cargo));
                    Assert.That(orderInfo.CargoWeight, Is.EqualTo(scaniaOrder.CargoWeight));
                    Assert.That(orderInfo.StartPoint, Is.EqualTo(scaniaOrder.StartPoint));
                    Assert.That(orderInfo.EndPoint, Is.EqualTo(scaniaOrder.EndPoint));
                    Assert.That(orderInfo.DeliveryType, Is.EqualTo(scaniaOrder.DeliveryType));
                    Assert.That(orderInfo.TripDistance, Is.EqualTo(scaniaOrder.TripDistance));
                    Assert.That(orderInfo.TripTime, Is.EqualTo(scaniaOrder.TripTime));
                    Assert.That(orderInfo.DeliveryPrice, Is.EqualTo(scaniaOrder.DeliveryPrice));

                    cnt++;
                }
                else
                {
                    Assert.IsInstanceOf<OrderInfoViewModel>(orderInfo);
                    Assert.That(orderInfo.Id, Is.EqualTo(dafOrder.Id));
                    Assert.That(orderInfo.Cargo, Is.EqualTo(dafOrder.Cargo));
                    Assert.That(orderInfo.CargoWeight, Is.EqualTo(dafOrder.CargoWeight));
                    Assert.That(orderInfo.StartPoint, Is.EqualTo(dafOrder.StartPoint));
                    Assert.That(orderInfo.EndPoint, Is.EqualTo(dafOrder.EndPoint));
                    Assert.That(orderInfo.DeliveryType, Is.EqualTo(dafOrder.DeliveryType));
                    Assert.That(orderInfo.TripDistance, Is.EqualTo(dafOrder.TripDistance));
                    Assert.That(orderInfo.TripTime, Is.EqualTo(dafOrder.TripTime));
                    Assert.That(orderInfo.DeliveryPrice, Is.EqualTo(dafOrder.DeliveryPrice));
                }
            }
        }

        // GetOrderInfoById
        [Test]
        public async Task GetOrderInfoById_ShouldReturnOrderInfoViewModel_WhenOrderExists()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();

            Order order = new Order
            {
                Id = orderId,
                Cargo = "Wood",
                CargoWeight = 13,
                StartPoint = "Ruse",
                EndPoint = "Pleven",
                DeliveryType = "important",
                TripDistance = 300,
                TripTime = "3h 30m",
                DeliveryPrice = 1500
            };

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            // Act
            OrderInfoViewModel serviceModel = await orderService.GetOrderInfoById(orderId);

            // Assert
            Assert.IsInstanceOf<OrderInfoViewModel>(serviceModel);
            Assert.That(serviceModel.Id, Is.EqualTo(order.Id));
            Assert.That(serviceModel.Cargo, Is.EqualTo(order.Cargo));
            Assert.That(serviceModel.CargoWeight, Is.EqualTo(order.CargoWeight));
            Assert.That(serviceModel.StartPoint, Is.EqualTo(order.StartPoint));
            Assert.That(serviceModel.EndPoint, Is.EqualTo(order.EndPoint));
            Assert.That(serviceModel.DeliveryType, Is.EqualTo(order.DeliveryType));
            Assert.That(serviceModel.TripDistance, Is.EqualTo(order.TripDistance));
            Assert.That(serviceModel.TripTime, Is.EqualTo(order.TripTime));
            Assert.That(serviceModel.DeliveryPrice, Is.EqualTo(order.DeliveryPrice));
        }

        [Test]
        public Task GetOrderInfoById_ShouldThrowArgumentException_WhenOrderDoesNotExist()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();

            Order order = null!;

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await orderService.GetOrderInfoById(orderId));

            return Task.CompletedTask;
        }

        [Test]
        public Task GetOrderInfoById_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenOrderDoesNotExist()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();

            Order order = null!;

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await orderService.GetOrderInfoById(orderId));

            Assert.That(argumentException.Message, Is.EqualTo(OrderNotExistMessage));

            return Task.CompletedTask;
        }

        // RemoveOrder
        [Test]
        public Task RemoveOrder_ShouldThrowArgumentException_WhenOrderDoesNotExist()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();

            Order order = null!;

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await orderService.RemoveOrder(orderId));

            return Task.CompletedTask;
        }

        [Test]
        public Task RemoveOrder_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenOrderDoesNotExist()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();

            Order order = null!;

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await orderService.RemoveOrder(orderId));

            Assert.That(argumentException.Message, Is.EqualTo(OrderNotExistMessage));

            return Task.CompletedTask;
        }

        // GetOrdersInfoWithoutTruckId
        [Test]
        public void GetOrdersInfoWithoutTruckId_ShouldReturnICollectionOfOrderInfoViewModel()
        {
            // Arrange
            Order scaniaOrder = new Order
            {
                Id = Guid.NewGuid(),
                Cargo = "Wood",
                CargoWeight = 13,
                StartPoint = "Ruse",
                EndPoint = "Pleven",
                DeliveryType = "important",
                TripDistance = 300,
                TripTime = "3h 30m",
                DeliveryPrice = 1500,
                TruckId = Guid.NewGuid()
            };

            Order dafOrder = new Order
            {
                Id = Guid.NewGuid(),
                Cargo = "Plastic",
                CargoWeight = 10,
                StartPoint = "Varna",
                EndPoint = "Lukovit",
                DeliveryType = "urgent",
                TripDistance = 500,
                TripTime = "5h 30m",
                DeliveryPrice = 3500,
                TruckId = Guid.NewGuid()
            };

            IQueryable<Order> orders = new List<Order>
            {
                scaniaOrder,
                dafOrder
            }
            .AsQueryable();

            unitOfWorkMock.Setup(x => x.Order.Find(o => o.TruckId == null)).Returns(orders);

            // Act
            ICollection<OrderInfoViewModel> serviceModel = orderService.GetOrdersInfoWithoutTruckId();

            // Assert
            Assert.IsInstanceOf<ICollection<OrderInfoViewModel>>(serviceModel);
            Assert.IsNotNull(serviceModel);
            Assert.That(serviceModel.Count, Is.EqualTo(orders.Count()));

            int cnt = 0;

            foreach (OrderInfoViewModel orderInfo in serviceModel)
            {
                if (cnt == 0)
                {
                    Assert.IsInstanceOf<OrderInfoViewModel>(orderInfo);
                    Assert.That(orderInfo.Id, Is.EqualTo(scaniaOrder.Id));
                    Assert.That(orderInfo.Cargo, Is.EqualTo(scaniaOrder.Cargo));
                    Assert.That(orderInfo.CargoWeight, Is.EqualTo(scaniaOrder.CargoWeight));
                    Assert.That(orderInfo.StartPoint, Is.EqualTo(scaniaOrder.StartPoint));
                    Assert.That(orderInfo.EndPoint, Is.EqualTo(scaniaOrder.EndPoint));
                    Assert.That(orderInfo.DeliveryType, Is.EqualTo(scaniaOrder.DeliveryType));
                    Assert.That(orderInfo.TripDistance, Is.EqualTo(scaniaOrder.TripDistance));
                    Assert.That(orderInfo.TripTime, Is.EqualTo(scaniaOrder.TripTime));
                    Assert.That(orderInfo.DeliveryPrice, Is.EqualTo(scaniaOrder.DeliveryPrice));

                    cnt++;
                }
                else
                {
                    Assert.IsInstanceOf<OrderInfoViewModel>(orderInfo);
                    Assert.That(orderInfo.Id, Is.EqualTo(dafOrder.Id));
                    Assert.That(orderInfo.Cargo, Is.EqualTo(dafOrder.Cargo));
                    Assert.That(orderInfo.CargoWeight, Is.EqualTo(dafOrder.CargoWeight));
                    Assert.That(orderInfo.StartPoint, Is.EqualTo(dafOrder.StartPoint));
                    Assert.That(orderInfo.EndPoint, Is.EqualTo(dafOrder.EndPoint));
                    Assert.That(orderInfo.DeliveryType, Is.EqualTo(dafOrder.DeliveryType));
                    Assert.That(orderInfo.TripDistance, Is.EqualTo(dafOrder.TripDistance));
                    Assert.That(orderInfo.TripTime, Is.EqualTo(dafOrder.TripTime));
                    Assert.That(orderInfo.DeliveryPrice, Is.EqualTo(dafOrder.DeliveryPrice));
                }
            }
        }

        // GetAdditionalOrderInfoById
        [Test]
        public async Task GetAdditionalOrderInfoById_ShouldReturnOrderAdditionalInfoViewModel_WhenOrderExists()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();

            Order order = new Order
            {
                Id = orderId,
                Cargo = "Wood",
                CargoWeight = 13,
                StartPoint = "Ruse",
                EndPoint = "Pleven",
                DeliveryType = "important",
                TripDistance = 300,
                TripTime = "3h 30m",
                DeliveryPrice = 1500,
                TruckId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };

            Truck truck = new Truck
            {
                Id = order.TruckId.Value,
                Brand = "Scania",
                Series = "S"
            };

            User user = new User
            {
                Id = order.UserId.Value,
                UserName = "user"
            };

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            unitOfWorkMock.Setup(x => x.Truck.GetById(order.TruckId.Value)).ReturnsAsync(truck);

            userManagerMock.Setup(x => x.FindByIdAsync(order.UserId.ToString())).ReturnsAsync(user);

            // Act
            OrderAdditionalInfoViewModel serviceModel = await orderService.GetAdditionalOrderInfoById(orderId);

            // Assert
            Assert.IsInstanceOf<OrderAdditionalInfoViewModel>(serviceModel);
            Assert.That(serviceModel.Id, Is.EqualTo(order.Id));
            Assert.That(serviceModel.Cargo, Is.EqualTo(order.Cargo));
            Assert.That(serviceModel.CargoWeight, Is.EqualTo(order.CargoWeight));
            Assert.That(serviceModel.StartPoint, Is.EqualTo(order.StartPoint));
            Assert.That(serviceModel.EndPoint, Is.EqualTo(order.EndPoint));
            Assert.That(serviceModel.DeliveryType, Is.EqualTo(order.DeliveryType));
            Assert.That(serviceModel.TripDistance, Is.EqualTo(order.TripDistance));
            Assert.That(serviceModel.TripTime, Is.EqualTo(order.TripTime));
            Assert.That(serviceModel.DeliveryPrice, Is.EqualTo(order.DeliveryPrice));

            Assert.IsInstanceOf<TruckInfoViewModel>(serviceModel.TruckInfo);
            Assert.That(serviceModel.TruckInfo!.Id, Is.EqualTo(truck.Id));
            Assert.That(serviceModel.TruckInfo!.Brand, Is.EqualTo(truck.Brand));
            Assert.That(serviceModel.TruckInfo!.Series, Is.EqualTo(truck.Series));

            Assert.IsInstanceOf<UserInfoViewModel>(serviceModel.UserInfo);
            Assert.That(serviceModel.UserInfo!.Id, Is.EqualTo(user.Id));
            Assert.That(serviceModel.UserInfo!.UserName, Is.EqualTo(user.UserName));

        }

        [Test]
        public async Task GetAdditionalOrderInfoById_ShouldReturnOrderAdditionalInfoViewModel_WhenOrderExists_AndWhenOrderDoesNotHaveTruckUser()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();

            Order order = new Order
            {
                Id = orderId,
                Cargo = "Wood",
                CargoWeight = 13,
                StartPoint = "Ruse",
                EndPoint = "Pleven",
                DeliveryType = "important",
                TripDistance = 300,
                TripTime = "3h 30m",
                DeliveryPrice = 1500,
                TruckId = null,
                UserId = null
            };

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            // Act
            OrderAdditionalInfoViewModel serviceModel = await orderService.GetAdditionalOrderInfoById(orderId);

            // Assert
            Assert.IsInstanceOf<OrderAdditionalInfoViewModel>(serviceModel);
            Assert.That(serviceModel.Id, Is.EqualTo(order.Id));
            Assert.That(serviceModel.Cargo, Is.EqualTo(order.Cargo));
            Assert.That(serviceModel.CargoWeight, Is.EqualTo(order.CargoWeight));
            Assert.That(serviceModel.StartPoint, Is.EqualTo(order.StartPoint));
            Assert.That(serviceModel.EndPoint, Is.EqualTo(order.EndPoint));
            Assert.That(serviceModel.DeliveryType, Is.EqualTo(order.DeliveryType));
            Assert.That(serviceModel.TripDistance, Is.EqualTo(order.TripDistance));
            Assert.That(serviceModel.TripTime, Is.EqualTo(order.TripTime));
            Assert.That(serviceModel.DeliveryPrice, Is.EqualTo(order.DeliveryPrice));

            Assert.That(serviceModel.TruckInfo, Is.EqualTo(null));

            Assert.That(serviceModel.UserInfo, Is.EqualTo(null));
        }

        [Test]
        public Task GetAdditionalOrderInfoById_ShouldThrowArgumentException_WhenOrderDoesNotExist()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();

            Order order = null!;

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await orderService.GetAdditionalOrderInfoById(orderId));

            return Task.CompletedTask;
        }

        [Test]
        public Task GetAdditionalOrderInfoById_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenOrderDoesNotExist()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();

            Order order = null!;

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await orderService.GetAdditionalOrderInfoById(orderId));

            Assert.That(argumentException.Message, Is.EqualTo(OrderNotExistMessage));

            return Task.CompletedTask;
        }

        // FinishOrder
        [Test]
        public Task FinishOrder_ShouldThrowArgumentException_WhenOrderDoesNotExist()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();

            Order order = null!;

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await orderService.FinishOrder(orderId));

            return Task.CompletedTask;
        }

        [Test]
        public Task FinishOrder_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenOrderDoesNotExist()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();

            Order order = null!;

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await orderService.FinishOrder(orderId));

            Assert.That(argumentException.Message, Is.EqualTo(OrderNotExistMessage));

            return Task.CompletedTask;
        }

        [Test]
        public Task FinishOrder_ShouldThrowArgumentException_WhenOrderTruckDoesNotExist()
        {

            // Arrange
            Guid orderId = Guid.NewGuid();

            Order order = new Order
            {
                TruckId = null
            };

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await orderService.FinishOrder(orderId));

            return Task.CompletedTask;
        }

        [Test]
        public Task FinishOrder_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenOrderTruckDoesNotExist()
        {

            // Arrange
            Guid orderId = Guid.NewGuid();

            Order order = new Order
            {
                TruckId = null
            };

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await orderService.FinishOrder(orderId));

            Assert.That(argumentException.Message, Is.EqualTo(TruckNotExistMessage));

            return Task.CompletedTask;
        }

        [Test]
        public Task FinishOrder_ShouldThrowArgumentException_WhenOrderUserDoesNotExist()
        {

            // Arrange
            Guid orderId = Guid.NewGuid();

            Order order = new Order
            {
                TruckId = Guid.NewGuid()
            };

            Truck truck = new Truck();

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            unitOfWorkMock.Setup(x => x.Truck.GetById(order.TruckId.Value)).ReturnsAsync(truck);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await orderService.FinishOrder(orderId));

            return Task.CompletedTask;
        }

        [Test]
        public Task FinishOrder_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenOrderUserDoesNotExist()
        {

            // Arrange
            Guid orderId = Guid.NewGuid();

            Order order = new Order
            {
                TruckId = Guid.NewGuid()
            };

            Truck truck = new Truck();

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            unitOfWorkMock.Setup(x => x.Truck.GetById(order.TruckId.Value)).ReturnsAsync(truck);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await orderService.FinishOrder(orderId));

            Assert.That(argumentException.Message, Is.EqualTo(UserNotExistMessage));

            return Task.CompletedTask;
        }

        // AddOrderToTruck
        [Test]
        public Task AddOrderToTruck_ShouldThrowArgumentException_WhenTruckDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = null!;

            Guid orderId = Guid.NewGuid();

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await orderService.AddOrderToTruck(truckId, orderId));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddOrderToTruck_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTruckDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = null!;

            Guid orderId = Guid.NewGuid();

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await orderService.AddOrderToTruck(truckId, orderId));

            Assert.That(argumentException.Message, Is.EqualTo(TruckNotExistMessage));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddOrderToTruck_ShouldThrowArgumentException_WhenOrderDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = new Truck();

            Guid orderId = Guid.NewGuid();

            Order order = null!;

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await orderService.AddOrderToTruck(truckId, orderId));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddOrderToTruck_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenOrderDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = new Truck();

            Guid orderId = Guid.NewGuid();

            Order order = null!;

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await orderService.AddOrderToTruck(truckId, orderId));

            Assert.That(argumentException.Message, Is.EqualTo(OrderNotExistMessage));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddOrderToTruck_ShouldThrowArgumentException_WhenTruckUserDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = new Truck();

            Guid orderId = Guid.NewGuid();

            Order order = new Order();

            User user = null!;

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            userManagerMock.Setup(x => x.FindByIdAsync(truck.UserId.ToString())).ReturnsAsync(user);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await orderService.AddOrderToTruck(truckId, orderId));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddOrderToTruck_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTruckUserDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = new Truck();

            Guid orderId = Guid.NewGuid();

            Order order = new Order();

            User user = null!;

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            unitOfWorkMock.Setup(x => x.Order.GetById(orderId)).ReturnsAsync(order);

            userManagerMock.Setup(x => x.FindByIdAsync(truck.UserId.ToString())).ReturnsAsync(user);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await orderService.AddOrderToTruck(truckId, orderId));

            Assert.That(argumentException.Message, Is.EqualTo(UserNotExistMessage));

            return Task.CompletedTask;
        }
    }
}

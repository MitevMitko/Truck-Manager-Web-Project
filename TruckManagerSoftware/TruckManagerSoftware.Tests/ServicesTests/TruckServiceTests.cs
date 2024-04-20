namespace TruckManagerSoftware.Tests.ServicesTests
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Moq;

    using Core.Models.Engine;
    using Core.Models.Order;
    using Core.Models.Garage;
    using Core.Models.Trailer;
    using Core.Models.Transmission;
    using Core.Models.Truck;
    using Core.Models.User;
    using Core.Services.Contract;
    using Core.Services.Implementation;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;

    using static Common.Messages.Messages.Engine;
    using static Common.Messages.Messages.Garage;
    using static Common.Messages.Messages.Transmission;
    using static Common.Messages.Messages.Truck;

    public class TruckServiceTests
    {
        private readonly TruckService truckService;

        private readonly Mock<IUnitOfWork> unitOfWorkMock;

        private readonly Mock<IImageService> imageServiceMock;

        private readonly Mock<IWebHostEnvironment> webHostEnvironmentMock;

        private readonly Mock<UserManager<User>> userManagerMock;

        public TruckServiceTests()
        {
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
            this.imageServiceMock = new Mock<IImageService>();
            this.webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            this.userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            this.truckService = new TruckService(
                unitOfWorkMock.Object,
                imageServiceMock.Object,
                webHostEnvironmentMock.Object,
                userManagerMock.Object);
        }

        // AddTruck
        [Test]
        public Task AddTruck_ShouldThrowArgumentException_WhenTruckGarageIdDoesNotExist()
        {
            // Arrange
            AddTruckViewModel model = new AddTruckViewModel();

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(t => t.Id == model.GarageId)).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await truckService.AddTruck(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddTruck_ShouldThrowArgumentException_CheckArgumentException_WhenTruckGarageIdDoesNotExist()
        {
            // Arrange
            AddTruckViewModel model = new AddTruckViewModel
            {
                GarageId = Guid.NewGuid()
            };

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == model.GarageId)).ReturnsAsync(false);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await truckService.AddTruck(model));

            Assert.That(argumentException.Message, Is.EqualTo(GarageNotExistMessage));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddTruck_ShouldThrowArgumentException_WhenTruckEngineIdDoesNotExist()
        {
            // Arrange
            AddTruckViewModel model = new AddTruckViewModel();

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == model.GarageId)).ReturnsAsync(true);

            unitOfWorkMock.Setup(x => x.Engine.AnyAsync(e => e.Id == model.EngineId)).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await truckService.AddTruck(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddTruck_ShouldThrowArgumentException_CheckArgumentException_WhenTruckEngineIdDoesNotExist()
        {
            // Arrange
            AddTruckViewModel model = new AddTruckViewModel();

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == model.GarageId)).ReturnsAsync(true);

            unitOfWorkMock.Setup(x => x.Engine.AnyAsync(e => e.Id == model.EngineId)).ReturnsAsync(false);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await truckService.AddTruck(model));

            Assert.That(argumentException.Message, Is.EqualTo(EngineNotExistMessage));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddTruck_ShouldThrowArgumentException_WhenTruckTransmissionIdDoesNotExist()
        {
            // Arrange
            AddTruckViewModel model = new AddTruckViewModel();

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == model.GarageId)).ReturnsAsync(true);

            unitOfWorkMock.Setup(x => x.Engine.AnyAsync(e => e.Id == model.EngineId)).ReturnsAsync(true);

            unitOfWorkMock.Setup(x => x.Transmission.AnyAsync(t => t.Id == model.TransmissionId)).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await truckService.AddTruck(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddTruck_ShouldThrowArgumentException_CheckArgumentException_WhenTruckTransmissionIdDoesNotExist()
        {
            // Arrange
            AddTruckViewModel model = new AddTruckViewModel();

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == model.GarageId)).ReturnsAsync(true);

            unitOfWorkMock.Setup(x => x.Engine.AnyAsync(e => e.Id == model.EngineId)).ReturnsAsync(true);

            unitOfWorkMock.Setup(x => x.Transmission.AnyAsync(t => t.Id == model.TransmissionId)).ReturnsAsync(false);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await truckService.AddTruck(model));

            Assert.That(argumentException.Message, Is.EqualTo(TransmissionNotExistMessage));

            return Task.CompletedTask;
        }

        // EditTruck
        [Test]
        public Task EditTruck_ShouldThrowArgumentException_WhenTruckDoesNotExist()
        {
            // Arrange
            Truck truck = null!;

            EditTruckViewModel model = new EditTruckViewModel();

            unitOfWorkMock.Setup(x => x.Truck.GetById(model.Id)).ReturnsAsync(truck);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await truckService.EditTruck(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task EditTruck_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTruckDoesNotExist()
        {
            // Arrange
            Truck truck = null!;

            EditTruckViewModel model = new EditTruckViewModel();

            unitOfWorkMock.Setup(x => x.Truck.GetById(model.Id)).ReturnsAsync(truck);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await truckService.EditTruck(model));

            Assert.That(argumentException.Message, Is.EqualTo(TruckNotExistMessage));

            return Task.CompletedTask;
        }

        [Test]
        public Task EditTruck_ShouldThrowArgumentException_WhenTruckGarageIdDoesNotExist()
        {
            // Arrange
            Truck truck = new Truck();

            EditTruckViewModel model = new EditTruckViewModel();

            unitOfWorkMock.Setup(x => x.Truck.GetById(model.Id)).ReturnsAsync(truck);

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == model.GarageId)).ReturnsAsync(false);

            // Act & Arrange
            Assert.ThrowsAsync<ArgumentException>(async () => await truckService.EditTruck(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task EditTruck_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTruckGarageIdDoesNotExist()
        {
            // Arrange
            Truck truck = new Truck();

            EditTruckViewModel model = new EditTruckViewModel();

            unitOfWorkMock.Setup(x => x.Truck.GetById(model.Id)).ReturnsAsync(truck);

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == model.GarageId)).ReturnsAsync(false);

            // Act & Arrange
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await truckService.EditTruck(model));

            Assert.That(argumentException.Message, Is.EqualTo(GarageNotExistMessage));

            return Task.CompletedTask;
        }

        [Test]
        public Task EditTruck_ShouldThrowArgumentException_WhenTruckEngineIdDoesNotExist()
        {
            // Arrange
            Truck truck = new Truck();

            EditTruckViewModel model = new EditTruckViewModel();

            unitOfWorkMock.Setup(x => x.Truck.GetById(model.Id)).ReturnsAsync(truck);

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == model.GarageId)).ReturnsAsync(true);

            unitOfWorkMock.Setup(x => x.Engine.AnyAsync(e => e.Id == model.EngineId)).ReturnsAsync(false);

            // Act & Arrange
            Assert.ThrowsAsync<ArgumentException>(async () => await truckService.EditTruck(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task EditTruck_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTruckEngineIdDoesNotExist()
        {
            // Arrange
            Truck truck = new Truck();

            EditTruckViewModel model = new EditTruckViewModel();

            unitOfWorkMock.Setup(x => x.Truck.GetById(model.Id)).ReturnsAsync(truck);

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == model.GarageId)).ReturnsAsync(true);

            unitOfWorkMock.Setup(x => x.Engine.AnyAsync(e => e.Id == model.EngineId)).ReturnsAsync(false);

            // Act & Arrange
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await truckService.EditTruck(model));

            Assert.That(argumentException.Message, Is.EqualTo(EngineNotExistMessage));

            return Task.CompletedTask;
        }

        [Test]
        public Task EditTruck_ShouldThrowArgumentException_WhenTruckTransmissionIdDoesNotExist()
        {
            // Arrange
            Truck truck = new Truck();

            EditTruckViewModel model = new EditTruckViewModel();

            unitOfWorkMock.Setup(x => x.Truck.GetById(model.Id)).ReturnsAsync(truck);

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == model.GarageId)).ReturnsAsync(true);

            unitOfWorkMock.Setup(x => x.Engine.AnyAsync(e => e.Id == model.EngineId)).ReturnsAsync(true);

            unitOfWorkMock.Setup(x => x.Transmission.AnyAsync(t => t.Id == model.TransmissionId)).ReturnsAsync(false);

            // Act & Arrange
            Assert.ThrowsAsync<ArgumentException>(async () => await truckService.EditTruck(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task EditTruck_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTruckTransmissionIdDoesNotExist()
        {
            // Arrange
            Truck truck = new Truck();

            EditTruckViewModel model = new EditTruckViewModel();

            unitOfWorkMock.Setup(x => x.Truck.GetById(model.Id)).ReturnsAsync(truck);

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == model.GarageId)).ReturnsAsync(true);

            unitOfWorkMock.Setup(x => x.Engine.AnyAsync(e => e.Id == model.EngineId)).ReturnsAsync(true);

            unitOfWorkMock.Setup(x => x.Transmission.AnyAsync(t => t.Id == model.TransmissionId)).ReturnsAsync(false);

            // Act & Arrange
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await truckService.EditTruck(model));

            Assert.That(argumentException.Message, Is.EqualTo(TransmissionNotExistMessage));

            return Task.CompletedTask;
        }

        // GetAdditionalTruckInfoById
       [Test]
        public async Task GetAdditionalTruckInfoById_ShouldReturnTruckAdditionalInfoViewModel_WhenTruckExists()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = new Truck
            {
                Id = truckId,
                Brand = "Scania",
                Series = "R",
                DrivenDistance = 2000,
                Image = Guid.NewGuid().ToString(),
                GarageId = Guid.NewGuid(),
                TrailerId = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                EngineId = Guid.NewGuid(),
                TransmissionId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };

            Garage garage = new Garage
            {
                Id = truck.GarageId.Value,
                Country = "Bulgaria",
                City = "Ruse",
                Size = "small"
            };

            Trailer trailer = new Trailer
            {
                Id = truck.TrailerId.Value,
                Title = "Trailer",
                Series = "Series"
            };

            Order order = new Order
            {
                Id = truck.OrderId.Value,
                Cargo = "Woods"
            };

            Engine engine = new Engine
            {
                Id = truck.EngineId.Value,
                Title = "Engine"
            };

            Transmission transmission = new Transmission
            {
                Id = truck.TransmissionId.Value,
                Title = "Transmission"
            };

            User user = new User
            {
                Id = truck.UserId.Value,
                UserName = "User"
            };

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);
            unitOfWorkMock.Setup(x => x.Garage.GetById(truck.GarageId!.Value)).ReturnsAsync(garage);
            unitOfWorkMock.Setup(x => x.Trailer.GetById(truck.TrailerId.Value)).ReturnsAsync(trailer);
            unitOfWorkMock.Setup(x => x.Order.GetById(truck.OrderId.Value)).ReturnsAsync(order);
            unitOfWorkMock.Setup(x => x.Engine.GetById(truck.EngineId.Value)).ReturnsAsync(engine);
            unitOfWorkMock.Setup(x => x.Transmission.GetById(truck.TransmissionId.Value)).ReturnsAsync(transmission);
            userManagerMock.Setup(x => x.FindByIdAsync(truck.UserId.ToString())).ReturnsAsync(user);

            // Act
            TruckAdditionalInfoViewModel serviceModel = await truckService.GetAdditionalTruckInfoById(truckId);

            // Assert
            Assert.IsInstanceOf<TruckAdditionalInfoViewModel>(serviceModel);
            Assert.That(serviceModel.Id, Is.EqualTo(truck.Id));
            Assert.That(serviceModel.Brand, Is.EqualTo(truck.Brand));
            Assert.That(serviceModel.Series, Is.EqualTo(truck.Series));
            Assert.That(serviceModel.DrivenDistance, Is.EqualTo(truck.DrivenDistance));
            Assert.That(serviceModel.Image, Is.EqualTo(truck.Image));

            Assert.IsInstanceOf<GarageInfoViewModel>(serviceModel.GarageInfo);
            Assert.That(serviceModel.GarageInfo!.Id, Is.EqualTo(garage.Id));
            Assert.That(serviceModel.GarageInfo!.Country, Is.EqualTo(garage.Country));
            Assert.That(serviceModel.GarageInfo!.City, Is.EqualTo(garage.City));
            Assert.That(serviceModel.GarageInfo!.Size, Is.EqualTo(garage.Size));

            Assert.IsInstanceOf<TrailerInfoViewModel>(serviceModel.TrailerInfo);
            Assert.That(serviceModel.TrailerInfo!.Id, Is.EqualTo(trailer.Id));
            Assert.That(serviceModel.TrailerInfo!.Title, Is.EqualTo(trailer.Title));
            Assert.That(serviceModel.TrailerInfo!.Series, Is.EqualTo(trailer.Series));

            Assert.IsInstanceOf<OrderInfoViewModel>(serviceModel.OrderInfo);
            Assert.That(serviceModel.OrderInfo!.Id, Is.EqualTo(order.Id));
            Assert.That(serviceModel.OrderInfo!.Cargo, Is.EqualTo(order.Cargo));

            Assert.IsInstanceOf<EngineInfoViewModel>(serviceModel.EngineInfo);
            Assert.That(serviceModel.EngineInfo!.Id, Is.EqualTo(engine.Id));
            Assert.That(serviceModel.EngineInfo!.Title, Is.EqualTo(engine.Title));

            Assert.IsInstanceOf<TransmissionInfoViewModel>(serviceModel.TransmissionInfo);
            Assert.That(serviceModel.TransmissionInfo!.Id, Is.EqualTo(transmission.Id));
            Assert.That(serviceModel.TransmissionInfo!.Title, Is.EqualTo(transmission.Title));

            Assert.IsInstanceOf<UserInfoViewModel>(serviceModel.UserInfo);
            Assert.That(serviceModel.UserInfo!.Id, Is.EqualTo(user.Id));
            Assert.That(serviceModel.UserInfo!.UserName, Is.EqualTo(user.UserName));
        }

        [Test]
        public async Task GetAdditionalTruckInfoById_ShouldReturnTruckAdditionalInfoViewModel_WhenTruckExists_AndWhenTruckDoesNotHaveGarageTrailerOrderEngineTransmissionUser()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = new Truck
            {
                Id = truckId,
                Brand = "Scania",
                Series = "R",
                DrivenDistance = 2000,
                Image = Guid.NewGuid().ToString(),
                GarageId = null,
                TrailerId = null,
                OrderId = null,
                EngineId = null,
                TransmissionId = null,
                UserId = null
            };

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act
            TruckAdditionalInfoViewModel serviceModel = await truckService.GetAdditionalTruckInfoById(truckId);

            // Assert
            Assert.IsInstanceOf<TruckAdditionalInfoViewModel>(serviceModel);
            Assert.That(serviceModel.Id, Is.EqualTo(truck.Id));
            Assert.That(serviceModel.Brand, Is.EqualTo(truck.Brand));
            Assert.That(serviceModel.Series, Is.EqualTo(truck.Series));
            Assert.That(serviceModel.DrivenDistance, Is.EqualTo(truck.DrivenDistance));
            Assert.That(serviceModel.Image, Is.EqualTo(truck.Image));

            Assert.That(serviceModel.GarageInfo, Is.EqualTo(null));

            Assert.That(serviceModel.TrailerInfo, Is.EqualTo(null));

            Assert.That(serviceModel.OrderInfo, Is.EqualTo(null));

            Assert.That(serviceModel.EngineInfo, Is.EqualTo(null));

            Assert.That(serviceModel.TransmissionInfo, Is.EqualTo(null));

            Assert.That(serviceModel.UserInfo, Is.EqualTo(null));
        }

        [Test]
        public Task GetAdditionalTruckInfoById_ShouldThrowArgumentException_WhenTruckDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = null!;

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await truckService.GetAdditionalTruckInfoById(truckId));

            return Task.CompletedTask;
        }

        [Test]
        public Task GetAdditionalTruckInfoById_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTruckDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = null!;

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await truckService.GetAdditionalTruckInfoById(truckId));

            Assert.That(argumentException.Message, Is.EqualTo(TruckNotExistMessage));

            return Task.CompletedTask;
        }

        // GetAllTrucksInfo
        [Test]
        public async Task GetAllTrucksInfo_ShouldReturnCollectionOfTruckInfoViewModel()
        {
            // Arrange
            Truck scaniaTruck = new Truck
            {
                Id = Guid.NewGuid(),
                Brand = "Scania",
                Series = "S",
                DrivenDistance = 2000,
                Image = Guid.NewGuid().ToString()
            };

            Truck dafTruck = new Truck
            {
                Id = Guid.NewGuid(),
                Brand = "Daf",
                Series = "XF",
                DrivenDistance = 3000,
                Image = Guid.NewGuid().ToString()
            };

            IEnumerable<Truck> trucks = new List<Truck>
            {
                scaniaTruck,
                dafTruck
            };

            unitOfWorkMock.Setup(x => x.Truck.GetAll()).ReturnsAsync(trucks);

            // Act
            ICollection<TruckInfoViewModel> serviceModel = await truckService.GetAllTrucksInfo();

            // Assert
            Assert.IsInstanceOf<ICollection<TruckInfoViewModel>>(serviceModel);
            Assert.IsNotNull(serviceModel);
            Assert.That(serviceModel.Count, Is.EqualTo(trucks.Count()));

            int cnt = 0;

            foreach (TruckInfoViewModel truckInfo in serviceModel)
            {
                if (cnt == 0)
                {
                    Assert.IsInstanceOf<TruckInfoViewModel>(truckInfo);
                    Assert.That(truckInfo.Id, Is.EqualTo(scaniaTruck.Id));
                    Assert.That(truckInfo.Brand, Is.EqualTo(scaniaTruck.Brand));
                    Assert.That(truckInfo.Series, Is.EqualTo(scaniaTruck.Series));
                    Assert.That(truckInfo.DrivenDistance, Is.EqualTo(scaniaTruck.DrivenDistance));
                    Assert.That(truckInfo.Image, Is.EqualTo(scaniaTruck.Image));

                    cnt++;
                }
                else
                {
                    Assert.IsInstanceOf<TruckInfoViewModel>(truckInfo);
                    Assert.That(truckInfo.Id, Is.EqualTo(dafTruck.Id));
                    Assert.That(truckInfo.Brand, Is.EqualTo(dafTruck.Brand));
                    Assert.That(truckInfo.Series, Is.EqualTo(dafTruck.Series));
                    Assert.That(truckInfo.DrivenDistance, Is.EqualTo(dafTruck.DrivenDistance));
                    Assert.That(truckInfo.Image, Is.EqualTo(dafTruck.Image));
                }
            }
        }

        // GetTruckInfoById
        [Test]
        public async Task GetTruckInfoById_ShouldReturnTruckInfoViewModel_WhenTruckExists()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = new Truck
            {
                Id = truckId,
                Brand = "Scania",
                Series = "S",
                DrivenDistance = 2000,
                Image = Guid.NewGuid().ToString(),
                GarageId = Guid.NewGuid(),
                EngineId = Guid.NewGuid(),
                TransmissionId = Guid.NewGuid(),
            };

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act
            TruckInfoViewModel serviceModel = await truckService.GetTruckInfoById(truckId);

            // Assert
            Assert.IsInstanceOf<TruckInfoViewModel>(serviceModel);
            Assert.That(serviceModel.Id, Is.EqualTo(truck.Id));
            Assert.That(serviceModel.Brand, Is.EqualTo(truck.Brand));
            Assert.That(serviceModel.Series, Is.EqualTo(truck.Series));
            Assert.That(serviceModel.DrivenDistance, Is.EqualTo(truck.DrivenDistance));
            Assert.That(serviceModel.Image, Is.EqualTo(truck.Image));
            Assert.That(serviceModel.GarageId, Is.EqualTo(truck.GarageId));
            Assert.That(serviceModel.EngineId, Is.EqualTo(truck.EngineId));
            Assert.That(serviceModel.TransmissionId, Is.EqualTo(truck.TransmissionId));
        }

        [Test]
        public Task GetTruckInfoById_ShouldThrowArgumentException_WhenTruckDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = null!;

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await truckService.GetTruckInfoById(truckId));

            return Task.CompletedTask;
        }

        [Test]
        public Task GetTruckInfoById_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTruckDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = null!;

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await truckService.GetTruckInfoById(truckId));

            Assert.That(argumentException.Message, Is.EqualTo(TruckNotExistMessage));

            return Task.CompletedTask;
        }

        // RemoveTruck
        [Test]
        public Task RemoveTruck_ShouldThrowArgumentException_WhenTruckDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = null!;

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await truckService.RemoveTruck(truckId));

            return Task.CompletedTask;
        }

        [Test]
        public Task RemoveTruck_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTruckDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = null!;

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await truckService.RemoveTruck(truckId));

            Assert.That(argumentException.Message, Is.EqualTo(TruckNotExistMessage));

            return Task.CompletedTask;
        }
    }
}

namespace TruckManagerSoftware.Tests.ServicesTests
{
    using Microsoft.AspNetCore.Identity;
    using Moq;

    using Core.Models.Garage;
    using Core.Services.Implementation;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;

    using static Common.Messages.Messages.Garage;
    using static Common.Messages.Messages.Trailer;
    using static Common.Messages.Messages.Truck;
    using static Common.Messages.Messages.User;

    public class GarageServiceTests
    {
        private readonly GarageService garageService;

        private readonly Mock<IUnitOfWork> unitOfWorkMock;

        private readonly Mock<UserManager<User>> userManagerMock;

        public GarageServiceTests()
        {
            this.unitOfWorkMock = new Mock<IUnitOfWork>();

            this.userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);

            this.garageService = new GarageService(
                userManagerMock.Object,
                unitOfWorkMock.Object);
        }

        // EditGarage
        [Test]
        public Task EditGarage_ShouldThrowArgumentException_WhenGarageDoesNotExist()
        {
            // Arrange
            EditGarageViewModel model = new EditGarageViewModel();

            Garage garage = null!;

            unitOfWorkMock.Setup(x => x.Garage.GetById(model.Id)).ReturnsAsync(garage);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await garageService.EditGarage(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task EditGarage_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenGarageDoesNotExist()
        {
            // Arrange
            EditGarageViewModel model = new EditGarageViewModel();

            Garage garage = null!;

            unitOfWorkMock.Setup(x => x.Garage.GetById(model.Id)).ReturnsAsync(garage);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await garageService.EditGarage(model));

            Assert.That(argumentException.Message, Is.EqualTo(GarageNotExistMessage));

            return Task.CompletedTask;
        }

        // GetAllGaragesInfo
        [Test]
        public async Task GetAllGaragesInfo_ShouldReturnICollectionOfGarageInfoViewModel()
        {
            // Arrange
            Garage garageBulgaria = new Garage
            {
                Id = Guid.NewGuid(),
                Country = "Bulgaria",
                City = "Ruse",
                Size = "large"
            };

            Garage garageGermany = new Garage
            {
                Id = Guid.NewGuid(),
                Country = "Germany",
                City = "Berlin",
                Size = "small"
            };

            IEnumerable<Garage> garages = new List<Garage>
            {
                garageBulgaria,
                garageGermany
            };

            unitOfWorkMock.Setup(x => x.Garage.GetAll()).ReturnsAsync(garages);

            // Act
            ICollection<GarageInfoViewModel> serviceModel = await garageService.GetAllGaragesInfo();

            // Assert
            Assert.IsInstanceOf<ICollection<GarageInfoViewModel>>(serviceModel);
            Assert.IsNotNull(serviceModel);
            Assert.That(serviceModel.Count, Is.EqualTo(garages.Count()));

            int cnt = 0;

            foreach (GarageInfoViewModel garageInfo in serviceModel)
            {
                if (cnt == 0)
                {
                    Assert.IsInstanceOf<GarageInfoViewModel>(garageInfo);
                    Assert.That(garageInfo.Id, Is.EqualTo(garageBulgaria.Id));
                    Assert.That(garageInfo.Country, Is.EqualTo(garageBulgaria.Country));
                    Assert.That(garageInfo.City, Is.EqualTo(garageBulgaria.City));
                    Assert.That(garageInfo.Size, Is.EqualTo(garageBulgaria.Size));

                    cnt++;
                }
                else
                {
                    Assert.IsInstanceOf<GarageInfoViewModel>(garageInfo);
                    Assert.That(garageInfo.Id, Is.EqualTo(garageGermany.Id));
                    Assert.That(garageInfo.Country, Is.EqualTo(garageGermany.Country));
                    Assert.That(garageInfo.City, Is.EqualTo(garageGermany.City));
                    Assert.That(garageInfo.Size, Is.EqualTo(garageGermany.Size));
                }
            }
        }

        // GetAllGaragesInfoWithFreeSpaceForTrailers
        [Test]
        public async Task GetAllGaragesInfoWithFreeSpaceForTrailers_ShouldReturnICollectionOfGarageInfoViewModel_WhenGaragesHaveFreeSpace()
        {
            // Arrange
            Garage garageBulgaria = new Garage
            {
                Id = Guid.NewGuid(),
                Country = "Bulgaria",
                City = "Ruse",
                Size = "small"
            };

            IEnumerable<Garage> garages = new List<Garage>
            {
                garageBulgaria
            };

            Trailer scaniaTrailer = new Trailer();

            Trailer dafTrailer = new Trailer();

            IQueryable<Trailer> trailers = new List<Trailer>
            {
                scaniaTrailer,
                dafTrailer
            }
            .AsQueryable();

            unitOfWorkMock.Setup(x => x.Garage.GetAll()).ReturnsAsync(garages);

            unitOfWorkMock.Setup(x => x.Trailer.Find(t => t.GarageId == garageBulgaria.Id)).Returns(trailers);

            // Act
            ICollection<GarageInfoViewModel> serviceModel = await garageService.GetAllGaragesInfoWithFreeSpaceForTrailers();

            // Assert
            Assert.IsInstanceOf<ICollection<GarageInfoViewModel>>(serviceModel);
            Assert.IsNotNull(serviceModel);
            Assert.That(serviceModel.Count, Is.EqualTo(garages.Count()));

            int cnt = 0;

            foreach (GarageInfoViewModel garageInfo in serviceModel)
            {
                if (cnt == 0)
                {
                    Assert.IsInstanceOf<GarageInfoViewModel>(garageInfo);
                    Assert.That(garageInfo.Id, Is.EqualTo(garageBulgaria.Id));
                    Assert.That(garageInfo.Country, Is.EqualTo(garageBulgaria.Country));
                    Assert.That(garageInfo.City, Is.EqualTo(garageBulgaria.City));
                    Assert.That(garageInfo.Size, Is.EqualTo(garageBulgaria.Size));

                    cnt++;
                }
                else
                {
                    continue;
                }
            }
        }

        [Test]
        public async Task GetAllGaragesInfoWithFreeSpaceForTrailers_ShouldReturnEmptyICollectionOfGarageInfoViewModel_WhenGaragesDoesNotHaveFreeSpace()
        {
            // Arrange
            Garage garageBulgaria = new Garage
            {
                Id = Guid.NewGuid(),
                Country = "Bulgaria",
                City = "Ruse",
                Size = "small"
            };

            IEnumerable<Garage> garages = new List<Garage>
            {
                garageBulgaria
            };

            Trailer scaniaTrailer = new Trailer();

            Trailer dafTrailer = new Trailer();

            Trailer volvoTrailer = new Trailer();

            Trailer renaultTrailer = new Trailer();

            Trailer mercedesTrailer = new Trailer();

            Trailer ivecoTrailer = new Trailer();

            IQueryable<Trailer> trailers = new List<Trailer>
            {
                scaniaTrailer,
                dafTrailer,
                volvoTrailer,
                renaultTrailer,
                mercedesTrailer,
                ivecoTrailer
            }
            .AsQueryable();

            unitOfWorkMock.Setup(x => x.Garage.GetAll()).ReturnsAsync(garages);

            unitOfWorkMock.Setup(x => x.Trailer.Find(t => t.GarageId == garageBulgaria.Id)).Returns(trailers);

            // Act
            ICollection<GarageInfoViewModel> serviceModel = await garageService.GetAllGaragesInfoWithFreeSpaceForTrailers();

            // Assert
            Assert.IsInstanceOf<ICollection<GarageInfoViewModel>>(serviceModel);
            Assert.IsNotNull(serviceModel);
            Assert.IsEmpty(serviceModel);
            Assert.That(serviceModel.Count, Is.LessThan(trailers.ToList().Count));
        }

        // GetAllGaragesInfoWithFreeSpaceForTrucks
        [Test]
        public async Task GetAllGaragesInfoWithFreeSpaceForTrucks_ShouldReturnICollectionOfGarageInfoViewModel_WhenGaragesHaveFreeSpace()
        {
            // Arrange
            Garage garageBulgaria = new Garage
            {
                Id = Guid.NewGuid(),
                Country = "Bulgaria",
                City = "Ruse",
                Size = "small"
            };

            IEnumerable<Garage> garages = new List<Garage>
            {
                garageBulgaria
            };

            Truck scaniaTruck = new Truck();

            Truck dafTruck = new Truck();

            IQueryable<Truck> trucks = new List<Truck>
            {
                scaniaTruck,
                dafTruck
            }
            .AsQueryable();

            unitOfWorkMock.Setup(x => x.Garage.GetAll()).ReturnsAsync(garages);

            unitOfWorkMock.Setup(x => x.Truck.Find(t => t.GarageId == garageBulgaria.Id)).Returns(trucks);

            // Act
            ICollection<GarageInfoViewModel> serviceModel = await garageService.GetAllGaragesInfoWithFreeSpaceForTrucks();

            // Assert
            Assert.IsInstanceOf<ICollection<GarageInfoViewModel>>(serviceModel);
            Assert.IsNotNull(serviceModel);
            Assert.That(serviceModel.Count, Is.EqualTo(garages.Count()));

            int cnt = 0;

            foreach (GarageInfoViewModel garageInfo in serviceModel)
            {
                if (cnt == 0)
                {
                    Assert.IsInstanceOf<GarageInfoViewModel>(garageInfo);
                    Assert.That(garageInfo.Id, Is.EqualTo(garageBulgaria.Id));
                    Assert.That(garageInfo.Country, Is.EqualTo(garageBulgaria.Country));
                    Assert.That(garageInfo.City, Is.EqualTo(garageBulgaria.City));
                    Assert.That(garageInfo.Size, Is.EqualTo(garageBulgaria.Size));

                    cnt++;
                }
                else
                {
                    continue;
                }
            }
        }

        [Test]
        public async Task GetAllGaragesInfoWithFreeSpaceForTrucks_ShouldReturnEmptyICollectionOfGarageInfoViewModel_WhenGaragesDoesNotHaveFreeSpace()
        {
            // Arrange
            Garage garageBulgaria = new Garage
            {
                Id = Guid.NewGuid(),
                Country = "Bulgaria",
                City = "Ruse",
                Size = "small"
            };

            IEnumerable<Garage> garages = new List<Garage>
            {
                garageBulgaria
            };

            Truck scaniaTruck = new Truck();

            Truck dafTruck = new Truck();

            Truck volvoTruck = new Truck();

            Truck renaultTruck = new Truck();

            Truck mercedesTruck = new Truck();

            Truck ivecoTruck = new Truck();

            IQueryable<Truck> trucks = new List<Truck>
            {
                scaniaTruck,
                dafTruck,
                volvoTruck,
                renaultTruck,
                mercedesTruck,
                ivecoTruck
            }
            .AsQueryable();

            unitOfWorkMock.Setup(x => x.Garage.GetAll()).ReturnsAsync(garages);

            unitOfWorkMock.Setup(x => x.Truck.Find(t => t.GarageId == garageBulgaria.Id)).Returns(trucks);

            // Act
            ICollection<GarageInfoViewModel> serviceModel = await garageService.GetAllGaragesInfoWithFreeSpaceForTrucks();

            // Assert
            Assert.IsInstanceOf<ICollection<GarageInfoViewModel>>(serviceModel);
            Assert.IsNotNull(serviceModel);
            Assert.IsEmpty(serviceModel);
            Assert.That(serviceModel.Count, Is.LessThan(trucks.ToList().Count));
        }

        // GetGarageTrailersInfo
        [Test]
        public async Task GetGarageTrailersInfo_ShouldReturnICollectionOfGarageTrailerInfoViewModel_WhenGarageExists()
        {
            // Arrange
            Guid garageId = Guid.NewGuid();

            Garage garage = new Garage();

            Trailer scaniaTrailer = new Trailer()
            {
                Id = Guid.NewGuid(),
                Title = "ScaniaTrailer",
                Series = "Series",
                TrailerType = "Type",
                BodyType = "Type",
                TareWeight = 1000,
                AxleCount = 3,
                TotalLength = 15.9,
                CargoTypes = "Wood",
                Image = Guid.NewGuid().ToString(),
                TruckId = Guid.NewGuid()
            };

            Trailer dafTrailer = new Trailer
            {
                Id = Guid.NewGuid(),
                Title = "DafTrailer",
                Series = "Series",
                TrailerType = "Type",
                BodyType = "Type",
                TareWeight = 3000,
                AxleCount = 2,
                TotalLength = 14.5,
                CargoTypes = "Steel",
                Image = Guid.NewGuid().ToString(),
                TruckId = Guid.NewGuid()
            };

            IQueryable<Trailer> trailers = new List<Trailer>
            {
                scaniaTrailer,
                dafTrailer
            }
            .AsQueryable();

            unitOfWorkMock.Setup(x => x.Garage.GetById(garageId)).ReturnsAsync(garage);

            unitOfWorkMock.Setup(x => x.Trailer.Find(t => t.GarageId == garageId)).Returns(trailers);

            // Act
            ICollection<GarageTrailerInfoViewModel> serviceModel = await garageService.GetGarageTrailersInfo(garageId);

            // Assert
            Assert.IsInstanceOf<ICollection<GarageTrailerInfoViewModel>>(serviceModel);
            Assert.IsNotNull(serviceModel);
            Assert.That(serviceModel.Count, Is.EqualTo(trailers.ToList().Count));

            int cnt = 0;

            foreach (GarageTrailerInfoViewModel garageTrailerInfo in serviceModel)
            {
                if (cnt == 0)
                {
                    Assert.IsInstanceOf<GarageTrailerInfoViewModel>(garageTrailerInfo);
                    Assert.That(garageTrailerInfo.Id, Is.EqualTo(scaniaTrailer.Id));
                    Assert.That(garageTrailerInfo.TruckId, Is.EqualTo(scaniaTrailer.TruckId));
                    Assert.That(garageTrailerInfo.Title, Is.EqualTo(scaniaTrailer.Title));
                    Assert.That(garageTrailerInfo.Series, Is.EqualTo(scaniaTrailer.Series));
                    Assert.That(garageTrailerInfo.TrailerType, Is.EqualTo(scaniaTrailer.TrailerType));
                    Assert.That(garageTrailerInfo.BodyType, Is.EqualTo(scaniaTrailer.BodyType));

                    cnt++;
                }
                else
                {
                    Assert.IsInstanceOf<GarageTrailerInfoViewModel>(garageTrailerInfo);
                    Assert.That(garageTrailerInfo.Id, Is.EqualTo(dafTrailer.Id));
                    Assert.That(garageTrailerInfo.TruckId, Is.EqualTo(dafTrailer.TruckId));
                    Assert.That(garageTrailerInfo.Title, Is.EqualTo(dafTrailer.Title));
                    Assert.That(garageTrailerInfo.Series, Is.EqualTo(dafTrailer.Series));
                    Assert.That(garageTrailerInfo.TrailerType, Is.EqualTo(dafTrailer.TrailerType));
                    Assert.That(garageTrailerInfo.BodyType, Is.EqualTo(dafTrailer.BodyType));
                }
            }
        }

        [Test]
        public Task GetGarageTrailersInfo_ShouldThrowArgumentException_WhenGarageDoesNotExist()
        {
            // Arrange
            Guid garageId = Guid.NewGuid();

            Garage garage = null!;

            unitOfWorkMock.Setup(x => x.Garage.GetById(garageId)).ReturnsAsync(garage);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await garageService.GetGarageTrailersInfo(garageId));

            return Task.CompletedTask;
        }

        [Test]
        public Task GetGarageTrailersInfo_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenGarageDoesNotExist()
        {
            // Arrange
            Guid garageId = Guid.NewGuid();

            Garage garage = null!;

            unitOfWorkMock.Setup(x => x.Garage.GetById(garageId)).ReturnsAsync(garage);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await garageService.GetGarageTrailersInfo(garageId));

            Assert.That(argumentException.Message, Is.EqualTo(GarageNotExistMessage));

            return Task.CompletedTask;
        }

        // GetGarageInfoById
        [Test]
        public async Task GetGarageInfoById_ShouldReturnGarageInfoViewModel_WhenGarageExists()
        {
            // Arrange
            Guid garageId = Guid.NewGuid();

            Garage garage = new Garage
            {
                Id = garageId,
                Country = "Bulgaria",
                City = "Ruse",
                Size = "small"
            };

            unitOfWorkMock.Setup(x => x.Garage.GetById(garageId)).ReturnsAsync(garage);

            // Act
            GarageInfoViewModel serviceModel = await garageService.GetGarageInfoById(garageId);

            // Assert
            Assert.IsInstanceOf<GarageInfoViewModel>(serviceModel);
            Assert.That(serviceModel.Id, Is.EqualTo(garage.Id));
            Assert.That(serviceModel.Country, Is.EqualTo(garage.Country));
            Assert.That(serviceModel.City, Is.EqualTo(garage.City));
            Assert.That(serviceModel.Size, Is.EqualTo(garage.Size));
            Assert.That(serviceModel.TrucksCapacity, Is.EqualTo(5));
            Assert.That(serviceModel.TrucksCapacity, Is.EqualTo(5));
        }

        [Test]
        public Task GetGarageInfoById_ShouldThrowArgumentException_WhenGarageDoesNotExist()
        {
            // Arrange
            Guid garageId = Guid.NewGuid();

            Garage garage = null!;

            unitOfWorkMock.Setup(x => x.Garage.GetById(garageId)).ReturnsAsync(garage);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await garageService.GetGarageInfoById(garageId));

            return Task.CompletedTask;
        }

        [Test]
        public Task GetGarageInfoById_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenGarageDoesNotExist()
        {
            // Arrange
            Guid garageId = Guid.NewGuid();

            Garage garage = null!;

            unitOfWorkMock.Setup(x => x.Garage.GetById(garageId)).ReturnsAsync(garage);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await garageService.GetGarageInfoById(garageId));

            Assert.That(argumentException.Message, Is.EqualTo(GarageNotExistMessage));

            return Task.CompletedTask;
        }

        // RemoveGarage
        [Test]
        public Task RemoveGarage_ShouldThrowArgumentException_WhenGarageDoesNotExist()
        {
            // Arrange
            Guid garageId = Guid.NewGuid();

            Garage garage = null!;

            unitOfWorkMock.Setup(x => x.Garage.GetById(garageId)).ReturnsAsync(garage);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await garageService.RemoveGarage(garageId));

            return Task.CompletedTask;
        }

        [Test]
        public Task RemoveGarage_ShouldThrowArgumentException_CheckArgumentException_WhenGarageDoesNotExist()
        {
            // Arrange
            Guid garageId = Guid.NewGuid();

            Garage garage = null!;

            unitOfWorkMock.Setup(x => x.Garage.GetById(garageId)).ReturnsAsync(garage);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await garageService.RemoveGarage(garageId));

            Assert.That(argumentException.Message, Is.EqualTo(GarageNotExistMessage));

            return Task.CompletedTask;
        }

        // GetGarageTrucksInfo
        [Test]
        public async Task GetGarageTrucksInfo_ShouldReturnICollectionOfGarageTruckInfoViewModel_WhenGarageExists()
        {
            // Assert
            Guid garageId = Guid.NewGuid();

            Garage garage = new Garage();

            Engine engine = new Engine
            {
                Id = Guid.NewGuid(),
                Title = "ScaniaEngine"
            };

            Transmission transmission = new Transmission
            {
                Id = Guid.NewGuid(),
                Title = "ScaniaTransmission"
            };

            Truck scaniaTruck = new Truck
            {
                Id = Guid.NewGuid(),
                Brand = "Scania",
                Series = "S",
                DrivenDistance = 2000,
                Image = Guid.NewGuid().ToString(),
                TrailerId = Guid.NewGuid(),
                EngineId = Guid.NewGuid(),
                TransmissionId = Guid.NewGuid()
            };

            IQueryable<Truck> trucks = new List<Truck>
            {
                scaniaTruck
            }
            .AsQueryable();

            unitOfWorkMock.Setup(x => x.Garage.GetById(garageId)).ReturnsAsync(garage);

            unitOfWorkMock.Setup(x => x.Truck.Find(t => t.GarageId == garageId)).Returns(trucks);

            unitOfWorkMock.Setup(x => x.Engine.GetById(scaniaTruck.EngineId!.Value)).ReturnsAsync(engine);

            unitOfWorkMock.Setup(x => x.Transmission.GetById(scaniaTruck.TransmissionId!.Value)).ReturnsAsync(transmission);

            // Act
            ICollection<GarageTruckInfoViewModel> serviceModel = await garageService.GetGarageTrucksInfo(garageId);

            // Assert
            Assert.IsInstanceOf<ICollection<GarageTruckInfoViewModel>>(serviceModel);
            Assert.IsNotNull(serviceModel);
            Assert.That(serviceModel.Count, Is.EqualTo(trucks.ToList().Count));

            int cnt = 0;

            foreach (GarageTruckInfoViewModel garageTruckInfo in serviceModel)
            {
                if (cnt == 0)
                {
                    Assert.IsInstanceOf<GarageTruckInfoViewModel>(garageTruckInfo);
                    Assert.That(garageTruckInfo.Id, Is.EqualTo(scaniaTruck.Id));
                    Assert.That(garageTruckInfo.TrailerId, Is.EqualTo(scaniaTruck.TrailerId));
                    Assert.That(garageTruckInfo.Brand, Is.EqualTo(scaniaTruck.Brand));
                    Assert.That(garageTruckInfo.Series, Is.EqualTo(scaniaTruck.Series));
                    Assert.That(garageTruckInfo.EngineTitle, Is.EqualTo(engine.Title));
                    Assert.That(garageTruckInfo.TransmissionTitle, Is.EqualTo(transmission.Title));

                    cnt++;
                }
                else
                {
                    continue;
                }
            }
        }

        [Test]
        public async Task GetGarageTrucksInfo_ShouldReturnICollectionOfGarageTruckInfoViewModel_WhenGarageExists_AndWhenGarageTruckDoesNotHaveEngineTransmission()
        {
            // Assert
            Guid garageId = Guid.NewGuid();

            Garage garage = new Garage();

            Truck scaniaTruck = new Truck
            {
                Id = Guid.NewGuid(),
                Brand = "Scania",
                Series = "S",
                DrivenDistance = 2000,
                Image = Guid.NewGuid().ToString(),
                TrailerId = Guid.NewGuid(),
                EngineId = null,
                TransmissionId = null
            };

            IQueryable<Truck> trucks = new List<Truck>
            {
                scaniaTruck
            }
            .AsQueryable();

            unitOfWorkMock.Setup(x => x.Garage.GetById(garageId)).ReturnsAsync(garage);

            unitOfWorkMock.Setup(x => x.Truck.Find(t => t.GarageId == garageId)).Returns(trucks);

            // Act
            ICollection<GarageTruckInfoViewModel> serviceModel = await garageService.GetGarageTrucksInfo(garageId);

            // Assert
            Assert.IsInstanceOf<ICollection<GarageTruckInfoViewModel>>(serviceModel);
            Assert.IsNotNull(serviceModel);
            Assert.That(serviceModel.Count, Is.EqualTo(trucks.ToList().Count));

            int cnt = 0;

            foreach (GarageTruckInfoViewModel garageTruckInfo in serviceModel)
            {
                if (cnt == 0)
                {
                    Assert.IsInstanceOf<GarageTruckInfoViewModel>(garageTruckInfo);
                    Assert.That(garageTruckInfo.Id, Is.EqualTo(scaniaTruck.Id));
                    Assert.That(garageTruckInfo.TrailerId, Is.EqualTo(scaniaTruck.TrailerId));
                    Assert.That(garageTruckInfo.Brand, Is.EqualTo(scaniaTruck.Brand));
                    Assert.That(garageTruckInfo.Series, Is.EqualTo(scaniaTruck.Series));
                    Assert.That(garageTruckInfo.EngineTitle, Is.EqualTo(null));
                    Assert.That(garageTruckInfo.TransmissionTitle, Is.EqualTo(null));

                    cnt++;
                }
                else
                {
                    continue;
                }
            }
        }

        [Test]
        public Task GetGarageTrucksInfo_ShouldThrowArgumentException_WhenGarageDoesNotExist()
        {
            // Arrange
            Guid garageId = Guid.NewGuid();

            Garage garage = null!;

            unitOfWorkMock.Setup(x => x.Garage.GetById(garageId)).ReturnsAsync(garage);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await garageService.GetGarageTrucksInfo(garageId));

            return Task.CompletedTask;
        }

        [Test]
        public Task GetGarageTrucksInfo_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenGarageDoesNotExist()
        {
            // Arrange
            Guid garageId = Guid.NewGuid();

            Garage garage = null!;

            unitOfWorkMock.Setup(x => x.Garage.GetById(garageId)).ReturnsAsync(garage);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await garageService.GetGarageTrucksInfo(garageId));

            Assert.That(argumentException.Message, Is.EqualTo(GarageNotExistMessage));

            return Task.CompletedTask;
        }

        // GetGarageTrucksTrailersInfo
        [Test]
        public async Task GetGarageTrucksTrailersInfo_ShouldReturnICollectionOfGetGarageTrucksTrailersInfo_WhenGarageExists()
        {
            // Arrange
            Guid garageId = Guid.NewGuid();

            Truck scaniaTruck = new Truck
            {
                Id = Guid.NewGuid(),
                Brand = "Scania",
                Series = "S",
                DrivenDistance = 2000,
                Image = Guid.NewGuid().ToString(),
                TrailerId = Guid.NewGuid()
            };

            Trailer scaniaTrailer = new Trailer()
            {
                Id = scaniaTruck.TrailerId.Value,
                Title = "ScaniaTrailer",
                Series = "Series"
            };

            IQueryable<Truck> trucks = new List<Truck>
            {
                scaniaTruck
            }
            .AsQueryable();

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == garageId)).ReturnsAsync(true);

            unitOfWorkMock.Setup(x => x.Truck.Find(t => t.GarageId == garageId && t.TrailerId != null)).Returns(trucks);

            unitOfWorkMock.Setup(x => x.Trailer.GetById(scaniaTruck.TrailerId.Value)).ReturnsAsync(scaniaTrailer);

            // Act
            ICollection<GarageTruckTrailerInfoViewModel> serviceModel = await garageService.GetGarageTrucksTrailersInfo(garageId);

            // Assert
            Assert.IsInstanceOf<ICollection<GarageTruckTrailerInfoViewModel>>(serviceModel);
            Assert.IsNotNull(serviceModel);
            Assert.That(serviceModel.Count, Is.EqualTo(trucks.ToList().Count));

            int cnt = 0;

            foreach (GarageTruckTrailerInfoViewModel garageTruckWithTrailer in serviceModel)
            {
                if (cnt == 0)
                {
                    Assert.IsInstanceOf<GarageTruckTrailerInfoViewModel>(garageTruckWithTrailer);
                    Assert.That(garageTruckWithTrailer.Id, Is.EqualTo(garageId));
                    Assert.That(garageTruckWithTrailer.TruckId, Is.EqualTo(scaniaTruck.Id));
                    Assert.That(garageTruckWithTrailer.TrailerId, Is.EqualTo(scaniaTrailer.Id));
                    Assert.That(garageTruckWithTrailer.TruckBrand, Is.EqualTo(scaniaTruck.Brand));
                    Assert.That(garageTruckWithTrailer.TruckSeries, Is.EqualTo(scaniaTruck.Series));
                    Assert.That(garageTruckWithTrailer.TrailerTitle, Is.EqualTo(scaniaTrailer.Title));
                    Assert.That(garageTruckWithTrailer.TrailerSeries, Is.EqualTo(scaniaTrailer.Series));

                    cnt++;
                }
                else
                {
                    continue;
                }
            }
        }

        [Test]
        public Task GetGarageTrucksTrailersInfo_ShouldThrowArgumentException_WhenGarageDoesNotExist()
        {
            // Arrange
            Guid garageId = Guid.NewGuid();

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == garageId)).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await garageService.GetGarageTrucksTrailersInfo(garageId));

            return Task.CompletedTask;
        }

        [Test]
        public Task GetGarageTrucksTrailersInfo_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenGarageDoesNotExist()
        {
            // Arrange
            Guid garageId = Guid.NewGuid();

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == garageId)).ReturnsAsync(false);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await garageService.GetGarageTrucksTrailersInfo(garageId));

            Assert.That(argumentException.Message, Is.EqualTo(GarageNotExistMessage));

            return Task.CompletedTask;
        }

        // AddGarageTruckToGarageTrailer
        [Test]
        public Task AddGarageTruckToGarageTrailer_ShouldThrowArgumentException_WhenTruckDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = null!;

            Guid trailerId = Guid.NewGuid();

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await garageService.AddGarageTruckToGarageTrailer(truckId, trailerId));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddGarageTruckToGarageTrailer_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTruckDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = null!;

            Guid trailerId = Guid.NewGuid();

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await garageService.AddGarageTruckToGarageTrailer(truckId, trailerId));

            Assert.That(argumentException.Message, Is.EqualTo(TruckNotExistMessage));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddGarageTruckToGarageTrailer_ShouldThrowArgumentException_WhenTrailerDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = new Truck();

            Guid trailerId = Guid.NewGuid();

            Trailer trailer = null!;

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            unitOfWorkMock.Setup(x => x.Trailer.GetById(trailerId)).ReturnsAsync(trailer);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await garageService.AddGarageTruckToGarageTrailer(truckId, trailerId));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddGarageTruckToGarageTrailer_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTrailerDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = new Truck();

            Guid trailerId = Guid.NewGuid();

            Trailer trailer = null!;

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            unitOfWorkMock.Setup(x => x.Trailer.GetById(trailerId)).ReturnsAsync(trailer);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await garageService.AddGarageTruckToGarageTrailer(truckId, trailerId));

            Assert.That(argumentException.Message, Is.EqualTo(TrailerNotExistMessage));

            return Task.CompletedTask;
        }

        // RemoveGarageTruckFromGarageTrailer
        [Test]
        public Task RemoveGarageTruckFromGarageTrailer_ShouldThrowArgumentException_WhenTruckDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = null!;

            Guid trailerId = Guid.NewGuid();

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await garageService.RemoveGarageTruckFromGarageTrailer(truckId, trailerId));

            return Task.CompletedTask;
        }

        [Test]
        public Task RemoveGarageTruckFromGarageTrailer_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTruckDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = null!;

            Guid trailerId = Guid.NewGuid();

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await garageService.RemoveGarageTruckFromGarageTrailer(truckId, trailerId));

            Assert.That(argumentException.Message, Is.EqualTo(TruckNotExistMessage));

            return Task.CompletedTask;
        }

        [Test]
        public Task RemoveGarageTruckFromGarageTrailer_ShouldThrowArgumentException_WhenTrailerDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = new Truck();

            Guid trailerId = Guid.NewGuid();

            Trailer trailer = null!;

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            unitOfWorkMock.Setup(x => x.Trailer.GetById(trailerId)).ReturnsAsync(trailer);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await garageService.RemoveGarageTruckFromGarageTrailer(truckId, trailerId));

            return Task.CompletedTask;
        }

        [Test]
        public Task RemoveGarageTruckFromGarageTrailer_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTrailerDoesNotExist()
        {
            // Arrange
            Guid truckId = Guid.NewGuid();

            Truck truck = new Truck();

            Guid trailerId = Guid.NewGuid();

            Trailer trailer = null!;

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            unitOfWorkMock.Setup(x => x.Trailer.GetById(trailerId)).ReturnsAsync(trailer);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await garageService.RemoveGarageTruckFromGarageTrailer(truckId, trailerId));

            Assert.That(argumentException.Message, Is.EqualTo(TrailerNotExistMessage));

            return Task.CompletedTask;
        }

        // AddGarageTruckToUser
        [Test]
        public Task AddGarageTruckToUser_ShouldThrowArgumentException_WhenUserDoesNotExist()
        {
            // Assert
            Guid userId = Guid.NewGuid();

            User user = null!;

            Guid truckId = Guid.NewGuid();

            userManagerMock.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(user);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await garageService.AddGarageTruckToUser(userId, truckId));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddGarageTruckToUser_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenUserDoesNotExist()
        {
            // Assert
            Guid userId = Guid.NewGuid();

            User user = null!;

            Guid truckId = Guid.NewGuid();

            userManagerMock.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(user);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await garageService.AddGarageTruckToUser(userId, truckId));

            Assert.That(argumentException.Message, Is.EqualTo(UserNotExistMessage));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddGarageTruckToUser_ShouldThrowArgumentException_WhenTruckDoesNotExist()
        {
            // Assert
            Guid userId = Guid.NewGuid();

            User user = new User();

            Guid truckId = Guid.NewGuid();

            Truck truck = null!;

            userManagerMock.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(user);

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await garageService.AddGarageTruckToUser(userId, truckId));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddGarageTruckToUser_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTruckDoesNotExist()
        {
            // Assert
            Guid userId = Guid.NewGuid();

            User user = new User();

            Guid truckId = Guid.NewGuid();

            Truck truck = null!;

            userManagerMock.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(user);

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await garageService.AddGarageTruckToUser(userId, truckId));

            Assert.That(argumentException.Message, Is.EqualTo(TruckNotExistMessage));

            return Task.CompletedTask;
        }

        // RemoveGarageTruckFromUser
        [Test]
        public Task RemoveGarageTruckFromUser_ShouldThrowArgumentException_WhenUserDoesNotExist()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            User user = null!;

            Guid truckId = Guid.NewGuid();

            userManagerMock.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(user);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await garageService.RemoveGarageTruckFromUser(userId, truckId));

            return Task.CompletedTask;
        }

        [Test]
        public Task RemoveGarageTruckFromUser_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenUserDoesNotExist()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            User user = null!;

            Guid truckId = Guid.NewGuid();

            userManagerMock.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(user);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await garageService.RemoveGarageTruckFromUser(userId, truckId));

            Assert.That(argumentException.Message, Is.EqualTo(UserNotExistMessage));

            return Task.CompletedTask;
        }

        [Test]
        public Task RemoveGarageTruckFromUser_ShouldThrowArgumentException_WhenTruckDoesNotExist()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            User user = new User();

            Guid truckId = Guid.NewGuid();

            Truck truck = null!;

            userManagerMock.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(user);

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await garageService.RemoveGarageTruckFromUser(userId, truckId));

            return Task.CompletedTask;
        }

        [Test]
        public Task RemoveGarageTruckFromUser_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTruckDoesNotExist()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            User user = new User();

            Guid truckId = Guid.NewGuid();

            Truck truck = null!;

            userManagerMock.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(user);

            unitOfWorkMock.Setup(x => x.Truck.GetById(truckId)).ReturnsAsync(truck);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await garageService.RemoveGarageTruckFromUser(userId, truckId));

            Assert.That(argumentException.Message, Is.EqualTo(TruckNotExistMessage));

            return Task.CompletedTask;
        }
    }
}

namespace TruckManagerSoftware.Tests.ServicesTests
{
    using Microsoft.AspNetCore.Hosting;
    using Moq;

    using Core.Models.Garage;
    using Core.Models.Trailer;
    using Core.Models.Truck;
    using Core.Services.Contract;
    using Core.Services.Implementation;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;

    using static Common.Messages.Messages.Garage;
    using static Common.Messages.Messages.Trailer;

    public class TrailerServiceTests
    {
        private readonly TrailerService trailerService;

        private readonly Mock<IUnitOfWork> unitOfWorkMock;

        private readonly Mock<IImageService> imageServiceMock;

        private readonly Mock<IWebHostEnvironment> webHostEnvironmentMock;

        public TrailerServiceTests()
        {
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
            this.imageServiceMock = new Mock<IImageService>();
            this.webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            this.trailerService = new TrailerService(
                unitOfWorkMock.Object,
                imageServiceMock.Object,
                webHostEnvironmentMock.Object);
        }

        // AddTrailer
        [Test]
        public Task AddTrailer_ShouldThrowArgumentException_WhenTrailerGarageIdDoesNotExist()
        {
            // Arrange
            AddTrailerViewModel model = new AddTrailerViewModel();

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == model.GarageId)).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await trailerService.AddTrailer(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddTrailer_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTrailerGarageIdDoesNotExist()
        {
            // Arrange
            AddTrailerViewModel model = new AddTrailerViewModel();

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == model.GarageId)).ReturnsAsync(false);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await trailerService.AddTrailer(model));

            Assert.That(argumentException.Message, Is.EqualTo(GarageNotExistMessage));

            return Task.CompletedTask;
        }

        // EditTrailer
        [Test]
        public Task EditTrailer_ShouldThrowArgumentException_WhenTrailerDoesNotExist()
        {
            // Arrange
            Trailer trailer = null!;

            EditTrailerViewModel model = new EditTrailerViewModel();

            unitOfWorkMock.Setup(x => x.Trailer.GetById(model.Id)).ReturnsAsync(trailer);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await trailerService.EditTrailer(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task EditTrailer_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTrailerDoesNotExist()
        {
            // Arrange
            Trailer trailer = null!;

            EditTrailerViewModel model = new EditTrailerViewModel();

            unitOfWorkMock.Setup(x => x.Trailer.GetById(model.Id)).ReturnsAsync(trailer);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await trailerService.EditTrailer(model));

            Assert.That(argumentException.Message, Is.EqualTo(TrailerNotExistMessage));

            return Task.CompletedTask;
        }

        [Test]
        public Task EditTrailer_ShouldThrowArgumentException_WhenTrailerGarageIdDoesNotExist()
        {
            // Arrange
            Trailer trailer = new Trailer();

            EditTrailerViewModel model = new EditTrailerViewModel();

            unitOfWorkMock.Setup(x => x.Trailer.GetById(model.Id)).ReturnsAsync(trailer);

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == model.GarageId)).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await trailerService.EditTrailer(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task EditTrailer_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTrailerGarageIdDoesNotExist()
        {
            // Arrange
            Trailer trailer = new Trailer();

            EditTrailerViewModel model = new EditTrailerViewModel();

            unitOfWorkMock.Setup(x => x.Trailer.GetById(model.Id)).ReturnsAsync(trailer);

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == model.GarageId)).ReturnsAsync(false);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await trailerService.EditTrailer(model));

            Assert.That(argumentException.Message, Is.EqualTo(GarageNotExistMessage));

            return Task.CompletedTask;
        }

        // GetAdditionalTrailerInfoById
        [Test]
        public async Task GetAdditionalTrailerInfoById_ShouldReturnTrailerAdditionalInfoViewModel_WhenTrailerExists()
        {
            // Arrange
            Guid trailerId = Guid.NewGuid();

            Trailer trailer = new Trailer
            {
                Id = trailerId,
                Title = "Trailer",
                Series = "Series",
                TrailerType = "Type",
                BodyType = "Type",
                TareWeight = 1500,
                AxleCount = 3,
                TotalLength = 16.3,
                CargoTypes = "Panels",
                Image = Guid.NewGuid().ToString(),
                GarageId = Guid.NewGuid(),
                TruckId = Guid.NewGuid()
            };

            Garage garage = new Garage
            {
                Id = trailer.GarageId.Value,
                Country = "Bulgaria",
                City = "Ruse",
                Size = "small"
            };

            Truck truck = new Truck
            {
                Id = trailer.TruckId.Value,
                Brand = "Scania",
                Series = "S"
            };

            unitOfWorkMock.Setup(x => x.Trailer.GetById(trailerId)).ReturnsAsync(trailer);
            unitOfWorkMock.Setup(x => x.Garage.GetById(trailer.GarageId.Value)).ReturnsAsync(garage);
            unitOfWorkMock.Setup(x => x.Truck.GetById(trailer.TruckId.Value)).ReturnsAsync(truck);

            // Act
            TrailerAdditionalInfoViewModel serviceModel = await trailerService.GetAdditionalTrailerInfoById(trailerId);

            // Assert
            Assert.IsInstanceOf<TrailerAdditionalInfoViewModel>(serviceModel);
            Assert.That(serviceModel.Id, Is.EqualTo(trailer.Id));
            Assert.That(serviceModel.Title, Is.EqualTo(trailer.Title));
            Assert.That(serviceModel.Series, Is.EqualTo(trailer.Series));
            Assert.That(serviceModel.TrailerType, Is.EqualTo(trailer.TrailerType));
            Assert.That(serviceModel.BodyType, Is.EqualTo(trailer.BodyType));
            Assert.That(serviceModel.TareWeight, Is.EqualTo(trailer.TareWeight));
            Assert.That(serviceModel.AxleCount, Is.EqualTo(trailer.AxleCount));
            Assert.That(serviceModel.TotalLength, Is.EqualTo(trailer.TotalLength));
            Assert.That(serviceModel.CargoTypes, Is.EqualTo(trailer.CargoTypes));
            Assert.That(serviceModel.Image, Is.EqualTo(trailer.Image));
            

            Assert.IsInstanceOf<GarageInfoViewModel>(serviceModel.GarageInfo);
            Assert.That(serviceModel.GarageInfo!.Id, Is.EqualTo(garage.Id));
            Assert.That(serviceModel.GarageInfo!.Country, Is.EqualTo(garage.Country));
            Assert.That(serviceModel.GarageInfo!.City, Is.EqualTo(garage.City));
            Assert.That(serviceModel.GarageInfo!.Size, Is.EqualTo(garage.Size));

            Assert.IsInstanceOf<TruckInfoViewModel>(serviceModel.TruckInfo);
            Assert.That(serviceModel.TruckInfo!.Id, Is.EqualTo(truck.Id));
            Assert.That(serviceModel.TruckInfo!.Brand, Is.EqualTo(truck.Brand));
            Assert.That(serviceModel.TruckInfo!.Series, Is.EqualTo(truck.Series));
        }

        [Test]
        public async Task GetAdditionalTrailerInfoById_ShouldReturnTrailerAdditionalInfoViewModel_WhenTrailerExists_AndWhenTrailerDoesNotHaveGarageTruck()
        {
            // Arrange
            Guid trailerId = Guid.NewGuid();

            Trailer trailer = new Trailer
            {
                Id = trailerId,
                Title = "Trailer",
                Series = "Series",
                TrailerType = "Type",
                BodyType = "Type",
                TareWeight = 1500,
                AxleCount = 3,
                TotalLength = 16.3,
                CargoTypes = "Panels",
                Image = Guid.NewGuid().ToString(),
                GarageId = null,
                TruckId = null
            };

            unitOfWorkMock.Setup(x => x.Trailer.GetById(trailerId)).ReturnsAsync(trailer);

            // Act
            TrailerAdditionalInfoViewModel serviceModel = await trailerService.GetAdditionalTrailerInfoById(trailerId);

            // Assert
            Assert.IsInstanceOf<TrailerAdditionalInfoViewModel>(serviceModel);
            Assert.That(serviceModel.Id, Is.EqualTo(trailer.Id));
            Assert.That(serviceModel.Title, Is.EqualTo(trailer.Title));
            Assert.That(serviceModel.Series, Is.EqualTo(trailer.Series));
            Assert.That(serviceModel.TrailerType, Is.EqualTo(trailer.TrailerType));
            Assert.That(serviceModel.BodyType, Is.EqualTo(trailer.BodyType));
            Assert.That(serviceModel.TareWeight, Is.EqualTo(trailer.TareWeight));
            Assert.That(serviceModel.AxleCount, Is.EqualTo(trailer.AxleCount));
            Assert.That(serviceModel.TotalLength, Is.EqualTo(trailer.TotalLength));
            Assert.That(serviceModel.CargoTypes, Is.EqualTo(trailer.CargoTypes));
            Assert.That(serviceModel.Image, Is.EqualTo(trailer.Image));
            
            Assert.That(serviceModel.GarageInfo, Is.EqualTo(null));

            Assert.That(serviceModel.TruckInfo, Is.EqualTo(null));
        }

        [Test]
        public Task GetAdditionalTrailerInfoById_ShouldThrowArgumentException_WhenTrailerDoesNotExist()
        {
            // Arrange
            Guid trailerId = Guid.NewGuid();

            Trailer trailer = null!;

            unitOfWorkMock.Setup(x => x.Trailer.GetById(trailerId)).ReturnsAsync(trailer);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await trailerService.GetAdditionalTrailerInfoById(trailerId));

            return Task.CompletedTask;
        }

        [Test]
        public Task GetAdditionalTrailerInfoById_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTrailerDoesNotExist()
        {
            // Arrange
            Guid trailerId = Guid.NewGuid();

            Trailer trailer = null!;

            unitOfWorkMock.Setup(x => x.Trailer.GetById(trailerId)).ReturnsAsync(trailer);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await trailerService.GetAdditionalTrailerInfoById(trailerId));

            Assert.That(argumentException.Message, Is.EqualTo(TrailerNotExistMessage));

            return Task.CompletedTask;
        }

        // GetAllTrailersInfo
        [Test]
        public async Task GetAllTrailersInfo_ShouldReturnICollectionOfTrailersInfoViewModel()
        {
            // Arrange
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
                Image = Guid.NewGuid().ToString()
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
                Image = Guid.NewGuid().ToString()
            };

            IEnumerable<Trailer> trailers = new List<Trailer>
            {
                scaniaTrailer,
                dafTrailer
            };

            unitOfWorkMock.Setup(x => x.Trailer.GetAll()).ReturnsAsync(trailers);

            // Act
            ICollection<TrailerInfoViewModel> serviceModel = await trailerService.GetAllTrailersInfo();

            // Assert
            Assert.IsInstanceOf<ICollection<TrailerInfoViewModel>>(serviceModel);
            Assert.IsNotNull(serviceModel);
            Assert.That(serviceModel.Count, Is.EqualTo(trailers.Count()));

            int cnt = 0;

            foreach (TrailerInfoViewModel trailerInfo in serviceModel)
            {
                if (cnt == 0)
                {
                    Assert.IsInstanceOf<TrailerInfoViewModel>(trailerInfo);
                    Assert.That(trailerInfo.Id, Is.EqualTo(scaniaTrailer.Id));
                    Assert.That(trailerInfo.Title, Is.EqualTo(scaniaTrailer.Title));
                    Assert.That(trailerInfo.Series, Is.EqualTo(scaniaTrailer.Series));
                    Assert.That(trailerInfo.TrailerType, Is.EqualTo(scaniaTrailer.TrailerType));
                    Assert.That(trailerInfo.BodyType, Is.EqualTo(scaniaTrailer.BodyType));
                    Assert.That(trailerInfo.TareWeight, Is.EqualTo(scaniaTrailer.TareWeight));
                    Assert.That(trailerInfo.AxleCount, Is.EqualTo(scaniaTrailer.AxleCount));
                    Assert.That(trailerInfo.TotalLength, Is.EqualTo(scaniaTrailer.TotalLength));
                    Assert.That(trailerInfo.CargoTypes, Is.EqualTo(scaniaTrailer.CargoTypes));
                    Assert.That(trailerInfo.Image, Is.EqualTo(scaniaTrailer.Image));

                    cnt++;
                }
                else
                {
                    Assert.IsInstanceOf<TrailerInfoViewModel>(trailerInfo);
                    Assert.That(trailerInfo.Id, Is.EqualTo(dafTrailer.Id));
                    Assert.That(trailerInfo.Title, Is.EqualTo(dafTrailer.Title));
                    Assert.That(trailerInfo.Series, Is.EqualTo(dafTrailer.Series));
                    Assert.That(trailerInfo.TrailerType, Is.EqualTo(dafTrailer.TrailerType));
                    Assert.That(trailerInfo.BodyType, Is.EqualTo(dafTrailer.BodyType));
                    Assert.That(trailerInfo.TareWeight, Is.EqualTo(dafTrailer.TareWeight));
                    Assert.That(trailerInfo.AxleCount, Is.EqualTo(dafTrailer.AxleCount));
                    Assert.That(trailerInfo.TotalLength, Is.EqualTo(dafTrailer.TotalLength));
                    Assert.That(trailerInfo.CargoTypes, Is.EqualTo(dafTrailer.CargoTypes));
                    Assert.That(trailerInfo.Image, Is.EqualTo(dafTrailer.Image));
                }
            }
        }

        // GetAllTrailersInfoByGarageIdWithoutTruckId
        [Test]
        public async Task GetAllTrailersInfoByGarageIdWithoutTruckId_ShouldReturnICollectionOfTrailerInfoViewModel_WhenGarageIdExists()
        {
            // Assert
            Guid garageId = Guid.NewGuid();

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
                Image = Guid.NewGuid().ToString()
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
                Image = Guid.NewGuid().ToString()
            };

            IQueryable<Trailer> trailers = new List<Trailer>
            {
                scaniaTrailer,
                dafTrailer
            }
            .AsQueryable();

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == garageId)).ReturnsAsync(true);

            unitOfWorkMock.Setup(x => x.Trailer.Find(t => t.GarageId == garageId && t.TruckId == null)).Returns(trailers);

            // Act
            ICollection<TrailerInfoViewModel> serviceModel = await trailerService.GetAllTrailersInfoByGarageIdWithoutTruckId(garageId);

            // Assert
            Assert.IsInstanceOf<ICollection<TrailerInfoViewModel>>(serviceModel);
            Assert.IsNotNull(serviceModel);
            Assert.That(serviceModel.Count, Is.EqualTo(trailers.ToList().Count));

            int cnt = 0;

            foreach (TrailerInfoViewModel trailerInfo in serviceModel)
            {
                if (cnt == 0)
                {
                    Assert.IsInstanceOf<TrailerInfoViewModel>(trailerInfo);
                    Assert.That(trailerInfo.Id, Is.EqualTo(scaniaTrailer.Id));
                    Assert.That(trailerInfo.Title, Is.EqualTo(scaniaTrailer.Title));
                    Assert.That(trailerInfo.Series, Is.EqualTo(scaniaTrailer.Series));
                    Assert.That(trailerInfo.TrailerType, Is.EqualTo(scaniaTrailer.TrailerType));
                    Assert.That(trailerInfo.BodyType, Is.EqualTo(scaniaTrailer.BodyType));

                    cnt++;
                }
                else
                {
                    Assert.IsInstanceOf<TrailerInfoViewModel>(trailerInfo);
                    Assert.That(trailerInfo.Id, Is.EqualTo(dafTrailer.Id));
                    Assert.That(trailerInfo.Title, Is.EqualTo(dafTrailer.Title));
                    Assert.That(trailerInfo.Series, Is.EqualTo(dafTrailer.Series));
                    Assert.That(trailerInfo.TrailerType, Is.EqualTo(dafTrailer.TrailerType));
                    Assert.That(trailerInfo.BodyType, Is.EqualTo(dafTrailer.BodyType));
                }
            }
        }

        [Test]
        public Task GetAllTrailersInfoByGarageIdWithoutTruckId_ShouldThrowArgumentException_WhenGarageIdDoesNotExist()
        {
            // Arrange
            Guid garageId = Guid.NewGuid();

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == garageId)).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await trailerService.GetAllTrailersInfoByGarageIdWithoutTruckId(garageId));

            return Task.CompletedTask;
        }

        [Test]
        public Task GetAllTrailersInfoByGarageIdWithoutTruckId_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenGarageIdDoesNotExist()
        {
            // Arrange
            Guid garageId = Guid.NewGuid();

            unitOfWorkMock.Setup(x => x.Garage.AnyAsync(g => g.Id == garageId)).ReturnsAsync(false);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await trailerService.GetAllTrailersInfoByGarageIdWithoutTruckId(garageId));

            Assert.That(argumentException.Message, Is.EqualTo(GarageNotExistMessage));

            return Task.CompletedTask;
        }

        // GetTrailerInfoById
        [Test]
        public async Task GetTrailerInfoById_ShouldReturnTrailerInfoViewModel_WhenTrailerExists()
        {
            Guid trailerId = Guid.NewGuid();

            Trailer trailer = new Trailer
            {
                Id = trailerId,
                Title = "Trailer",
                Series = "Series",
                TrailerType = "Type",
                BodyType = "Type",
                TareWeight = 1000,
                AxleCount = 3,
                TotalLength = 15.9,
                CargoTypes = "Wood",
                Image = Guid.NewGuid().ToString(),
                GarageId = Guid.NewGuid(),
                TruckId = Guid.NewGuid()
            };

            unitOfWorkMock.Setup(x => x.Trailer.GetById(trailerId)).ReturnsAsync(trailer);

            // Act
            TrailerInfoViewModel serviceModel = await trailerService.GetTrailerInfoById(trailerId);

            // Assert
            Assert.IsInstanceOf<TrailerInfoViewModel>(serviceModel);
            Assert.That(serviceModel.Id, Is.EqualTo(trailer.Id));
            Assert.That(serviceModel.Title, Is.EqualTo(trailer.Title));
            Assert.That(serviceModel.Series, Is.EqualTo(trailer.Series));
            Assert.That(serviceModel.TrailerType, Is.EqualTo(trailer.TrailerType));
            Assert.That(serviceModel.BodyType, Is.EqualTo(trailer.BodyType));
            Assert.That(serviceModel.TareWeight, Is.EqualTo(trailer.TareWeight));
            Assert.That(serviceModel.AxleCount, Is.EqualTo(trailer.AxleCount));
            Assert.That(serviceModel.TotalLength, Is.EqualTo(trailer.TotalLength));
            Assert.That(serviceModel.CargoTypes, Is.EqualTo(trailer.CargoTypes));
            Assert.That(serviceModel.Image, Is.EqualTo(trailer.Image));
            Assert.That(serviceModel.GarageId, Is.EqualTo(trailer.GarageId));
            Assert.That(serviceModel.TruckId, Is.EqualTo(trailer.TruckId));
        }

        [Test]
        public Task GetTrailerInfoById_ShouldThrowArgumentException_WhenTrailerDoesNotExist()
        {
            // Arrange
            Guid trailerId = Guid.NewGuid();

            Trailer trailer = null!;

            unitOfWorkMock.Setup(x => x.Trailer.GetById(trailerId)).ReturnsAsync(trailer);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await trailerService.GetTrailerInfoById(trailerId));

            return Task.CompletedTask;
        }

        [Test]
        public Task GetTrailerInfoById_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTrailerDoesNotExist()
        {
            // Arrange
            Guid trailerId = Guid.NewGuid();

            Trailer trailer = null!;

            unitOfWorkMock.Setup(x => x.Trailer.GetById(trailerId)).ReturnsAsync(trailer);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await trailerService.GetTrailerInfoById(trailerId));

            Assert.That(argumentException.Message, Is.EqualTo(TrailerNotExistMessage));

            return Task.CompletedTask;
        }

        // RemoveTrailer
        [Test]
        public Task RemoveTrailer_ShouldThrowArgumentException_WhenTrailerDoesNotExist()
        {
            // Arrange
            Guid trailerId = Guid.NewGuid();

            Trailer trailer = null!;

            unitOfWorkMock.Setup(x => x.Trailer.GetById(trailerId)).ReturnsAsync(trailer);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await trailerService.RemoveTrailer(trailerId));

            return Task.CompletedTask;
        }

        [Test]
        public Task RemoveTrailer_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTrailerDoesNotExist()
        {
            // Arrange
            Guid trailerId = Guid.NewGuid();

            Trailer trailer = null!;

            unitOfWorkMock.Setup(x => x.Trailer.GetById(trailerId)).ReturnsAsync(trailer);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await trailerService.RemoveTrailer(trailerId));

            Assert.That(argumentException.Message, Is.EqualTo(TrailerNotExistMessage));

            return Task.CompletedTask;
        }
    }
}

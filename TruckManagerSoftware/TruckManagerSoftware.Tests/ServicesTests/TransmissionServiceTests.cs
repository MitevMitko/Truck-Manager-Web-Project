namespace TruckManagerSoftware.Tests.ServicesTests
{
    using Moq;

    using Core.Models.Transmission;
    using Core.Services.Implementation;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;

    using static Common.Messages.Messages.Transmission;

    public class TransmissionServiceTests
    {
        private readonly TransmissionService transmissionService;

        private readonly Mock<IUnitOfWork> unitOfWorkMock;

        public TransmissionServiceTests()
        {
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
            this.transmissionService = new TransmissionService(unitOfWorkMock.Object);
        }

        // AddTransmission
        [Test]
        public Task AddTransmission_ShouldThrowArgumentException_WhenTransmissionTitleExists()
        {
            // Arrange
            AddTransmissionViewModel model = new AddTransmissionViewModel();

            unitOfWorkMock.Setup(x => x.Transmission.AnyAsync(t => t.Title == model.Title)).ReturnsAsync(true);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await transmissionService.AddTransmission(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddTransmission_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTransmissionTitleExists()
        {
            // Arrange
            AddTransmissionViewModel model = new AddTransmissionViewModel();

            unitOfWorkMock.Setup(x => x.Transmission.AnyAsync(t => t.Title == model.Title)).ReturnsAsync(true);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await transmissionService.AddTransmission(model));

            Assert.That(argumentException.Message, Is.EqualTo(TransmissionTitleExistsMessage));

            return Task.CompletedTask;
        }

        // EditTransmission
        [Test]
        public Task EditTransmission_ShouldThrowArgumentException_WhenTransmissionDoesNotExist()
        {
            // Arrange
            Transmission transmission = null!;

            EditTransmissionViewModel model = new EditTransmissionViewModel();

            unitOfWorkMock.Setup(x => x.Transmission.GetById(model.Id)).ReturnsAsync(transmission);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await transmissionService.EditTransmission(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task EditTransmission_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTransmissionDoesNotExist()
        {
            // Arrange
            Transmission transmission = null!;

            EditTransmissionViewModel model = new EditTransmissionViewModel();

            unitOfWorkMock.Setup(x => x.Transmission.GetById(model.Id)).ReturnsAsync(transmission);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await transmissionService.EditTransmission(model));

            Assert.That(argumentException.Message, Is.EqualTo(TransmissionNotExistMessage));

            return Task.CompletedTask;
        }

        // GetAllTransmissionsInfo
        [Test]
        public async Task GetAllTransmissionsInfo_ShouldReturnICollectionOfTransmissionInfoViewModel()
        {
            // Arrange
            Transmission scaniaTransmission = new Transmission
            {
                Id = Guid.NewGuid(),
                Title = "ScaniaTransmission",
                GearsCount = 14,
                Retarder = true
            };

            Transmission dafTransmission = new Transmission
            {
                Id = Guid.NewGuid(),
                Title = "DafTransmission",
                GearsCount = 12,
                Retarder = false
            };

            IEnumerable<Transmission> transmissions = new List<Transmission>
            {
                scaniaTransmission,
                dafTransmission
            };

            unitOfWorkMock.Setup(x => x.Transmission.GetAll()).ReturnsAsync(transmissions);

            // Act
            ICollection<TransmissionInfoViewModel> serviceModel = await transmissionService.GetAllTransmissionsInfo();

            // Assert
            Assert.IsInstanceOf<ICollection<TransmissionInfoViewModel>>(serviceModel);
            Assert.IsNotNull(serviceModel);
            Assert.That(serviceModel.Count, Is.EqualTo(transmissions.Count()));

            int cnt = 0;

            foreach (TransmissionInfoViewModel transmissionInfo in serviceModel)
            {
                if (cnt == 0)
                {
                    Assert.IsInstanceOf<TransmissionInfoViewModel>(transmissionInfo);
                    Assert.That(transmissionInfo.Id, Is.EqualTo(scaniaTransmission.Id));
                    Assert.That(transmissionInfo.Title, Is.EqualTo(scaniaTransmission.Title));
                    Assert.That(transmissionInfo.GearsCount, Is.EqualTo(scaniaTransmission.GearsCount));
                    Assert.That(transmissionInfo.Retarder, Is.EqualTo(scaniaTransmission.Retarder ? "Available" : "N/A"));

                    cnt++;
                }
                else
                {
                    Assert.IsInstanceOf<TransmissionInfoViewModel>(transmissionInfo);
                    Assert.That(transmissionInfo.Id, Is.EqualTo(dafTransmission.Id));
                    Assert.That(transmissionInfo.Title, Is.EqualTo(dafTransmission.Title));
                    Assert.That(transmissionInfo.GearsCount, Is.EqualTo(dafTransmission.GearsCount));
                    Assert.That(transmissionInfo.Retarder, Is.EqualTo(dafTransmission.Retarder ? "Available" : "N/A"));
                }
            }
        }

        // GetTransmissionInfoById
        [Test]
        public async Task GetTransmissionInfoById_ShouldReturnTransmissionInfoViewModel_WhenTransmissionExists()
        {
            // Arrange
            Guid transmissionId = Guid.NewGuid();

            Transmission transmission = new Transmission
            {
                Id = transmissionId,
                Title = "Transmission",
                GearsCount = 14,
                Retarder = true
            };

            unitOfWorkMock.Setup(x => x.Transmission.GetById(transmissionId)).ReturnsAsync(transmission);

            // Act
            TransmissionInfoViewModel serviceModel = await transmissionService.GetTransmissionInfoById(transmissionId);

            // Assert
            Assert.IsInstanceOf<TransmissionInfoViewModel>(serviceModel);
            Assert.That(serviceModel.Id, Is.EqualTo(transmission.Id));
            Assert.That(serviceModel.Title, Is.EqualTo(transmission.Title));
            Assert.That(serviceModel.GearsCount, Is.EqualTo(transmission.GearsCount));
            Assert.That(serviceModel.Retarder, Is.EqualTo(transmission.Retarder ? "Available" : "N/A"));
        }

        [Test]
        public Task GetTransmissionInfoById_ShouldThrowArgumentException_WhenTransmissionDoesNotExist()
        {
            // Arrange
            Guid transmissionId = Guid.NewGuid();

            Transmission transmission = null!;

            unitOfWorkMock.Setup(x => x.Transmission.GetById(transmissionId)).ReturnsAsync(transmission);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await transmissionService.GetTransmissionInfoById(transmissionId));

            return Task.CompletedTask;
        }

        [Test]
        public Task GetTransmissionInfoById_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTransmissionDoesNotExist()
        {
            // Arrange
            Guid transmissionId = Guid.NewGuid();

            Transmission transmission = null!;

            unitOfWorkMock.Setup(x => x.Transmission.GetById(transmissionId)).ReturnsAsync(transmission);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await transmissionService.GetTransmissionInfoById(transmissionId));

            Assert.That(argumentException.Message, Is.EqualTo(TransmissionNotExistMessage));

            return Task.CompletedTask;
        }

        // RemoveTransmission
        [Test]
        public Task RemoveTransmission_ShouldThrowArgumentException_WhenTransmissionDoesNotExist()
        {
            // Arrange
            Guid transmissionId = Guid.NewGuid();

            Transmission transmission = null!;

            unitOfWorkMock.Setup(x => x.Transmission.GetById(transmissionId)).ReturnsAsync(transmission);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await transmissionService.RemoveTransmission(transmissionId));

            return Task.CompletedTask;
        }

        [Test]
        public Task RemoveTransmission_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenTransmissionDoesNotExist()
        {
            // Arrange
            Guid transmissionId = Guid.NewGuid();

            Transmission transmission = null!;

            unitOfWorkMock.Setup(x => x.Transmission.GetById(transmissionId)).ReturnsAsync(transmission);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await transmissionService.RemoveTransmission(transmissionId));

            Assert.That(argumentException.Message, Is.EqualTo(TransmissionNotExistMessage));

            return Task.CompletedTask;
        }
    }
}

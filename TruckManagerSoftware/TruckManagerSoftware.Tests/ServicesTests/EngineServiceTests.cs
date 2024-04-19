namespace TruckManagerSoftware.Tests.ServicesTests
{
    using Moq;

    using Core.Models.Engine;
    using Core.Services.Implementation;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;

    using static Common.Messages.Messages.Engine;

    public class EngineServiceTests
    {
        private readonly EngineService engineService;

        private readonly Mock<IUnitOfWork> unitOfWorkMock;

        public EngineServiceTests()
        {
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
            this.engineService = new EngineService(unitOfWorkMock.Object);
        }

        // AddEngine
        [Test]
        public Task AddEngine_ShouldThrowArgumentException_WhenEngineTitleExists()
        {
            // Arrange
            AddEngineViewModel model = new AddEngineViewModel();

            unitOfWorkMock.Setup(x => x.Engine.AnyAsync(e => e.Title == model.Title)).ReturnsAsync(true);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await engineService.AddEngine(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddEngine_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenEngineTitleExists()
        {
            // Arrange
            AddEngineViewModel model = new AddEngineViewModel();

            unitOfWorkMock.Setup(x => x.Engine.AnyAsync(e => e.Title == model.Title)).ReturnsAsync(true);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await engineService.AddEngine(model));

            Assert.That(argumentException.Message, Is.EqualTo(EngineTitleExistsMessage));

            return Task.CompletedTask;
        }

        // EditEngine
        [Test]
        public Task EditEngine_ShouldThrowArgumentException_WhenEngineDoesNotExist()
        {
            // Arrange
            Engine engine = null!;

            EditEngineViewModel model = new EditEngineViewModel();

            unitOfWorkMock.Setup(x => x.Engine.GetById(model.Id)).ReturnsAsync(engine);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await engineService.EditEngine(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task EditEngine_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenEngineDoesNotExist()
        {
            // Arrange
            Engine engine = null!;

            EditEngineViewModel model = new EditEngineViewModel();

            unitOfWorkMock.Setup(x => x.Engine.GetById(model.Id)).ReturnsAsync(engine);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await engineService.EditEngine(model));

            Assert.That(argumentException.Message, Is.EqualTo(EngineNotExistMessage));

            return Task.CompletedTask;
        }

        // GetAllEnginesInfo
        [Test]
        public async Task GetAllEnginesInfo_ShouldReturnICollectionOfEngineInfoViewModel()
        {
            // Arrange
            Engine scaniaEngine = new Engine
            {
                Id = Guid.NewGuid(),
                Title = "ScaniaEngine",
                PowerHp = 650,
                PowerKw = 350,
                TorqueNm = 1500
            };

            Engine dafEngine = new Engine
            {
                Id = Guid.NewGuid(),
                Title = "DafEngine",
                PowerHp = 400,
                PowerKw = 200,
                TorqueNm = 1000
            };

            IEnumerable<Engine> engines = new List<Engine>
            {
                scaniaEngine,
                dafEngine
            };

            unitOfWorkMock.Setup(x => x.Engine.GetAll()).ReturnsAsync(engines);

            // Act
            ICollection<EngineInfoViewModel> serviceModel = await engineService.GetAllEnginesInfo();

            // Assert
            Assert.IsInstanceOf<ICollection<EngineInfoViewModel>>(serviceModel);
            Assert.IsNotNull(serviceModel);
            Assert.That(serviceModel.Count, Is.EqualTo(engines.Count()));

            int cnt = 0;

            foreach (EngineInfoViewModel engineInfo in serviceModel)
            {
                if (cnt == 0)
                {
                    Assert.IsInstanceOf<EngineInfoViewModel>(engineInfo);
                    Assert.That(engineInfo.Id, Is.EqualTo(scaniaEngine.Id));
                    Assert.That(engineInfo.Title, Is.EqualTo(scaniaEngine.Title));
                    Assert.That(engineInfo.PowerHp, Is.EqualTo(scaniaEngine.PowerHp));
                    Assert.That(engineInfo.PowerKw, Is.EqualTo(scaniaEngine.PowerKw));
                    Assert.That(engineInfo.TorqueNm, Is.EqualTo(scaniaEngine.TorqueNm));

                    cnt++;
                }
                else
                {
                    Assert.IsInstanceOf<EngineInfoViewModel>(engineInfo);
                    Assert.That(engineInfo.Id, Is.EqualTo(dafEngine.Id));
                    Assert.That(engineInfo.Title, Is.EqualTo(dafEngine.Title));
                    Assert.That(engineInfo.PowerHp, Is.EqualTo(dafEngine.PowerHp));
                    Assert.That(engineInfo.PowerKw, Is.EqualTo(dafEngine.PowerKw));
                    Assert.That(engineInfo.TorqueNm, Is.EqualTo(dafEngine.TorqueNm));
                }
            }
        }

        // GetEngineInfoById
        [Test]
        public async Task GetEngineInfoById_ShouldReturnEngineViewModel_WhenEngineExists()
        {
            // Arrange
            Guid engineId = Guid.NewGuid();

            Engine engine = new Engine
            {
                Id = engineId,
                Title = "Engine",
                PowerHp = 500,
                PowerKw = 350,
                TorqueNm = 1300
            };

            unitOfWorkMock.Setup(x => x.Engine.GetById(engineId)).ReturnsAsync(engine);

            // Act
            EngineInfoViewModel serviceModel = await engineService.GetEngineInfoById(engineId);

            // Assert
            Assert.IsInstanceOf<EngineInfoViewModel>(serviceModel);
            Assert.That(serviceModel.Id, Is.EqualTo(engine.Id));
            Assert.That(serviceModel.Title, Is.EqualTo(engine.Title));
            Assert.That(serviceModel.PowerHp, Is.EqualTo(engine.PowerHp));
            Assert.That(serviceModel.PowerKw, Is.EqualTo(engine.PowerKw));
            Assert.That(serviceModel.TorqueNm, Is.EqualTo(engine.TorqueNm));
        }

        [Test]
        public Task GetEngineInfoById_ShouldThrowArgumentException_WhenEngineDoesNotExist()
        {
            // Arrange
            Guid engineId = Guid.NewGuid();

            Engine engine = null!;

            unitOfWorkMock.Setup(x => x.Engine.GetById(engineId)).ReturnsAsync(engine);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await engineService.GetEngineInfoById(engineId));

            return Task.CompletedTask;
        }

        [Test]
        public Task GetEngineInfoById_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenEngineDoesNotExist()
        {
            // Arrange
            Guid engineId = Guid.NewGuid();

            Engine engine = null!;

            unitOfWorkMock.Setup(x => x.Engine.GetById(engineId)).ReturnsAsync(engine);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await engineService.GetEngineInfoById(engineId));

            Assert.That(argumentException.Message, Is.EqualTo(EngineNotExistMessage));

            return Task.CompletedTask;
        }

        // RemoveEngine
        [Test]
        public Task RemoveEngine_ShouldThrowArgumentException_WhenEngineDoesNotExist()
        {
            // Arrange
            Guid engineId = Guid.NewGuid();

            Engine engine = null!;

            unitOfWorkMock.Setup(x => x.Engine.GetById(engineId)).ReturnsAsync(engine);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await engineService.RemoveEngine(engineId));

            return Task.CompletedTask;
        }

        [Test]
        public Task RemoveEngine_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenEngineDoesNotExist()
        {
            // Arrange
            Guid engineId = Guid.NewGuid();

            Engine engine = null!;

            unitOfWorkMock.Setup(x => x.Engine.GetById(engineId)).ReturnsAsync(engine);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await engineService.RemoveEngine(engineId));

            Assert.That(argumentException.Message, Is.EqualTo(EngineNotExistMessage));

            return Task.CompletedTask;
        }
    }
}

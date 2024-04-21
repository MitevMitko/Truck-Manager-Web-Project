namespace TruckManagerSoftware.Tests.ServicesTests
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Moq;

    using Core.Models.Garage;
    using Core.Models.Order;
    using Core.Models.Truck;
    using Core.Models.User;
    using Core.Services.Contract;
    using Core.Services.Implementation;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;

    using static Common.Messages.Messages.Common;
    using static Common.Messages.Messages.User;

    public class UserServiceTests
    {
        private readonly UserService userService;

        private readonly Mock<IUnitOfWork> unitOfWorkMock;

        private readonly Mock<IImageService> imageServiceMock;

        private readonly Mock<IWebHostEnvironment> webHostEnvironmentMock;

        private readonly Mock<IHttpContextAccessor> httpContextAccessorMock;

        private readonly Mock<IUserClaimsPrincipalFactory<User>> userClaimsPrincipalFactory;

        private readonly Mock<IOptions<IdentityOptions>> identityOptions;

        private readonly Mock<ILogger<SignInManager<User>>> logger;

        private readonly Mock<IAuthenticationSchemeProvider> authenticationSchemeProvider;

        private readonly Mock<IUserConfirmation<User>> userConfirmation;

        private readonly Mock<SignInManager<User>> signInManagerMock;

        private readonly Mock<UserManager<User>> userManagerMock;

        public UserServiceTests()
        {
            this.unitOfWorkMock = new Mock<IUnitOfWork>();

            this.imageServiceMock = new Mock<IImageService>();

            this.webHostEnvironmentMock = new Mock<IWebHostEnvironment>();

            this.userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null,
                null, null, null, null, null);

            this.httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            this.userClaimsPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();

            this.identityOptions = new Mock<IOptions<IdentityOptions>>();

            this.logger = new Mock<ILogger<SignInManager<User>>>();

            this.authenticationSchemeProvider = new Mock<IAuthenticationSchemeProvider>();

            this.userConfirmation = new Mock<IUserConfirmation<User>>();

            this.signInManagerMock = new Mock<SignInManager<User>>(
                userManagerMock.Object,
                httpContextAccessorMock.Object,
                userClaimsPrincipalFactory.Object,
                identityOptions.Object,
                logger.Object,
                authenticationSchemeProvider.Object,
                userConfirmation.Object);

            this.userService = new UserService(
                signInManagerMock.Object,
                userManagerMock.Object,
                imageServiceMock.Object,
                unitOfWorkMock.Object,
                webHostEnvironmentMock.Object);
        }

        // ChangeUserStatus
        [Test]
        public Task ChangeUserStatus_ShouldThrowArgumentException_WhenUserDoesNotExist()
        {
            // Arrange
            User user = null!;

            ChangeStatusViewModel model = new ChangeStatusViewModel();

            userManagerMock.Setup(x => x.FindByIdAsync(model.Id.ToString())).ReturnsAsync(user);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await userService.ChangeUserStatus(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task ChangeUserStatus_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenUserDoesNotExist()
        {
            // Arrange
            User user = null!;

            ChangeStatusViewModel model = new ChangeStatusViewModel();

            userManagerMock.Setup(x => x.FindByIdAsync(model.Id.ToString())).ReturnsAsync(user);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await userService.ChangeUserStatus(model));

            Assert.That(argumentException.Message, Is.EqualTo(UserNotExistMessage));

            return Task.CompletedTask;
        }

        // GetUserInfoById
        [Test]
        public async Task GetUserInfoById_ShouldReturnUserInfoViewModel_WhenUserExists()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            User user = new User
            {
                Id = userId,
                UserName = "user",
                Email = "user@user.com",
                Status = "roaming",
                Avatar = Guid.NewGuid().ToString(),
                GarageId = Guid.NewGuid(),
                TruckId = Guid.NewGuid(),
                OrderId = Guid.NewGuid()
            };

            Garage garage = new Garage
            {
                Id = user.GarageId.Value,
                Country = "Bulgaria",
                City = "Ruse",
                Size = "small"
            };

            Truck truck = new Truck
            {
                Id = user.TruckId.Value,
                Brand = "Scania",
                Series = "S"
            };

            Order order = new Order
            {
                Id = user.OrderId.Value,
                Cargo = "Wood"
            };

            userManagerMock.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(user);

            unitOfWorkMock.Setup(x => x.Garage.GetById(user.GarageId.Value)).ReturnsAsync(garage);

            unitOfWorkMock.Setup(x => x.Truck.GetById(user.TruckId.Value)).ReturnsAsync(truck);

            unitOfWorkMock.Setup(x => x.Order.GetById(user.OrderId.Value)).ReturnsAsync(order);

            // Act
            UserInfoViewModel serviceModel = await userService.GetUserInfoById(userId);

            // Assert
            Assert.IsInstanceOf<UserInfoViewModel>(serviceModel);
            Assert.That(serviceModel.Id, Is.EqualTo(user.Id));
            Assert.That(serviceModel.UserName, Is.EqualTo(user.UserName));
            Assert.That(serviceModel.Email, Is.EqualTo(user.Email));
            Assert.That(serviceModel.Status, Is.EqualTo(user.Status));
            Assert.That(serviceModel.Avatar, Is.EqualTo(user.Avatar));

            Assert.IsInstanceOf<GarageInfoViewModel>(serviceModel.GarageInfo);
            Assert.That(serviceModel.GarageInfo!.Id, Is.EqualTo(garage.Id));
            Assert.That(serviceModel.GarageInfo.Country, Is.EqualTo(garage.Country));
            Assert.That(serviceModel.GarageInfo.City, Is.EqualTo(garage.City));
            Assert.That(serviceModel.GarageInfo.Size, Is.EqualTo(garage.Size));

            Assert.IsInstanceOf<TruckInfoViewModel>(serviceModel.TruckInfo);
            Assert.That(serviceModel.TruckInfo!.Id, Is.EqualTo(truck.Id));
            Assert.That(serviceModel.TruckInfo.Brand, Is.EqualTo(truck.Brand));
            Assert.That(serviceModel.TruckInfo.Series, Is.EqualTo(truck.Series));

            Assert.IsInstanceOf<OrderInfoViewModel>(serviceModel.OrderInfo);
            Assert.That(serviceModel.OrderInfo!.Id, Is.EqualTo(order.Id));
            Assert.That(serviceModel.OrderInfo.Cargo, Is.EqualTo(order.Cargo));
        }

        [Test]
        public async Task GetUserInfoById_ShouldReturnUserInfoViewModel_WhenUserExists_AndWhenUserDoesNotHaveGarageTruckOrder()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            User user = new User
            {
                Id = userId,
                UserName = "user",
                Email = "user@user.com",
                Status = "roaming",
                Avatar = Guid.NewGuid().ToString(),
                GarageId = null,
                TruckId = null,
                OrderId = null
            };

            userManagerMock.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(user);

            // Act
            UserInfoViewModel serviceModel = await userService.GetUserInfoById(userId);

            // Assert
            Assert.IsInstanceOf<UserInfoViewModel>(serviceModel);
            Assert.That(serviceModel.Id, Is.EqualTo(user.Id));
            Assert.That(serviceModel.UserName, Is.EqualTo(user.UserName));
            Assert.That(serviceModel.Email, Is.EqualTo(user.Email));
            Assert.That(serviceModel.Status, Is.EqualTo(user.Status));
            Assert.That(serviceModel.Avatar, Is.EqualTo(user.Avatar));

            Assert.That(serviceModel.GarageInfo, Is.EqualTo(null));

            Assert.That(serviceModel.TruckInfo, Is.EqualTo(null));

            Assert.That(serviceModel.OrderInfo, Is.EqualTo(null));
        }

        [Test]
        public Task GetUserInfoById_ShouldThrowArgumentException_WhenUserDoesNotExist()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            User user = null!;

            userManagerMock.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(user);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await userService.GetUserInfoById(userId));

            return Task.CompletedTask;
        }

        [Test]
        public Task GetUserInfoById_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenUserDoesNotExist()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            User user = null!;

            userManagerMock.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(user);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await userService.GetUserInfoById(userId));

            Assert.That(argumentException.Message, Is.EqualTo(UserNotExistMessage));

            return Task.CompletedTask;
        }

        // LoginUser
        [Test]
        public Task LoginUser_ShouldThrowArgumentException_WhenUserUserNameDoesNotExist()
        {
            // Arrange
            LoginViewModel model = new LoginViewModel();

            User user = null!;

            userManagerMock.Setup(x => x.FindByNameAsync(model.UserName)).ReturnsAsync(user);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await userService.LoginUser(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task LoginUser_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenUserUserNameDoesNotExist()
        {
            // Arrange
            LoginViewModel model = new LoginViewModel();

            User user = null!;

            userManagerMock.Setup(x => x.FindByNameAsync(model.UserName)).ReturnsAsync(user);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await userService.LoginUser(model));

            Assert.That(argumentException.Message, Is.EqualTo(UserNotExistMessage));

            return Task.CompletedTask;
        }

        [Test]
        public Task LoginUser_ShouldThrowArgumentException_WhenSignInResultIsNotSucceeded()
        {
            // Arrange
            LoginViewModel model = new LoginViewModel();

            User user = new User();

            userManagerMock.Setup(x => x.FindByNameAsync(model.UserName)).ReturnsAsync(user);

            signInManagerMock.Setup(x => x.PasswordSignInAsync(user, model.Password, false, false)).ReturnsAsync(new SignInResult());

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await userService.LoginUser(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task LoginUser_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenSignInResultIsNotSucceeded()
        {
            // Arrange
            LoginViewModel model = new LoginViewModel();

            User user = new User();

            userManagerMock.Setup(x => x.FindByNameAsync(model.UserName)).ReturnsAsync(user);

            signInManagerMock.Setup(x => x.PasswordSignInAsync(user, model.Password, false, false)).ReturnsAsync(new SignInResult());

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await userService.LoginUser(model));

            Assert.That(argumentException.Message, Is.EqualTo(SomethingWentWrongMessage));

            return Task.CompletedTask;
        }

        // RegisterUser
        [Test]
        public Task RegisterUser_ShouldThrowArgumentException_WhenUserUserNameExists()
        {
            // Arrange
            RegisterViewModel model = new RegisterViewModel();

            User user = new User();

            userManagerMock.Setup(x => x.FindByNameAsync(model.UserName)).ReturnsAsync(user);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await userService.RegisterUser(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task RegisterUser_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenUserUserNameExists()
        {
            // Arrange
            RegisterViewModel model = new RegisterViewModel();

            User user = new User();

            userManagerMock.Setup(x => x.FindByNameAsync(model.UserName)).ReturnsAsync(user);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await userService.RegisterUser(model));

            Assert.That(argumentException.Message, Is.EqualTo(UserUserNameAlreadyExits));

            return Task.CompletedTask;
        }

        [Test]
        public Task RegisterUser_ShouldThrowArgumentException_WhenUserEmailExists()
        {
            // Arrange
            RegisterViewModel model = new RegisterViewModel();

            User user = new User();

            userManagerMock.Setup(x => x.FindByEmailAsync(model.Email)).ReturnsAsync(user);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await userService.RegisterUser(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task RegisterUser_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenUserEmailExists()
        {
            // Arrange
            RegisterViewModel model = new RegisterViewModel();

            User user = new User();

            userManagerMock.Setup(x => x.FindByEmailAsync(model.Email)).ReturnsAsync(user);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await userService.RegisterUser(model));

            Assert.That(argumentException.Message, Is.EqualTo(UserEmailAlreadyExists));

            return Task.CompletedTask;
        }

        // UploadUserImage
        [Test]
        public Task UploadUserImage_ShouldThrowArgumentException_WhenUserDoesNotExist()
        {
            // Arrange
            UploadUserImageViewModel model = new UploadUserImageViewModel();

            User user = null!;

            userManagerMock.Setup(x => x.FindByIdAsync(model.Id.ToString())).ReturnsAsync(user);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await userService.UploadUserImage(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task UploadUserImage_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenUserDoesNotExist()
        {
            // Arrange
            UploadUserImageViewModel model = new UploadUserImageViewModel();

            User user = null!;

            userManagerMock.Setup(x => x.FindByIdAsync(model.Id.ToString())).ReturnsAsync(user);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await userService.UploadUserImage(model));

            Assert.That(argumentException.Message, Is.EqualTo(UserNotExistMessage));

            return Task.CompletedTask;
        }
    }
}

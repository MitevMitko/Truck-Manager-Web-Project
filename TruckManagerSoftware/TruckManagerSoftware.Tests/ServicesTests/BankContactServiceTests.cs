namespace TruckManagerSoftware.Tests.ServicesTests
{
    using Moq;

    using Core.Models.BankContact;
    using Core.Services.Implementation;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;

    using static Common.Messages.Messages.BankContact;

    public class BankContactServiceTests
    {
        private readonly BankContactService bankContactService;

        private readonly Mock<IUnitOfWork> unitOfWorkMock;

        public BankContactServiceTests()
        {
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
            this.bankContactService = new BankContactService(unitOfWorkMock.Object);
        }

        // AddBankContact
        [Test]
        public Task AddBankContact_ShouldThrowArgumentException_WhenBankContactNameExists()
        {
            // Arrange
            AddBankContactViewModel model = new AddBankContactViewModel();

            unitOfWorkMock.Setup(x => x.BankContact.AnyAsync(bc => bc.Name == model.Name)).ReturnsAsync(true);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await bankContactService.AddBankContact(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddBankContact_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenBankContactNameExists()
        {
            // Arrange
            AddBankContactViewModel model = new AddBankContactViewModel();

            unitOfWorkMock.Setup(x => x.BankContact.AnyAsync(bc => bc.Name == model.Name)).ReturnsAsync(true);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await bankContactService.AddBankContact(model));

            Assert.That(argumentException.Message, Is.EqualTo(BankContactNameExistsMessage));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddBankContact_ShouldThrowArgumentException_WhenBankContactEmailExists()
        {
            // Arrange
            AddBankContactViewModel model = new AddBankContactViewModel();

            unitOfWorkMock.Setup(x => x.BankContact.AnyAsync(bc => bc.Email == model.Email)).ReturnsAsync(true);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await bankContactService.AddBankContact(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddBankContact_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenBankContactEmailExists()
        {
            // Arrange
            AddBankContactViewModel model = new AddBankContactViewModel();

            unitOfWorkMock.Setup(x => x.BankContact.AnyAsync(bc => bc.Email == model.Email)).ReturnsAsync(true);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await bankContactService.AddBankContact(model));

            Assert.That(argumentException.Message, Is.EqualTo(BankContactEmailExistsMessage));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddBankContact_ShouldThrowArgumentException_WhenBankContactPhoneNumberExits()
        {
            // Arrange
            AddBankContactViewModel model = new AddBankContactViewModel();

            unitOfWorkMock.Setup(x => x.BankContact.AnyAsync(bc => bc.PhoneNumber == model.PhoneNumber)).ReturnsAsync(true);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await bankContactService.AddBankContact(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task AddBankContact_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenBankContactPhoneNumberExists()
        {
            // Arrange
            AddBankContactViewModel model = new AddBankContactViewModel();

            unitOfWorkMock.Setup(x => x.BankContact.AnyAsync(bc => bc.PhoneNumber == model.PhoneNumber)).ReturnsAsync(true);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await bankContactService.AddBankContact(model));

            Assert.That(argumentException.Message, Is.EqualTo(BankContactPhoneNumberExistsMessage));

            return Task.CompletedTask;
        }

        // EditBankContact
        [Test]
        public Task EditBankContact_ShouldThrowArgumentException_WhenBankContactDoesNotExist()
        {
            // Arrange
            BankContact bankContact = null!;

            EditBankContactViewModel model = new EditBankContactViewModel();

            unitOfWorkMock.Setup(x => x.BankContact.GetById(model.Id)).ReturnsAsync(bankContact);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await bankContactService.EditBankContact(model));

            return Task.CompletedTask;
        }

        [Test]
        public Task EditBankContact_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenBankContactDoesNotExist()
        {
            // Arrange
            BankContact bankContact = null!;

            EditBankContactViewModel model = new EditBankContactViewModel();

            unitOfWorkMock.Setup(x => x.BankContact.GetById(model.Id)).ReturnsAsync(bankContact);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await bankContactService.EditBankContact(model));

            Assert.That(argumentException.Message, Is.EqualTo(BankContactNotExistMessage));

            return Task.CompletedTask;
        }

        // GetAllBankContactsInfo
        [Test]
        public async Task GetAllBankContactsInfo_ShouldReturnICollectionOfBankContactInfoViewModel()
        {
            // Arrange
            BankContact dskBank = new BankContact
            {
                Id = Guid.NewGuid(),
                Name = "DSK",
                Email = "dsk@dsk.bg",
                PhoneNumber = "1234567890"
            };

            BankContact ubbBank = new BankContact
            {
                Id = Guid.NewGuid(),
                Name = "UBB",
                Email = "ubb@ubb.bg",
                PhoneNumber = "1234568560"
            };

            IEnumerable<BankContact> bankContacts = new List<BankContact>
            {
                dskBank,
                ubbBank
            };

            unitOfWorkMock.Setup(x => x.BankContact.GetAll()).ReturnsAsync(bankContacts);

            // Act
            ICollection<BankContactInfoViewModel> serviceModel = await bankContactService.GetAllBankContactsInfo();

            // Assert
            Assert.IsInstanceOf<ICollection<BankContactInfoViewModel>>(serviceModel);
            Assert.IsNotNull(serviceModel);
            Assert.That(serviceModel.Count, Is.EqualTo(bankContacts.Count()));

            int cnt = 0;

            foreach (BankContactInfoViewModel bankContactInfo in serviceModel)
            {
                if (cnt == 0)
                {
                    Assert.IsInstanceOf<BankContactInfoViewModel>(bankContactInfo);
                    Assert.That(bankContactInfo.Id, Is.EqualTo(dskBank.Id));
                    Assert.That(bankContactInfo.Name, Is.EqualTo(dskBank.Name));
                    Assert.That(bankContactInfo.Email, Is.EqualTo(dskBank.Email));
                    Assert.That(bankContactInfo.PhoneNumber, Is.EqualTo(dskBank.PhoneNumber));

                    cnt++;
                }
                else
                {
                    Assert.IsInstanceOf<BankContactInfoViewModel>(bankContactInfo);
                    Assert.That(bankContactInfo.Id, Is.EqualTo(ubbBank.Id));
                    Assert.That(bankContactInfo.Name, Is.EqualTo(ubbBank.Name));
                    Assert.That(bankContactInfo.Email, Is.EqualTo(ubbBank.Email));
                    Assert.That(bankContactInfo.PhoneNumber, Is.EqualTo(ubbBank.PhoneNumber));
                }
            }
        }

        // GetBankContactInfoById
        [Test]
        public async Task GetBankContactInfoById_ShouldReturnBankContactInfoViewModel_WhenBankContactExists()
        {
            // Arrange
            Guid bankContactId = Guid.NewGuid();

            BankContact bankContact = new BankContact
            {
                Id = bankContactId,
                Name = "DSK",
                Email = "dsk@dsk.bg",
                PhoneNumber = "1234567890"
            };

            unitOfWorkMock.Setup(x => x.BankContact.GetById(bankContactId)).ReturnsAsync(bankContact);

            // Act
            BankContactInfoViewModel serviceModel = await bankContactService.GetBankContactInfoById(bankContactId);

            // Assert
            Assert.IsInstanceOf<BankContactInfoViewModel>(serviceModel);
            Assert.That(serviceModel.Id, Is.EqualTo(bankContact.Id));
            Assert.That(serviceModel.Name, Is.EqualTo(bankContact.Name));
            Assert.That(serviceModel.Email, Is.EqualTo(bankContact.Email));
            Assert.That(serviceModel.PhoneNumber, Is.EqualTo(bankContact.PhoneNumber));
        }

        [Test]
        public Task GetBankContactInfoById_ShouldThrowArgumentException_WhenBankContactDoesNotExist()
        {
            // Arrange
            Guid bankContactId = Guid.NewGuid();

            BankContact bankContact = null!;

            unitOfWorkMock.Setup(x => x.BankContact.GetById(bankContactId)).ReturnsAsync(bankContact);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await bankContactService.GetBankContactInfoById(bankContactId));

            return Task.CompletedTask;
        }

        [Test]
        public Task GetBankContactInfoById_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenBankContactDoesNotExist()
        {
            // Arrange
            Guid bankContactId = Guid.NewGuid();

            BankContact bankContact = null!;

            unitOfWorkMock.Setup(x => x.BankContact.GetById(bankContactId)).ReturnsAsync(bankContact);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await bankContactService.GetBankContactInfoById(bankContactId));

            Assert.That(argumentException.Message, Is.EqualTo(BankContactNotExistMessage));

            return Task.CompletedTask;
        }

        // RemoveBankContact
        [Test]
        public Task RemoveBankContact_ShouldThrowArgumentException_WhenBankContactDoesNotExits()
        {
            // Arrange
            Guid bankContactId = Guid.NewGuid();

            BankContact bankContact = null!;

            unitOfWorkMock.Setup(x => x.BankContact.GetById(bankContactId)).ReturnsAsync(bankContact);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await bankContactService.RemoveBankContact(bankContactId));

            return Task.CompletedTask;
        }

        [Test]
        public Task RemoveBankContact_ShouldThrowArgumentException_CheckArgumentExceptionMessage_WhenBankContactDoesNotExist()
        {
            // Arrange
            Guid bankContactId = Guid.NewGuid();

            BankContact bankContact = null!;

            unitOfWorkMock.Setup(x => x.BankContact.GetById(bankContactId)).ReturnsAsync(bankContact);

            // Act & Assert
            ArgumentException argumentException = Assert.ThrowsAsync<ArgumentException>(async () => await bankContactService.RemoveBankContact(bankContactId));

            Assert.That(argumentException.Message, Is.EqualTo(BankContactNotExistMessage));

            return Task.CompletedTask;
        }
    }
}

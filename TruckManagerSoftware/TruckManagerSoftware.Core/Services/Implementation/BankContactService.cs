namespace TruckManagerSoftware.Core.Services.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Contract;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;
    using Models.BankContact;

    using static Common.Messages.Messages.BankContact;

    public class BankContactService : IBankContactService
    {
        private readonly IUnitOfWork unitOfWork;

        public BankContactService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddBankContact(AddBankContactViewModel model)
        {
            // Create new bank contact
            BankContact bankContact = new BankContact()
            {
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            // Add the created bank contact to the database
            await unitOfWork.BankContact.Add(bankContact);

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task EditBankContact(EditBankContactViewModel model)
        {
            // Get the bank contact by id
            // If bank contact does not exist
            // Throw argument exception
            BankContact bankContact = await unitOfWork.BankContact.GetById(model.Id) ?? throw new ArgumentException(BankContactNotExistMessage);

            // Assign the edited data
            // To the bank contact
            bankContact.Name = model.Name;
            bankContact.Email = model.Email;
            bankContact.PhoneNumber = model.PhoneNumber;

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task<ICollection<BankContactInfoViewModel>> GetAllBankContactsInfo()
        {
            // Create collection of bank contact view model
            ICollection<BankContactInfoViewModel> model = new List<BankContactInfoViewModel>();

            // Get all bank contacts
            // From the database
            IEnumerable<BankContact> bankContacts = await unitOfWork.BankContact.GetAll();

            // Assign the data from the bank contacts
            // From the database to the bank contact view model
            // And then add the bank contact view model
            // To the collection of bank contact view model
            foreach (BankContact bankContact in bankContacts)
            {
                BankContactInfoViewModel bankContactInfo = new BankContactInfoViewModel()
                {
                    Id = bankContact.Id,
                    Name = bankContact.Name,
                    Email = bankContact.Email,
                    PhoneNumber = bankContact.PhoneNumber
                };

                model.Add(bankContactInfo);
            }

            return model;
        }

        public async Task<BankContactInfoViewModel> GetBankContactInfoById(Guid id)
        {
            // Get the bank contact by id
            // If bank contact does not exist
            // Throw argument exception
            BankContact bankContact = await unitOfWork.BankContact.GetById(id) ?? throw new ArgumentException(BankContactNotExistMessage);

            // Create bank contact view model
            // Assign the data from the bank contact
            // And return the bank contact view model
            return new BankContactInfoViewModel
            {
                Id = bankContact.Id,
                Name = bankContact.Name,
                Email = bankContact.Email,
                PhoneNumber = bankContact.PhoneNumber
            };
        }
    }
}

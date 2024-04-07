namespace TruckManagerSoftware.Core.Services.Contract
{
    using Models.BankContact;

    public interface IBankContactService
    {
        Task AddBankContact(AddBankContactViewModel model);

        Task EditBankContact(EditBankContactViewModel model);

        Task RemoveBankContact(Guid id);

        Task<BankContactInfoViewModel> GetBankContactInfoById(Guid id);

        Task<ICollection<BankContactInfoViewModel>> GetAllBankContactsInfo();
    }
}

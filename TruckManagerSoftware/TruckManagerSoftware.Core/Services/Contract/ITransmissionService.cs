namespace TruckManagerSoftware.Core.Services.Contract
{
    using TruckManagerSoftware.Core.Models.Transmission;

    public interface ITransmissionService
    {
        Task AddTransmission(AddTransmissionViewModel model);

        Task EditTransmission(EditTransmissionViewModel model);

        Task<TransmissionInfoViewModel> GetTransmissionInfoById(Guid id);

        Task<ICollection<TransmissionInfoViewModel>> GetAllTransmissionsInfo();
    }
}

namespace TruckManagerSoftware.Core.Services.Contract
{
    using Models.Garage;

    public interface IGarageService
    {
        Task AddGarage(AddGarageViewModel model);

        Task EditGarage(EditGarageViewModel model);

        Task RemoveGarage(Guid id);

        Task<GarageInfoViewModel> GetGarageInfoById(Guid id);

        Task<ICollection<GarageInfoViewModel>> GetAllGaragesInfo();
    }
}

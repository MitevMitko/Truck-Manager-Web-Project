namespace TruckManagerSoftware.Core.Services.Contract
{
    using Models.Garage;

    public interface IGarageService
    {
        Task AddGarage(AddGarageViewModel model);

        Task AddGarageTruckToGarageTrailer(Guid id, Guid trailerId);

        Task RemoveGarageTruckFromGarageTrailer(Guid id, Guid trailerId);

        Task EditGarage(EditGarageViewModel model);

        Task RemoveGarage(Guid id);

        Task<GarageInfoViewModel> GetGarageInfoById(Guid id);

        Task<ICollection<GarageInfoViewModel>> GetAllGaragesInfo();

        Task<ICollection<GarageInfoViewModel>> GetAllGaragesInfoWithFreeSpaceForTrucks();

        Task<ICollection<GarageInfoViewModel>> GetAllGaragesInfoWithFreeSpaceForTrailers();

        Task<ICollection<GarageTruckInfoViewModel>> GetGarageTrucksInfo(Guid id);

        Task<ICollection<GarageTrailerInfoViewModel>> GetGarageTrailersInfo(Guid id);

        Task<ICollection<GarageTruckTrailerInfoViewModel>> GetGarageTrucksTrailersInfo(Guid id);
    }
}

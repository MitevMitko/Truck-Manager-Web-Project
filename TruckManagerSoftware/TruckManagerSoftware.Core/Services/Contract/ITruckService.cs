namespace TruckManagerSoftware.Core.Services.Contract
{
    using Models.Truck;

    public interface ITruckService
    {
        Task AddTruck(AddTruckViewModel model);

        Task EditTruck(EditTruckViewModel model);

        Task<TruckInfoViewModel> GetTruckInfoById(Guid id);

        Task<ICollection<TruckInfoViewModel>> GetAllTrucksInfo();
    }
}

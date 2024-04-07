namespace TruckManagerSoftware.Core.Services.Contract
{
    using Models.Engine;

    public interface IEngineService
    {
        Task AddEngine(AddEngineViewModel model);

        Task EditEngine(EditEngineViewModel model);

        Task RemoveEngine(Guid id);

        Task<EngineInfoViewModel> GetEngineInfoById(Guid id);

        Task<ICollection<EngineInfoViewModel>> GetAllEnginesInfo();
    }
}

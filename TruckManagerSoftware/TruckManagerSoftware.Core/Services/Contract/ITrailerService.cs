namespace TruckManagerSoftware.Core.Services.Contract
{
    using Models.Trailer;

    public interface ITrailerService
    {
        Task AddTrailer(AddTrailerViewModel model);

        Task EditTrailer(EditTrailerViewModel model);

        Task RemoveTrailer(Guid id);

        Task<TrailerInfoViewModel> GetTrailerInfoById(Guid id);

        Task<ICollection<TrailerInfoViewModel>> GetAllTrailersInfo();
    }
}

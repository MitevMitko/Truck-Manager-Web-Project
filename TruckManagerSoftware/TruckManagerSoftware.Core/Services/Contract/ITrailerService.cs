namespace TruckManagerSoftware.Core.Services.Contract
{
    using Models.Trailer;

    public interface ITrailerService
    {
        Task AddTrailer(AddTrailerViewModel model);

        Task EditTrailer(EditTrailerViewModel model);

        Task<TrailerInfoViewModel> GetTrailerInfoById(Guid id);

        Task<ICollection<TrailerInfoViewModel>> GetAllTrailersInfo();
    }
}

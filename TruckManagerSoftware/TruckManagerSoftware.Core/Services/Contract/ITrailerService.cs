namespace TruckManagerSoftware.Core.Services.Contract
{
    using Models.Trailer;

    public interface ITrailerService
    {
        Task AddTrailer(AddTrailerViewModel model);

        Task EditTrailer(EditTrailerViewModel model);

        Task RemoveTrailer(Guid id);

        Task<TrailerInfoViewModel> GetTrailerInfoById(Guid id);

        Task<TrailerAdditionalInfoViewModel> GetAdditionalTrailerInfoById(Guid id);

        Task<ICollection<TrailerInfoViewModel>> GetAllTrailersInfo();

        Task<ICollection<TrailerInfoViewModel>> GetAllTrailersInfoByGarageIdWithoutTruckId(Guid id);
    }
}

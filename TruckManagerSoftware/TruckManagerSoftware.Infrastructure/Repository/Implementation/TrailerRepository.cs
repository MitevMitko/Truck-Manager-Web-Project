namespace TruckManagerSoftware.Infrastructure.Repository.Implementation
{
    using Contract;
    using Data;
    using Data.Models;

    public class TrailerRepository : GenericRepository<Trailer>, ITrailerRepository
    {
        public TrailerRepository(ApplicationDbContext context) : base(context) { }
    }
}

namespace TruckManagerSoftware.Infrastructure.Repository.Implementation
{
    using Contract;
    using Data;
    using Data.Models;

    public class EngineRepository : GenericRepository<Engine>, IEngineRepository
    {
        public EngineRepository(ApplicationDbContext context) : base(context) { }
    }
}

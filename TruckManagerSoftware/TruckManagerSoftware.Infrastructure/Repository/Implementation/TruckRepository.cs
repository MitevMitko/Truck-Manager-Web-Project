namespace TruckManagerSoftware.Infrastructure.Repository.Implementation
{
    using Contract;
    using Data;
    using Data.Models;

    public class TruckRepository : GenericRepository<Truck>, ITruckRepository
    {
        public TruckRepository(ApplicationDbContext context) : base(context) { }
    }
}

namespace TruckManagerSoftware.Infrastructure.Repository.Implementation
{
    using Contract;
    using Data;
    using Data.Models;

    public class GarageRepository : GenericRepository<Garage>, IGarageRepository
    {
        public GarageRepository(ApplicationDbContext context) : base(context) { }
    }
}

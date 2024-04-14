namespace TruckManagerSoftware.Infrastructure.Repository.Implementation
{
    using Contract;
    using Data;
    using Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GarageRepository : GenericRepository<Garage>, IGarageRepository
    {
        public GarageRepository(ApplicationDbContext context) : base(context) { }
    }
}

namespace TruckManagerSoftware.Infrastructure.Repository.Implementation
{
    using Contract;
    using Data;
    using Data.Models;

    public class TransmissionRepository : GenericRepository<Transmission>, ITransmissionRepository
    {
        public TransmissionRepository(ApplicationDbContext context) : base(context) { }
    }
}

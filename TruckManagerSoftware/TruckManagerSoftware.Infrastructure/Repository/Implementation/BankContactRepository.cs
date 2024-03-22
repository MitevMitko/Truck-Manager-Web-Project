namespace TruckManagerSoftware.Infrastructure.Repository.Implementation
{
    using Contract;
    using Data;
    using Data.Models;

    public class BankContactRepository : GenericRepository<BankContact>, IBankContactRepository
    {
        public BankContactRepository(ApplicationDbContext context) : base(context) { }
    }
}

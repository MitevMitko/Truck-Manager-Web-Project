namespace TruckManagerSoftware.Infrastructure.Repository.Implementation
{
    using Contract;
    using Data;
    using Data.Models;

    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }
    }
}

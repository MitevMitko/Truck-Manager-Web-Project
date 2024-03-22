namespace TruckManagerSoftware.Infrastructure.UnitOfWork.Implementation
{
    using System.Threading.Tasks;

    using Contract;
    using Data;
    using Repository.Contract;
    using Repository.Implementation;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            this.BankContact = new BankContactRepository(context);
            this.Engine = new EngineRepository(context);
            this.Garage = new GarageRepository(context);
            this.Order = new OrderRepository(context);
            this.Trailer = new TrailerRepository(context);
            this.Transmission = new TransmissionRepository(context);
            this.Truck = new TruckRepository(context);
        }

        public IBankContactRepository BankContact { get; private set; }

        public IEngineRepository Engine { get; private set; }

        public IGarageRepository Garage { get; private set; }

        public IOrderRepository Order { get; private set; }

        public ITrailerRepository Trailer { get; private set; }

        public ITransmissionRepository Transmission { get; private set; }

        public ITruckRepository Truck { get; private set; }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}

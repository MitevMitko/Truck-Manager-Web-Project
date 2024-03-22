namespace TruckManagerSoftware.Infrastructure.UnitOfWork.Contract
{
    using Repository.Contract;

    public interface IUnitOfWork : IDisposable
    {
        IBankContactRepository BankContact { get; }

        IEngineRepository Engine { get; }

        IGarageRepository Garage { get; }

        IOrderRepository Order { get; }

        ITrailerRepository Trailer { get; }

        ITransmissionRepository Transmission { get; }

        ITruckRepository Truck { get; }

        Task CompleteAsync();
    }
}

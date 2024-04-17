namespace TruckManagerSoftware.Core.Services.Contract
{
    using Models.Order;

    public interface IOrderService
    {
        Task AddOrder(AddOrderViewModel model);

        Task EditOrder(EditOrderViewModel model);

        Task AddOrderToTruck(Guid id, Guid orderId);

        Task RemoveOrder(Guid id);

        Task FinishOrder(Guid id);

        Task<OrderInfoViewModel> GetOrderInfoById(Guid id);

        Task<OrderAdditionalInfoViewModel> GetAdditionalOrderInfoById(Guid id);

        Task<ICollection<OrderInfoViewModel>> GetAllOrdersInfo();

        ICollection<OrderInfoViewModel> GetOrdersInfoWithoutTruckId();
    }
}

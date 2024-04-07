namespace TruckManagerSoftware.Core.Services.Contract
{
    using Models.Order;

    public interface IOrderService
    {
        Task AddOrder(AddOrderViewModel model);

        Task EditOrder(EditOrderViewModel model);

        Task RemoveOrder(Guid id);

        Task<OrderInfoViewModel> GetOrderInfoById(Guid id);

        Task<ICollection<OrderInfoViewModel>> GetAllOrdersInfo();
    }
}

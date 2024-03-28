namespace TruckManagerSoftware.Core.Services.Contract
{
    using Models.Order;

    public interface IOrderService
    {
        Task AddOrder(AddOrderViewModel model);

        Task EditOrder(EditOrderViewModel model);

        Task<OrderInfoViewModel> GetOrderInfoById(Guid id);

        Task<ICollection<OrderInfoViewModel>> GetAllOrdersInfo();
    }
}

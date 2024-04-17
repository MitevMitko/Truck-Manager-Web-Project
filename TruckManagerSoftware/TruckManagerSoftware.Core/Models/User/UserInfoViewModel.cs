namespace TruckManagerSoftware.Core.Models.User
{
    using Order;
    using Garage;
    using Truck;

    public class UserInfoViewModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string? Avatar {  get; set; }

        public GarageInfoViewModel? GarageInfo { get; set; }

        public TruckInfoViewModel? TruckInfo { get; set; }

        public OrderInfoViewModel? OrderInfo { get; set; } = null!;
    }
}

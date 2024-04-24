namespace TruckManagerSoftware.Core.Models.Order
{
    public class OrderInfoViewModel
    {
        public Guid Id { get; set; }

        public string Cargo { get; set; } = null!;

        public int CargoWeight { get; set; }

        public string StartPoint { get; set; } = null!;

        public string EndPoint { get; set; } = null!;

        public string DeliveryType { get; set; } = null!;

        public int TripDistance { get; set; }

        public string TripTime { get; set; } = null!;

        public int DeliveryPrice { get; set; }

        public Guid? TruckId { get; set; }

        public Guid? UserId { get; set; }
    }
}

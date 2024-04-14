namespace TruckManagerSoftware.Core.Models.Truck
{
    using Infrastructure.Data.Models;

    public class TruckAdditionalInfoViewModel
    {
        public Guid Id { get; set; }

        public string Brand { get; set; } = null!;

        public string Series { get; set; } = null!;

        public double DrivenDistance { get; set; }

        public string? Image { get; set; }

        public Garage? Garage { get; set; }

        public Trailer? Trailer { get; set; }

        public Order? Order { get; set; }

        public Engine? Engine { get; set; }

        public Transmission? Transmission { get; set; }

        public User? User { get; set; }
    }
}

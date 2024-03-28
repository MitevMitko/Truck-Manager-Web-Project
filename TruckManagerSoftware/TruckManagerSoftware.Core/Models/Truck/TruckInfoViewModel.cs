namespace TruckManagerSoftware.Core.Models.Truck
{
    using System.ComponentModel.DataAnnotations;

    public class TruckInfoViewModel
    {
        public Guid Id { get; set; }

        public string Brand { get; set; } = null!;

        public string Series { get; set; } = null!;

        public double DrivenDistance { get; set; }

        public string? Image { get; set; }
    }
}

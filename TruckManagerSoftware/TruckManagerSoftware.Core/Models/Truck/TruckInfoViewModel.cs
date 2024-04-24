namespace TruckManagerSoftware.Core.Models.Truck
{
    public class TruckInfoViewModel
    {
        public Guid Id { get; set; }

        public string Brand { get; set; } = null!;

        public string Series { get; set; } = null!;

        public double DrivenDistance { get; set; }

        public string? Image { get; set; }

        public Guid? GarageId { get; set; }

        public Guid? EngineId { get; set; }

        public Guid? TransmissionId { get; set; }

        public Guid? TrailerId { get; set; }

        public Guid? OrderId { get; set; }

        public Guid? UserId { get; set; }
    }
}

namespace TruckManagerSoftware.Core.Models.Garage
{
    public class GarageTruckInfoViewModel
    {
        public Guid Id { get; set; }

        public Guid? TrailerId { get; set; }

        public string Brand { get; set; } = null!;

        public string Series { get; set; } = null!;

        public string EngineTitle { get; set; } = null!;

        public string TransmissionTitle { get; set; } = null!;
    }
}

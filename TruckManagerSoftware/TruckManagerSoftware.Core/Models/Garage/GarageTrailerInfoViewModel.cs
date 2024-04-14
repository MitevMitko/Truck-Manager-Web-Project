namespace TruckManagerSoftware.Core.Models.Garage
{
    public class GarageTrailerInfoViewModel
    {
        public Guid Id { get; set; }

        public Guid? TruckId { get; set; }

        public string Title { get; set; } = null!;

        public string Series { get; set; } = null!;

        public string TrailerType { get; set; } = null!;

        public string BodyType { get; set; } = null!;
    }
}

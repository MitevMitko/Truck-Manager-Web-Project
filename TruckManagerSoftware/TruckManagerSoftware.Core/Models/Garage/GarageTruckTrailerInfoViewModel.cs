namespace TruckManagerSoftware.Core.Models.Garage
{
    public class GarageTruckTrailerInfoViewModel
    {
        public Guid Id { get; set; }

        public Guid TruckId { get; set; }

        public Guid? TruckUserId { get; set; }

        public Guid TrailerId { get; set; }

        public string TruckBrand { get; set; } = null!;

        public string TruckSeries { get; set; } = null!;

        public string TrailerTitle { get; set; } = null!;

        public string TrailerSeries { get; set; } = null!;
    }
}

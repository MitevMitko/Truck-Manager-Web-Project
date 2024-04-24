namespace TruckManagerSoftware.Core.Models.Garage
{
    public class GarageInfoViewModel
    {
        public Guid Id { get; set; }

        public string Country { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Size { get; set; } = null!;

        public int TrucksCapacity { get; set; }

        public int TrailersCapacity { get; set; }

        public int TrucksCount { get; set; }

        public int TrailersCount { get; set; }
    }
}

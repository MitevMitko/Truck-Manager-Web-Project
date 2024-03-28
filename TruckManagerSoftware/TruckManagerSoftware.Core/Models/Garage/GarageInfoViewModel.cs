namespace TruckManagerSoftware.Core.Models.Garage
{
    public class GarageInfoViewModel
    {
        public Guid Id { get; set; }

        public string Country { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Size { get; set; } = null!;
    }
}

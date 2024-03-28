namespace TruckManagerSoftware.Core.Models.Trailer
{
    public class TrailerInfoViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Series { get; set; } = null!;

        public string TrailerType { get; set; } = null!;

        public string BodyType { get; set; } = null!;

        public int TareWeight { get; set; }

        public int AxleCount { get; set; }

        public double TotalLength { get; set; }

        public string CargoTypes { get; set; } = null!;

        public string? Image { get; set; }
    }
}

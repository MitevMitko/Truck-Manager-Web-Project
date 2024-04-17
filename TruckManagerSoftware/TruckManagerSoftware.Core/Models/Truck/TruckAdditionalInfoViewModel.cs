namespace TruckManagerSoftware.Core.Models.Truck
{
    using Engine;
    using Garage;
    using Order;
    using Trailer;
    using Transmission;
    using User;

    public class TruckAdditionalInfoViewModel
    {
        public Guid Id { get; set; }

        public string Brand { get; set; } = null!;

        public string Series { get; set; } = null!;

        public double DrivenDistance { get; set; }

        public string? Image { get; set; }

        public GarageInfoViewModel? GarageInfo { get; set; }

        public TrailerInfoViewModel? TrailerInfo { get; set; }

        public OrderInfoViewModel? OrderInfo { get; set; }

        public EngineInfoViewModel? EngineInfo { get; set; }

        public TransmissionInfoViewModel? TransmissionInfo { get; set; }

        public UserInfoViewModel? UserInfo { get; set; }
    }
}

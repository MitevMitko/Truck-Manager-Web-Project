namespace TruckManagerSoftware.Core.Models.Order
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants.DataConstants.Order;

    public class AddOrderViewModel
    {
        [Required]
        [StringLength(CargoMaxLength, MinimumLength = CargoMinLength)]
        public string Cargo { get; set; } = null!;

        [Required]
        public int CargoWeight { get; set; }

        [Required]
        [StringLength(StartPointMaxLength, MinimumLength = StartPointMinLength)]
        public string StartPoint { get; set; } = null!;

        [Required]
        [StringLength(EndPointMaxLength, MinimumLength = EndPointMinLength)]
        public string EndPoint { get; set; } = null!;

        [Required]
        [StringLength(DeliveryTypeMaxLength, MinimumLength = DeliveryTypeMinLength)]
        public string DeliveryType { get; set; } = null!;

        [Required]
        public int TripDistance { get; set; }

        [Required]
        [StringLength(TripTimeMaxLength, MinimumLength = TripTimeMinLength)]
        public string TripTime { get; set; } = null!;

        [Required]
        public int DeliveryPrice { get; set; }
    }
}

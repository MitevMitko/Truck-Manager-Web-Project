namespace TruckManagerSoftware.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.DataConstants.DataConstants.Order;

    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(CargoMaxLength)]
        public string Cargo { get; set; } = null!;

        [Required]
        public int CargoWeight { get; set; }

        [Required]
        [MaxLength(StartPointMaxLength)]
        public string StartPoint { get; set; } = null!;

        [Required]
        [MaxLength(EndPointMaxLength)]
        public string EndPoint { get; set; } = null!;

        [Required]
        [MaxLength(DeliveryTypeMaxLength)]
        public string DeliveryType { get; set; } = null!;

        [Required]
        public int TripDistance { get; set; }

        [Required]
        public string TripTime { get; set; } = null!;

        [Required]
        public int DeliveryPrice { get; set; }

        [ForeignKey(nameof(Truck))]
        public Guid? TruckId { get; set; }
        public Truck? Truck { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}

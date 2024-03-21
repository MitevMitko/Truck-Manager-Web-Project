namespace TruckManagerSoftware.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.DataConstants.DataConstants.Truck;

    public class Truck
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(BrandMaxLength)]
        public string Brand { get; set; } = null!;

        [Required]
        [MaxLength(SeriesMaxLength)]
        public string Series { get; set; } = null!;

        [Required]
        public double DrivenDistance { get; set; }

        [MaxLength(ImageMaxLength)]
        public string? Image { get; set; }

        [ForeignKey(nameof(Garage))]
        public Guid? GarageId { get; set; }
        public Garage? Garage { get; set; }

        [ForeignKey(nameof(Trailer))]
        public Guid? TrailerId { get; set; }
        public Trailer? Trailer { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(Engine))]
        public Guid? EngineId { get; set; }
        public Engine? Engine { get; set; }

        [ForeignKey(nameof(Transmission))]
        public Guid? TransmissionId { get; set; }
        public Transmission? Transmission { get; set; }
    }
}

namespace TruckManagerSoftware.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.DataConstants.DataConstants.Trailer;

    public class Trailer
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(SeriesMaxLength)]
        public string Series { get; set; } = null!;

        [Required]
        [MaxLength(TrailerTypeMaxLength)]
        public string TrailerType { get; set; } = null!;

        [Required]
        [MaxLength(BodyTypeMaxLength)]
        public string BodyType { get; set; } = null!;

        [Required]
        public int TareWeight { get; set; }

        [Required]
        public int AxleCount { get; set; }

        [Required]
        public double TotalLength { get; set;}

        [Required]
        [MaxLength(CargoTypesMaxLength)]
        public string CargoTypes { get; set; } = null!;

        [MaxLength(ImageMaxLength)]
        public string? Image { get; set; }

        [ForeignKey(nameof(Garage))]
        public Guid? GarageId { get; set; }
        public Garage? Garage { get; set; }

        [ForeignKey(nameof(Truck))]
        public Guid? TruckId { get; set; }
        public Truck? Truck { get; set; }
    }
}

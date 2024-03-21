namespace TruckManagerSoftware.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants.DataConstants.Transmission;
    public class Transmission
    {
        public Transmission()
        {
            this.Trucks = new HashSet<Truck>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        public int GearsCount { get; set; }

        [Required]
        public bool Retarder { get; set; }

        public ICollection<Truck> Trucks { get; set; } = null!;
    }
}

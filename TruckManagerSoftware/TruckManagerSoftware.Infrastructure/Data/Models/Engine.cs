namespace TruckManagerSoftware.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants.DataConstants.Engine;

    public class Engine
    {
        public Engine()
        {
            this.Trucks = new HashSet<Truck>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        public int PowerHp { get; set; }

        [Required]
        public int PowerKw { get; set; }

        [Required]
        public int TorqueNm { get; set; }

        public ICollection<Truck> Trucks { get; set; } = null!;
    }
}

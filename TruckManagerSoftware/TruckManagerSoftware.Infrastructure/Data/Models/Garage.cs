namespace TruckManagerSoftware.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants.DataConstants.Garage;

    public class Garage
    {
        public Garage()
        {
            this.Trucks = new HashSet<Truck>();
            this.Trailers = new HashSet<Trailer>();
            this.Users = new HashSet<User>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(CountryMaxLength)]
        public string Country { get; set; } = null!;

        [Required]
        [MaxLength(CityMaxLength)]
        public string City { get; set; } = null!;

        [Required]
        [MaxLength(SizeMaxLength)]
        public string Size { get; set; } = null!;

        public ICollection<Truck> Trucks { get; set; } = null!;

        public ICollection<Trailer> Trailers { get; set; } = null!;

        public ICollection<User> Users { get; set; } = null!;
    }
}

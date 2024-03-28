namespace TruckManagerSoftware.Core.Models.Garage
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants.DataConstants.Garage;

    public class AddGarageViewModel
    {
        [Required]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        public string Country { get; set; } = null!;

        [Required]
        [StringLength(CityMaxLength, MinimumLength = CityMinLength)]
        public string City { get; set; } = null!;

        [Required]
        [StringLength(SizeMaxLength, MinimumLength = SizeMinLength)]
        public string Size { get; set; } = null!;
    }
}

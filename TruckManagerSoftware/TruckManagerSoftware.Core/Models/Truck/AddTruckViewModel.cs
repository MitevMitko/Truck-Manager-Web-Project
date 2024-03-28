namespace TruckManagerSoftware.Core.Models.Truck
{
    using Microsoft.AspNetCore.Http;

    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants.DataConstants.Truck;

    public class AddTruckViewModel
    {
        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string Brand { get; set; } = null!;

        [Required]
        [StringLength(SeriesMaxLength, MinimumLength = SeriesMinLength)]
        public string Series { get; set; } = null!;

        [Required]
        public double DrivenDistance { get; set; }

        public IFormFile? Image { get; set; }
    }
}

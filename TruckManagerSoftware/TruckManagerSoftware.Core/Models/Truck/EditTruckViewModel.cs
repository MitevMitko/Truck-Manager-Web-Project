namespace TruckManagerSoftware.Core.Models.Truck
{
    using Microsoft.AspNetCore.Http;

    using System.ComponentModel.DataAnnotations;

    using Engine;
    using Garage;
    using Transmission;

    using static Common.DataConstants.DataConstants.Truck;

    public class EditTruckViewModel
    {
        public EditTruckViewModel()
        {
            this.Garages = new HashSet<GarageInfoViewModel>();
            this.Engines = new HashSet<EngineInfoViewModel>();
            this.Transmissions = new HashSet<TransmissionInfoViewModel>();
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string Brand { get; set; } = null!;

        [Required]
        [StringLength(SeriesMaxLength, MinimumLength = SeriesMinLength)]
        public string Series { get; set; } = null!;

        [Required]
        public double DrivenDistance { get; set; }

        public IFormFile? Image { get; set; }

        [Required]
        public Guid? GarageId { get; set; }

        [Required]
        public Guid? EngineId { get; set; }

        [Required]
        public Guid? TransmissionId { get; set; }

        public ICollection<GarageInfoViewModel> Garages { get; set; } = null!;

        public ICollection<EngineInfoViewModel> Engines { get; set; } = null!;

        public ICollection<TransmissionInfoViewModel> Transmissions { get; set; } = null!;
    }
}

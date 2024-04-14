namespace TruckManagerSoftware.Core.Models.Trailer
{
    using Microsoft.AspNetCore.Http;

    using System.ComponentModel.DataAnnotations;

    using Garage;

    using static Common.DataConstants.DataConstants.Trailer;

    public class EditTrailerViewModel
    {
        public EditTrailerViewModel()
        {
            this.Garages = new HashSet<GarageInfoViewModel>();
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(SeriesMaxLength, MinimumLength = SeriesMinLength)]
        public string Series { get; set; } = null!;

        [Required]
        [StringLength(TrailerTypeMaxLength, MinimumLength = TrailerTypeMinLength)]
        public string TrailerType { get; set; } = null!;

        [Required]
        [StringLength(BodyTypeMaxLength, MinimumLength = BodyTypeMinLength)]
        public string BodyType { get; set; } = null!;

        [Required]
        public int TareWeight { get; set; }

        [Required]
        public int AxleCount { get; set; }

        [Required]
        public double TotalLength { get; set; }

        [Required]
        [StringLength(CargoTypesMaxLength, MinimumLength = CargoTypesMinLength)]
        public string CargoTypes { get; set; } = null!;

        public IFormFile? Image { get; set; }

        [Required]
        public Guid? GarageId { get; set; }

        public ICollection<GarageInfoViewModel> Garages { get; set; } = null!;
    }
}

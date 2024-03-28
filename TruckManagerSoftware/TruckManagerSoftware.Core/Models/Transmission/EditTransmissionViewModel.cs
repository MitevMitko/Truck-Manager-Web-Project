namespace TruckManagerSoftware.Core.Models.Transmission
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants.DataConstants.Transmission;

    public class EditTransmissionViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        public int GearsCount { get; set; }

        [Required]
        public bool Retarder { get; set; }
    }
}

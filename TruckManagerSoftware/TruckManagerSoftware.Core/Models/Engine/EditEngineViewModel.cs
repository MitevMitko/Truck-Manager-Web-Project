namespace TruckManagerSoftware.Core.Models.Engine
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants.DataConstants.Engine;

    public class EditEngineViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        public int PowerHp { get; set; }

        [Required]
        public int PowerKw { get; set; }

        [Required]
        public int TorqueNm { get; set; }
    }
}

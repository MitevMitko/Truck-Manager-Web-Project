namespace TruckManagerSoftware.Core.Models.User
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants.DataConstants.User;

    public class ChangeStatusViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(StatusMaxLength, MinimumLength = StatusMinLength)]
        public string Status { get; set; } = null!;

    }
}

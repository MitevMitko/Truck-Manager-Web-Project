namespace TruckManagerSoftware.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants.DataConstants.BankContact;

    public class BankContact
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;
    }
}

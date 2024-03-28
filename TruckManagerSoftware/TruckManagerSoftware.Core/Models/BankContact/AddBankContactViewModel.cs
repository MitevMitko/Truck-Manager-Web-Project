namespace TruckManagerSoftware.Core.Models.BankContact
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants.DataConstants.BankContact;

    public class AddBankContactViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]

        public string Email { get; set; } = null!;

        [Required]
        [Phone]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        public string PhoneNumber { get; set; } = null!;
    }
}

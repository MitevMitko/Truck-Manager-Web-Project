namespace TruckManagerSoftware.Core.Models.BankContact
{
    public class BankContactInfoViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
    }
}

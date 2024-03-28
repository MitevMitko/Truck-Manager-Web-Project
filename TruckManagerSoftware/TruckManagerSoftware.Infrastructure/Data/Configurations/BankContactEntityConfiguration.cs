namespace TruckManagerSoftware.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class BankContactEntityConfiguration : IEntityTypeConfiguration<BankContact>
    {
        public void Configure(EntityTypeBuilder<BankContact> builder)
        {
            builder.HasData(SeedBandContacts());
        }

        private List<BankContact> SeedBandContacts()
        {
            List<BankContact> bankContacts = new List<BankContact>();

            BankContact bankContact = new BankContact()
            {
                Id = Guid.Parse("f0b0dc9a-5826-4dfd-9aa4-cad5a902268b"),
                Name = "Unicredi Bulbank",
                Email = "unicredit@unicredit.com",
                PhoneNumber = "1234567890"
            };

            bankContacts.Add(bankContact);

            bankContact = new BankContact()
            {
                Id = Guid.Parse("bfd81dcf-f66a-4e88-a534-7b58ba4681b6"),
                Name = "DSK",
                Email = "dsk@dsk.com",
                PhoneNumber = "0123456789"
            };

            bankContacts.Add(bankContact);

            return bankContacts;
        }
    }
}

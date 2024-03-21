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
                Id = Guid.NewGuid(),
                Name = "Unicredi Bulbank",
                Email = "unicredit@unicredit.com",
                PhoneNumber = "1234567890"
            };

            bankContacts.Add(bankContact);

            bankContact = new BankContact()
            {
                Id = Guid.NewGuid(),
                Name = "DSK",
                Email = "dsk@dsk.com",
                PhoneNumber = "0123456789"
            };

            bankContacts.Add(bankContact);

            return bankContacts;
        }
    }
}

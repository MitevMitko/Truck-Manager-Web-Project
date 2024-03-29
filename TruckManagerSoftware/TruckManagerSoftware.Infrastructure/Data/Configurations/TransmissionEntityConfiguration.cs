namespace TruckManagerSoftware.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class TransmissionEntityConfiguration : IEntityTypeConfiguration<Transmission>
    {
        public void Configure(EntityTypeBuilder<Transmission> builder)
        {
            // builder.HasData(SeedTransmissions());
        }

        private List<Transmission> SeedTransmissions()
        {
            List<Transmission> transmissions = new List<Transmission>();

            Transmission transmission = new Transmission()
            {
                Id = Guid.Parse("e1663ec8-2b8c-4782-910c-435081921fac"),
                Title = "Opticruise GRS 905",
                GearsCount = 12,
                Retarder = false
            };

            transmissions.Add(transmission);

            transmission = new Transmission()
            {
                Id = Guid.Parse("b5f61d07-9576-491a-a337-809a31268a17"),
                Title = "Opticruise GRSO 925",
                GearsCount = 14,
                Retarder = false
            };

            transmissions.Add(transmission);

            transmission = new Transmission()
            {
                Id = Guid.Parse("e71a4b60-1ed1-4982-b900-70a76f0706a8"),
                Title = "Opticruise GRSO 925R",
                GearsCount = 14,
                Retarder = true
            };

            transmissions.Add(transmission);

            return transmissions;
        }
    }
}

namespace TruckManagerSoftware.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class GarageEntityConfiguration : IEntityTypeConfiguration<Garage>
    {
        public void Configure(EntityTypeBuilder<Garage> builder)
        {
            // builder.HasData(SeedGarages());
        }

        private List<Garage> SeedGarages()
        {
            List<Garage> garages = new List<Garage>();

            Garage garage = new Garage()
            {
                Id = Guid.Parse("e1945cc7-f084-4c9c-b0a1-0e7824d6bc9b"),
                Country = "Bulgaria",
                City = "Ruse",
                Size = "small"
            };

            garages.Add(garage);

            garage = new Garage()
            {
                Id = Guid.Parse("16d31ab1-2b09-44a0-ae5e-0c1526078157"),
                Country = "Italy",
                City = "Venice",
                Size = "medium"
            };

            garages.Add(garage);

            garage = new Garage()
            {
                Id = Guid.Parse("c17a0f07-e39c-4420-a338-3f7b15a15f59"),
                Country = "Germany",
                City = "Berlin",
                Size = "large"
            };

            garages.Add(garage);

            garage = new Garage()
            {
                Id = Guid.Parse("54779e9a-eb54-491a-b442-78dcff15462f"),
                Country = "Bulgaria",
                City = "Varna",
                Size = "large"
            };

            garages.Add(garage);

            garage = new Garage()
            {
                Id = Guid.Parse("58197c1b-2059-4382-a956-aecb0d834217"),
                Country = "Bulgaria",
                City = "Burgas",
                Size = "large"
            };

            garages.Add(garage);

            return garages;
        }
    }
}

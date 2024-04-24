namespace TruckManagerSoftware.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class TruckEntityConfiguration : IEntityTypeConfiguration<Truck>
    {
        public void Configure(EntityTypeBuilder<Truck> builder)
        {
            builder
                .HasOne(t => t.Garage)
                .WithMany(g => g.Trucks)
                .HasForeignKey(t => t.GarageId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(t => t.Trailer)
                .WithOne(t => t.Truck)
                .HasForeignKey<Truck>(t => t.TrailerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(t => t.Order)
                .WithOne(o => o.Truck)
                .HasForeignKey<Truck>(t => t.OrderId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(t => t.User)
                .WithOne(u => u.Truck)
                .HasForeignKey<Truck>(t => t.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(t => t.Engine)
                .WithMany(e => e.Trucks)
                .HasForeignKey(t => t.EngineId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(t => t.Transmission)
                .WithMany(t => t.Trucks)
                .HasForeignKey(t => t.TransmissionId)
                .OnDelete(DeleteBehavior.SetNull);

            // builder.HasData(SeedTrucks());
        }

        private List<Truck> SeedTrucks()
        {
            List<Truck> trucks = new List<Truck>();

            Truck truck = new Truck()
            {
                Id = Guid.Parse("7d380a02-1932-4c39-b2d6-cd58678fd442"),
                Brand = "Scania",
                Series = "R",
                DrivenDistance = 1500,
                Image = "984b0f36-cb0e-4d64-b517-cb25a1fd61cb",
                GarageId = Guid.Parse("e1945cc7-f084-4c9c-b0a1-0e7824d6bc9b"),
                TrailerId = null,
                OrderId = null,
                UserId = null,
                EngineId = Guid.Parse("7f02705e-b364-4a2d-8b7a-734458317e5d"),
                TransmissionId = Guid.Parse("e1663ec8-2b8c-4782-910c-435081921fac")
            };

            trucks.Add(truck);

            truck = new Truck()
            {
                Id = Guid.Parse("6a647b36-271f-4434-a152-2548f8a2ff0e"),
                Brand = "DAF",
                Series = "XD",
                DrivenDistance = 2000,
                Image = null,
                GarageId = Guid.Parse("16d31ab1-2b09-44a0-ae5e-0c1526078157"),
                TrailerId = null,
                OrderId = null,
                UserId = null,
                EngineId = Guid.Parse("682f1317-f1d8-46c4-b7ec-13af1ee27906"),
                TransmissionId = Guid.Parse("b5f61d07-9576-491a-a337-809a31268a17")
            };

            trucks.Add(truck);

            truck = new Truck()
            {
                Id = Guid.Parse("4ccf808a-2db5-4d36-82f0-e6ff4a1f8b4b"),
                Brand = "Renault",
                Series = "Premium",
                DrivenDistance = 2500,
                Image = null,
                GarageId = Guid.Parse("c17a0f07-e39c-4420-a338-3f7b15a15f59"),
                TrailerId = null,
                OrderId = null,
                UserId = null,
                EngineId = Guid.Parse("57677635-5723-437d-8a94-3d26f51cd0f8"),
                TransmissionId = Guid.Parse("e71a4b60-1ed1-4982-b900-70a76f0706a8")
            };

            trucks.Add(truck);

            return trucks;
        }
    }
}

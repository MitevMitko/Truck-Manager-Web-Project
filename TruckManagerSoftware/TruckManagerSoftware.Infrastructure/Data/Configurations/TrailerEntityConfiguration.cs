﻿namespace TruckManagerSoftware.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class TrailerEntityConfiguration : IEntityTypeConfiguration<Trailer>
    {
        public void Configure(EntityTypeBuilder<Trailer> builder)
        {
            builder
                .HasOne(t => t.Garage)
                .WithMany(g => g.Trailers)
                .HasForeignKey(t => t.GarageId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(t => t.Truck)
                .WithOne(t => t.Trailer)
                .HasForeignKey<Trailer>(t => t.TruckId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasData(SeedTrailers());
        }

        private List<Trailer> SeedTrailers()
        {
            List<Trailer> trailers = new List<Trailer>();

            Trailer trailer = new Trailer()
            {
                Id = Guid.Parse("3ce51feb-0d77-4f61-aeb0-c44a4b0540d3"),
                Title = "Wooden Floor Flatbed",
                Series = "FLB",
                TrailerType = "Single",
                BodyType = "Flatbed",
                TareWeight = 5300,
                AxleCount = 2,
                TotalLength = 13.70,
                CargoTypes = "Construction equipment and materials",
                Image = "fadd6754-0ce5-430a-a13d-bdaed1e201c4",
                GarageId = Guid.Parse("e1945cc7-f084-4c9c-b0a1-0e7824d6bc9b"),
                TruckId = Guid.Parse("7d380a02-1932-4c39-b2d6-cd58678fd442")
            };

            trailers.Add(trailer);

            trailer = new Trailer()
            {
                Id = Guid.Parse("3c7e1a88-4c69-46c2-915f-3763f97c7fe5"),
                Title = "Container Carrier",
                Series = "CNT",
                TrailerType = "Single",
                BodyType = "Container Carrier",
                TareWeight = 5100,
                AxleCount = 3,
                TotalLength = 12.40,
                CargoTypes = "Containers and container tanks",
                Image = "6dcf5eef-e16d-4fbb-98c0-7b4d145e7fa6",
                GarageId = Guid.Parse("16d31ab1-2b09-44a0-ae5e-0c1526078157"),
                TruckId = Guid.Parse("6a647b36-271f-4434-a152-2548f8a2ff0e")
            };

            trailers.Add(trailer);

            trailer = new Trailer()
            {
                Id = Guid.Parse("928604bb-8f63-4b15-8bb6-fda54428c3a8"),
                Title = "Curtainsider",
                Series = "STD",
                TrailerType = "single",
                BodyType = "Curtainsider",
                TareWeight = 5860,
                AxleCount = 2,
                TotalLength = 13.70,
                CargoTypes = "General, Dry goods",
                Image = "ada888de-4db3-4212-b50f-92fed8ca5874",
                GarageId = Guid.Parse("c17a0f07-e39c-4420-a338-3f7b15a15f59"),
                TruckId = Guid.Parse("4ccf808a-2db5-4d36-82f0-e6ff4a1f8b4b")
            };

            trailers.Add(trailer);

            trailer = new Trailer()
            {
                Id = Guid.Parse("35328ace-d3cb-4208-8e48-358eb5905ae1"),
                Title = "Steel Dumper",
                Series = "DMP",
                TrailerType = "Single",
                BodyType = "Dumper",
                TareWeight = 6650,
                AxleCount = 3,
                TotalLength = 9.10,
                CargoTypes = "Bulk cargo and materials",
                Image = "02403d2f-9448-4e4a-b84c-6ca9377ccb7d",
                GarageId = Guid.Parse("16d31ab1-2b09-44a0-ae5e-0c1526078157"),
                TruckId = null
            };

            trailers.Add(trailer);

            return trailers;
        }
    }
}
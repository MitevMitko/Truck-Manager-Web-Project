namespace TruckManagerSoftware.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasOne(o => o.Truck)
                .WithOne(t => t.Order)
                .HasForeignKey<Order>(o => o.TruckId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(o => o.User)
                .WithOne(u => u.Order)
                .HasForeignKey<Order>(o => o.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // builder.HasData(SeedOrders());
        }

        private List<Order> SeedOrders()
        {
            List<Order> orders = new List<Order>();

            Order order = new Order()
            {
                Id = Guid.Parse("5ccb7fe1-1c13-4dd0-8dec-12867917581c"),
                Cargo = "Beans",
                CargoWeight = 12,
                StartPoint = "Bratislava",
                EndPoint = "Grimsby",
                DeliveryType = "urgent",
                TripDistance = 1824,
                TripTime = "26h 12m",
                DeliveryPrice = 95493,
                TruckId = Guid.Parse("7d380a02-1932-4c39-b2d6-cd58678fd442"),
                UserId = null
            };

            orders.Add(order);

            order = new Order()
            {
                Id = Guid.Parse("3496d7fc-c71a-43e6-b1bf-cf4c4082feac"),
                Cargo = "Oil",
                CargoWeight = 23,
                StartPoint = "Vienna",
                EndPoint = "Mannheim",
                DeliveryType = "important",
                TripDistance = 1045,
                TripTime = "15h 18m",
                DeliveryPrice = 32273,
                TruckId = Guid.Parse("6a647b36-271f-4434-a152-2548f8a2ff0e"),
                UserId = null
            };

            orders.Add(order);

            order = new Order()
            {
                Id = Guid.Parse("432f8be9-b405-4cce-a090-df244c49beda"),
                Cargo = "Pesto",
                CargoWeight = 18,
                StartPoint = "Dresden",
                EndPoint = "Frankfurt",
                DeliveryType = "standard",
                TripDistance = 864,
                TripTime = "12h 40m",
                DeliveryPrice = 28663,
                TruckId = null,
                UserId = null
            };

            orders.Add(order);

            return orders;
        }
    }
}

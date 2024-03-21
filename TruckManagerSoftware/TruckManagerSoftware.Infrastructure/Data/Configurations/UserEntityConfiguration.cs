﻿namespace TruckManagerSoftware.Infrastructure.Data.Configurations
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(u => u.Garage)
                .WithMany(o => o.Users)
                .HasForeignKey(u => u.GarageId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(u => u.Truck)
                .WithOne(t => t.User)
                .HasForeignKey<User>(u => u.TruckId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(u => u.Order)
                .WithOne(o => o.User)
                .HasForeignKey<User>(u => u.OrderId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasData(SeedUsers());
        }

        private List<User> SeedUsers()
        {
            List<User> users = new List<User>();
            PasswordHasher<User> hasher = new PasswordHasher<User>();

            User user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = "administrator@mail.com",
                NormalizedUserName = "ADMINISTRATOR@MAIL.COM",
                Email = "administrator@mail.com",
                NormalizedEmail = "ADMINISTRATOR@MAIL.COM",
                Status = "roaming",
                Avatar = null,
                GarageId = null,
                TruckId = null,
                OrderId = null
            };

            user.PasswordHash = hasher.HashPassword(user, "administrator");

            users.Add(user);

            user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = "user@mail.com",
                NormalizedUserName = "USER@MAIL.COM",
                Email = "user@mail.com",
                NormalizedEmail = "USER@MAIL.COM",
                Status = "roaming",
                Avatar = null,
                GarageId = null,
                TruckId = null,
                OrderId = null
            };

            user.PasswordHash = hasher.HashPassword(user, "user");

            users.Add(user);

            return users;
        }
    }
}

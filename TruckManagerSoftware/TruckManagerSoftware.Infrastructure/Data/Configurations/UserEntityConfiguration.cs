namespace TruckManagerSoftware.Infrastructure.Data.Configurations
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.Extensions.DependencyInjection;
    using Models;

    using static Common.DataConstants.DataConstants.User;

    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(u => u.UserName)
                .HasMaxLength(UserNameMaxLength)
                .IsRequired();

            builder
                .Property(u => u.NormalizedUserName)
                .HasMaxLength(UserNameMaxLength)
                .IsRequired();

            builder
                .Property(u => u.Email)
                .HasMaxLength(EmailMaxLength)
                .IsRequired();

            builder
                .Property(u => u.NormalizedEmail)
                .HasMaxLength(EmailMaxLength)
                .IsRequired();

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

            // builder.HasData(SeedUsers());
        }

        private List<User> SeedUsers()
        {
            List<User> users = new List<User>();
            PasswordHasher<User> hasher = new PasswordHasher<User>();

            User user = new User()
            {
                Id = Guid.Parse("71fb597c-02f6-4faa-909d-e25e60e8e4e7"),
                UserName = "administrator",
                NormalizedUserName = "ADMINISTRATOR",
                Email = "administrator@mail.com",
                NormalizedEmail = "ADMINISTRATOR@MAIL.COM",
                Status = "roaming",
                Avatar = null,
                GarageId = null,
                TruckId = null,
                OrderId = null
            };

            user.PasswordHash = hasher.HashPassword(user, "administrator");
            user.SecurityStamp = hasher.HashPassword(user, "securitystamp1");

            users.Add(user);

            user = new User()
            {
                Id = Guid.Parse("119ca1f9-3f45-4391-a92e-408dce588da6"),
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@mail.com",
                NormalizedEmail = "USER@MAIL.COM",
                Status = "roaming",
                Avatar = null,
                GarageId = null,
                TruckId = null,
                OrderId = null
            };

            user.PasswordHash = hasher.HashPassword(user, "user");
            user.SecurityStamp = hasher.HashPassword(user, "securitystamp2");

            users.Add(user);

            return users;
        }
    }
}

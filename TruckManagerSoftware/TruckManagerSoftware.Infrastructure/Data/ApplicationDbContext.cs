namespace TruckManagerSoftware.Infrastructure.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using System.Reflection;

    using Models;

    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<BankContact> BankContacts { get; set; } = null!;

        public DbSet<Engine> Engines { get; set; } = null!;

        public DbSet<Garage> Garages { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<Trailer> Trailers { get; set; } = null!;

        public DbSet<Transmission> Transmissions { get; set; } = null!;

        public DbSet<Truck> Trucks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(ApplicationDbContext)) ?? Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(configAssembly);

            base.OnModelCreating(builder);
        }
    }
}

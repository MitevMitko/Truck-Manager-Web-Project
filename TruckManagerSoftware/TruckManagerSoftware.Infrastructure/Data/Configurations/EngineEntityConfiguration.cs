namespace TruckManagerSoftware.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class EngineEntityConfiguration : IEntityTypeConfiguration<Engine>
    {
        public void Configure(EntityTypeBuilder<Engine> builder)
        {
            // builder.HasData(SeedEngines());
        }

        private List<Engine> SeedEngines()
        {
            List<Engine> engines = new List<Engine>();

            Engine engine = new Engine()
            {
                Id = Guid.Parse("7f02705e-b364-4a2d-8b7a-734458317e5d"),
                Title = "DC13 114 360 Euro 5",
                PowerHp = 360,
                PowerKw = 265,
                TorqueNm = 1850
            };

            engines.Add(engine);

            engine = new Engine()
            {
                Id = Guid.Parse("682f1317-f1d8-46c4-b7ec-13af1ee27906"),
                Title = "DC16 18 560 Euro 5 V8",
                PowerHp = 560,
                PowerKw = 412,
                TorqueNm = 2700
            };

            engines.Add(engine);

            engine = new Engine()
            {
                Id = Guid.Parse("57677635-5723-437d-8a94-3d26f51cd0f8"),
                Title = "DC16 102 580 Euro 6 V8",
                PowerHp = 580,
                PowerKw = 427,
                TorqueNm = 2950
            };

            engines.Add(engine);

            return engines;
        }
    }
}

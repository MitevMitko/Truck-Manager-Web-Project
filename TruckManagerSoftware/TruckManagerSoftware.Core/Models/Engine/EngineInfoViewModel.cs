namespace TruckManagerSoftware.Core.Models.Engine
{
    public class EngineInfoViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public int PowerHp { get; set; }

        public int PowerKw { get; set; }

        public int TorqueNm { get; set; }
    }
}

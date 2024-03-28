﻿namespace TruckManagerSoftware.Core.Models.Transmission
{
    public class TransmissionInfoViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public int GearsCount { get; set; }

        public bool Retarder { get; set; }
    }
}

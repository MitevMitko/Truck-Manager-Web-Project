namespace TruckManagerSoftware.Infrastructure.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.DataConstants.DataConstants.User;

    public class User : IdentityUser<Guid>
    {
        [Required]
        [MaxLength(StatusMaxLength)]
        public string Status { get; set; } = null!;

        [MaxLength(AvatarMaxLength)]
        public string? Avatar { get; set; }

        [ForeignKey(nameof(Garage))]
        public Guid? GarageId { get; set; }
        public Garage? Garage { get; set; }

        [ForeignKey(nameof(Truck))]
        public Guid? TruckId { get; set; }
        public Truck? Truck { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
    }
}

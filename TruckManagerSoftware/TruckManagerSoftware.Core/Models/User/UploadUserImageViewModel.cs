namespace TruckManagerSoftware.Core.Models.User
{
    using Microsoft.AspNetCore.Http;

    using System.ComponentModel.DataAnnotations;

    public class UploadUserImageViewModel
    {
        [Required]
        public Guid Id { get; set; }

        public IFormFile? Image { get; set; }
    }
}

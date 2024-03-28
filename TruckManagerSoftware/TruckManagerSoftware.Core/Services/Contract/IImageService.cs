namespace TruckManagerSoftware.Core.Services.Contract
{
    using Microsoft.AspNetCore.Http;
    using SixLabors.ImageSharp;

    public interface IImageService
    {
        Image ConvertIFormFileToImage(IFormFile imageFile);

        Image ResizeImage(Image imageFile, int width, int height);

        void RemoveImage(string imagePath);

        bool CheckIfImageShouldBeResized(Image imageFile, int width, int height);

        string SaveImage(Image imageFile, string destinationPath);
    }
}

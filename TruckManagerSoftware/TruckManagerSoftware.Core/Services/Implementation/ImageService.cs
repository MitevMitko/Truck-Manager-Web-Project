namespace TruckManagerSoftware.Core.Services.Implementation
{
    using Microsoft.AspNetCore.Http;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Jpeg;
    using SixLabors.ImageSharp.Processing;

    using System.IO;

    using Contract;

    public class ImageService : IImageService
    {
        public bool CheckIfImageShouldBeResized(Image imageFile, int width, int height)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                // Check if the image is in the desired sizes
                return imageFile.Width > width || imageFile.Height > height;
            }
        }

        public Image ConvertIFormFileToImage(IFormFile imageFile)
        {
            using (Stream stream = imageFile.OpenReadStream())
            {
                return Image.Load(stream);
            }
        }

        public void RemoveImage(string imagePath)
        {
            // Delete the image
            // From the image path
            File.Delete(imagePath);
        }

        public Image ResizeImage(Image imageFile, int width, int height)
        {
            // Resize the image
            imageFile.Mutate(i => i.Resize(width, height));

            // Return the resized image
            return imageFile;
        }

        public string SaveImage(Image imageFile, string destinationPath)
        {
            // Create new title for the image
            string imageTitle = Guid.NewGuid().ToString();

            // Create the destination path with the new image title
            string imagePath = Path.Combine(destinationPath, $"{imageTitle}.jpg");

            //Save the image to the desired path
            imageFile.Save(imagePath, new JpegEncoder());

            return imageTitle;
        }
    }
}

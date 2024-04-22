namespace TruckManagerSoftware.Tests.ServicesTests
{
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;

    using Core.Services.Implementation;


    public class ImageServiceTests
    {
        private readonly ImageService imageService;

        public ImageServiceTests()
        {
            this.imageService = new ImageService();
        }

        // CheckIfImageShouldBeResized
        [Test]
        public void CheckIfImageShouldBeResized_ShouldReturnTrue_WhenImageWidthIsBiggerThanNeededWidth()
        {
            // Arrange
            Image<Rgba32> image = new(1200, 600);

            int width = 800;

            int height = 600;

            // Act
            bool result = imageService.CheckIfImageShouldBeResized(image, width, height);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void CheckIfImageShouldBeResized_ShouldReturnTrue_WhenImageHeightIsBiggerThanNeededHeight()
        {
            // Arrange
            Image<Rgba32> image = new(1200, 600);

            int width = 1200;

            int height = 400;

            // Act
            bool result = imageService.CheckIfImageShouldBeResized(image, width, height);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void CheckIfImageShouldBeResized_ShouldReturnTrue_WhenImageWidthAndHeightIsBiggerThanNeededWidthAndHeight()
        {
            // Arrange
            Image<Rgba32> image = new(1400, 800);

            int width = 1200;

            int height = 600;

            // Act
            bool result = imageService.CheckIfImageShouldBeResized(image, width, height);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void CheckIfImageShouldBeResized_ShouldReturnFalse_WhenImageWidthAndHeightIsSmallerThanNeededWidthAndHeight()
        {
            // Arrange
            Image<Rgba32> image = new(800, 400);

            int width = 1200;

            int height = 600;

            // Act
            bool result = imageService.CheckIfImageShouldBeResized(image, width, height);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void CheckIfImageShouldBeResized_ShouldReturnFalse_WhenImageWidthAndHeightIsEqualToNeededWidthAndHeight()
        {
            // Arrange
            Image<Rgba32> image = new(1200, 600);

            int width = 1200;

            int height = 600;

            // Act
            bool result = imageService.CheckIfImageShouldBeResized(image, width, height);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void CheckIfImageShouldBeResized_ShouldReturnFalse_WhenImageWidthIsSmallerThanNeededWidth_AndImageHeightIsEqualToNeededHeight()
        {
            // Arrange
            Image<Rgba32> image = new(1000, 600);

            int width = 1200;

            int height = 600;

            // Act
            bool result = imageService.CheckIfImageShouldBeResized(image, width, height);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void CheckIfImageShouldBeResized_ShouldReturnFalse_WhenImageWidthIsEqualToNeededWidth_AndImageHeightIsSmallerThanNeededHeight()
        {
            // Arrange
            Image<Rgba32> image = new(1200, 400);

            int width = 1200;

            int height = 600;

            // Act
            bool result = imageService.CheckIfImageShouldBeResized(image, width, height);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        // ResizeImage
        [Test]
        public void ResizeImage_ShouldReturnImageWithWidthEqualToNeededWidth_AndImageHeightEqualToNeededHeight()
        {
            // Arrange
            Image<Rgba32> image = new(1200, 600);

            int width = 800;

            int height = 600;

            // Act
            Image resizedImage = imageService.ResizeImage(image, width, height);

            // Assert
            Assert.IsInstanceOf<Image>(resizedImage);
            Assert.That(resizedImage.Width, Is.EqualTo(width));
            Assert.That(resizedImage.Height, Is.EqualTo(height));
        }

    }
}

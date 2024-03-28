namespace TruckManagerSoftware.Core.Services.Implementation
{
    using SixLabors.ImageSharp;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Contract;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;
    using Models.Trailer;

    using static Common.DataConstants.DataConstants.Image;
    using static Common.Messages.Messages.Trailer;
    using static Common.Messages.Messages.Image;

    public class TrailerService : ITrailerService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IImageService imageService;

        public TrailerService(IUnitOfWork unitOfWork, IImageService imageService)
        {
            this.unitOfWork = unitOfWork;
            this.imageService = imageService;
        }

        public async Task AddTrailer(AddTrailerViewModel model)
        {
            // Create new trailer
            Trailer trailer = new Trailer()
            {
                Title = model.Title,
                Series = model.Series,
                TrailerType = model.TrailerType,
                BodyType = model.BodyType,
                TareWeight = model.TareWeight,
                AxleCount = model.AxleCount,
                TotalLength = model.TotalLength,
                CargoTypes = model.CargoTypes,
                Image = null
            };

            // Check if there is uploaded image file
            if (model.Image != null)
            {
                // Check if the trailer's images path exists
                // If the path does not exist
                // Throw argument exception
                if (Directory.Exists(TrailersImagesPath))
                {
                    throw new ArgumentException(FolderNotExistMessage);
                }

                // Convert the uploaded file
                // From IFormFile to Image
                Image convertedUploadedFile = imageService.ConvertIFormFileToImage(model.Image);

                // Check if the uploaded
                // File should be resized
                if (imageService.CheckIfImageShouldBeResized(convertedUploadedFile, ImageWidth, ImageHeight))
                {
                    // Returns the resized uploaded file
                    Image resizedImage = imageService.ResizeImage(convertedUploadedFile, ImageWidth, ImageHeight);

                    // Saves the resized uploaded file
                    // And returns the new title
                    // Of the uploaded file
                    string imageTitle = imageService.SaveImage(resizedImage, TrailersImagesPath);

                    trailer.Image = imageTitle;
                }
                else
                {
                    // Saves the uploaded file
                    // And returns the new title
                    // Of the uploaded file
                    string imageTitle = imageService.SaveImage(convertedUploadedFile, TrailersImagesPath);

                    trailer.Image = imageTitle;
                }
            }

            // Add the created trailer to the database
            await unitOfWork.Trailer.Add(trailer);

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task EditTrailer(EditTrailerViewModel model)
        {
            // Get the trailer by id
            // If trailer does not exist
            // Throw argument exception
            Trailer trailer = await unitOfWork.Trailer.GetById(model.Id) ?? throw new ArgumentException(TrailerNotExistMessage);

            // Assign the edited data
            // To the trailer
            trailer.Title = model.Title;
            trailer.Series = model.Series;
            trailer.TrailerType = model.TrailerType;
            trailer.BodyType = model.BodyType;
            trailer.TareWeight = model.TareWeight;
            trailer.AxleCount = model.AxleCount;
            trailer.TotalLength = model.TotalLength;
            trailer.CargoTypes = model.CargoTypes;

            // Check if there is uploaded image file
            if (model.Image != null)
            {
                // Check if the trailer's images path exists
                // If the path does not exist
                // Throw argument exception
                if (Directory.Exists(TrailersImagesPath))
                {
                    throw new ArgumentException(FolderNotExistMessage);
                }

                if (trailer.Image != null)
                {
                    // Create the destination path with the new image title
                    string imagePath = Path.Combine(TrailersImagesPath, trailer.Image);

                    // Check if the image
                    // From the image path exists
                    if (!File.Exists(imagePath))
                    {
                        throw new ArgumentException(ImageNotExistMessage);
                    }

                    imageService.RemoveImage(imagePath);
                }

                // Convert the uploaded file
                // From IFormFile to Image
                Image convertedUploadedFile = imageService.ConvertIFormFileToImage(model.Image);

                // Check if the uploaded
                // File should be resized
                if (imageService.CheckIfImageShouldBeResized(convertedUploadedFile, ImageWidth, ImageHeight))
                {
                    // Returns the resized uploaded file
                    Image resizedImage = imageService.ResizeImage(convertedUploadedFile, ImageWidth, ImageHeight);

                    // Saves the resized uploaded file
                    // And returns the new title
                    // Of the uploaded file
                    string imageTitle = imageService.SaveImage(resizedImage, TrailersImagesPath);

                    trailer.Image = imageTitle;
                }
                else
                {
                    // Saves the uploaded file
                    // And returns the new title
                    // Of the uploaded file
                    string imageTitle = imageService.SaveImage(convertedUploadedFile, TrailersImagesPath);

                    trailer.Image = imageTitle;
                }
            }

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task<ICollection<TrailerInfoViewModel>> GetAllTrailersInfo()
        {
            // Create collection of trailer view model
            ICollection<TrailerInfoViewModel> model = new List<TrailerInfoViewModel>();

            // Get all trailers
            // From the database
            IEnumerable<Trailer> trailers = await unitOfWork.Trailer.GetAll();

            // Assign the data from the trailers
            // From the database to the trailer view model
            // And then add the trailer view model
            // To the collection of trailer view model
            foreach (Trailer trailer in trailers)
            {
                TrailerInfoViewModel trailerInfo = new TrailerInfoViewModel()
                {
                    Id = trailer.Id,
                    Title = trailer.Title,
                    Series = trailer.Series,
                    TrailerType = trailer.TrailerType,
                    BodyType = trailer.BodyType,
                    TareWeight = trailer.TareWeight,
                    AxleCount = trailer.AxleCount,
                    TotalLength = trailer.TotalLength,
                    CargoTypes = trailer.CargoTypes,
                    Image = trailer.Image
                };

                model.Add(trailerInfo);
            }

            return model;
        }

        public async Task<TrailerInfoViewModel> GetTrailerInfoById(Guid id)
        {
            // Get the trailer by id
            // If trailer does not exist
            // Throw argument exception
            Trailer trailer = await unitOfWork.Trailer.GetById(id) ?? throw new ArgumentException(TrailerNotExistMessage);

            // Create trailer view model
            // Assign the data from the trailer
            // And return the trailer view model
            return new TrailerInfoViewModel
            {
                Id = trailer.Id,
                Title = trailer.Title,
                Series = trailer.Series,
                TrailerType = trailer.TrailerType,
                BodyType = trailer.BodyType,
                TareWeight = trailer.TareWeight,
                AxleCount = trailer.AxleCount,
                TotalLength = trailer.TotalLength,
                CargoTypes = trailer.CargoTypes,
                Image = trailer.Image
            };
        }
    }
}

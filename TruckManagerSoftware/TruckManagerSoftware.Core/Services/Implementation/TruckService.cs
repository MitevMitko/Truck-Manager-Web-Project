namespace TruckManagerSoftware.Core.Services.Implementation
{
    using SixLabors.ImageSharp;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Contract;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;
    using Models.Truck;

    using static Common.DataConstants.DataConstants.Image;
    using static Common.Messages.Messages.Truck;
    using static Common.Messages.Messages.Image;

    public class TruckService : ITruckService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IImageService imageService;

        public TruckService(IUnitOfWork unitOfWork, IImageService imageService)
        {
            this.unitOfWork = unitOfWork;
            this.imageService = imageService;
        }

        public async Task AddTruck(AddTruckViewModel model)
        {
            // Create new truck
            Truck truck = new Truck()
            {
                Brand = model.Brand,
                Series = model.Series,
                DrivenDistance = model.DrivenDistance,
                Image = null
            };

            // Check if there is uploaded image file
            if (model.Image != null)
            {
                // Check if the truck's images path exists
                // If the path does not exist
                // Throw argument exception
                if (Directory.Exists(TrucksImagesPath))
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

                    truck.Image = imageTitle;
                }
                else
                {
                    // Saves the uploaded file
                    // And returns the new title
                    // Of the uploaded file
                    string imageTitle = imageService.SaveImage(convertedUploadedFile, TrailersImagesPath);

                    truck.Image = imageTitle;
                }
            }

            // Add the created truck to the database
            await unitOfWork.Truck.Add(truck);

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task EditTruck(EditTruckViewModel model)
        {
            // Get the truck by id
            // If truck does not exist
            // Throw argument exception
            Truck truck = await unitOfWork.Truck.GetById(model.Id) ?? throw new ArgumentException(TruckNotExistMessage);

            // Assign the edited data
            // To the truck
            truck.Brand = model.Brand;
            truck.Series = model.Series;
            truck.DrivenDistance = model.DrivenDistance;

            // Check if there is uploaded image file
            if (model.Image != null)
            {
                // Check if the truck's images path exists
                // If the path does not exist
                // Throw argument exception
                if (Directory.Exists(TrucksImagesPath))
                {
                    throw new ArgumentException(FolderNotExistMessage);
                }

                if (truck.Image != null)
                {
                    // Create the destination path with the new image title
                    string imagePath = Path.Combine(TrailersImagesPath, truck.Image);

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

                    truck.Image = imageTitle;
                }
                else
                {
                    // Saves the uploaded file
                    // And returns the new title
                    // Of the uploaded file
                    string imageTitle = imageService.SaveImage(convertedUploadedFile, TrailersImagesPath);

                    truck.Image = imageTitle;
                }

                // Save changes to the database
                await unitOfWork.CompleteAsync();
            }
        }

        public async Task<ICollection<TruckInfoViewModel>> GetAllTrucksInfo()
        {
            // Create collection of truck view model
            ICollection<TruckInfoViewModel> model = new List<TruckInfoViewModel>();

            // Get all trucks
            // From the database
            IEnumerable<Truck> trucks = await unitOfWork.Truck.GetAll();

            // Assign the data from the trucks
            // From the database to the truck view model
            // And then add the truck view model
            // To the collection of truck view model
            foreach (Truck truck in trucks)
            {
                TruckInfoViewModel truckInfo = new TruckInfoViewModel()
                {
                    Id = truck.Id,
                    Brand = truck.Brand,
                    Series = truck.Series,
                    DrivenDistance = truck.DrivenDistance,
                    Image = truck.Image
                };

                model.Add(truckInfo);
            }

            return model;
        }

        public async Task<TruckInfoViewModel> GetTruckInfoById(Guid id)
        {
            // Get the truck by id
            // If truck does not exist
            // Throw argument exception
            Truck truck = await unitOfWork.Truck.GetById(id) ?? throw new ArgumentException(TruckNotExistMessage);

            // Create truck view model
            // Assign the data from the truck
            // And return the truck view model
            return new TruckInfoViewModel
            {
                Id = truck.Id,
                Brand = truck.Brand,
                Series = truck.Series,
                DrivenDistance = truck.DrivenDistance,
                Image = truck.Image
            };
        }
    }
}

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
    using Microsoft.AspNetCore.Hosting;

    public class TruckService : ITruckService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IImageService imageService;

        private readonly IWebHostEnvironment webHostEnvironment;

        public TruckService(IUnitOfWork unitOfWork, IImageService imageService, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.imageService = imageService;
            this.webHostEnvironment = webHostEnvironment;
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
            if (model.Image != null && model.Image.Length != 0)
            {
                // Get the physical path
                // To the root directory
                // Of the web application
                string rootPath = webHostEnvironment.ContentRootPath;

                // Combine the root path
                // With the truck's images folder
                string trucksImagesPath = Path.Combine(rootPath, TrucksImagesPath);

                // Check if the truck's images path exists
                // If the path does not exist
                // Throw argument exception
                if (!Directory.Exists(trucksImagesPath))
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
                    string imageTitle = imageService.SaveImage(resizedImage, trucksImagesPath);

                    truck.Image = imageTitle;
                }
                else
                {
                    // Saves the uploaded file
                    // And returns the new title
                    // Of the uploaded file
                    string imageTitle = imageService.SaveImage(convertedUploadedFile, trucksImagesPath);

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
            // From the database
            Truck truck = await unitOfWork.Truck.GetById(model.Id);

            // If truck does not exist
            // Throw argument exception
            if (truck == null)
            {
                throw new ArgumentException(TruckNotExistMessage);
            }

            // Assign the edited data
            // To the truck
            truck.Brand = model.Brand;
            truck.Series = model.Series;
            truck.DrivenDistance = model.DrivenDistance;

            // Check if there is uploaded image file
            if (model.Image != null && model.Image.Length != 0)
            {
                // Get the physical path
                // To the root directory
                // Of the web application
                string rootPath = webHostEnvironment.ContentRootPath;

                // Combine the root path
                // With the truck's images folder
                string trucksImagesPath = Path.Combine(rootPath, TrucksImagesPath);

                // Check if the truck's images path exists
                // If the path does not exist
                // Throw argument exception
                if (!Directory.Exists(trucksImagesPath))
                {
                    throw new ArgumentException(FolderNotExistMessage);
                }

                if (truck.Image != null && truck.Image.Length != 0)
                {
                    // Create the destination path with the new image title
                    string imagePath = Path.Combine(trucksImagesPath, truck.Image);

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
                    string imageTitle = imageService.SaveImage(resizedImage, trucksImagesPath);

                    truck.Image = imageTitle;
                }
                else
                {
                    // Saves the uploaded file
                    // And returns the new title
                    // Of the uploaded file
                    string imageTitle = imageService.SaveImage(convertedUploadedFile, trucksImagesPath);

                    truck.Image = imageTitle;
                }
            }

            // Save changes to the database
            await unitOfWork.CompleteAsync();
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
            // From the database
            Truck truck = await unitOfWork.Truck.GetById(id);

            // If truck does not exist
            // Throw argument exception
            if (truck == null)
            {
                throw new ArgumentException(TruckNotExistMessage);
            }

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

        public async Task RemoveTruck(Guid id)
        {
            // Get the truck by id
            // From the database
            Truck truck = await unitOfWork.Truck.GetById(id);

            // If truck does not exist
            // Throw argument exception
            if (truck == null)
            {
                throw new ArgumentException(TruckNotExistMessage);
            }

            if (truck.Image != null && truck.Image.Length != 0)
            {
                // Get the physical path
                // To the root directory
                // Of the web application
                string rootPath = webHostEnvironment.ContentRootPath;

                // Combine the root path
                // With the truck's images folder
                string trucksImagesPath = Path.Combine(rootPath, TrucksImagesPath);

                // Create the destination path with the truck's image title
                string imagePath = Path.Combine(trucksImagesPath, $"{truck.Image}.jpg");

                // Check if the image
                // From the image path exists
                if (!File.Exists(imagePath))
                {
                    throw new ArgumentException(ImageNotExistMessage);
                }

                imageService.RemoveImage(imagePath);
            }

            // Remove the truck
            // From the database
            unitOfWork.Truck.Remove(truck);

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }
    }
}

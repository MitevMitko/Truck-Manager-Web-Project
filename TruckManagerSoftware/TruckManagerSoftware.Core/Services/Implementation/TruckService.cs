namespace TruckManagerSoftware.Core.Services.Implementation
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Hosting;
    using SixLabors.ImageSharp;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Contract;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;
    using Models.Engine;
    using Models.Order;
    using Models.Garage;
    using Models.Trailer;
    using Models.Transmission;
    using Models.Truck;
    using Models.User;

    using static Common.DataConstants.DataConstants.Image;
    using static Common.Messages.Messages.Engine;
    using static Common.Messages.Messages.Image;
    using static Common.Messages.Messages.Garage;
    using static Common.Messages.Messages.Transmission;
    using static Common.Messages.Messages.Truck;

    public class TruckService : ITruckService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IImageService imageService;

        private readonly IWebHostEnvironment webHostEnvironment;

        private readonly UserManager<User> userManager;

        public TruckService(IUnitOfWork unitOfWork,
            IImageService imageService,
            IWebHostEnvironment webHostEnvironment,
            UserManager<User> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.imageService = imageService;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
        }

        public async Task AddTruck(AddTruckViewModel model)
        {
            // Check if the garage
            // With property Id == model.GarageId exists
            // If the garage does not exist
            // Throw argument exception
            if (!await unitOfWork.Garage.AnyAsync(g => g.Id == model.GarageId))
            {
                throw new ArgumentException(GarageNotExistMessage);
            }

            // Check if the engine
            // With property Id == model.EngineId exists
            // If the engine does not exist
            // Throw argument exception
            if (!await unitOfWork.Engine.AnyAsync(e => e.Id == model.EngineId))
            {
                throw new ArgumentException(EngineNotExistMessage);
            }

            // Check if the transmission
            // With property Id == model.TransmissionId exists
            // If the transmission does not exist
            // Throw argument exception
            if (!await unitOfWork.Transmission.AnyAsync(t => t.Id == model.TransmissionId))
            {
                throw new ArgumentException(TransmissionNotExistMessage);
            }

            // Create new truck
            Truck truck = new Truck()
            {
                Brand = model.Brand,
                Series = model.Series,
                DrivenDistance = model.DrivenDistance,
                Image = null,
                GarageId = model.GarageId,
                EngineId = model.EngineId,
                TransmissionId = model.TransmissionId
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

            // Check if the garage
            // With property Id == model.GarageId exists
            // If the garage does not exist
            // Throw argument exception
            if (!await unitOfWork.Garage.AnyAsync(g => g.Id == model.GarageId))
            {
                throw new ArgumentException(GarageNotExistMessage);
            }

            // Check if the engine
            // With property Id == model.EngineId exists
            // If the engine does not exist
            // Throw argument exception
            if (!await unitOfWork.Engine.AnyAsync(e => e.Id == model.EngineId))
            {
                throw new ArgumentException(EngineNotExistMessage);
            }

            // Check if the transmission
            // With property Id == model.TransmissionId exists
            // If the transmission does not exist
            // Throw argument exception
            if (!await unitOfWork.Transmission.AnyAsync(t => t.Id == model.TransmissionId))
            {
                throw new ArgumentException(TransmissionNotExistMessage);
            }

            // Assign the edited data
            // To the truck
            truck.Brand = model.Brand;
            truck.Series = model.Series;
            truck.DrivenDistance = model.DrivenDistance;
            truck.GarageId = model.GarageId;
            truck.EngineId = model.EngineId;
            truck.TransmissionId = model.TransmissionId;

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
                    string imagePath = Path.Combine(trucksImagesPath, $"{truck.Image}.jpg");

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

        public async Task<TruckAdditionalInfoViewModel> GetAdditionalTruckInfoById(Guid id)
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

            // Create TruckAdditionalInfoViewModel
            // Assign the data from the truck
            TruckAdditionalInfoViewModel truckAdditionalInfo = new TruckAdditionalInfoViewModel()
            {
                Id = truck.Id,
                Brand = truck.Brand,
                Series = truck.Series,
                DrivenDistance = truck.DrivenDistance,
                Image = truck.Image
            };

            // If the truck's property called GarageId has value
            // Get the truck's garage from the database
            // If the truck's property GarageId does not have value
            // Return null
            if (truck.GarageId.HasValue)
            {
                // Get the truck's garage
                // With id == truck.GarageId
                // From the database
                Garage garage = await unitOfWork.Garage.GetById(truck.GarageId.Value);

                // Assign the data
                // From garage
                // To the GarageInfoViewModel
                truckAdditionalInfo.GarageInfo = new GarageInfoViewModel()
                {
                    Id = garage.Id,
                    Country = garage.Country,
                    City = garage.City,
                    Size = garage.Size
                };
            }
            else
            {
                truckAdditionalInfo.GarageInfo = null;
            }

            // If the truck's property called TrailerId has value
            // Get the truck's trailer from the database
            // If the truck's property TrailerId does not have value
            // Return null
            if (truck.TrailerId.HasValue)
            {
                // Get the truck's trailer
                // With id == truck.TrailerId
                // From the database
                Trailer trailer = await unitOfWork.Trailer.GetById(truck.TrailerId.Value);

                // Assign the data
                // From trailer
                // To the TrailerInfoViewModel
                truckAdditionalInfo.TrailerInfo = new TrailerInfoViewModel()
                {
                    Title = trailer.Title,
                    Series = trailer.Series
                };
            }
            else
            {
                truckAdditionalInfo.TrailerInfo = null;
            }

            // If the truck's property called OrderId has value
            // Get the truck's order from the database
            // If the truck's property OrderId does not have value
            // Return null
            if (truck.OrderId.HasValue)
            {
                // Get the truck's order
                // With id == truck.OrderId
                // From the database
                Order order = await unitOfWork.Order.GetById(truck.OrderId.Value);

                // Assign the data
                // From order
                // To the OrderInfoViewModel
                truckAdditionalInfo.OrderInfo = new OrderInfoViewModel()
                {
                    Cargo = order.Cargo
                };
            }
            else
            {
                truckAdditionalInfo.OrderInfo = null;
            }

            // If the truck's property called EngineId has value
            // Get the truck's engine from the database
            // If the truck's property EngineId does not have value
            // Return null
            if (truck.EngineId.HasValue)
            {
                // Get the truck's engine
                // With id == truck.EngineId
                // From the database
                Engine engine = await unitOfWork.Engine.GetById(truck.EngineId.Value);


                // Assign the data
                // From engine
                // To the EngineInfoViewModel
                truckAdditionalInfo.EngineInfo = new EngineInfoViewModel()
                {
                    Title = engine.Title
                };
            }
            else
            {
                truckAdditionalInfo.EngineInfo = null;
            }

            // If the truck's property called TransmissionId has value
            // Get the truck's transmission from the database
            // If the truck's property TransmissionId does not have value
            // Return null
            if (truck.TransmissionId.HasValue)
            {
                // Get the truck's transmission
                // With id == truck.TransmissionId
                // From the database
                Transmission transmission = await unitOfWork.Transmission.GetById(truck.TransmissionId.Value);

                // Assign the data
                // From transmission
                // To the TransmissionInfoViewModel
                truckAdditionalInfo.TransmissionInfo = new TransmissionInfoViewModel()
                {
                    Title = transmission.Title
                };
            }
            else
            {
                truckAdditionalInfo.TransmissionInfo = null;
            }

            // If the truck's property called UserId has value
            // Get the truck's user from the database
            // If the truck's property UserId does not have value
            // Return null
            if (truck.UserId.HasValue)
            {
                // Get the truck's user
                // With id == truck.UserId
                // From the database
                User user = await userManager.FindByIdAsync(truck.UserId.ToString());

                // Assign the data
                // From user
                // To the UserInfoViewModel
                truckAdditionalInfo.UserInfo = new UserInfoViewModel()
                {
                    UserName = user.UserName
                };
            }
            else
            {
                truck.User = null;
            }

            return truckAdditionalInfo;
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
                Image = truck.Image,
                GarageId = truck.GarageId,
                EngineId = truck.EngineId,
                TransmissionId = truck.TransmissionId
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

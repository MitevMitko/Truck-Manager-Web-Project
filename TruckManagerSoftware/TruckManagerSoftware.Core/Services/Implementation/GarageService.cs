namespace TruckManagerSoftware.Core.Services.Implementation
{
    using Microsoft.AspNetCore.Identity;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Contract;
    using Enums;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;
    using Models.Garage;

    using static Common.Messages.Messages.Garage;
    using static Common.Messages.Messages.Trailer;
    using static Common.Messages.Messages.Truck;
    using static Common.Messages.Messages.User;

    public class GarageService : IGarageService
    {
        private readonly UserManager<User> userManager;

        private readonly IUnitOfWork unitOfWork;

        public GarageService(UserManager<User> userManager,
            IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        public async Task AddGarage(AddGarageViewModel model)
        {
            // Create new garage
            Garage garage = new Garage()
            {
                Country = model.Country,
                City = model.City,
                Size = model.Size.ToString()
            };

            // Add the created garage to the database
            await unitOfWork.Garage.Add(garage);

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task EditGarage(EditGarageViewModel model)
        {
            // Get the garage by id
            // From the database
            Garage garage = await unitOfWork.Garage.GetById(model.Id);

            // If garage does not exist
            // Throw argument exception
            if (garage == null)
            {
                throw new ArgumentException(GarageNotExistMessage);
            }

            // Assign the edited data
            // To the garage
            garage.Country = model.Country;
            garage.City = model.City;
            garage.Size = model.Size;

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task<ICollection<GarageInfoViewModel>> GetAllGaragesInfo()
        {
            // Create collection of garage view model
            ICollection<GarageInfoViewModel> model = new List<GarageInfoViewModel>();

            // Get all garages
            // From the database
            IEnumerable<Garage> garages = await unitOfWork.Garage.GetAll();

            // Assign the data from the garages
            // From the database to the garage view model
            // And then add the garage view model
            // To the collection of garage view model
            foreach (Garage garage in garages)
            {
                GarageInfoViewModel garageInfo = new GarageInfoViewModel()
                {
                    Id = garage.Id,
                    Country = garage.Country,
                    City = garage.City,
                    Size = garage.Size
                };

                model.Add(garageInfo);
            }

            return model;
        }

        public async Task<ICollection<GarageInfoViewModel>> GetAllGaragesInfoWithFreeSpaceForTrailers()
        {
            // Create collection of garage view model
            ICollection<GarageInfoViewModel> model = new List<GarageInfoViewModel>();

            // Get all garages
            // From the database
            IEnumerable<Garage> garages = await unitOfWork.Garage.GetAll();

            foreach (Garage garage in garages)
            {
                // Initialize variable from type GarageSizeEnum
                GarageSizeEnum garageSize = new GarageSizeEnum();

                // Define the size of the garage
                // As GarageSizeEnum type
                switch (garage.Size)
                {
                    case "small":
                        garageSize = GarageSizeEnum.small;
                        break;

                    case "medium":
                        garageSize = GarageSizeEnum.medium;
                        break;

                    case "large":
                        garageSize = GarageSizeEnum.large;
                        break;
                }

                // Take the value of the garage's size
                int garageSizeValue = (int)garageSize;

                // Get all trailers with
                // GarageId == garage.Id
                // From the database
                ICollection<Trailer> garageTrailers = unitOfWork.Trailer.Find(t => t.GarageId == garage.Id).ToList();

                // Check if there is
                // Free space for trailers
                // In the current garage
                // Assign the data from the current garage
                // To the garage view model
                // And then add the garage view model
                // To the collection of garage view model
                if (garageTrailers.Count < garageSizeValue)
                {
                    GarageInfoViewModel garageInfo = new GarageInfoViewModel()
                    {
                        Id = garage.Id,
                        Country = garage.Country,
                        City = garage.City,
                        Size = garage.Size
                    };

                    model.Add(garageInfo);
                }
                else
                {
                    continue;
                }
            }

            return model;
        }

        public async Task<ICollection<GarageInfoViewModel>> GetAllGaragesInfoWithFreeSpaceForTrucks()
        {
            // Create collection of garage view model
            ICollection<GarageInfoViewModel> model = new List<GarageInfoViewModel>();

            // Get all garages
            // From the database
            IEnumerable<Garage> garages = await unitOfWork.Garage.GetAll();

            foreach (Garage garage in garages)
            {
                // Initialize variable from type GarageSizeEnum
                GarageSizeEnum garageSize = new GarageSizeEnum();

                // Define the size of the garage
                // As GarageSizeEnum type
                switch (garage.Size)
                {
                    case "small":
                        garageSize = GarageSizeEnum.small;
                        break;

                    case "medium":
                        garageSize = GarageSizeEnum.medium;
                        break;

                    case "large":
                        garageSize = GarageSizeEnum.large;
                        break;
                }

                // Take the value of the garage's size
                int garageSizeValue = (int)garageSize;

                // Get all trucks with
                // GarageId == garage.Id
                // From the database
                ICollection<Truck> garageTrucks = unitOfWork.Truck.Find(t => t.GarageId == garage.Id).ToList();

                // Check if there is
                // Free space for trucks
                // In the current garage
                // Assign the data from the current garage
                // To the garage view model
                // And then add the garage view model
                // To the collection of garage view model
                if (garageTrucks.Count < garageSizeValue)
                {
                    GarageInfoViewModel garageInfo = new GarageInfoViewModel()
                    {
                        Id = garage.Id,
                        Country = garage.Country,
                        City = garage.City,
                        Size = garage.Size
                    };

                    model.Add(garageInfo);
                }
                else
                {
                    continue;
                }
            }

            return model;
        }

        public async Task<ICollection<GarageTrailerInfoViewModel>> GetGarageTrailersInfo(Guid id)
        {
            // Get the garage by id
            // From the database
            Garage garage = await unitOfWork.Garage.GetById(id);

            // If garage does not exist
            // Throw argument exception
            if (garage == null)
            {
                throw new ArgumentException(GarageNotExistMessage);
            }

            // Create collection of GarageTrailerInfoViewModel
            ICollection<GarageTrailerInfoViewModel> model = new List<GarageTrailerInfoViewModel>();

            // Get all trailers with property GarageId == id
            // From the database
            ICollection<Trailer> garageTrailers = unitOfWork.Trailer.Find(t => t.GarageId == id).ToList();

            // Assign the trailers data from the garage
            // To the GarageTrailerInfoViewModel
            // And then add the GarageTrailerInfoViewModel
            // To the collection of GarageTrailerInfoViewModel
            foreach (Trailer garageTrailer in garageTrailers)
            {
                // Populate GarageTrailerInfoViewModel called garageTrailerInfo
                // With the data from garage from the database
                GarageTrailerInfoViewModel garageTrailerInfo = new GarageTrailerInfoViewModel()
                {
                    Id = garageTrailer.Id,
                    TruckId = garageTrailer.TruckId,
                    Title = garageTrailer.Title,
                    Series = garageTrailer.Series,
                    TrailerType = garageTrailer.TrailerType,
                    BodyType = garageTrailer.BodyType
                };

                model.Add(garageTrailerInfo);
            }

            return model;
        }

        public async Task<GarageInfoViewModel> GetGarageInfoById(Guid id)
        {
            // Get the garage by id
            // From the database
            Garage garage = await unitOfWork.Garage.GetById(id);

            // If garage does not exist
            // Throw argument exception
            if (garage == null)
            {
                throw new ArgumentException(GarageNotExistMessage);
            }

            // Initialize variable from type GarageSizeEnum
            GarageSizeEnum garageSize = new GarageSizeEnum();

            // Define the size of the garage
            // As GarageSizeEnum type
            switch (garage.Size)
            {
                case "small":
                    garageSize = GarageSizeEnum.small;
                    break;

                case "medium":
                    garageSize = GarageSizeEnum.medium;
                    break;

                case "large":
                    garageSize = GarageSizeEnum.large;
                    break;
            }

            // Take the value of the garage's size
            int garageSizeValue = (int)garageSize;

            // Create garage view model
            // Assign the data from the garage
            // And return the garage view model
            return new GarageInfoViewModel
            {
                Id = garage.Id,
                Country = garage.Country,
                City = garage.City,
                Size = garage.Size,
                TrucksCapacity = garageSizeValue,
                TrailersCapacity = garageSizeValue
            };
        }

        public async Task RemoveGarage(Guid id)
        {
            // Get the garage by id
            // From the database
            Garage garage = await unitOfWork.Garage.GetById(id);

            // If garage does not exist
            // Throw argument exception
            if (garage == null)
            {
                throw new ArgumentException(GarageNotExistMessage);
            }

            // Remove the garage
            // From the database
            unitOfWork.Garage.Remove(garage);

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task<ICollection<GarageTruckInfoViewModel>> GetGarageTrucksInfo(Guid id)
        {
            // Get the garage by id
            // From the database
            Garage garage = await unitOfWork.Garage.GetById(id);

            // If garage does not exist
            // Throw argument exception
            if (garage == null)
            {
                throw new ArgumentException(GarageNotExistMessage);
            }

            // Create collection of GarageTruckInfoViewModel
            ICollection<GarageTruckInfoViewModel> model = new List<GarageTruckInfoViewModel>();

            // Get all trucks with property GarageId == id
            // From the database
            ICollection<Truck> garageTrucks = unitOfWork.Truck.Find(t => t.GarageId == id).ToList();

            // Assign the trucks data from the garage
            // To the GarageTruckInfoViewModel
            // And then add the GarageTruckInfoViewModel
            // To the collection of GarageTruckInfoViewModel
            foreach (Truck garageTruck in garageTrucks)
            {
                // Populate GarageTruckInfoViewModel called garageTruckInfo
                // With the data from garage from the database
                GarageTruckInfoViewModel garageTruckInfo = new GarageTruckInfoViewModel()
                {
                    Id = garageTruck.Id,
                    TrailerId = garageTruck.TrailerId,
                    Brand = garageTruck.Brand,
                    Series = garageTruck.Series
                };

                // If the garageTruck's property EngineId has value
                // Get the Engine called truckEngine
                // With property Id == EngineId from the database
                // And assign the truckEngine's title
                // To the garageTruckInfo's property called EngineTitle
                if (garageTruck.EngineId.HasValue)
                {
                    Engine truckEngine = await unitOfWork.Engine.GetById(garageTruck.EngineId.Value);
                    garageTruckInfo.EngineTitle = truckEngine.Title;
                }
                else
                {
                    garageTruckInfo.EngineTitle = null!;
                }

                // If the garageTruck's property TransmissionId has value
                // Get the Transmission called truckTransmission
                // With property Id == TransmissionId from the database
                // And assign the truckTransmission's title
                // To the garageTruckInfo's property called TransmissionTitle
                if (garageTruck.TransmissionId.HasValue)
                {
                    Transmission truckTransmission = await unitOfWork.Transmission.GetById(garageTruck.TransmissionId!.Value);
                    garageTruckInfo.TransmissionTitle = truckTransmission.Title;
                }
                else
                {
                    garageTruckInfo.TransmissionTitle = null!;
                }

                model.Add(garageTruckInfo);
            }

            return model;
        }

        public async Task<ICollection<GarageTruckTrailerInfoViewModel>> GetGarageTrucksTrailersInfo(Guid id)
        {
            // Check if the garage
            // With property Id == id exists
            // If the garage does not exist
            // Throw argument exception
            if (!await unitOfWork.Garage.AnyAsync(g => g.Id == id))
            {
                throw new ArgumentException(GarageNotExistMessage);
            }

            // Create collection of GarageTruckTrailerInfoViewModel
            ICollection<GarageTruckTrailerInfoViewModel> model = new List<GarageTruckTrailerInfoViewModel>();

            // Get all garage's trucks
            // With trailers
            // From the database
            ICollection<Truck> garageTrucksWithTrailers = unitOfWork.Truck.Find(t => t.GarageId == id && t.TrailerId != null).ToList();

            foreach (Truck garageTruckWithTrailer in garageTrucksWithTrailers)
            {
                // Get the trailer
                // With Id == garageTruckWithTrailer.TrailerId
                // From the database
                Trailer garageTruckTrailer = await unitOfWork.Trailer.GetById(garageTruckWithTrailer.TrailerId!.Value);

                // Assign the garageTruckWithTrailer
                // And garageTruckTrailer data
                // To the GarageTruckTrailerInfoViewModel
                GarageTruckTrailerInfoViewModel garageTruckTrailerInfo = new GarageTruckTrailerInfoViewModel()
                {
                    Id = id,
                    TruckId = garageTruckWithTrailer.Id,
                    TrailerId = garageTruckTrailer.Id,
                    TruckBrand = garageTruckWithTrailer.Brand,
                    TruckSeries = garageTruckWithTrailer.Series,
                    TrailerTitle = garageTruckTrailer.Title,
                    TrailerSeries = garageTruckTrailer.Series
                };

                // Add the GarageTruckTrailerInfoViewModel
                // To the collection of GarageTruckTrailerInfoViewModel
                model.Add(garageTruckTrailerInfo);
            }

            return model;
        }

        public async Task AddGarageTruckToGarageTrailer(Guid id, Guid trailerId)
        {
            // Get truck with
            // Property Id == id
            // From the database
            Truck garageTruck = await unitOfWork.Truck.GetById(id);

            // Check if the truck exists
            // If the truck does not exist
            // Throw argument exception
            if (garageTruck == null)
            {
                throw new ArgumentException(TruckNotExistMessage);
            }

            // Get trailer with
            // Property trailerId == trailerId
            // From the database
            Trailer garageTrailer = await unitOfWork.Trailer.GetById(trailerId);

            // Check if the trailer exists
            // If the trailer does not exist
            // Throw argument exception
            if (garageTrailer == null)
            {
                throw new ArgumentException(TrailerNotExistMessage);
            }

            // Assign the garageTrailer
            // To the garageTruck
            garageTruck.TrailerId = garageTrailer.Id;

            // Assign the garageTruck
            // To the garageTrailer
            garageTrailer.TruckId = garageTruck.Id;

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task RemoveGarageTruckFromGarageTrailer(Guid id, Guid trailerId)
        {
            // Get truck with
            // Property Id == id
            // From the database
            Truck garageTruck = await unitOfWork.Truck.GetById(id);

            // Check if the truck exists
            // If the truck does not exist
            // Throw argument exception
            if (garageTruck == null)
            {
                throw new ArgumentException(TruckNotExistMessage);
            }

            // Get trailer with
            // Property trailerId == trailerId
            // From the database
            Trailer garageTrailer = await unitOfWork.Trailer.GetById(trailerId);

            // Check if the trailer exists
            // If the trailer does not exist
            // Throw argument exception
            if (garageTrailer == null)
            {
                throw new ArgumentException(TrailerNotExistMessage);
            }

            // Remove the garageTrailer
            // From the garageTruck
            garageTruck.Trailer = null;

            // Remove the garageTruck
            // From the garageTrailer
            garageTrailer.Truck = null;

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task AddGarageTruckToUser(Guid id, Guid truckId)
        {
            // Get user with
            // Property Id == id
            // From the database
            User user = await userManager.FindByIdAsync(id.ToString());

            // Check if the user exists
            // If the user does not exist
            // Throw argument exception
            if (user == null)
            {
                throw new ArgumentException(UserNotExistMessage);
            }

            // Get truck with
            // Property Id == truckId
            // From the database
            Truck garageTruck = await unitOfWork.Truck.GetById(truckId);

            // Check if the truck exists
            // If the truck does not exist
            // Throw argument exception
            if (garageTruck == null)
            {
                throw new ArgumentException(TruckNotExistMessage);
            }

            // Assign the user
            // To the garageTruck.User
            garageTruck.UserId = user.Id;

            // Assign the garageTruck
            // To the user.Truck
            user.TruckId = garageTruck.Id;

            // Assign the garageTruck.Garage
            // To the user.Garage
            user.GarageId = garageTruck.GarageId;

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task RemoveGarageTruckFromUser(Guid id, Guid truckId)
        {
            // Get user with
            // Property Id == id
            // From the database
            User user = await userManager.FindByIdAsync(id.ToString());

            // Check if the user exists
            // If the user does not exist
            // Throw argument exception
            if (user == null)
            {
                throw new ArgumentException(UserNotExistMessage);
            }

            // Get truck with
            // Property Id == truckId
            // From the database
            Truck garageTruck = await unitOfWork.Truck.GetById(truckId);

            // Check if the truck exists
            // If the truck does not exist
            // Throw argument exception
            if (garageTruck == null)
            {
                throw new ArgumentException(TruckNotExistMessage);
            }

            // Remove the garageTruck
            // From the user
            garageTruck.UserId = null;

            // Remove the user
            // From the garageTruck
            user.TruckId = null;

            // Remove the garage
            // With Id == garageTruck.GarageId
            // From the user property GarageId
            user.GarageId = null;

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }
    }
}

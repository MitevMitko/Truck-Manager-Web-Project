namespace TruckManagerSoftware.Core.Services.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Contract;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;
    using Models.Garage;

    using static Common.Messages.Messages.Garage;

    public class GarageService : IGarageService
    {
        private readonly IUnitOfWork unitOfWork;

        public GarageService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddGarage(AddGarageViewModel model)
        {
            // Create new garage
            Garage garage = new Garage()
            {
                Country = model.Country,
                City = model.City,
                Size = model.Size.ToString(),
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

            // Create garage view model
            // Assign the data from the garage
            // And return the garage view model
            return new GarageInfoViewModel
            {
                Id = garage.Id,
                Country = garage.Country,
                City = garage.City,
                Size = garage.Size
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
    }
}

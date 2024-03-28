namespace TruckManagerSoftware.Core.Services.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Contract;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;
    using Models.Transmission;

    using static Common.Messages.Messages.Transmission;

    public class TransmissionService : ITransmissionService
    {
        private readonly IUnitOfWork unitOfWork;

        public TransmissionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddTransmission(AddTransmissionViewModel model)
        {
            // Create new transmission
            Transmission transmission = new Transmission() 
            {
                Title = model.Title,
                GearsCount = model.GearsCount,
                Retarder = model.Retarder
            };

            // Add the created transmission to the database
            await unitOfWork.Transmission.Add(transmission);

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task EditTransmission(EditTransmissionViewModel model)
        {
            // Get the transmission by id
            // If transmission does not exist
            // Throw argument exception
            Transmission transmission = await unitOfWork.Transmission.GetById(model.Id) ?? throw new ArgumentException(TransmissionNotExistMessage);

            // Assign the edited data
            // To the transmission
            transmission.Title = model.Title;
            transmission.GearsCount = model.GearsCount;
            transmission.Retarder = model.Retarder;

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task<ICollection<TransmissionInfoViewModel>> GetAllTransmissionsInfo()
        {
            // Create collection of transmission view model
            ICollection<TransmissionInfoViewModel> model = new List<TransmissionInfoViewModel>();

            // Get all transmissions
            // From the database
            IEnumerable<Transmission> transmissions = await unitOfWork.Transmission.GetAll();

            // Assign the data from the transmissions
            // From the database to the transmission view model
            // And then add the transmission view model
            // To the collection of transmission view model
            foreach (Transmission transmission in transmissions)
            {
                TransmissionInfoViewModel transmissionInfo = new TransmissionInfoViewModel()
                {
                    Id = transmission.Id,
                    Title = transmission.Title,
                    GearsCount = transmission.GearsCount,
                    Retarder = transmission.Retarder
                };

                model.Add(transmissionInfo);
            }

            return model;
        }

        public async Task<TransmissionInfoViewModel> GetTransmissionInfoById(Guid id)
        {
            // Get the transmission by id
            // If transmission does not exist
            // Throw argument exception
            Transmission transmission = await unitOfWork.Transmission.GetById(id) ?? throw new ArgumentException(TransmissionNotExistMessage);

            // Create transmission view model
            // Assign the data from the transmission
            // And return the transmission view model
            return new TransmissionInfoViewModel
            {
                Id = transmission.Id,
                Title = transmission.Title,
                GearsCount= transmission.GearsCount,
                Retarder= transmission.Retarder
            };
        }
    }
}

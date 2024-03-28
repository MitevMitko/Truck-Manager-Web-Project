namespace TruckManagerSoftware.Core.Services.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Contract;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;
    using Models.Engine;

    using static Common.Messages.Messages.Engine;

    public class EngineService : IEngineService
    {
        private readonly IUnitOfWork unitOfWork;

        public EngineService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddEngine(AddEngineViewModel model)
        {
            // Create new engine
            Engine engine = new Engine() 
            {
                Title = model.Title,
                PowerHp = model.PowerHp,
                PowerKw = model.PowerKw,
                TorqueNm = model.TorqueNm
            };

            // Add the created engine to the database
            await unitOfWork.Engine.Add(engine);

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task EditEngine(EditEngineViewModel model)
        {
            // Get the engine by id
            // If engine does not exist
            // Throw argument exception
            Engine engine = await unitOfWork.Engine.GetById(model.Id) ?? throw new ArgumentException(EngineNotExistMessage);

            // Assign the edited data
            // To the engine
            engine.Title = model.Title;
            engine.PowerHp = model.PowerHp;
            engine.PowerKw = model.PowerKw;
            engine.TorqueNm = model.TorqueNm;

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task<ICollection<EngineInfoViewModel>> GetAllEnginesInfo()
        {
            // Create collection of engine view model
            ICollection<EngineInfoViewModel> model = new List<EngineInfoViewModel>();

            // Get all engines
            // From the database
            IEnumerable<Engine> engines = await unitOfWork.Engine.GetAll();

            // Assign the data from the engines
            // From the database to the engine view model
            // And then add the engine view model
            // To the collection of engine view model
            foreach (Engine engine in engines)
            {
                EngineInfoViewModel engineInfo = new EngineInfoViewModel() 
                {
                    Id = engine.Id,
                    Title = engine.Title,
                    PowerHp = engine.PowerHp,
                    PowerKw = engine.PowerKw,
                    TorqueNm = engine.TorqueNm
                };

                model.Add(engineInfo);
            }

            return model;
        }

        public async Task<EngineInfoViewModel> GetEngineInfoById(Guid id)
        {
            // Get the engine by id
            // If engine does not exist
            // Throw argument exception
            Engine engine = await unitOfWork.Engine.GetById(id) ?? throw new ArgumentException(EngineNotExistMessage);

            // Create engine view model
            // Assign the data from the engine
            // And return the engine view model
            return new EngineInfoViewModel
            {
                Id = engine.Id,
                Title = engine.Title,
                PowerHp = engine.PowerHp,
                PowerKw = engine.PowerKw,
                TorqueNm = engine.TorqueNm
            };
        }
    }
}

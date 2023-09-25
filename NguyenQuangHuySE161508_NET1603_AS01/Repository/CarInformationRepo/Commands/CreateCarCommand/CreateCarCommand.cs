using BusinessObjects;
using BusinessObjects.Entities;
using DataAccessLayer;
using Repository.CustomerRepo.Models;

namespace Repository.CarInformationRepo.Commands.CreateCarCommand
{
    public class CreateCarCommand : ICreateCarCommand
    {
        private readonly IGenericRepository<CarInformation> _carRepository;
        public CreateCarCommand(IGenericRepository<CarInformation> carRepository)
        {
            _carRepository = carRepository;
        }
        public CommandResult<bool> Create(CarInformation model)
        {
            string errorMessages = string.Empty;
            bool result = false;

            result = _carRepository.Insert(model);
            if (!result)
            {
                errorMessages = "Created failed!!! Please try again";
            }

            return new CommandResult<bool>()
            {
                Message = errorMessages,
                Type = CommandType.Create,
                ResponseObject = result,
            };
        }
    }
}

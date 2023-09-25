using BusinessObjects;
using BusinessObjects.Entities;
using DataAccessLayer;

namespace Repository.CarInformationRepo.Commands.UpdateCarCommand
{
    public class UpdateCarCommand : IUpdateCarCommand
    {
        private readonly IGenericRepository<CarInformation> _carRepository;
        public UpdateCarCommand(IGenericRepository<CarInformation> carRepository)
        {
            _carRepository = carRepository;
        }
        public CommandResult<bool> Update(CarInformation model)
        {
            string errorMessages = string.Empty;
            bool result = false;

            result = _carRepository.Update(model);
            if (!result)
            {
                errorMessages = "Updated failed!!! Please try again";
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

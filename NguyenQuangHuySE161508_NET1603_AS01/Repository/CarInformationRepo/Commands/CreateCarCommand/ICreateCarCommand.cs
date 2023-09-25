using BusinessObjects;
using BusinessObjects.Entities;

namespace Repository.CarInformationRepo.Commands.CreateCarCommand
{
    public interface ICreateCarCommand
    {
        CommandResult<bool> Create(CarInformation model);
    }
}

using BusinessObjects;
using BusinessObjects.Entities;

namespace Repository.CarInformationRepo.Commands.UpdateCarCommand
{
    public interface IUpdateCarCommand
    {
        CommandResult<bool> Update(CarInformation model);
    }
}

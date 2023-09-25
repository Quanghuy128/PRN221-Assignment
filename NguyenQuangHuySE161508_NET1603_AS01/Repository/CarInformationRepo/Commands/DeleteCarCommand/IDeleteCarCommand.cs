using BusinessObjects;

namespace Repository.CarInformationRepo.Commands.DeleteCarCommand
{
    public interface IDeleteCarCommand
    {
        CommandResult<bool> Delete(int id);
    }
}

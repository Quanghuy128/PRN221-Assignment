using BusinessObjects;

namespace Repository.CustomerRepo.Commands.DeleteCustomerCommand
{
    public interface IDeleteCustomerCommand
    {
        CommandResult<bool> Delete(int id);
    }
}

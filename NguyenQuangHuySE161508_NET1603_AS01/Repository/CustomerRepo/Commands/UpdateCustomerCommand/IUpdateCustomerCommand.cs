using BusinessObjects.Entities;
using BusinessObjects;

namespace Repository.CustomerRepo.Commands.UpdateCustomerCommand
{
    public interface IUpdateCustomerCommand
    {
        CommandResult<bool> Update(Customer model);
    }
}

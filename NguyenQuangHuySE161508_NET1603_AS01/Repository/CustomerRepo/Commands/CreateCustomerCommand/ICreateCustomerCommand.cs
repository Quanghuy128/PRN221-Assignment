using BusinessObjects;
using BusinessObjects.Entities;
using Repository.CustomerRepo.Models;

namespace Repository.CustomerRepo.Commands.CreateCustomerCommand
{
    public interface ICreateCustomerCommand
    {
        public CommandResult<bool> Create(Customer model);
    }
}

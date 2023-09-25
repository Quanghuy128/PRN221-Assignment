using BusinessObjects;
using BusinessObjects.Entities;
using DataAccessLayer;
using Repository.CustomerRepo.Models;

namespace Repository.CustomerRepo.Commands.UpdateCustomerCommand
{
    public class UpdateCustomerCommand : IUpdateCustomerCommand
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        public UpdateCustomerCommand(IGenericRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public CommandResult<bool> Update(Customer model)
        {
            string errorMessages = string.Empty;
            bool result = false;

            var entity = _customerRepository.TableNoTracking.FirstOrDefault(x => x.CustomerId == model.CustomerId);
            if(entity is null)
            {
                errorMessages = "Not found customer";
            }

            entity.CustomerName = model.CustomerName;
            entity.Telephone = model.Telephone;
            entity.Email = model.Email;
            entity.CustomerBirthday = model.CustomerBirthday;
            entity.CustomerStatus = model.CustomerStatus;
            entity.Password = model.Password;


            result = _customerRepository.Update(entity);
            if (!result)
            {
                errorMessages = "Updated failed!!! Please try again";
            }

            return new CommandResult<bool>()
            {
                Message = errorMessages,
                Type = CommandType.Update,
                ResponseObject = result,
            };
        }
    }
}

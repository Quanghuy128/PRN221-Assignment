using BusinessObjects;
using BusinessObjects.Entities;
using DataAccessLayer;
using Repository.CustomerRepo.Models;

namespace Repository.CustomerRepo.Commands.CreateCustomerCommand
{
    public class CreateCustomerCommand : ICreateCustomerCommand
    {
        private readonly IGenericRepository<Customer> _customerRepository;

        public CreateCustomerCommand(IGenericRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public CommandResult<bool> Create(Customer model)
        {
            string errorMessages = string.Empty;
            bool result = false;

            var duplicatedCus = _customerRepository.TableNoTracking
                .Where(x => x.Email == model.Email)
                .Select(x => new CustomerViewModel()
                {
                    Email = x.Email,
                })
                .SingleOrDefault();
            errorMessages = duplicatedCus == null ? "" : "Duplicated Email !!!";

            if(duplicatedCus == null)
            {
                result = _customerRepository.Insert(model);
                if(!result)
                {
                    errorMessages = "Created failed!!! Please try again";
                }
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

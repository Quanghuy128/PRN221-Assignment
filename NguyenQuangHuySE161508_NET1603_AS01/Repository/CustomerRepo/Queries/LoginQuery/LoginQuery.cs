using BusinessObjects;
using BusinessObjects.Entities;
using DataAccessLayer;
using Repository.CustomerRepo.Models;

namespace Repository.CustomerRepo.Queries.LoginQuery
{
    public class LoginQuery : ILoginQuery
    {
        private readonly IGenericRepository<Customer> _customerRepository;

        public LoginQuery(IGenericRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public CommandResult<CustomerViewModel> Get(string email)
        {
            string errorMessage = string.Empty;

            var model = _customerRepository.TableNoTracking
                .Where(x => x.Email == email && x.CustomerStatus == 1)
                .Select(x => new CustomerViewModel() 
                {   
                    CustomerId = x.CustomerId,
                    CustomerName = x.CustomerName,
                    Telephone = x.Telephone,
                    Email = x.Email,
                    CustomerBirthday = x.CustomerBirthday,
                    CustomerStatus = x.CustomerStatus,
                    Password = x.Password,
                })
                .SingleOrDefault();

            if(model == null)
            {
                errorMessage = "Login failed! email invalid!!!";
            }

            return new CommandResult<CustomerViewModel>()
            {
                Message = errorMessage,
                Type = CommandType.Get,
                ResponseObject = model
            };
        }
    }
}

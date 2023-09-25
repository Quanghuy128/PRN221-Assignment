using BusinessObjects;
using BusinessObjects.Entities;
using DataAccessLayer;
using Repository.CustomerRepo.Models;

namespace Repository.CustomerRepo.Queries.GetCustomersQuery
{
    public class GetCustomersQuery : IGetCustomersQuery
    {
        private readonly IGenericRepository<Customer> _customerRepository;

        public GetCustomersQuery(IGenericRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public CommandResult<IEnumerable<CustomerViewModel>> GetAll(string? searchValue)
        {
            string errorMessage = string.Empty;
            var result = _customerRepository.TableNoTracking.Where(x => x.Email.ToLower().Contains(searchValue.ToLower()) 
                                                                || x.CustomerId.ToString().Contains(searchValue) 
                                                                || x.CustomerName.ToLower().Contains(searchValue.ToLower()) 
                                                                || x.Telephone.Contains(searchValue) 
                                                                || x.CustomerBirthday.ToString().ToLower().Contains(searchValue.ToLower()))
            .Select
            (
                x => new CustomerViewModel()
                {
                    CustomerId = x.CustomerId,
                    CustomerName = x.CustomerName,
                    Telephone = x.Telephone,
                    Email = x.Email,
                    CustomerBirthday = x.CustomerBirthday,
                    CustomerStatus = x.CustomerStatus,
                    Password = x.Password,
                }    
            );

            if (result == null) errorMessage = "No Customers!!!";
            return new CommandResult<IEnumerable<CustomerViewModel>>()
            {
                Message = errorMessage,
                Type = CommandType.Get,
                ResponseObject = result
            };
        }
    }
}

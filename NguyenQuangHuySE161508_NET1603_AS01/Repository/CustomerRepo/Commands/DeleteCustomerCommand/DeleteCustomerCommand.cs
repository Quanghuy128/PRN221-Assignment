
using BusinessObjects;
using BusinessObjects.Entities;
using DataAccessLayer;

namespace Repository.CustomerRepo.Commands.DeleteCustomerCommand
{
    public class DeleteCustomerCommand : IDeleteCustomerCommand
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        public DeleteCustomerCommand(IGenericRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public CommandResult<bool> Delete(int id)
        {
            string errorMessage = string.Empty;
            bool res = false;
            var model = _customerRepository.TableNoTracking.Where(x => x.CustomerId == id).SingleOrDefault();
            if (model == null) errorMessage = "Not found!!!";
            else {
                res = _customerRepository.Delete(model);
            }
            return new CommandResult<bool>
            {
                Message = errorMessage,
                Type = CommandType.Delete,
                ResponseObject = res
            };
        }
    }
}

using BusinessObjects;
using Repository.CustomerRepo.Models;

namespace Repository.CustomerRepo.Queries.GetCustomersQuery
{
    public interface IGetCustomersQuery
    {
        public CommandResult<IEnumerable<CustomerViewModel>> GetAll(string? searchValue);
    }
}

using BusinessObjects;
using Repository.CustomerRepo.Models;

namespace Repository.CustomerRepo.Queries.LoginQuery
{
    public interface ILoginQuery
    {
        public CommandResult<CustomerViewModel> Get(string email);
    }
}

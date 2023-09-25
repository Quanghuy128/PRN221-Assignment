using BusinessObjects;
using Repository.RentingTransactionRepo.Models;

namespace Repository.RentingTransactionRepo.Queries.GetRentingTransactionCustomerQuery
{
    public interface IGetRentingTransactionCustomerQuery
    {
        CommandResult<IEnumerable<RentingTransactionCustomerViewModel>> GetAll(int id);
    }
}

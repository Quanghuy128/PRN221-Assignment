using BusinessObjects;
using Repository.RentingTransactionRepo.Models;

namespace Repository.RentingTransactionRepo.Queries.GetRentingTransactionDetailQuery
{
    public interface IGetRentingTransactionDetailQuery
    {
        CommandResult<IEnumerable<RentingTransactionDetailViewModel>> GetAll(DateTime? start, DateTime? end);
    }
}

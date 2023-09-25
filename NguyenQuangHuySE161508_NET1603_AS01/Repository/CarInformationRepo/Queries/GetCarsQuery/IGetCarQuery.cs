using BusinessObjects;
using Repository.CarInformationRepo.Models;

namespace Repository.CarInformationRepo.Queries.GetCarsQuery
{
    public interface IGetCarQuery
    {
        public CommandResult<IEnumerable<CarViewModel>> GetAll(string? searchValue);
    }
}

using BusinessObjects;
using BusinessObjects.Entities;
using Repository.CarInformationRepo.Models;
using Repository.CustomerRepo.Models;
using Repository.RentingTransactionRepo.Models;

namespace Repository
{
    public interface IService
    {
        //Customers
        public CommandResult<IEnumerable<CustomerViewModel>> GetAllCustomers(string? searchValue);
        public CommandResult<CustomerViewModel> GetCustomerByEmail(string email);
        public CommandResult<bool> CreateCustomer(Customer model);
        public CommandResult<bool> DeleteCustomer(int id);
        public CommandResult<bool> UpdateCustomer(Customer model);

        //Cars
        public CommandResult<IEnumerable<CarViewModel>> GetAllCars(string? searchValue);
        public CommandResult<CarViewModel> GetCarById(string email);
        public CommandResult<bool> CreateCar(CarInformation model);
        public CommandResult<bool> DeleteCar(int id);
        public CommandResult<bool> UpdateCar(CarInformation model);

        //renting
        public CommandResult<IEnumerable<RentingTransactionDetailViewModel>> GetAllRentingTransaction(DateTime? start, DateTime? end);
        public CommandResult<IEnumerable<RentingTransactionCustomerViewModel>> GetAllRentingTransactionCustomer(int id);
    }
}

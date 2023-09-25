using BusinessObjects;
using BusinessObjects.Entities;
using Repository.CarInformationRepo.Commands.CreateCarCommand;
using Repository.CarInformationRepo.Commands.DeleteCarCommand;
using Repository.CarInformationRepo.Commands.UpdateCarCommand;
using Repository.CarInformationRepo.Models;
using Repository.CarInformationRepo.Queries.GetCarsQuery;
using Repository.CustomerRepo.Commands.CreateCustomerCommand;
using Repository.CustomerRepo.Commands.DeleteCustomerCommand;
using Repository.CustomerRepo.Commands.UpdateCustomerCommand;
using Repository.CustomerRepo.Models;
using Repository.CustomerRepo.Queries.GetCustomersQuery;
using Repository.CustomerRepo.Queries.LoginQuery;
using Repository.RentingTransactionRepo.Models;
using Repository.RentingTransactionRepo.Queries.GetRentingTransactionCustomerQuery;
using Repository.RentingTransactionRepo.Queries.GetRentingTransactionDetailQuery;

namespace Repository
{
    public class Service : IService
    {
        private readonly ILoginQuery loginQuery;
        private readonly IGetCustomersQuery getCustomersQuery;
        private readonly ICreateCustomerCommand createCustomerCommand;
        private readonly IUpdateCustomerCommand updateCustomerCommand;
        private readonly IDeleteCustomerCommand deleteCustomerCommand;

        private readonly IGetCarQuery getCarQuery;
        private readonly ICreateCarCommand createCarCommand;
        private readonly IUpdateCarCommand updateCarCommand;
        private readonly IDeleteCarCommand deleteCarCommand;

        private readonly IGetRentingTransactionDetailQuery getRentingTransactionDetailQuery;
        private readonly IGetRentingTransactionCustomerQuery getRentingTransactionCustomerQuery;

        public Service(ILoginQuery loginQuery,
            IGetCustomersQuery getCustomersQuery,
            ICreateCustomerCommand createCustomerCommand,
            IUpdateCustomerCommand updateCustomerCommand,
            IDeleteCustomerCommand deleteCustomerCommand,
            IGetCarQuery getCarQuery,
            ICreateCarCommand createCarCommand,
            IUpdateCarCommand updateCarCommand,
            IDeleteCarCommand deleteCarCommand,
            IGetRentingTransactionDetailQuery getRentingTransactionDetailQuery,
            IGetRentingTransactionCustomerQuery getRentingTransactionCustomerQuery
            )
        {
            this.loginQuery = loginQuery;

            this.getCustomersQuery = getCustomersQuery;
            this.createCustomerCommand = createCustomerCommand;
            this.updateCustomerCommand = updateCustomerCommand;
            this.deleteCustomerCommand = deleteCustomerCommand;

            this.getCarQuery = getCarQuery;
            this.createCarCommand = createCarCommand;
            this.updateCarCommand = updateCarCommand;
            this.deleteCarCommand = deleteCarCommand;

            this.getRentingTransactionDetailQuery = getRentingTransactionDetailQuery;
            this.getRentingTransactionCustomerQuery = getRentingTransactionCustomerQuery;
        }
        public CommandResult<bool> CreateCar(CarInformation model)
        {
            return createCarCommand.Create(model);
        }

        public CommandResult<bool> CreateCustomer(Customer model)
        {
            return createCustomerCommand.Create(model);
        }

        public CommandResult<bool> DeleteCar(int id)
        {
            return deleteCarCommand.Delete(id);
        }

        public CommandResult<bool> DeleteCustomer(int id)
        {
            return deleteCustomerCommand.Delete(id);
        }

        public CommandResult<IEnumerable<RentingTransactionCustomerViewModel>> GetAllRentingTransactionCustomer(int id)
        {
            return getRentingTransactionCustomerQuery.GetAll(id);
        }

        public CommandResult<IEnumerable<CarViewModel>> GetAllCars(string? searchValue)
        {
            return getCarQuery.GetAll(searchValue);
        }

        public CommandResult<IEnumerable<CustomerViewModel>> GetAllCustomers(string? searchValue)
        {
            return getCustomersQuery.GetAll(searchValue);
        }

        public CommandResult<IEnumerable<RentingTransactionDetailViewModel>> GetAllRentingTransaction(DateTime? start, DateTime? end)
        {
            return this.getRentingTransactionDetailQuery.GetAll(start, end);
        }

        public CommandResult<CarViewModel> GetCarById(string email)
        {
            return null;
        }

        public CommandResult<CustomerViewModel> GetCustomerByEmail(string email)
        {
            return loginQuery.Get(email);
        }

        public CommandResult<bool> UpdateCar(CarInformation model)
        {
            return updateCarCommand.Update(model);
        }

        public CommandResult<bool> UpdateCustomer(Customer model)
        {
            return updateCustomerCommand.Update(model);
        }
    }
}

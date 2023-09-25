using BusinessObjects;
using BusinessObjects.Entities;
using DataAccessLayer;
using Repository.RentingTransactionRepo.Models;

namespace Repository.RentingTransactionRepo.Queries.GetRentingTransactionCustomerQuery
{
    public class GetRentingTransactionCustomerQuery : IGetRentingTransactionCustomerQuery
    {
        private readonly IGenericRepository<RentingTransaction> _rentingTransactionRepository;
        private readonly IGenericRepository<RentingDetail> _rentingDetailRepository;
        private readonly IGenericRepository<CarInformation> _carRepository;
        private readonly IGenericRepository<Customer> _customerRepository;

        public GetRentingTransactionCustomerQuery(
            IGenericRepository<RentingTransaction> rentingTransactionRepository,
            IGenericRepository<RentingDetail> rentingDetailRepository,
            IGenericRepository<CarInformation> carRepository,
            IGenericRepository<Customer> customerRepository)
        {
            _rentingTransactionRepository = rentingTransactionRepository;
            _rentingDetailRepository = rentingDetailRepository;
            _carRepository = carRepository;
            _customerRepository = customerRepository;
        }
        public CommandResult<IEnumerable<RentingTransactionCustomerViewModel>> GetAll(int id)
        {
            string errorMessage = string.Empty;
            var model = _rentingDetailRepository.TableNoTracking
                .Join(_rentingTransactionRepository.TableNoTracking,
                   detail => detail.RentingTransactionId,
                   transaction => transaction.RentingTransationId,
                   (detail, transaction) => new RentingTransactionCustomerViewModel()
                   {
                       RentingTransationId = transaction.RentingTransationId,
                       RentingDate = transaction.RentingDate,
                       TotalPrice = transaction.TotalPrice,
                       CustomerId = transaction.CustomerId,
                       RentingStatus = transaction.RentingStatus,
                       CarId = detail.CarId,
                       StartDate = detail.StartDate,
                       EndDate = detail.EndDate,
                       Price = detail.Price
                   }
                   )
                .Join(_carRepository.TableNoTracking,
                    rentModel => rentModel.CarId,
                    car => car.CarId,
                    (rentModel, car) => new RentingTransactionCustomerViewModel()
                    {
                        RentingTransationId = rentModel.RentingTransationId,
                        RentingDate = rentModel.RentingDate,
                        TotalPrice = rentModel.TotalPrice,
                        CustomerId = rentModel.CustomerId,
                        RentingStatus = rentModel.RentingStatus,
                        CarId = rentModel.CarId,
                        StartDate = rentModel.StartDate,
                        EndDate = rentModel.EndDate,
                        Price = rentModel.Price,

                        CarName = car.CarName,
                        CarDescription = car.CarDescription,
                        NumberOfDoors = car.NumberOfDoors,
                        SeatingCapacity = car.SeatingCapacity,
                        CarRentingPricePerDay = car.CarRentingPricePerDay,
                        FuelType = car.FuelType,
                        Year = car.Year,
                        CarStatus = car.CarStatus,
                        ManufacturerId = car.ManufacturerId,
                        SupplierId = car.SupplierId,
                    }
                    )
                .Join(_customerRepository.TableNoTracking,
                    rentModel => rentModel.CustomerId,
                    customer => customer.CustomerId,
                    (rentModel, customer) => new RentingTransactionCustomerViewModel()
                    {
                        RentingTransationId = rentModel.RentingTransationId,
                        RentingDate = rentModel.RentingDate,
                        TotalPrice = rentModel.TotalPrice,
                        CustomerId = rentModel.CustomerId,
                        RentingStatus = rentModel.RentingStatus,
                        CarId = rentModel.CarId,
                        StartDate = rentModel.StartDate,
                        EndDate = rentModel.EndDate,
                        Price = rentModel.Price,

                        CarName = rentModel.CarName,
                        CarDescription = rentModel.CarDescription,
                        NumberOfDoors = rentModel.NumberOfDoors,
                        SeatingCapacity = rentModel.SeatingCapacity,
                        CarRentingPricePerDay = rentModel.CarRentingPricePerDay,
                        FuelType = rentModel.FuelType,
                        Year = rentModel.Year,
                        CarStatus = rentModel.CarStatus,
                        ManufacturerId = rentModel.ManufacturerId,
                        SupplierId = rentModel.SupplierId,

                        CustomerName = customer.CustomerName,
                        Email = customer.Email,
                        Telephone = customer.Telephone,
                    }
                    ).Where(x => x.CustomerId == id);

            if (errorMessage is null) errorMessage = "Not found renting !!!";
            return new CommandResult<IEnumerable<RentingTransactionCustomerViewModel>>
            {
                Message = errorMessage,
                Type = CommandType.Get,
                ResponseObject = model,
            };
        }
    }
}

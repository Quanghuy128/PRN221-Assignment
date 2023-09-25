using BusinessObjects;
using BusinessObjects.Entities;
using DataAccessLayer;
using Repository.CarInformationRepo.Models;
using Repository.RentingTransactionRepo.Models;

namespace Repository.RentingTransactionRepo.Queries.GetRentingTransactionDetailQuery
{
    public class GetRentingTransactionDetailQuery : IGetRentingTransactionDetailQuery
    {
        private readonly IGenericRepository<RentingTransaction> _rentingTransactionRepository;
        private readonly IGenericRepository<RentingDetail> _rentingDetailRepository;
        private readonly IGenericRepository<CarInformation> _carRepository;

        public GetRentingTransactionDetailQuery(
            IGenericRepository<RentingTransaction> rentingTransactionRepository,
            IGenericRepository<RentingDetail> rentingDetailRepository,
            IGenericRepository<CarInformation> carRepository)
        {
            _rentingTransactionRepository = rentingTransactionRepository;
            _rentingDetailRepository = rentingDetailRepository;
            _carRepository = carRepository;
        }
        public CommandResult<IEnumerable<RentingTransactionDetailViewModel>> GetAll(DateTime? start, DateTime? end)
        {
            string errorMessage = string.Empty;
            var model = _rentingDetailRepository.TableNoTracking
                .Join(_rentingTransactionRepository.TableNoTracking,
                   detail => detail.RentingTransactionId,
                   transaction => transaction.RentingTransationId,
                   (detail, transaction) => new RentingTransactionDetailViewModel()
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
                    (rentModel, car) => new RentingTransactionDetailViewModel()
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
                    ).Where(x =>  x.StartDate >= start && x.EndDate <= end);

            if (errorMessage is null) errorMessage = "Not found reting !!!";
            return new CommandResult<IEnumerable<RentingTransactionDetailViewModel>>
            {
                Message = errorMessage,
                Type = CommandType.Get,
                ResponseObject = model,
            };
        }
    }
}

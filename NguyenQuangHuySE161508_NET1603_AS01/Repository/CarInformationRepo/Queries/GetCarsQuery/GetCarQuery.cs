using BusinessObjects;
using BusinessObjects.Entities;
using DataAccessLayer;
using Repository.CarInformationRepo.Models;

namespace Repository.CarInformationRepo.Queries.GetCarsQuery
{
    public class GetCarQuery : IGetCarQuery
    {
        private readonly IGenericRepository<CarInformation> _carRepository;
        private readonly IGenericRepository<Manufacturer> _manufactureRepository;
        private readonly IGenericRepository<Supplier> _supplierRepository;
        public GetCarQuery(IGenericRepository<CarInformation> carRepository, IGenericRepository<Manufacturer> manufactureRepository, IGenericRepository<Supplier> supplierRepository)
        {
            _carRepository = carRepository;
            _manufactureRepository = manufactureRepository;
            _supplierRepository = supplierRepository;
        }
        public CommandResult<IEnumerable<CarViewModel>> GetAll(string? searchValue)
        {
            string errorMessage = string.Empty;
            var model = _carRepository.TableNoTracking.Where(x => x.CarName.Contains(searchValue))
                .Join(_manufactureRepository.TableNoTracking,
                   car => car.ManufacturerId,
                   manufacturer => manufacturer.ManufacturerId,
                   (car, manufacturer) => new CarViewModel()
                   {
                       CarId = car.CarId,
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
                       ManufacturerName = manufacturer.ManufacturerName,
                   }
                   )
                .Join(_supplierRepository.TableNoTracking,
                    car => car.SupplierId,
                    supplier => supplier.SupplierId,
                    (car, supplier) => new CarViewModel()
                    {
                        CarId = car.CarId,
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
                        ManufacturerName = car.ManufacturerName,
                        SupplierName = supplier.SupplierName,
                    }
                    );
            if (model == null) errorMessage = "Not found cars!!!";
            return new CommandResult<IEnumerable<CarViewModel>>
            {
                Message = errorMessage,
                Type = CommandType.Get,
                ResponseObject = model
            };
        }
    }
}

using BusinessObjects;
using BusinessObjects.Entities;
using DataAccessLayer;

namespace Repository.CarInformationRepo.Commands.DeleteCarCommand
{
    public class DeleteCarCommand : IDeleteCarCommand
    {
        private readonly IGenericRepository<CarInformation> _carRepository;
        private readonly IGenericRepository<RentingDetail> _rentingDetailRepository;
        public DeleteCarCommand(IGenericRepository<CarInformation> carRepository,
            IGenericRepository<RentingDetail> rentingDetailRepository)
        {
            _carRepository = carRepository;
            _rentingDetailRepository = rentingDetailRepository;
        }
        public CommandResult<bool> Delete(int id)
        {
            string errorMessage = string.Empty;
            string message = string.Empty;
            bool res = false;

            var check = _rentingDetailRepository.TableNoTracking.FirstOrDefault(x => x.CarId == id);

            if (check == null)
            {
                var model = _carRepository.TableNoTracking.Where(x => x.CarId == id).SingleOrDefault();
                if (model == null) errorMessage = "Not found!!!";
                else
                {
                    message = "Delete Successfully";
                    res = _carRepository.Delete(model);
                }
            }
            else
            {
                var changedCar = _carRepository.TableNoTracking.Where(x => x.CarId == id)
                    .Select(
                        car => new CarInformation
                        {
                            CarId = car.CarId,
                            CarName = car.CarName,
                            CarDescription = car.CarDescription,
                            NumberOfDoors = car.NumberOfDoors,
                            SeatingCapacity = car.SeatingCapacity,
                            CarRentingPricePerDay = car.CarRentingPricePerDay,
                            FuelType = car.FuelType,
                            Year = car.Year,
                            CarStatus = Convert.ToByte(car.CarStatus == 1 ? 0 : 1),
                            ManufacturerId = car.ManufacturerId,
                            SupplierId = car.SupplierId,
                        }
                    ).SingleOrDefault();
                if (changedCar == null) errorMessage = "Not found!!!";
                else
                {
                    message = "Car includes renting. Change status!!";
                    res = _carRepository.Update(changedCar);
                }
            }

            return new CommandResult<bool>
            {
                Message = message=="" ? errorMessage : message,
                Type = CommandType.Delete,
                ResponseObject = res
            };
        }
    }
}

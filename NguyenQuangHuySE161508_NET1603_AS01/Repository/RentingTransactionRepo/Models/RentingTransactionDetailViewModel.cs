namespace Repository.RentingTransactionRepo.Models
{
    public class RentingTransactionDetailViewModel : RentingDetailViewModel
    {
        public int RentingTransationId { get; set; }

        public DateTime? RentingDate { get; set; }

        public decimal? TotalPrice { get; set; }

        public int CustomerId { get; set; }

        public byte? RentingStatus { get; set; }
    }
    public class RentingDetailViewModel : Car
    {
        public int CarId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal? Price { get; set; }
    }
    public class Car
    {
        public string CarName { get; set; } = null!;

        public string? CarDescription { get; set; }

        public int? NumberOfDoors { get; set; }

        public int? SeatingCapacity { get; set; }

        public string? FuelType { get; set; }

        public int? Year { get; set; }

        public int ManufacturerId { get; set; }

        public int SupplierId { get; set; }

        public byte? CarStatus { get; set; }

        public decimal? CarRentingPricePerDay { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class CarInformation
{
    public int CarId { get; set; }

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

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<RentingDetail> RentingDetails { get; set; } = new List<RentingDetail>();

    public virtual Supplier Supplier { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Manufacturer
{
    public int ManufacturerId { get; set; }

    public string ManufacturerName { get; set; } = null!;

    public string? Description { get; set; }

    public string? ManufacturerCountry { get; set; }

    public virtual ICollection<CarInformation> CarInformations { get; set; } = new List<CarInformation>();
}

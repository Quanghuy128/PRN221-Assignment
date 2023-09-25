using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class RentingDetail
{
    public int RentingTransactionId { get; set; }

    public int CarId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal? Price { get; set; }

    public virtual CarInformation Car { get; set; } = null!;

    public virtual RentingTransaction RentingTransaction { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class RentingTransaction
{
    public int RentingTransationId { get; set; }

    public DateTime? RentingDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public int CustomerId { get; set; }

    public byte? RentingStatus { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<RentingDetail> RentingDetails { get; set; } = new List<RentingDetail>();
}

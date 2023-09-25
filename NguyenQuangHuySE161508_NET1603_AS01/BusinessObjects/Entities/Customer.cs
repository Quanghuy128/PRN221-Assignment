using DataAccessLayer;
using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? Telephone { get; set; }

    public string Email { get; set; } = null!;

    public DateTime? CustomerBirthday { get; set; }

    public byte? CustomerStatus { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<RentingTransaction> RentingTransactions { get; set; } = new List<RentingTransaction>();
}

using System;
using System.Collections.Generic;

namespace Labb2Databaser.Models;

public partial class Kunder
{
    public int KundId { get; set; }

    public string? FörNamn { get; set; }

    public string? EfterNamn { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<KundRecensioner> KundRecensioners { get; set; } = new List<KundRecensioner>();

    public virtual ICollection<Ordrar> Ordrars { get; set; } = new List<Ordrar>();
}

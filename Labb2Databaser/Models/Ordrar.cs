using System;
using System.Collections.Generic;

namespace Labb2Databaser.Models;

public partial class Ordrar
{
    public int OrderId { get; set; }

    public string? Isbn { get; set; }

    public int? KundId { get; set; }

    public int? ButikId { get; set; }

    public DateTime? Datum { get; set; }

    public virtual Butiker? Butik { get; set; }

    public virtual Böcker? IsbnNavigation { get; set; }

    public virtual Kunder? Kund { get; set; }
}

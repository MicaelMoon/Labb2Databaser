using System;
using System.Collections.Generic;

namespace Labb2Databaser.Models;

public partial class KundRecensioner
{
    public int RecensionId { get; set; }

    public int? KundId { get; set; }

    public string? Isbn { get; set; }

    public string? RecesionText { get; set; }

    public DateTime? RecentionDatum { get; set; }

    public int? BetygAvTio { get; set; }

    public virtual Böcker? IsbnNavigation { get; set; }

    public virtual Kunder? Kund { get; set; }
}

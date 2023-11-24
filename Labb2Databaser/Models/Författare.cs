using System;
using System.Collections.Generic;

namespace Labb2Databaser.Models;

public partial class Författare
{
    public int FörfattarId { get; set; }

    public string? FörNamn { get; set; }

    public string? EfterNamn { get; set; }

    public string? FödelseDatum { get; set; }

    public virtual ICollection<Böcker> Böckers { get; set; } = new List<Böcker>();
}

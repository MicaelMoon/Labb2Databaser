using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Labb2Databaser.Models;

public partial class Författare
{
    public int FörfattarId { get; set; }

    public string? FörNamn { get; set; }

    public string? EfterNamn { get; set; }

    public string? FödelseDatum { get; set; }

    public string FullName => $"{FörNamn} {EfterNamn}";

    public virtual ICollection<Böcker> Böckers { get; set; } = new List<Böcker>();

}

using System;
using System.Collections.Generic;

namespace Labb2Databaser.Models;

public partial class Förlag
{
    public int FörLagId { get; set; }

    public string? FörLagNamn { get; set; }

    public string? Adress { get; set; }
}

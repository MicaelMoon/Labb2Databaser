﻿using System;
using System.Collections.Generic;

namespace Labb2Databaser.Models;

public partial class Butiker
{
    public int ButikId { get; set; }

    public string? ButikNamn { get; set; }

    public string? ButikAdress { get; set; }

    public virtual ICollection<LagerSaldo> LagerSaldos { get; set; } = new List<LagerSaldo>();

    public virtual ICollection<Ordrar> Ordrars { get; set; } = new List<Ordrar>();
}

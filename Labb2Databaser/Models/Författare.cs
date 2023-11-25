using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Labb2Databaser.Models;

public partial class Författare
{

    public Författare(string firstName, string lastName, string birthDate)
    {
        this.FörNamn = firstName;
        this.EfterNamn = lastName;
        this.FödelseDatum = birthDate;
    }

    public int FörfattarId { get; set; }

    public string? FörNamn { get; set; }

    public string? EfterNamn { get; set; }

    public string? FödelseDatum { get; set; }

    public virtual ICollection<Böcker> Böckers { get; set; } = new List<Böcker>();

    public async Task AddAuthorToSystem(int Id, string firstName, string lastName, string BirthDay)
    {
        
    }
}

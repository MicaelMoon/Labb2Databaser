using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Labb2Databaser.Data;
using Labb2Databaser.UserControlls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Labb2Databaser.Models;

public partial class Böcker
{
    public string Isbn { get; set; } = null!;

    public string? Titel { get; set; }

    public string? Language { get; set; }

    public int? Sidor { get; set; }

    public int? Pris { get; set; }

    public string? Utgivningsdatum { get; set; }

    public int? FörfattarId { get; set; }

    public virtual Författare? Författar { get; set; }

    public virtual ICollection<KundRecensioner> KundRecensioners { get; set; } = new List<KundRecensioner>();

    public virtual ICollection<LagerSaldo> LagerSaldos { get; set; } = new List<LagerSaldo>();

    public virtual ICollection<Ordrar> Ordrars { get; set; } = new List<Ordrar>();

    public async Task AddBookToButikAsync(Butiker butik)
    {
        LagerSaldo bookToAdd = await MainWindow._dbContext.LagerSaldos
            .FirstOrDefaultAsync(l => l.ButikId == butik.ButikId && l.Isbn == this.Isbn);

        if(bookToAdd != null)
        {
            bookToAdd.Antal += 1;
        }
        else
        {
            bookToAdd = new LagerSaldo
            {
				ButikId = butik.ButikId,
				Isbn = this.Isbn,
                Antal = 1,
			};
        }
        await MainWindow._dbContext.SaveChangesAsync();
        MessageBox.Show($"Save successul. {bookToAdd.Antal}");
    }
}

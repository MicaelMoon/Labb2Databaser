﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows;
using Labb2Databaser.Data;
using Labb2Databaser.MyWindows;
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

    private Författare? authorExist;

	public async Task AddBookToButikAsync(Butiker butik, Böcker book) // get rid of the book
    {
        var lagerSaldoExist = await MainWindow._dbContext.LagerSaldos
            .FirstOrDefaultAsync(l => l.ButikId == butik.ButikId && l.Isbn == book.Isbn);

        if(lagerSaldoExist == null)
        {
            //Finns inget saldo. Skapa ett
            LagerSaldo lagerSaldo = new LagerSaldo();
            lagerSaldo.Isbn = book.Isbn;
            lagerSaldo.ButikId = butik.ButikId;
            lagerSaldo.Antal = 1;

            MainWindow._dbContext.LagerSaldos.Add(lagerSaldo);

            MessageBox.Show($"You've sucessfuly added \"{book.Titel}\" to the store \"{butik.ButikNamn}\"");
        }
        else
        {
			//LagerSaldo exist  Increase the antal by 1
			lagerSaldoExist.Antal += 1;
            MessageBox.Show($"You've added an additional copy of \"{book.Titel}\" to the store \"{butik.ButikNamn}\"");
		}

        await MainWindow._dbContext.SaveChangesAsync();
    }

    public async Task RemoveBookFromButikAsync(Butiker butik)
    {
		var lagerSaldoExist = await MainWindow._dbContext.LagerSaldos
	        .FirstOrDefaultAsync(l => l.ButikId == butik.ButikId && l.Isbn == this.Isbn);

        if (lagerSaldoExist == null || lagerSaldoExist.Antal == 0)
		{
			MessageBox.Show($"\"{butik.ButikNamn}\" does not have any copies of \"{this.Titel}\"");
		}
		else
		{
			lagerSaldoExist.Antal -= 1;
			MessageBox.Show($"You've removed a copy of \"{this.Titel}\" from the store \"{butik.ButikNamn}\"");
		}

		await MainWindow._dbContext.SaveChangesAsync();
	}
    public async Task AddBookToSystemAsync(string isbn, string titel, string language, int pages, int price, string releaseDate, string authorFirstName, string authorLastName)
    {
        this.Isbn = isbn;
        this.Titel = titel;
        this.Language = language;
        this.Sidor = pages;
        this.Pris = price;
        this.Utgivningsdatum = releaseDate;

		var authorExist = await MainWindow._dbContext.Författares
		.FirstOrDefaultAsync(f => f.FörNamn == authorFirstName && f.EfterNamn == authorLastName);

		if (authorExist != null)
        {
            this.FörfattarId = authorExist.FörfattarId;

			MainWindow._dbContext.Böckers.Add(this);
			MainWindow._dbContext.SaveChangesAsync();

			MessageBox.Show($"You've sucessfuly added {this.Titel} as a new book to our system");


		}
		else
        {
            NewAuthorWindow newAuthorWindow = new NewAuthorWindow();
            newAuthorWindow.FirstNameTextBox.Text += authorFirstName;
            newAuthorWindow.LastNameTextBox.Text += authorLastName;

            bool? result = newAuthorWindow.ShowDialog();

            if(result == true)
            {
				var authorNowExist = await MainWindow._dbContext.Författares
                    .FirstOrDefaultAsync(f => f.FörNamn == authorFirstName && f.EfterNamn == authorLastName);

				this.FörfattarId = authorNowExist.FörfattarId;

				MainWindow._dbContext.Böckers.Add(this);
				MainWindow._dbContext.SaveChangesAsync();

				MessageBox.Show($"You've sucessfuly added \"{newAuthorWindow.FirstNameTextBox.Text} {newAuthorWindow.LastNameTextBox.Text}\" as a new author to our system.\n As well as a new book called \"{this.Titel}\"");
			}
            else
            {
                MessageBox.Show("You failed to add the author");
            }

            //MainWindow._dbContext.SaveChangesAsync();
        }
        //'MainWindow._dbContext.Böckers.Add(this);' haveing this code here didnt work?
    }
}

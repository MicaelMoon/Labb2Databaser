using System;
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
	public async Task AddBookToButikAsync(Butiker butik, Böcker book)
    {
        bool antalExists = false;

        foreach(var l in MainWindow._dbContext.LagerSaldos)
        {
            if(l.ButikId == butik.ButikId && l.Isbn == this.Isbn)
            {
                l.Antal += 1;
            }
        }//Checks if the book you wanna add already exists. if so, onlu increase the antal by 1

        if(antalExists = false)
        {
            MainWindow._dbContext.Böckers.Add(book);
        }

        
        await MainWindow._dbContext.SaveChangesAsync();
        MessageBox.Show($"Save successul.{book.Titel}");
    }

    public async Task AddBookToSystemAsync(string isbn, string titel, string language, int pages, int price, string releaseDate, string authorFirstName, string authorLastName)
    {
        this.Isbn = isbn;
        this.Titel = titel;
        this.Language = language;
        this.Sidor = pages;
        this.Pris = price;
        this.Utgivningsdatum = releaseDate;

        Författare authorExist = null;

		try
		{
			await foreach (var f in MainWindow._dbContext.Författares)
			{
				if (f.FörNamn == authorFirstName && f.EfterNamn == authorLastName)
				{
					authorExist = f;
					break;
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Exception in foreach: {ex.Message}");
		}


		//var authorExist = await MainWindow._dbContext.Författares
		//.FirstOrDefaultAsync(f => f.FörNamn == authorFirstName && f.EfterNamn == authorLastName);

		if (authorExist != null)
        {
            this.FörfattarId = authorExist.FörfattarId;
        }
        else
        {
            NewAuthorWindow newAuthorWindow = new NewAuthorWindow();
            newAuthorWindow.FirstName.Text += authorFirstName;
            newAuthorWindow.LastName.Text += authorLastName;

            bool? result = newAuthorWindow.ShowDialog();

            if(result == true)
            {
                MessageBox.Show($"You've sucessfuly added {newAuthorWindow.FirstNameTextBox.Text} {newAuthorWindow.LastName.Text} as a new author to our system");
            }
            else
            {
                MessageBox.Show("You failed to add the author");
            }

            MessageBox.Show("Pause");

        }
    }
}

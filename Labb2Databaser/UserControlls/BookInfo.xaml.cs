using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Labb2Databaser.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Labb2Databaser.UserControlls
{
	/// <summary>
	/// Interaction logic for BookInfo.xaml
	/// </summary>
	public partial class BookInfo : UserControl
	{
		public Butiker? selectedButik;
		public Böcker? selectedBook;
		LagerSaldo selectedButikLagerSaldo;

		public BookInfo()
		{
			InitializeComponent();
		}

		public void BookInfoStartUp()
		{
			LoadData();
		}
		public async Task LoadData()
		{
			var butiker = MainWindow._dbContext.Butikers.ToList();

			if (selectedButik != null)
			{
				selectedButikLagerSaldo = await MainWindow._dbContext.LagerSaldos
					.FirstOrDefaultAsync(l => l.ButikId == selectedButik.ButikId);

				foreach(var l in MainWindow._dbContext.LagerSaldos)
				{
					if(l.ButikId == selectedButik.ButikId) //Jag hittade din butik i denna coliumn
					{
						var book = await MainWindow._dbContext.Böckers //**ERROR 
							.FirstOrDefaultAsync(b => b.Isbn == l.Isbn);

						BooksInStoreListBox.Items.Add($"{book}\nStock ={l.Antal}");
					}
				}
				BooksInStoreListBox.ItemsSource = selectedButikLagerSaldo.ToString();
			}

			ButikListBox.ItemsSource = butiker;

			/*
			if (selectedBook != null && selectedButik != null)
			{

				Result.Text = $"{selectedButik.ButikNamn}\nhas {booksInStore.Antal} copies of \n{selectedBook.Titel}\nin stock";
			}
			*/
		}

		private void ButikListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListBox listBox = sender as ListBox;
			selectedButik = listBox.SelectedItem as Butiker;

			LoadData();
		}

		private void BooksInStoreListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListBox listBox = sender as ListBox;

			selectedBook = sender as Böcker;
			LoadData();
		}
	}
}

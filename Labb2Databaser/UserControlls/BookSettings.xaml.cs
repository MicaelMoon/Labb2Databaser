using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Labb2Databaser.UserControlls
{
	public partial class BookSettings : UserControl
	{
		public static Böcker? selectedBook;
		public static Butiker? selectedButik;
		public BookSettings()
		{
			InitializeComponent();
		}

		public async Task LoadData()
		{
			var allBooks = MainWindow._dbContext.Böckers.ToList();
			var allButiks = MainWindow._dbContext.Butikers.ToList();

			BooksListbox.ItemsSource = allBooks;
			ButiksListbox.ItemsSource = allButiks;

			AddBookBtn.Visibility = Visibility.Collapsed;
			RemoveBookBtn.Visibility = Visibility.Collapsed;

			if (selectedBook != null)
			{
				AddBookText.Text = $"Add the book: {selectedBook.Titel}";
			}

			if(selectedButik != null)
			{
				AddBookText.Text += $"\nTo to butik: {selectedButik.ButikNamn}";
			}

			if(selectedButik !=null && selectedBook != null)
			{
				var lagerSaldoExist = await MainWindow._dbContext.LagerSaldos
					.FirstOrDefaultAsync(l => l.ButikId == selectedButik.ButikId && l.Isbn == selectedBook.Isbn);

				LagerSaldoTextBlock.Text = $"\"{selectedButik.ButikNamn}\" has {(lagerSaldoExist?.Antal ?? 0)} copies of \"{selectedBook.Titel}\" in stock";
				AddBookBtn.Visibility = Visibility.Visible;
				RemoveBookBtn.Visibility = Visibility.Visible;
				LagerSaldoTextBlock .Visibility = Visibility.Visible;
			}
		}

		private void ButiksCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListBox listBox1 = sender as ListBox;
			selectedButik = listBox1.SelectedItem as Butiker;

			LoadData();
		}

		private void BooksCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListBox listBox = sender as ListBox;
			selectedBook = listBox.SelectedItem as Böcker;

			LoadData();
		}

		private void AddBookBtn_Click(object sender, RoutedEventArgs e)
		{
			selectedBook.AddBookToButikAsync(selectedButik, selectedBook);
			LoadData();
		}

		private void RemoveBookBtn_Click(object sender, RoutedEventArgs e)
		{
			selectedBook.RemoveBookFromButikAsync(selectedButik);
			LoadData();
		}
	}
}

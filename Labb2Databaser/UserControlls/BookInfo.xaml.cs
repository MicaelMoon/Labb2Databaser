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
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Labb2Databaser.UserControlls
{
	/// <summary>
	/// Interaction logic for BookInfo.xaml
	/// </summary>
	public partial class BookInfo : UserControl
	{
		public bool selectedAsButik = false;
		public bool selectedAsBook = false;

		public Butiker? selectedButik;
		public Böcker? selectedBook;

		public BookInfo()
		{
			InitializeComponent();
		}

		public void BookInfoStartUp()
		{
			LoadData();
		}
		public void LoadData()
		{

			BookBtn.Visibility = Visibility.Visible;
			ButikBtn.Visibility = Visibility.Visible;

				if(selectedAsButik == true)//If you selected butik button then it will show you all butiks
			{
				var butiker = MainWindow._dbContext.Butikers.ToList();

				SelectedAsListBox.ItemsSource = butiker;
				SelectedAsListBox.Visibility = Visibility.Visible;

				if(selectedButik != null) //Fills listbox books of that the selected butik has
				{
					var lagerSaldo = MainWindow._dbContext.LagerSaldos.ToList();

					foreach(LagerSaldo l in lagerSaldo)
					{
						if(l.Isbn == selectedBook.Isbn || l.ButikId == selectedButik.ButikId)
						{
							RelatedBBListBox.Items.Add(l);
						}
					}
					RelatedBBListBox.DisplayMemberPath = "Titel";
				}
				SelectedAsListBox.DisplayMemberPath = "ButikNamn";
			}
			else if(selectedAsBook == true)
			{
				var books = MainWindow._dbContext.Böckers.ToList();

				SelectedAsListBox.ItemsSource = books;
				SelectedAsListBox.Visibility =Visibility.Visible;

				if(selectedAsBook != null)
				{
					
				}
			}
		}

		private void SelectedAsBtn_Click(object sender, RoutedEventArgs e)
		{
			Button button = sender as Button;

			if (button.Content is "Butiks")
			{
				selectedAsButik = true;
				selectedAsBook = false;
			}
			else if(button.Content is "Books")
			{
				selectedAsBook = true;
				selectedAsButik = false;
			}
			
			LoadData();
		}

		private void SelectedAsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListBox listBox = sender as ListBox;

			if(listBox.SelectedItem is Böcker)
			{
				var currentBook = listBox.SelectedItem as Böcker;

				var butiks = MainWindow._dbContext.Butikers.ToList();

				RelatedBBListBox.ItemsSource = butiks;
			}
			else if(listBox.SelectedItem is Butiker)
			{
				var currentButik = listBox.SelectedItem as Butiker;

				var books = MainWindow._dbContext.Böckers.ToList();

				RelatedBBListBox.ItemsSource= books;

				RelatedBBListBox.DisplayMemberPath = "Titel";
			}

			RelatedBBListBox.Visibility = Visibility.Visible;

			LoadData();
		}

		private void RelatedBBListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int amount = 0;
			ListBox listbox = sender as ListBox;

			if (selectedAsBook)
			{
				foreach (LagerSaldo l in MainWindow._dbContext.LagerSaldos)
				{
					if (l.Isbn == selectedBook.Isbn && l.ButikId == selectedButik.ButikId)
					{
						amount += 1;
					}
				}
			}
			MessageBox.Show($"Result: {amount}");
		}
	}
}

using System;
using System.Collections.Generic;
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

			SelectedAsListBox.Items.Clear();

			if(selectedAsButik == true)//If you selected butik button then it will show you all butiks
			{
				var butiker = MainWindow._dbContext.Butikers.ToList();

				SelectedAsListBox.ItemsSource = butiker;
				SelectedAsListBox.Visibility = Visibility.Visible;

				if(selectedAsButik != null) //Fills listbox books of that the selected butik has
				{
					var lagerSaldo = MainWindow._dbContext.LagerSaldos.ToList();

					foreach(LagerSaldo l in lagerSaldo)
					{
						if(l.Isbn == selectedBook.Isbn || l.ButikId == selectedButik.ButikId)
						{
							RelatedBBListBox.Items.Add(l);
						}
					}
				}
			}
			else if(selectedAsBook == true)
			{
				var books = MainWindow._dbContext.Böckers.ToList();

				SelectedAsListBox.ItemsSource = books;
				SelectedAsListBox.Visibility =Visibility.Visible;
			}
		}

		private void ButikBtn_Click(object sender, RoutedEventArgs e)
		{
			selectedAsButik = true;
			selectedAsBook = false;
			
			LoadData();
		}

		private void BookBtn_Click(object sender, RoutedEventArgs e)
		{
			selectedAsBook = true;
			selectedAsButik = false;

			LoadData();
		}

		private void SelectedAsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListBox listBox = sender as ListBox;

			if(listBox.SelectedItem is Böcker)
			{

			}
		}
	}
}

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

namespace Labb2Databaser.UserControlls
{
	/// <summary>
	/// Interaction logic for NewBookUC.xaml
	/// </summary>
	public partial class NewBookUC : UserControl
	{
		public Författare selectedAuthor;

		public NewBookUC()
		{
			InitializeComponent();
			LoadData();
		}

		private async Task LoadData()
		{
			var allAuthors = MainWindow._dbContext.Författares.ToList();

			AuthorComboBox.ItemsSource = allAuthors;

			if(selectedAuthor != null)
			{
				AuthorFirstNameTextBox.Text = selectedAuthor.FörNamn;
				AuthorLastNameTextBox.Text = selectedAuthor.EfterNamn;
			}
		}

		private void SubmitBtn_Click(object sender, RoutedEventArgs e)
		{
			Böcker book = new Böcker();

			string title = TitleTextBox.Text;
			string isbn = ISBNTextBox.Text;
			string authorFirstName = AuthorFirstNameTextBox.Text;
			string authorLastName = AuthorLastNameTextBox.Text;
			string releaseDate = ReleaseDateTextBox.Text;
			string language = LanguageTextBox.Text;
			string pagesString = PagesTextBox.Text;
			string priceString = PriceTextBox.Text;

			int pages = 0;
			int price = 0;

			int.TryParse(pagesString, out pages);
			int.TryParse(priceString, out price);


			if (pages != 0 && price != 0)
			{
				try
				{
					book.AddBookToSystemAsync(isbn, title, language, pages, price, releaseDate, authorFirstName, authorLastName);
					MainWindow._dbContext.Böckers.Add(book);
				}
				catch (Exception InvalidOperationException)
				{
					MessageBox.Show("You need to enter a unik ISBN");
				}
			}
			else if (pages == 0)
			{
				MessageBox.Show("You need to ender the amount pages");
			}
			else if (price == 0)
			{
				MessageBox.Show("You need to ender a price");
			}
		}

		private void AuthorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			selectedAuthor = comboBox.SelectedItem as Författare;

			ComboboxPlaceholder.Text = selectedAuthor.FörNamn;
			ComboboxPlaceholder.Visibility = Visibility.Collapsed;

			LoadData();
		}
	}
}

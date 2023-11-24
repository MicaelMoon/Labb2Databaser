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
		public NewBookUC()
		{
			InitializeComponent();
		}

		private void SubmitBtn_Click(object sender, RoutedEventArgs e)
		{
			Böcker book = new Böcker();

			string title = TitleTextBox.Text;
			string isbn = ISBNTextBox.Text;
			string author = AuthorTextBox.Text;
			string releaseDate = ReleaseDateTextBox.Text;
			string language = LanguageTextBox.Text;
			int pages = int.Parse(PriceTextBox.Text);
			int price = int.Parse(PriceTextBox.Text);


			book.AddBookToSystemAsync(isbn, title, language, pages, price, releaseDate, author);
		}
	}
}

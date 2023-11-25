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
using System.Windows.Shapes;
using Labb2Databaser.Models;

namespace Labb2Databaser.MyWindows
{
	/// <summary>
	/// Interaction logic for NewAuthorWindow.xaml
	/// </summary>
	public partial class NewAuthorWindow : Window
	{
		public NewAuthorWindow()
		{
			InitializeComponent();
			TitleText.Text = "Your selected author is not registred in our system." +
				"\nPlease fill in all the fields to register a new one";
		}

		private void SubmitAuthor_Click(object sender, RoutedEventArgs e)
		{
			Författare author = new Författare();
		}
	}
}

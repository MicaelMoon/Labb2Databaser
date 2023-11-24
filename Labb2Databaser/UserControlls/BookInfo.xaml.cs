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

		public BookInfo()
		{
			InitializeComponent();
		}

		public void LoadData()
		{
			BookBtn.Visibility = Visibility.Visible;
			ButikBtn.Visibility = Visibility.Visible;


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
	}
}

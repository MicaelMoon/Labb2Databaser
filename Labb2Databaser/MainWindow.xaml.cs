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
using Labb2Databaser.Data;
using Labb2Databaser.UserControlls;

namespace Labb2Databaser
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static LibraryDBContext _dbContext;
		public BookSettings bookSettings;
		public BookInfo bookInfo;
		
		public MainWindow()
		{
			InitializeComponent();
			_dbContext = new LibraryDBContext();
		}


		private void BookSettings_Click(object sender, RoutedEventArgs e)
		{
			bookSettings = new BookSettings();
			//bookSettings.SetValue(Grid.WidthProperty());

			this.MainGrid.Children.Add(bookSettings);


			Grid.SetRow(bookSettings, 2);
			Grid.SetColumn(bookSettings, 2);

			bookSettings.LoadData();
		}

		private void InfoBtn_Click(object sender, RoutedEventArgs e)
		{
			bookInfo.BookInfoStartUp();
		}
	}
}

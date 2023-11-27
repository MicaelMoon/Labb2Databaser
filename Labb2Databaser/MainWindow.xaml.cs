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
using Labb2Databaser.Models;
using Labb2Databaser.MyWindows;
using Labb2Databaser.UserControlls;
using Microsoft.EntityFrameworkCore;

namespace Labb2Databaser
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static LibraryDBContext _dbContext;

		public BookSettings bookSettings;
		public NewBookUC newBookUC;

		public MainWindow()
		{
			InitializeComponent();
			_dbContext = new LibraryDBContext();
		}

		private void BookSettingsbtn_Click(object sender, RoutedEventArgs e)
		{
			RemoveExistingUserControls();

			bookSettings = new BookSettings();

			this.MainGrid.Children.Add(bookSettings);


			Grid.SetRow(bookSettings, 2);
			Grid.SetColumn(bookSettings, 2);

			bookSettings.LoadData();
		}

		private void AddBookToSystem_Click(object sender, RoutedEventArgs e)
		{
			RemoveExistingUserControls();
			newBookUC = new NewBookUC();

			this.MainGrid.Children.Add(newBookUC);

			Grid.SetRow(newBookUC, 2);
			Grid.SetColumn(newBookUC, 2);
		}

		private void RemoveExistingUserControls()
		{
			MainGrid.Children.Remove(newBookUC);
			MainGrid.Children.Remove(bookSettings);
		}

		private void NewAuthorBtn_Click(object sender, RoutedEventArgs e)
		{
			RegisterAuthorAsync();
		}
		private async Task RegisterAuthorAsync()
		{
			NewAuthorWindow newAuthorWindow = new NewAuthorWindow();

			bool? result = newAuthorWindow.ShowDialog();

			if (result == true)
			{
				var authorNowExist = await MainWindow._dbContext.Författares
					.FirstOrDefaultAsync(f => f.FörNamn == authorFirstName && f.EfterNamn == authorLastName);

				this.FörfattarId = authorNowExist.FörfattarId;

				MainWindow._dbContext.Böckers.Add(this);
				MainWindow._dbContext.SaveChangesAsync();
				MessageBox.Show($"You've sucessfuly added {newAuthorWindow.FirstNameTextBox.Text} {newAuthorWindow.LastNameTextBox.Text} as a new author to our system");
			}
			else
			{
				MessageBox.Show("You failed to add the author");
			}
		}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Labb2Databaser.UserControlls
{
    public partial class DeleteDataUC : UserControl
    {
		public Böcker selectedBook;
		public Författare selectedAuthor;

		public DeleteDataUC()
        {
            InitializeComponent();
        }

		private void ShowBooksBtn_Click(object sender, RoutedEventArgs e)
		{
			var allBooks = MainWindow._dbContext.Böckers.ToList();

			DataListBoxTitle.Text = "Books";

			DataListBox.ItemsSource = allBooks;

			DataListBox.DisplayMemberPath = "Titel";

			UpdateBtn.Click += ShowAuthorsBtn_Click;
		}

		private void ShowAuthorsBtn_Click(object sender, RoutedEventArgs e)
		{
			var allAuthors = MainWindow._dbContext.Författares.ToList();

			DataListBoxTitle.Text = "Authors";

			DataListBox.ItemsSource = allAuthors;

			DataListBox.DisplayMemberPath = "FullName";

			UpdateBtn.Click += ShowBooksBtn_Click;
		}

		private void DataListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            ListBox listbox = sender as ListBox;
            
            if(DataListBox.SelectedItem is Böcker)
            {
                selectedBook = DataListBox.SelectedItem as Böcker;
            }
            else if(DataListBox.SelectedItem is Författare)
            {
                selectedAuthor = DataListBox.SelectedItem as Författare;
            }
		}

		private void DeleteBtn_Click(object sender, RoutedEventArgs e)
		{
            DeleteDataAsync();
		}

        private async Task DeleteDataAsync()
        {
			string message = "You've successfully deleted ";

			if (DataListBox.SelectedItem is Böcker)
			{
				MainWindow._dbContext.Böckers.Remove(DataListBox.SelectedItem as Böcker);
				message += selectedBook.Titel;
				UpdateBtn.Click += ShowBooksBtn_Click;
			}
			else
			{
				MainWindow._dbContext.Författares.Remove(DataListBox.SelectedItem as Författare);
				message += selectedAuthor.FullName;
				UpdateBtn.Click += ShowAuthorsBtn_Click;
			}

            await MainWindow._dbContext.SaveChangesAsync();

            MessageBox.Show(message);

			selectedBook = null;
			selectedAuthor = null;

		}
	}
}

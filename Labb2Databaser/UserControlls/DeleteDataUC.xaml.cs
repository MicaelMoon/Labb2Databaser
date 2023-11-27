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

namespace Labb2Databaser.UserControlls
{
    public partial class DeleteDataUC : UserControl
    {
        public bool isBook;

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

			isBook = true;
		}

        private async Task LoadData()
        {

        }
		private void ShowAuthorsBtn_Click(object sender, RoutedEventArgs e)
		{
            var allAuthors = MainWindow._dbContext.Författares.ToList();

            DataListBoxTitle.Text = "Authors";

            DataListBox.ItemsSource = allAuthors;

			DataListBox.DisplayMemberPath = "FullName";

			isBook = false;
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
			if (isBook == true)
			{
				MainWindow._dbContext.Böckers.Remove(selectedBook);
			}
			else
			{
				MainWindow._dbContext.Författares.Remove(selectedAuthor);
			}

            await MainWindow._dbContext.SaveChangesAsync();

            MessageBox.Show($"You've successfully deleted {(isBook ? selectedBook.Titel : selectedAuthor.FullName)}");

			selectedBook = null;
			selectedAuthor = null;

		}
	}
}

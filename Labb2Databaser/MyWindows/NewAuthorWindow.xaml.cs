﻿using System;
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

			DateTime currentDate = DateTime.Now;
			BirthDateTextBox.Text = currentDate.ToString("yyyy-MM-dd");
		}

		private async Task SubmitAuthorAsync()
		{
			Författare author = new Författare
			{
				FörNamn = FirstNameTextBox.Text,
				EfterNamn = LastNameTextBox.Text,
				FödelseDatum = BirthDateTextBox.Text
			};

			MainWindow._dbContext.Författares.Add(author);

			await MainWindow._dbContext.SaveChangesAsync();
			this.DialogResult = true;
			this.Close();
		} //Could have a method in Författare that saves.

		private void SubmitAuthor_Click(object sender, RoutedEventArgs e)
		{
			SubmitAuthorAsync();
		}
	}
}

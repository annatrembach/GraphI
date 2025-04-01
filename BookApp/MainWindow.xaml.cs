using System;
using System.Data;
using System.Windows;

namespace BookApp
{
    public partial class MainWindow : Window
    {
        private readonly DatabaseAccess _databaseAccess;

        public MainWindow()
        {
            InitializeComponent();
            _databaseAccess = new DatabaseAccess();
        }

        private void LoadBooks_Click(object sender, RoutedEventArgs e)
        {
            DataTable booksTable = _databaseAccess.GetBooks();

            if (booksTable.Rows.Count == 0)
            {
                MessageBox.Show("Дані не знайдено!", "Увага", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            BooksDataGrid.ItemsSource = booksTable.DefaultView;
        }

        private void CreateBook_Click(object sender, RoutedEventArgs e)
        {
            int id = _databaseAccess.GetNextId();
            int isbn = int.Parse(ISBNTextBox.Text);
            string name = NameTextBox.Text;
            string author = AuthorTextBox.Text;
            string publisher = PublisherTextBox.Text;
            int year = int.Parse(YearTextBox.Text);

            _databaseAccess.AddBook(id, isbn, name, author, publisher, year);
            LoadBooks_Click(sender, e);
        }

        private void UpdateBook_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is DataRowView selectedRow)
            {
                int id = (int)selectedRow["Id"];
                int isbn = int.Parse(ISBNTextBox.Text);
                string name = NameTextBox.Text;
                string author = AuthorTextBox.Text;
                string publisher = PublisherTextBox.Text;
                int year = int.Parse(YearTextBox.Text);

                _databaseAccess.UpdateBook(id, isbn, name, author, publisher, year);
                LoadBooks_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть книгу для оновлення.");
            }
        }


        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is DataRowView selectedRow)
            {
                int id = (int)selectedRow["Id"];

                _databaseAccess.DeleteBook(id);
                LoadBooks_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть книгу для видалення.");
            }
        }

    }
}

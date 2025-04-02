using System;
using System.Data;
using System.Windows;
using System.Collections.ObjectModel;

namespace BookApp
{
    public partial class MainWindow : Window
    {
        private readonly DatabaseAccess _databaseAccess;

        public ObservableCollection<Book> Books { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _databaseAccess = new DatabaseAccess();
            Books = new ObservableCollection<Book>();
            BooksListBox.ItemsSource = Books;
        }

        private void LoadBooks_Click(object sender, RoutedEventArgs e)
        {
            DataTable booksTable = _databaseAccess.GetBooks();

            if (booksTable.Rows.Count == 0)
            {
                MessageBox.Show("Дані не знайдено!", "Увага", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            Books.Clear();
            foreach (DataRow row in booksTable.Rows)
            {
                Books.Add(new Book
                {
                    Id = (int)row["Id"],
                    ISBN = (int)row["ISBN"],
                    Name = (string)row["Name"],
                    Author = (string)row["Author"],
                    Publisher = (string)row["Publisher"],
                    Year = (int)row["Year"]
                });
            }

            // If you still want to show data in a different way, use BooksListBox for selection.
        }

        private void BooksListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (BooksListBox.SelectedItem is Book selectedBook)
            {
                ISBNTextBox.Text = selectedBook.ISBN.ToString();
                NameTextBox.Text = selectedBook.Name;
                AuthorTextBox.Text = selectedBook.Author;
                PublisherTextBox.Text = selectedBook.Publisher;
                YearTextBox.Text = selectedBook.Year.ToString();
            }
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
            if (BooksListBox.SelectedItem is Book selectedBook)
            {
                int id = selectedBook.Id;
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
            if (BooksListBox.SelectedItem is Book selectedBook)
            {
                int id = selectedBook.Id;

                _databaseAccess.DeleteBook(id);
                LoadBooks_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть книгу для видалення.");
            }
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public int ISBN { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
    }
}

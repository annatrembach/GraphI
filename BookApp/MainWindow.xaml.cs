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

    }
}

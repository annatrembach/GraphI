using System;
using System.Configuration;
using System.Windows;

namespace BookApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetConnectionString();
        }

        private void GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"]?.ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Рядок з'єднання не знайдено!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Рядок з'єднання: " + connectionString, "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

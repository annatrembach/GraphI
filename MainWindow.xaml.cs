using System.Windows;
using System.Windows.Input;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CommandBinding saveBinding = new CommandBinding(
                ApplicationCommands.Save, Save_Executed, Save_CanExecute);
            this.CommandBindings.Add(saveBinding);

            CommandBinding openBinding = new CommandBinding(
                ApplicationCommands.Open, Open_Executed, Open_CanExecute);
            this.CommandBindings.Add(openBinding);

            CommandBinding deleteBinding = new CommandBinding(
                ApplicationCommands.Delete, Delete_Executed, Delete_CanExecute);
            this.CommandBindings.Add(deleteBinding);

        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Команда Save виконана.");
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Команда Open виконана.");
        }

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Команда Delete виконана.");
        }
    }
}

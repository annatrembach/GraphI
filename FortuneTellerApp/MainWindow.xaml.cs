using System;
using System.Windows;
using System.Windows.Input;

namespace FortuneTellerApp
{
    public partial class MainWindow : Window
    {
        private readonly string[] responses = { "Так", "Ні", "Скоріше так", "Скоріше ні" };
        public ICommand AskCommand { get; }

        public MainWindow()
        {
            InitializeComponent();
            AskCommand = new RelayCommand(GenerateResponse);
            DataContext = this;
        }

        private void GenerateResponse(object parameter)
        {
            Random random = new Random();
            ResponseText.Text = responses[random.Next(responses.Length)];
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object parameter) => _execute(parameter);
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}

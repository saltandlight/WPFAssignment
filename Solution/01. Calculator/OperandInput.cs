using System;
using System.Windows.Input;

namespace Calculator
{
    internal class OperandInput : ICommand
    {
        private MainWindowViewModel mainWindowViewModel;
        public event EventHandler CanExecuteChanged;

        public OperandInput(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mainWindowViewModel.inputText += parameter;
        }
    }
}
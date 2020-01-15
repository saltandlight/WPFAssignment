using System;
using System.Windows.Input;

namespace Calculator
{
    internal class OperatorInput : ICommand
    {
        private MainWindowViewModel mainWindowViewModel;
        public event EventHandler CanExecuteChanged;
        public OperatorInput(MainWindowViewModel mainWindowViewModel)
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
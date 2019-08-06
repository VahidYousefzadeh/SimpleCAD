using System;
using System.Windows.Input;

namespace Viewer.Dialogs
{
    public sealed class Command : ICommand
    {
        private readonly Predicate<object> m_canExecute;
        private readonly Action<object> m_execute;

        public Command(Action<object> execute, Predicate<object> canExecute)
        {
            m_canExecute = canExecute;
            m_execute = execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return m_canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            m_execute(parameter);
        }
    }
}
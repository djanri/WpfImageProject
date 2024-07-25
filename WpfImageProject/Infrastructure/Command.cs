using System.Windows.Input;

namespace WpfImageNet8.Infrastructure
{
    public class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        Action<object> _executeCallback;
        Func<object, bool> _canExecuteCallback;

        public Command(Action<object> executeCallback, Func<object, bool> canExecuteCallback = null)
        {
            _executeCallback = executeCallback;
            _canExecuteCallback = canExecuteCallback;
        }

        private void OnCanExecuteChanged()
        {
            var evt = CanExecuteChanged;
            if (evt != null) evt(this, EventArgs.Empty);
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecuteCallback == null || _canExecuteCallback(parameter);
        }

        public void Execute(object? parameter)
        {
            _executeCallback(parameter);
        }
    }
}

using System;
using System.Windows.Input;

namespace FreePcName.Model
{
    /// <summary>
    /// Реализация команды.
    /// </summary>
    class RelayCommand : ICommand
    {
        /// <summary>
        /// Делегат для метода выполнения.
        /// </summary>
        private readonly Action<object> execute;

        /// <summary>
        /// Делегат для метода проверки доступности выполнения.
        /// </summary>
        private readonly Func<object, bool> canExecute;

        /// <summary>
        /// Событие изменения доступности выполнения команды.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// Конструктор команды.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Метод проверки доступности вызова делегата.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return canExecute is null || canExecute(parameter);
        }

        /// <summary>
        /// Метод вызова делегата выполнения команды.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
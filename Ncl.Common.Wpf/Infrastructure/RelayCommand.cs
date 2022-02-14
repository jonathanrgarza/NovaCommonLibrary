using System;
using System.Windows.Input;

namespace Ncl.Common.Wpf.Infrastructure
{
    /// <summary>
    ///     Represents a command which relays a command call by executing the given delegates.
    ///     This command does not support command parameters.
    /// </summary>
    public class RelayCommand : IRelayCommand
    {
        private readonly Func<bool>? _canExecuteFunction;
        private readonly Action _executeAction;

        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommand" />.
        /// </summary>
        /// <param name="executeAction">The action to execute when this command is executed.</param>
        /// <param name="canExecuteFunction">
        ///     The function to call when CanExecute is called.
        ///     A default value results in the command always return true for CanExecute. Default is null.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="executeAction" /> is null.</exception>
        public RelayCommand(Action executeAction, Func<bool>? canExecuteFunction = null)
        {
            _executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
            _canExecuteFunction = canExecuteFunction;
        }

        /// <summary>
        ///     Determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if this command can be executed, otherwise, <see langword="false" />.
        /// </returns>
        public bool CanExecute()
        {
            return _canExecuteFunction == null || _canExecuteFunction();
        }

        /// <summary>
        ///     Executes the command.
        /// </summary>
        public void Execute()
        {
            _executeAction();
        }

        /// <summary>
        ///     Raises the <see cref="CanExecuteChanged" /> event so that listeners can update
        ///     based on the <see cref="RelayCommand" />'s new state.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc />
        bool ICommand.CanExecute(object? parameter)
        {
            return CanExecute();
        }

        /// <inheritdoc />
        void ICommand.Execute(object? parameter)
        {
            Execute();
        }

        /// <inheritdoc />
        public event EventHandler? CanExecuteChanged;
    }

    /// <summary>
    ///     Represents a command which relays a command call by executing the given delegates.
    ///     This command does support a command parameter.
    /// </summary>
    /// <typeparam name="T">The command parameter's type.</typeparam>
    public class RelayCommand<T> : IRelayCommand<T>
    {
        private readonly Func<T?, bool>? _canExecuteFunction;
        private readonly Action<T?> _executeAction;

        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommand" />.
        /// </summary>
        /// <param name="executeAction">The action to execute when this command is executed.</param>
        /// <param name="canExecuteFunction">
        ///     The function to call when CanExecute is called.
        ///     A default value results in the command always return true for CanExecute. Default is null.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="executeAction" /> is null.</exception>
        public RelayCommand(Action<T?> executeAction, Func<T?, bool>? canExecuteFunction = null)
        {
            _executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
            _canExecuteFunction = canExecuteFunction;
        }

        /// <summary>
        ///     Determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>
        ///     <see langword="true" /> if this command can be executed, otherwise, <see langword="false" />.
        /// </returns>
        public bool CanExecute(T? parameter)
        {
            return _canExecuteFunction == null || _canExecuteFunction(parameter);
        }

        /// <summary>
        ///     Executes the command.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        public void Execute(T? parameter)
        {
            _executeAction(parameter);
        }

        /// <summary>
        ///     Raises the <see cref="CanExecuteChanged" /> event so that listeners can update
        ///     based on the <see cref="RelayCommand" />'s new state.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc />
        bool ICommand.CanExecute(object? parameter)
        {
            return CanExecute((T?) parameter);
        }

        /// <inheritdoc />
        void ICommand.Execute(object? parameter)
        {
            Execute((T?) parameter);
        }

        /// <inheritdoc />
        public event EventHandler? CanExecuteChanged;
    }
}
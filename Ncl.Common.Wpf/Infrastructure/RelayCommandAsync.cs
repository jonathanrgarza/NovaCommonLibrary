using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Ncl.Common.Core.Extensions;
using Ncl.Common.Core.Infrastructure;

namespace Ncl.Common.Wpf.Infrastructure
{
    /// <summary>
    ///     Represents an asynchronous command which relays a command call by executing the given delegates.
    ///     This command does not support command parameters.
    /// </summary>
    public class RelayCommandAsync : IRelayCommandAsync
    {
        private readonly Func<bool> _canExecuteFunction;
        private readonly Action<Exception> _exceptionHandleAction;
        private readonly Func<Task> _executeFunction;

        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommandAsync" />.
        /// </summary>
        /// <param name="executeFunction">The function to execute when this command is executed.</param>
        /// <param name="canExecuteFunction">
        ///     The function to call when CanExecute is called.
        ///     A default value results in the command always return true for CanExecute. Default is null.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="executeFunction" /> is null.</exception>
        public RelayCommandAsync(Func<Task> executeFunction, Func<bool> canExecuteFunction = null)
        {
            _executeFunction = executeFunction ?? throw new ArgumentNullException(nameof(executeFunction));
            _canExecuteFunction = canExecuteFunction;
            _exceptionHandleAction = null;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommandAsync" />.
        /// </summary>
        /// <param name="executeFunction">The function to execute when this command is executed.</param>
        /// <param name="canExecuteFunction">
        ///     The function to call when CanExecute is called.
        ///     A default value results in the command always return true for CanExecute. Default is null.
        /// </param>
        /// <param name="exceptionHandlerAction">
        ///     The exception handler delegate. If null, exceptions will be swallowed.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="executeFunction" /> is null.</exception>
        public RelayCommandAsync(Func<Task> executeFunction, Func<bool> canExecuteFunction,
            Action<Exception> exceptionHandlerAction)
        {
            _executeFunction = executeFunction ?? throw new ArgumentNullException(nameof(executeFunction));
            _canExecuteFunction = canExecuteFunction;
            _exceptionHandleAction = exceptionHandlerAction;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommandAsync" />.
        /// </summary>
        /// <param name="executeFunction">The function to execute when this command is executed.</param>
        /// <param name="canExecuteFunction">
        ///     The function to call when CanExecute is called.
        ///     A default value results in the command always return true for CanExecute. Default is null.
        /// </param>
        /// <param name="exceptionHandler">
        ///     The exception handler. If null, exceptions will be swallowed.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="executeFunction" /> is null.</exception>
        public RelayCommandAsync(Func<Task> executeFunction, Func<bool> canExecuteFunction,
            IExceptionHandler exceptionHandler)
        {
            _executeFunction = executeFunction ?? throw new ArgumentNullException(nameof(executeFunction));
            _canExecuteFunction = canExecuteFunction;

            if (exceptionHandler == null)
                return;
            _exceptionHandleAction = exceptionHandler.HandleException;
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
        ///     Executes the command asynchronously.
        /// </summary>
        /// <returns>The <see cref="Task" /> which represents the asynchronous operation.</returns>
        public Task ExecuteAsync()
        {
            return _executeFunction();
        }

        /// <summary>
        ///     Raises the <see cref="CanExecuteChanged" /> event so that listeners can update
        ///     based on the <see cref="RelayCommandAsync" />'s new state.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc />
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        /// <inheritdoc />
        void ICommand.Execute(object parameter)
        {
            ExecuteAsync().FireAndForgetSafe(_exceptionHandleAction);
        }

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;
    }

    /// <summary>
    ///     Represents an asynchronous command which relays a command call by executing the given delegates.
    ///     This command does support a command parameter.
    /// </summary>
    /// <typeparam name="T">The command parameter's type.</typeparam>
    public class RelayCommandAsync<T> : IRelayCommandAsync<T>
    {
        private readonly Func<T, bool> _canExecuteFunction;
        private readonly Action<Exception> _exceptionHandleAction;
        private readonly Func<T, Task> _executeFunction;

        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommandAsync{T}" />.
        /// </summary>
        /// <param name="executeFunction">The function to execute when this command is executed.</param>
        /// <param name="canExecuteFunction">
        ///     The function to call when CanExecute is called.
        ///     A default value results in the command always return true for CanExecute. Default is null.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="executeFunction" /> is null.</exception>
        public RelayCommandAsync(Func<T, Task> executeFunction, Func<T, bool> canExecuteFunction = null)
        {
            _executeFunction = executeFunction ?? throw new ArgumentNullException(nameof(executeFunction));
            _canExecuteFunction = canExecuteFunction;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommandAsync" />.
        /// </summary>
        /// <param name="executeFunction">The function to execute when this command is executed.</param>
        /// <param name="canExecuteFunction">
        ///     The function to call when CanExecute is called.
        ///     A default value results in the command always return true for CanExecute. Default is null.
        /// </param>
        /// <param name="exceptionHandlerAction">
        ///     The exception handler delegate. If null, exceptions will be swallowed.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="executeFunction" /> is null.</exception>
        public RelayCommandAsync(Func<T, Task> executeFunction, Func<T, bool> canExecuteFunction,
            Action<Exception> exceptionHandlerAction)
        {
            _executeFunction = executeFunction ?? throw new ArgumentNullException(nameof(executeFunction));
            _canExecuteFunction = canExecuteFunction;
            _exceptionHandleAction = exceptionHandlerAction;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommandAsync" />.
        /// </summary>
        /// <param name="executeFunction">The function to execute when this command is executed.</param>
        /// <param name="canExecuteFunction">
        ///     The function to call when CanExecute is called.
        ///     A default value results in the command always return true for CanExecute. Default is null.
        /// </param>
        /// <param name="exceptionHandler">The exception handler. If null, exceptions will be swallowed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="executeFunction" /> is null.</exception>
        public RelayCommandAsync(Func<T, Task> executeFunction, Func<T, bool> canExecuteFunction,
            IExceptionHandler exceptionHandler)
        {
            _executeFunction = executeFunction ?? throw new ArgumentNullException(nameof(executeFunction));
            _canExecuteFunction = canExecuteFunction;

            if (exceptionHandler == null)
                return;
            _exceptionHandleAction = exceptionHandler.HandleException;
        }

        /// <summary>
        ///     Determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>
        ///     <see langword="true" /> if this command can be executed, otherwise, <see langword="false" />.
        /// </returns>
        public bool CanExecute(T parameter)
        {
            return _canExecuteFunction == null || _canExecuteFunction(parameter);
        }

        /// <summary>
        ///     Executes the command.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        public Task ExecuteAsync(T parameter)
        {
            return _executeFunction(parameter);
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
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T) parameter);
        }

        /// <inheritdoc />
        void ICommand.Execute(object parameter)
        {
            ExecuteAsync((T) parameter).FireAndForgetSafe(_exceptionHandleAction);
        }

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;
    }
}
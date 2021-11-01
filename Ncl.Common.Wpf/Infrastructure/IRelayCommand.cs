using System.Windows.Input;

namespace Ncl.Common.Wpf.Infrastructure
{
    /// <summary>
    ///     The interface for a relay command which relays a command call by executing delegates.
    ///     This command does not support command parameters.
    /// </summary>
    public interface IRelayCommand : ICommand
    {
        /// <summary>
        ///     Determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if this command can be executed, otherwise, <see langword="false" />.
        /// </returns>
        bool CanExecute();

        /// <summary>
        ///     Executes the command.
        /// </summary>
        void Execute();

        /// <summary>
        ///     Raises the <see cref="IRelayCommand.CanExecuteChanged" /> event so that listeners can update
        ///     based on the <see cref="IRelayCommand" />'s new state.
        /// </summary>
        void RaiseCanExecuteChanged();
    }

    /// <summary>
    ///     The interface for a relay command which relays a command call by executing delegates.
    ///     This command does support a command parameter.
    /// </summary>
    /// <typeparam name="T">The command parameter's type.</typeparam>
    public interface IRelayCommand<in T> : ICommand
    {
        /// <summary>
        ///     Determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>
        ///     <see langword="true" /> if this command can be executed, otherwise, <see langword="false" />.
        /// </returns>
        bool CanExecute(T parameter);

        /// <summary>
        ///     Executes the command.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        void Execute(T parameter);

        /// <summary>
        ///     Raises the <see cref="IRelayCommand{T}.CanExecuteChanged" /> event so that listeners can update
        ///     based on the <see cref="IRelayCommand{T}" />'s new state.
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}
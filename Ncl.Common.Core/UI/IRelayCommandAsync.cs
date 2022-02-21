using System.Threading.Tasks;
using System.Windows.Input;

namespace Ncl.Common.Core.UI
{
    /// <summary>
    ///     The interface for an asynchronous relay command which relays a command call by executing delegates.
    ///     This command does not support command parameters.
    /// </summary>
    public interface IRelayCommandAsync : ICommand
    {
        /// <summary>
        ///     Determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if this command can be executed, otherwise, <see langword="false" />.
        /// </returns>
        bool CanExecute();

        /// <summary>
        ///     Executes the command asynchronously.
        /// </summary>
        /// <returns>The <see cref="Task" /> which represents the asynchronous operation.</returns>
        Task ExecuteAsync();

        /// <summary>
        ///     Raises the <see cref="IRelayCommandAsync.CanExecuteChanged" /> event so that listeners can update
        ///     based on the <see cref="IRelayCommandAsync" />'s new state.
        /// </summary>
        void RaiseCanExecuteChanged();
    }

    /// <summary>
    ///     The interface for an asynchronous relay command which relays a command call by executing delegates.
    ///     This command does support a command parameter.
    /// </summary>
    /// <typeparam name="T">The command parameter's type.</typeparam>
    public interface IRelayCommandAsync<in T> : ICommand
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
        ///     Executes the command, asynchronously.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>The <see cref="Task" /> which represents the asynchronous operation.</returns>
        Task ExecuteAsync(T parameter);

        /// <summary>
        ///     Raises the <see cref="IRelayCommandAsync{T}.CanExecuteChanged" /> event so that listeners can update
        ///     based on the <see cref="IRelayCommandAsync{T}" />'s new state.
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}
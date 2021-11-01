using System;
using System.Threading.Tasks;
using Ncl.Common.Core.Infrastructure;

namespace Ncl.Common.Core.Extensions
{
    /// <summary>
    ///     Extensions for <see cref="Task"/>.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        ///     Runs an asynchronous task and returns immediately without waiting for completion.
        ///     Safely handles any exception that occur by calling the given error handler delegate.
        /// </summary>
        /// <param name="task">The task to run.</param>
        /// <param name="errorHandlerAction">
        ///     The error handler delegate for any exception that might occur. If null, exceptions will be swallowed.
        /// </param>
        public static async void FireAndForgetSafe(this Task task, Action<Exception> errorHandlerAction)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception e)
            {
                errorHandlerAction?.Invoke(e);
            }
        }

        /// <summary>
        ///     Runs an asynchronous task and returns immediately without waiting for completion.
        ///     Safely handles any exception that occur by calling the given error handler delegate.
        /// </summary>
        /// <param name="task">The task to run.</param>
        /// <param name="errorHandler">
        ///     The error handler for any exception that might occur. If null, exceptions will be swallowed.
        /// </param>
        public static async void FireAndForgetSafe(this Task task, IExceptionHandler errorHandler)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception e)
            {
                errorHandler?.HandleException(e);
            }
        }
    }
}
using System;

namespace Ncl.Common.Core.Infrastructure
{
    /// <summary>
    ///     An interface for a handler for exceptions.
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        ///     Handles an exception that occured.
        /// </summary>
        /// <param name="exception">The exception that occured.</param>
        void HandleException(Exception exception);
    }
}
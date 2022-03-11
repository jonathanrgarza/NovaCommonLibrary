using System;
using Ncl.Common.Core.Infrastructure;

namespace Ncl.Common.Core.Events
{
    /// <summary>
    ///     Event arguments for a action execution event.
    /// </summary>
    public class ActionExecutionEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="ActionExecutionEventArgs" />.
        /// </summary>
        /// <param name="executionType">The execution type.</param>
        /// <param name="description">The action's description.</param>
        /// <param name="isAsync">If the action being executed is being performed asynchronously.</param>
        public ActionExecutionEventArgs(ActionServiceExecutionType executionType, string description, bool isAsync)
        {
            ExecutionType = executionType;
            Description = description;
            IsAsync = isAsync;
        }

        /// <summary>
        ///     Gets the description associated with the action.
        /// </summary>
        public string Description { get; }

        /// <summary>
        ///     Gets the execution type.
        /// </summary>
        public ActionServiceExecutionType ExecutionType { get; }

        /// <summary>
        ///     Gets if the action being executed is being performed asynchronously.
        /// </summary>
        public bool IsAsync { get; }
    }
}
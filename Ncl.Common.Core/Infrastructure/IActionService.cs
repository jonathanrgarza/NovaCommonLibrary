using System;
using System.Threading.Tasks;
using Ncl.Common.Core.Events;

namespace Ncl.Common.Core.Infrastructure
{
    /// <summary>
    ///     Interface for a service that handles undo/redo actions.
    /// </summary>
    public interface IActionService
    {
        /// <summary>
        ///     Gets the current redo description, if any.
        /// </summary>
        string CurrentRedoDescription { get; }

        /// <summary>
        ///     Gets the current undo description, if any.
        /// </summary>
        string CurrentUndoDescription { get; }

        /// <summary>
        ///     Gets if any asynchronous operation is currently ongoing.
        /// </summary>
        bool IsAsyncActionOngoing { get; }

        /// <summary>
        ///     Gets if the pending redo action is an async action (IUndoRedoAsynchronousAction).
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if the current pending redo action is async, <see langword="false" /> if not.
        ///     <see langword="null" /> if there is no currently pending redo action.
        /// </returns>
        bool? IsPendingRedoActionAsync { get; }

        /// <summary>
        ///     Gets if the pending undo action is an async action (IUndoRedoAsynchronousAction).
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if the current pending undo action is async, <see langword="false" /> if not.
        ///     <see langword="null" /> if there is no currently pending undo action.
        /// </returns>
        bool? IsPendingUndoActionAsync { get; }

        /// <summary>
        ///     Gets if the redo buffer is currently empty.
        /// </summary>
        bool IsRedoBufferEmpty { get; }

        /// <summary>
        ///     Gets if the undo buffer is currently empty.
        /// </summary>
        bool IsUndoBufferEmpty { get; }

        /// <summary>
        ///     Gets if undo/redo actions are not enabled.
        /// </summary>
        bool IsUndoRedoActionsDisabled { get; }

        /// <summary>
        ///     Gets if the number of undo/redo actions are not limited.
        /// </summary>
        bool IsUnlimitedUndoRedoActions { get; }

        /// <summary>
        ///     Gets/Sets the maximum undo action kept.
        /// </summary>
        int MaxUndoActions { get; set; }

        /// <summary>
        ///     Gets the number of elements in the redo buffer.
        /// </summary>
        int RedoBufferCount { get; }

        /// <summary>
        ///     Gets the number of elements in the undo buffer.
        /// </summary>
        int UndoBufferCount { get; }

        /// <summary>
        ///     The event that is raised right after an action is invoked.
        /// </summary>
        event EventHandler<ActionExecutionEventArgs> OnPostActionExecute;

        /// <summary>
        ///     The event that is raised right before an action is invoked.
        /// </summary>
        event EventHandler<ActionExecutionEventArgs> OnPreActionExecute;

        /// <summary>
        ///     Executes an action.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <see langword="null" />.</exception>
        /// <exception cref="InvalidOperationException">
        ///     The <see cref="ActionService" /> is currently in use by a previous action operation.
        /// </exception>
        void ExecuteAction(IUndoRedoAction action);

        /// <summary>
        ///     Executes an action, asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns>A <see cref="Task" /> that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <see langword="null" />.</exception>
        /// <exception cref="InvalidOperationException">
        ///     The <see cref="ActionService" /> is currently in use by a previous action operation.
        /// </exception>
        Task ExecuteActionAsync(IUndoRedoAsynchronousAction action);

        /// <summary>
        ///     Executes the pending action's undo operation.
        /// </summary>
        /// <returns><see langword="true" /> if an undo operation was performed, otherwise, <see langword="false" />.</returns>
        /// <exception cref="InvalidOperationException">
        ///     The <see cref="ActionService" /> is currently in use by a previous action operation.
        /// </exception>
        bool PerformUndo();

        /// <summary>
        ///     Executes the pending undo action's undo operation, asynchronously.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task{T}" /> that represents the asynchronous operation.
        ///     A <see langword="true" /> result if an undo operation was performed,
        ///     <see langword="false" /> if there was not one available and <see langword="null" /> if
        ///     the pending undo action does not support asynchronous undo.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     The <see cref="ActionService" /> is currently in use by a previous action operation.
        /// </exception>
        Task<bool?> PerformUndoAsync();

        /// <summary>
        ///     Executes the pending redo action's redo operation.
        /// </summary>
        /// <returns><see langword="true" /> if an redo operation was performed, otherwise, <see langword="false" />.</returns>
        /// <exception cref="InvalidOperationException">
        ///     The <see cref="ActionService" /> is currently in use by a previous action operation.
        /// </exception>
        bool PerformRedo();

        /// <summary>
        ///     Executes the pending redo action's redo operation, asynchronously.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task{T}" /> that represents the asynchronous operation.
        ///     A <see langword="true" /> result if an redo operation was performed,
        ///     <see langword="false" /> if there was not one available and <see langword="null" /> if
        ///     the pending redo action does not support asynchronous redo.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     The <see cref="ActionService" /> is currently in use by a previous action operation.
        /// </exception>
        Task<bool?> PerformRedoAsync();

        /// <summary>
        ///     Clears the redo stack.
        /// </summary>
        void ClearRedoStack();

        /// <summary>
        ///     Clears the undo stack.
        /// </summary>
        void ClearUndoStack();

        /// <summary>
        ///     Clears both the undo and redo stack.
        /// </summary>
        void ClearStacks();
    }
}
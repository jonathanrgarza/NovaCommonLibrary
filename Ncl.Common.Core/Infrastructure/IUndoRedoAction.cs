using System.Threading.Tasks;

namespace Ncl.Common.Core.Infrastructure
{
    /// <summary>
    ///     An action which can be undone and redone.
    /// </summary>
    public interface IUndoRedoAction
    {
        /// <summary>
        ///     Gets the description for the original action.
        /// </summary>
        string Description { get; }

        /// <summary>
        ///     Gets the description for the redo action.
        /// </summary>
        string RedoDescription { get; }

        /// <summary>
        ///     Gets the description for the undo action.
        /// </summary>
        string UndoDescription { get; }

        /// <summary>
        ///     Executes an action for the first time.
        /// </summary>
        void Execute();

        /// <summary>
        ///     Performs an undo of an action.
        /// </summary>
        void Undo();

        /// <summary>
        ///     Performs an redo of an action.
        /// </summary>
        void Redo();
    }

    /// <summary>
    ///     An action which can be undone and redone, performed asynchronously.
    /// </summary>
    public interface IUndoRedoAsynchronousAction : IUndoRedoAction
    {
        /// <summary>
        ///     Executes an action for the first time, asynchronously.
        /// </summary>
        Task ExecuteAsync();

        /// <summary>
        ///     Performs an undo of an action, asynchronously.
        /// </summary>
        Task UndoAsync();

        /// <summary>
        ///     Performs an redo of an action, asynchronously.
        /// </summary>
        Task RedoAsync();
    }
}
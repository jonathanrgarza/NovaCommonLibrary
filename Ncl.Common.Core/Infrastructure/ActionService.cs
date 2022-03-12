using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Ncl.Common.Core.Collections;
using Ncl.Common.Core.Events;
using Ncl.Common.Core.Utilities;

namespace Ncl.Common.Core.Infrastructure
{
    /// <summary>
    ///     A service which handles undo/redo actions.
    /// </summary>
    public class ActionService : IActionService
    {
        /// <summary>
        ///     The default max number of undoable/re-doable actions.
        /// </summary>
        public const int DefaultMaxUndoRedoActions = 25;

        /// <summary>
        ///     The value which represents a
        /// </summary>
        public const int UnlimitedUndoRedoActions = -1;

        protected readonly LimitedStack<IUndoRedoAction> _redoStack;
        protected readonly LimitedStack<IUndoRedoAction> _undoStack;

        protected Task _asyncTask = Task.CompletedTask;

        private int _maxUndoActions;

        /// <summary>
        ///     Initializes a new instance of <see cref="ActionService" />.
        /// </summary>
        public ActionService()
        {
            _maxUndoActions = DefaultMaxUndoRedoActions;

            _undoStack = new LimitedStack<IUndoRedoAction>(DefaultMaxUndoRedoActions);
            _redoStack = new LimitedStack<IUndoRedoAction>(DefaultMaxUndoRedoActions);
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="ActionService" />.
        /// </summary>
        /// <param name="maxActionCount">The maximum number of undo and redo actions.</param>
        public ActionService(int maxActionCount)
        {
            _maxUndoActions = maxActionCount;

            _undoStack = new LimitedStack<IUndoRedoAction>(maxActionCount);
            _redoStack = new LimitedStack<IUndoRedoAction>(maxActionCount);
        }

        /// <inheritdoc />
        public string CurrentRedoDescription =>
            IsUndoRedoActionsDisabled || _redoStack.IsEmpty ? null : _redoStack.Peek()?.RedoDescription;

        /// <inheritdoc />
        public string CurrentUndoDescription =>
            IsUndoRedoActionsDisabled || _undoStack.IsEmpty ? null : _undoStack.Peek()?.UndoDescription;

        /// <inheritdoc />
        public bool IsAsyncActionOngoing => _asyncTask != null && !_asyncTask.IsCompleted;

        /// <inheritdoc />
        public bool IsActionExecuting { get; protected set; }

        /// <inheritdoc />
        public bool? IsPendingRedoActionAsync =>
            IsUndoRedoActionsDisabled || _redoStack.IsEmpty
                ? null
                : (bool?) (_redoStack.Peek() is IUndoRedoAsynchronousAction);

        /// <inheritdoc />
        public bool? IsPendingUndoActionAsync =>
            IsUndoRedoActionsDisabled || _undoStack.IsEmpty
                ? null
                : (bool?) (_undoStack.Peek() is IUndoRedoAsynchronousAction);

        /// <inheritdoc />
        public bool IsRedoBufferEmpty => _redoStack.IsEmpty;

        /// <inheritdoc />
        public bool IsUndoBufferEmpty => _undoStack.IsEmpty;

        /// <inheritdoc />
        public bool IsUndoRedoActionsDisabled => _maxUndoActions == 0;

        /// <inheritdoc />
        public bool IsUnlimitedUndoRedoActions => _maxUndoActions == UnlimitedUndoRedoActions;

        /// <inheritdoc />
        public int MaxUndoActions
        {
            get => _maxUndoActions;
            set
            {
                if (_maxUndoActions == value)
                    return;

                CheckAsyncTaskInProgress();

                _maxUndoActions = value;

                int convertedMax = value;
                switch (value)
                {
                    case UnlimitedUndoRedoActions:
                        convertedMax = LimitedStack<IUndoRedoAction>.UnlimitedCapacity;
                        break;
                    case 0:
                        ClearStacks();
                        //Set to min capacity that is not zero (as that means unlimited capacity)
                        _undoStack.MaxCapacity = 1;
                        _redoStack.MaxCapacity = 1;
                        return;
                }

                _undoStack.MaxCapacity = convertedMax;
                _redoStack.MaxCapacity = convertedMax;
            }
        }

        /// <inheritdoc />
        public int RedoBufferCount => _redoStack.Count;

        /// <inheritdoc />
        public int UndoBufferCount => _undoStack.Count;

        /// <inheritdoc />
        public event EventHandler<ActionExecutionEventArgs> OnPostActionExecute;

        /// <inheritdoc />
        public event EventHandler<ActionExecutionEventArgs> OnPreActionExecute;

        /// <inheritdoc />
        public void ExecuteAction(IUndoRedoAction action)
        {
            Guard.AgainstNullArgument(action, nameof(action));
            CheckAsyncTaskInProgress();

            IsActionExecuting = true;
            try
            {
                RaiseOnPreActionExecute(ActionServiceExecutionType.Original, action, false);

                action.Execute();
                if (!IsUndoRedoActionsDisabled)
                {
                    _undoStack.Push(action);
                    ClearRedoStack();
                }

                RaiseOnPostActionExecute(ActionServiceExecutionType.Original, action, false);
            }
            finally
            {
                IsActionExecuting = false;
            }
        }

        /// <inheritdoc />
        public Task ExecuteActionAsync(IUndoRedoAsynchronousAction action)
        {
            Guard.AgainstNullArgument(action, nameof(action));
            CheckAsyncTaskInProgress();

            Task asyncTask = ExecuteActionAsyncInternal(action);
            _asyncTask = asyncTask;
            return asyncTask;
        }

        /// <inheritdoc />
        public bool PerformUndo()
        {
            if (IsUndoRedoActionsDisabled)
                return false;

            if (_undoStack.IsEmpty)
                return false;

            CheckAsyncTaskInProgress();

            IsActionExecuting = true;
            try
            {
                IUndoRedoAction action = _undoStack.Pop();

                Debug.Assert(action != null);
                RaiseOnPreActionExecute(ActionServiceExecutionType.Undo, action, false);

                action.Undo();
                _redoStack.Push(action);

                RaiseOnPostActionExecute(ActionServiceExecutionType.Undo, action, false);
                return true;
            }
            finally
            {
                IsActionExecuting = false;
            }
        }

        /// <inheritdoc />
        public Task<bool?> PerformUndoAsync()
        {
            CheckAsyncTaskInProgress();

            Task<bool?> asyncTask = PerformUndoAsyncInternal();
            _asyncTask = asyncTask;
            return asyncTask;
        }

        /// <inheritdoc />
        public bool PerformRedo()
        {
            if (IsUndoRedoActionsDisabled)
                return false;

            if (_redoStack.IsEmpty)
                return false;

            CheckAsyncTaskInProgress();

            IsActionExecuting = true;
            try
            {
                IUndoRedoAction action = _redoStack.Pop();

                Debug.Assert(action != null);
                RaiseOnPreActionExecute(ActionServiceExecutionType.Redo, action, false);

                action.Redo();
                _undoStack.Push(action);

                RaiseOnPostActionExecute(ActionServiceExecutionType.Redo, action, false);
                return true;
            }
            finally
            {
                IsActionExecuting = false;
            }
        }

        /// <inheritdoc />
        public Task<bool?> PerformRedoAsync()
        {
            CheckAsyncTaskInProgress();

            Task<bool?> asyncTask = PerformRedoAsyncInternal();
            _asyncTask = asyncTask;
            return asyncTask;
        }

        /// <summary>
        ///     Clears the redo stack.
        /// </summary>
        public void ClearRedoStack()
        {
            _redoStack?.Clear();
        }

        /// <summary>
        ///     Clears the undo stack.
        /// </summary>
        public void ClearUndoStack()
        {
            _undoStack?.Clear();
        }

        /// <summary>
        ///     Clears both the undo and redo stack.
        /// </summary>
        public void ClearStacks()
        {
            _undoStack?.Clear();
            _redoStack?.Clear();
        }

        /// <summary>
        ///     Executes an action, asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns>A <see cref="Task" /> that represents the asynchronous operation.</returns>
        protected async Task ExecuteActionAsyncInternal(IUndoRedoAsynchronousAction action)
        {
            IsActionExecuting = true;
            try
            {
                RaiseOnPreActionExecute(ActionServiceExecutionType.Original, action, true);

                await action.ExecuteAsync();
                if (!IsUndoRedoActionsDisabled)
                {
                    _undoStack.Push(action);
                    ClearRedoStack();
                }

                RaiseOnPostActionExecute(ActionServiceExecutionType.Original, action, true);
            }
            finally
            {
                IsActionExecuting = false;
            }
        }

        /// <summary>
        ///     Executes the pending undo action's undo operation, asynchronously.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task{T}" /> that represents the asynchronous operation.
        ///     A <see langword="true" /> result if an undo operation was performed,
        ///     <see langword="false" /> if there was not one available and <see langword="null" /> if
        ///     the pending undo action does not support asynchronous undo.
        /// </returns>
        protected async Task<bool?> PerformUndoAsyncInternal()
        {
            if (IsUndoRedoActionsDisabled)
                return false;

            if (_undoStack.IsEmpty)
                return false;

            if (IsPendingUndoActionAsync != true)
                return null;

            IUndoRedoAction action = _undoStack.Pop();

            Debug.Assert(action != null);
            Debug.Assert(action is IUndoRedoAsynchronousAction);

            var asyncAction = (IUndoRedoAsynchronousAction) action;

            IsActionExecuting = true;
            try
            {
                RaiseOnPreActionExecute(ActionServiceExecutionType.Undo, action, true);

                await asyncAction.UndoAsync();
                _redoStack.Push(action);

                RaiseOnPostActionExecute(ActionServiceExecutionType.Undo, action, true);
                return true;
            }
            finally
            {
                IsActionExecuting = false;
            }
        }

        /// <summary>
        ///     Executes the pending redo action's redo operation, asynchronously.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task{T}" /> that represents the asynchronous operation.
        ///     A <see langword="true" /> result if an redo operation was performed,
        ///     <see langword="false" /> if there was not one available and <see langword="null" /> if
        ///     the pending redo action does not support asynchronous redo.
        /// </returns>
        protected async Task<bool?> PerformRedoAsyncInternal()
        {
            if (IsUndoRedoActionsDisabled)
                return false;

            if (_redoStack.IsEmpty)
                return false;

            if (IsPendingRedoActionAsync != true)
                return null;

            IUndoRedoAction action = _redoStack.Pop();

            Debug.Assert(action != null);
            Debug.Assert(action is IUndoRedoAsynchronousAction);

            var asyncAction = (IUndoRedoAsynchronousAction) action;

            IsActionExecuting = true;
            try
            {
                RaiseOnPreActionExecute(ActionServiceExecutionType.Redo, action, true);

                await asyncAction.RedoAsync();
                _undoStack.Push(action);

                RaiseOnPostActionExecute(ActionServiceExecutionType.Redo, action, true);
                return true;
            }
            finally
            {
                IsActionExecuting = false;
            }
        }

        /// <summary>
        ///     Raises a OnPreActionExecute event.
        /// </summary>
        /// <param name="type">The execution type.</param>
        /// <param name="action">The action.</param>
        /// <param name="isAsync">If the action is being executed async.</param>
        protected virtual void RaiseOnPreActionExecute(ActionServiceExecutionType type, IUndoRedoAction action,
            bool isAsync)
        {
            if (OnPreActionExecute == null)
                return;
            ActionExecutionEventArgs eventArgs = GetActionExecutionEventArgs(type, action, isAsync);
            OnPreActionExecute.Invoke(this, eventArgs);
        }

        /// <summary>
        ///     Raises a OnPostActionExecute event.
        /// </summary>
        /// <param name="type">The execution type.</param>
        /// <param name="action">The action.</param>
        /// <param name="isAsync">If the action is being executed async.</param>
        protected virtual void RaiseOnPostActionExecute(ActionServiceExecutionType type, IUndoRedoAction action,
            bool isAsync)
        {
            if (OnPostActionExecute == null)
                return;
            ActionExecutionEventArgs eventArgs = GetActionExecutionEventArgs(type, action, isAsync);
            OnPostActionExecute.Invoke(this, eventArgs);
        }

        protected static ActionExecutionEventArgs GetActionExecutionEventArgs(ActionServiceExecutionType type,
            IUndoRedoAction action, bool isAsync)
        {
            string description;
            switch (type)
            {
                case ActionServiceExecutionType.Original:
                    description = action.Description;
                    break;
                case ActionServiceExecutionType.Undo:
                    description = action.UndoDescription;
                    break;
                case ActionServiceExecutionType.Redo:
                    description = action.RedoDescription;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Unsupported value");
            }

            var eventArgs = new ActionExecutionEventArgs(type, description, isAsync);
            return eventArgs;
        }

        protected void CheckAsyncTaskInProgress()
        {
            if (_asyncTask != null && !_asyncTask.IsCompleted)
                throw new InvalidOperationException("An asynchronous operation is already in progress");
        }
    }
}
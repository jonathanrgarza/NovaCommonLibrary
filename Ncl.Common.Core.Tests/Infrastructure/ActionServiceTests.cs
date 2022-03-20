using System;
using System.Threading.Tasks;
using Ncl.Common.Core.Infrastructure;
using Xunit;

namespace Ncl.Common.Core.Tests.Infrastructure
{
    public class ActionServiceTests
    {
        public const string DefaultDescription = "Stub Action";
        public const string DefaultRedoDescription = "Redo Action";
        public const string DefaultUndoDescription = "Undo Action";

        [Fact]
        public void Constructor_WithNoParams_ShouldCreateInstance()
        {
            // Arrange
            // Act
            var instance = new ActionService();

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void Constructor1_WithPositiveMaxActionCount_ShouldCreateInstance()
        {
            // Arrange
            const int expected = 5;

            // Act
            var instance = new ActionService(expected);

            // Assert
            Assert.NotNull(instance);
            Assert.Equal(expected, instance.MaxUndoActions);
        }

        [Fact]
        public void Constructor1_WithZeroMaxActionCount_ShouldCreateInstance()
        {
            // Arrange
            const int expected = 0;

            // Act
            var instance = new ActionService(expected);

            // Assert
            Assert.NotNull(instance);
            Assert.Equal(expected, instance.MaxUndoActions);
        }

        [Fact]
        public void Constructor1_WithUnlimitedMaxActionCount_ShouldCreateInstance()
        {
            // Arrange
            const int expected = ActionService.UnlimitedUndoRedoActions;

            // Act
            var instance = new ActionService(expected);

            // Assert
            Assert.NotNull(instance);
            Assert.Equal(expected, instance.MaxUndoActions);
        }

        [Fact]
        public void Constructor1_WithNegativeMaxActionCount_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            static void TestCode()
            {
                // Act
                _ = new ActionService(-2);
            }

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(TestCode);
        }

        [Fact]
        public void MaxActionCount_WithPositiveMaxActionCount_ShouldSetValue()
        {
            // Arrange
            const int expected = 5;
            var instance = new ActionService
            {
                // Act
                MaxUndoActions = expected
            };

            // Assert
            Assert.Equal(expected, instance.MaxUndoActions);
        }

        [Fact]
        public void MaxActionCount_WithSameValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 5;
            var instance = new ActionService(expected)
            {
                // Act
                MaxUndoActions = expected
            };

            // Assert
            Assert.Equal(expected, instance.MaxUndoActions);
        }

        [Fact]
        public void MaxActionCount_WithZeroMaxActionCount_ShouldSetValue()
        {
            // Arrange
            const int expected = 0;
            var instance = new ActionService
            {
                // Act
                MaxUndoActions = expected
            };

            // Assert
            Assert.Equal(expected, instance.MaxUndoActions);
        }

        [Fact]
        public void MaxActionCount_WithUnlimitedMaxActionCount_ShouldSetValue()
        {
            // Arrange
            const int expected = ActionService.UnlimitedUndoRedoActions;
            var instance = new ActionService
            {
                // Act
                MaxUndoActions = expected
            };

            // Assert
            Assert.Equal(expected, instance.MaxUndoActions);
        }

        [Fact]
        public void MaxActionCount_WithNegativeMaxActionCount_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            static void TestCode()
            {
                // Act
                var _ = new ActionService
                {
                    MaxUndoActions = -2
                };
            }

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(TestCode);
        }

        [Fact]
        public void CurrentRedoDescription_WithDefaultInstance_ShouldReturnNull()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();

            // Act
            string actual = instance.CurrentRedoDescription;

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void CurrentRedoDescription_WithDisableUndoRedoActions_ShouldReturnNull()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();
            IUndoRedoAction action = GetDefaultSimpleAction();
            instance.MaxUndoActions = 0;
            instance.ExecuteAction(action);

            // Act
            string actual = instance.CurrentRedoDescription;

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void CurrentRedoDescription_WithActionExecuted_ShouldReturnNull()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();
            IUndoRedoAction action = GetDefaultSimpleAction();
            instance.ExecuteAction(action);

            // Act
            string actual = instance.CurrentRedoDescription;

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void CurrentRedoDescription_WithUndoActionExecuted_ShouldReturnPendingRedoDescription()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();
            IUndoRedoAction action = GetDefaultSimpleAction();
            instance.ExecuteAction(action);
            instance.Undo();

            // Act
            string actual = instance.CurrentRedoDescription;

            // Assert
            Assert.Equal(DefaultRedoDescription, actual);
        }

        [Fact]
        public void CurrentUndoDescription_WithDefaultInstance_ShouldReturnNull()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();

            // Act
            string actual = instance.CurrentUndoDescription;

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void CurrentUndoDescription_WithDisableUndoRedoActions_ShouldReturnNull()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();
            IUndoRedoAction action = GetDefaultSimpleAction();
            instance.MaxUndoActions = 0;
            instance.ExecuteAction(action);

            // Act
            string actual = instance.CurrentUndoDescription;

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void CurrentUndoDescription_WithActionExecuted_ShouldReturnPendingUndoDescription()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();
            IUndoRedoAction action = GetDefaultSimpleAction();
            instance.ExecuteAction(action);

            // Act
            string actual = instance.CurrentUndoDescription;

            // Assert
            Assert.Equal(DefaultUndoDescription, actual);
        }

        [Fact]
        public void CurrentUndoDescription_WithUndoActionExecuted_ShouldReturnNull()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();
            IUndoRedoAction action = GetDefaultSimpleAction();
            instance.ExecuteAction(action);
            instance.Undo();

            // Act
            string actual = instance.CurrentUndoDescription;

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void IsAsyncActionOngoing_WithDefaultInstance_ShouldReturnFalse()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();

            // Act
            bool actual = instance.IsAsyncActionOngoing;

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void IsAsyncActionOngoing_WithOngoingAsyncAction_ShouldReturnTrue()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();
            IUndoRedoAsynchronousAction action = GetBlockingAsyncAction();
            instance.ExecuteActionAsync(action);

            // Act
            bool actual = instance.IsAsyncActionOngoing;

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void IsActionExecuting_WithDefaultInstance_ShouldReturnFalse()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();

            // Act
            bool actual = instance.IsActionExecuting;

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void IsActionExecuting_WithOngoingAsyncAction_ShouldReturnTrue()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();
            IUndoRedoAsynchronousAction action = GetBlockingAsyncAction();
            instance.ExecuteActionAsync(action);

            // Act
            bool actual = instance.IsActionExecuting;

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void IsPendingUndoActionAsync_WithDefaultInstance_ShouldReturnNull()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();

            // Act
            bool? actual = instance.IsPendingUndoActionAsync;

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void IsPendingUndoActionAsync_WithNonAsyncActionPending_ShouldReturnFalse()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();
            IUndoRedoAction action = GetDefaultSimpleAction();
            instance.ExecuteAction(action);

            // Act
            bool? actual = instance.IsPendingUndoActionAsync;

            // Assert
            Assert.NotNull(actual);
            Assert.False(actual.Value);
        }

        [Fact]
        public void IsPendingUndoActionAsync_WithAsyncActionExecuted_ShouldReturnTrue()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();
            IUndoRedoAction action = GetDefaultSimpleAsyncAction();
            instance.ExecuteAction(action);

            // Act
            bool? actual = instance.IsPendingUndoActionAsync;

            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Value);
        }

        [Fact]
        public void IsPendingRedoActionAsync_WithDefaultInstance_ShouldReturnNull()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();

            // Act
            bool? actual = instance.IsPendingRedoActionAsync;

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void IsPendingRedoActionAsync_WithNonAsyncUndoActionPending_ShouldReturnFalse()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();
            IUndoRedoAction action = GetDefaultSimpleAction();
            instance.ExecuteAction(action);
            instance.Undo();

            // Act
            bool? actual = instance.IsPendingRedoActionAsync;

            // Assert
            Assert.NotNull(actual);
            Assert.False(actual.Value);
        }

        [Fact]
        public void IsPendingRedoActionAsync_WithAsyncUndoActionExecuted_ShouldReturnTrue()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();
            IUndoRedoAction action = GetDefaultSimpleAsyncAction();
            instance.ExecuteAction(action);
            instance.Undo();

            // Act
            bool? actual = instance.IsPendingRedoActionAsync;

            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Value);
        }

        [Fact]
        public void IsUndoBufferEmpty_WithNoActionExecuted_ShouldReturnTrue()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();

            // Act
            bool actual = instance.IsUndoBufferEmpty;

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void IsUndoBufferEmpty_WithActionExecuted_ShouldReturnFalse()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();
            IUndoRedoAction action = GetDefaultSimpleAction();
            instance.ExecuteAction(action);

            // Act
            bool actual = instance.IsUndoBufferEmpty;

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void IsRedoBufferEmpty_WithNoActionExecuted_ShouldReturnTrue()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();

            // Act
            bool actual = instance.IsRedoBufferEmpty;

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void IsRedoBufferEmpty_WithActionExecuted_ShouldReturnTrue()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();
            IUndoRedoAction action = GetDefaultSimpleAction();
            instance.ExecuteAction(action);

            // Act
            bool actual = instance.IsRedoBufferEmpty;

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void IsRedoBufferEmpty_WithUndoActionExecuted_ShouldReturnFalse()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();
            IUndoRedoAction action = GetDefaultSimpleAction();
            instance.ExecuteAction(action);
            instance.Undo();

            // Act
            bool actual = instance.IsRedoBufferEmpty;

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void IsUndoRedoActionsDisabled_WithDefaultInstance_ShouldReturnFalse()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();

            // Act
            bool actual = instance.IsUndoRedoActionsDisabled;

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void IsUndoRedoActionsDisabled_WithMaxUndoActionsIsZero_ShouldReturnTrue()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();
            instance.MaxUndoActions = 0;

            // Act
            bool actual = instance.IsUndoRedoActionsDisabled;

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void IsUnlimitedUndoRedoActions_WithDefaultInstance_ShouldReturnFalse()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();

            // Act
            bool actual = instance.IsUnlimitedUndoRedoActions;

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void IsUnlimitedUndoRedoActions_WithMaxUndoActionsIsUnlimited_ShouldReturnTrue()
        {
            // Arrange
            ActionService instance = GetDefaultInstance();
            instance.MaxUndoActions = ActionService.UnlimitedUndoRedoActions;

            // Act
            bool actual = instance.IsUnlimitedUndoRedoActions;

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void UndoBufferCounty_WithNoActionExecuted_ShouldReturnZero()
        {
            // Arrange
            const int expected = 0;
            ActionService instance = GetDefaultInstance();

            // Act
            int actual = instance.UndoBufferCount;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UndoBufferCount_WithActionExecuted_ShouldReturnOne()
        {
            // Arrange
            const int expected = 1;
            ActionService instance = GetDefaultInstance();
            IUndoRedoAction action = GetDefaultSimpleAction();
            instance.ExecuteAction(action);

            // Act
            int actual = instance.UndoBufferCount;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RedoBufferCount_WithNoActionExecuted_ShouldReturnZero()
        {
            // Arrange
            const int expected = 0;
            ActionService instance = GetDefaultInstance();

            // Act
            int actual = instance.RedoBufferCount;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RedoBufferCount_WithActionExecuted_ShouldReturnZero()
        {
            // Arrange
            const int expected = 0;
            ActionService instance = GetDefaultInstance();
            IUndoRedoAction action = GetDefaultSimpleAction();
            instance.ExecuteAction(action);

            // Act
            int actual = instance.RedoBufferCount;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RedoBufferCount_WithUndoActionExecuted_ShouldReturnOne()
        {
            // Arrange
            const int expected = 1;
            ActionService instance = GetDefaultInstance();
            IUndoRedoAction action = GetDefaultSimpleAction();
            instance.ExecuteAction(action);
            instance.Undo();

            // Act
            int actual = instance.RedoBufferCount;

            // Assert
            Assert.Equal(expected, actual);
        }

        private static ActionService GetDefaultInstance()
        {
            return new ActionService();
        }

        private static IUndoRedoAction GetDefaultSimpleAction()
        {
            static void ExecuteAction()
            {
            }

            return new GenericUndoRedoStub(ExecuteAction);
        }

        private static IUndoRedoAsynchronousAction GetDefaultSimpleAsyncAction()
        {
            return new UndoRedoAsyncActionStub();
        }

        private static IUndoRedoAction GetGenericAction(Action executeAction, Action undoAction = null,
            Action redoAction = null)
        {
            return new GenericUndoRedoStub(executeAction, undoAction, redoAction);
        }

        private static IUndoRedoAsynchronousAction GetBlockingAsyncAction()
        {
            return new BlockingUndoRedoAsyncActionStub();
        }

        private class UndoRedoAsyncActionStub : IUndoRedoAsynchronousAction
        {
            /// <inheritdoc />
            public string Description => DefaultDescription;

            /// <inheritdoc />
            public string RedoDescription => DefaultRedoDescription;

            /// <inheritdoc />
            public string UndoDescription => DefaultUndoDescription;

            /// <inheritdoc />
            public void Execute()
            {
                //Do nothing
            }

            /// <inheritdoc />
            public void Undo()
            {
                //Do nothing
            }

            /// <inheritdoc />
            public void Redo()
            {
                //Do nothing
            }

            public Task ExecuteAsync()
            {
                //Do nothing
                return Task.CompletedTask;
            }

            public Task UndoAsync()
            {
                //Do nothing
                return Task.CompletedTask;
            }

            public Task RedoAsync()
            {
                //Do nothing
                return Task.CompletedTask;
            }
        }

        private class GenericUndoRedoStub : IUndoRedoAction
        {
            private readonly Action _executeAction;
            private readonly Action _redoAction;
            private readonly Action _undoAction;

            public GenericUndoRedoStub(Action executeAction, Action undoAction = null, Action redoAction = null)
            {
                _executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
                _undoAction = undoAction ?? executeAction;
                _redoAction = redoAction ?? executeAction;
            }

            /// <inheritdoc />
            public string Description => DefaultDescription;

            /// <inheritdoc />
            public string RedoDescription => DefaultRedoDescription;

            /// <inheritdoc />
            public string UndoDescription => DefaultUndoDescription;

            /// <inheritdoc />
            public void Execute()
            {
                _executeAction();
            }

            /// <inheritdoc />
            public void Undo()
            {
                _undoAction();
            }

            /// <inheritdoc />
            public void Redo()
            {
                _redoAction();
            }
        }

        private class BlockingUndoRedoAsyncActionStub : IUndoRedoAsynchronousAction
        {
            /// <inheritdoc />
            public string Description => DefaultDescription;

            /// <inheritdoc />
            public string RedoDescription => DefaultRedoDescription;

            /// <inheritdoc />
            public string UndoDescription => DefaultUndoDescription;

            /// <inheritdoc />
            public void Execute()
            {
                //Do nothing
            }

            /// <inheritdoc />
            public void Undo()
            {
                //Do nothing
            }

            /// <inheritdoc />
            public void Redo()
            {
                //Do nothing
            }

            public Task ExecuteAsync()
            {
                return Task.Delay(1000 * 60);
            }

            public Task UndoAsync()
            {
                return Task.Delay(1000 * 60);
            }

            public Task RedoAsync()
            {
                return Task.Delay(1000 * 60);
            }
        }
    }
}
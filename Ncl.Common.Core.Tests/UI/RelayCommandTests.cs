using System;
using System.Windows.Input;
using Ncl.Common.Core.UI;
using Xunit;

namespace Ncl.Common.Core.Tests.UI
{
    public class RelayCommandTests
    {
        [Fact]
        public void RelayCommand_WithValidParameters_ShouldCreateInstance()
        {
            // Arrange
            // Act
            var instance = new RelayCommand(MockExecute);

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void RelayCommand_WithInvalidExecuteAction_ShouldThrowArgumentNullException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = new RelayCommand(null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>("executeAction", TestCode);
        }

        [Fact]
        public void CanExecute_WithDefaultValue_ShouldReturnsTrue()
        {
            // Arrange
            var instance = new RelayCommand(MockExecute);

            // Act
            bool actual = instance.CanExecute();

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void CanExecute_WithAlwaysFalseFunc_ShouldReturnsFalse()
        {
            // Arrange
            var instance = new RelayCommand(MockExecute, () => false);

            // Act
            bool actual = instance.CanExecute();

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void Execute_WithValidExecuteAction_ShouldExecuteAction()
        {
            // Arrange
            bool isInvoked = false;
            var instance = new RelayCommand(() => isInvoked = true);

            // Act
            instance.Execute();

            // Assert
            Assert.True(isInvoked);
        }

        [Fact]
        public void RaiseCanExecuteChanged_WithValidInstance_ShouldRaiseCanExecuteChangedEvent()
        {
            // Arrange
            var instance = new RelayCommand(MockExecute);

            bool wasRaised = false;

            void EventHandler(object sender, EventArgs args)
            {
                wasRaised = true;
            }

            instance.CanExecuteChanged += EventHandler;

            // Act
            instance.RaiseCanExecuteChanged();

            // Assert
            Assert.True(wasRaised);
        }

        [Fact]
        public void ICommandCanExecute_WithDefaultValue_ShouldReturnsTrue()
        {
            // Arrange
            ICommand instance = new RelayCommand(MockExecute);

            // Act
            bool actual = instance.CanExecute(null);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void ICommandCanExecute_WithAlwaysFalseFunc_ShouldReturnsFalse()
        {
            // Arrange
            ICommand instance = new RelayCommand(MockExecute, () => false);

            // Act
            bool actual = instance.CanExecute(null);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void ICommandExecute_WithValidExecuteAction_ShouldExecuteAction()
        {
            // Arrange
            bool isInvoked = false;
            ICommand instance = new RelayCommand(() => isInvoked = true);

            // Act
            instance.Execute(null);

            // Assert
            Assert.True(isInvoked);
        }

        private static void MockExecute()
        {
        }
    }

    public class GenericRelayCommandTests
    {
        [Fact]
        public void RelayCommand_WithValidParameters_ShouldCreateInstance()
        {
            // Arrange
            // Act
            var instance = new RelayCommand<int>(MockExecute);

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void RelayCommand_WithInvalidExecuteAction_ShouldThrowArgumentNullException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = new RelayCommand<int>(null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>("executeAction", TestCode);
        }

        [Fact]
        public void CanExecute_WithDefaultValue_ShouldReturnsTrue()
        {
            // Arrange
            var instance = new RelayCommand<int>(MockExecute);

            // Act
            bool actual = instance.CanExecute(0);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void CanExecute_WithAlwaysFalseFunc_ShouldReturnsFalse()
        {
            // Arrange
            var instance = new RelayCommand<int>(MockExecute, _ => false);

            // Act
            bool actual = instance.CanExecute(0);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void CanExecute_WithParameter_ShouldPassParameterToFunction()
        {
            // Arrange
            const int expected = 1;
            var instance = new RelayCommand<int>(MockExecute, CanExecuteHandler);

            int actual = -1;

            bool CanExecuteHandler(int parameter)
            {
                actual = parameter;
                return true;
            }

            // Act
            instance.CanExecute(1);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Execute_WithValidExecuteAction_ShouldExecuteAction()
        {
            // Arrange
            bool isInvoked = false;
            var instance = new RelayCommand<int>(_ => isInvoked = true);

            // Act
            instance.Execute(0);

            // Assert
            Assert.True(isInvoked);
        }

        [Fact]
        public void Execute_WithParameter_ShouldPassParameterToFunction()
        {
            // Arrange
            const int expected = 1;
            var instance = new RelayCommand<int>(ExecuteHandler);

            int actual = -1;

            void ExecuteHandler(int parameter)
            {
                actual = parameter;
            }

            // Act
            instance.Execute(1);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RaiseCanExecuteChanged_WithValidInstance_ShouldRaiseCanExecuteChangedEvent()
        {
            // Arrange
            var instance = new RelayCommand<int>(MockExecute);

            bool wasRaised = false;

            void EventHandler(object sender, EventArgs args)
            {
                wasRaised = true;
            }

            instance.CanExecuteChanged += EventHandler;

            // Act
            instance.RaiseCanExecuteChanged();

            // Assert
            Assert.True(wasRaised);
        }

        [Fact]
        public void ICommandCanExecute_WithDefaultValue_ShouldReturnsTrue()
        {
            // Arrange
            ICommand instance = new RelayCommand<int>(MockExecute);

            // Act
            bool actual = instance.CanExecute(0);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void ICommandCanExecute_WithAlwaysFalseFunc_ShouldReturnsFalse()
        {
            // Arrange
            ICommand instance = new RelayCommand<int>(MockExecute, _ => false);

            // Act
            bool actual = instance.CanExecute(0);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void ICommandExecute_WithValidExecuteAction_ShouldExecuteAction()
        {
            // Arrange
            bool isInvoked = false;
            ICommand instance = new RelayCommand<int>(_ => isInvoked = true);

            // Act
            instance.Execute(0);

            // Assert
            Assert.True(isInvoked);
        }

        private static void MockExecute(int parameter)
        {
        }
    }
}
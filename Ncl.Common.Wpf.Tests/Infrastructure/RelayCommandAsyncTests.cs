using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Ncl.Common.Core.Infrastructure;
using Ncl.Common.Wpf.Infrastructure;
using Xunit;

namespace Ncl.Common.Wpf.Tests.Infrastructure
{
    public class RelayCommandAsyncTests
    {
        [Fact]
        public void RelayCommand_WithValidParameters_ShouldCreateInstance()
        {
            // Arrange
            // Act
            var instance = new RelayCommandAsync(MockExecute);

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void RelayCommand_WithInvalidExecuteFunction_ShouldThrowArgumentNullException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = new RelayCommandAsync(null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>("executeFunction", TestCode);
        }

        [Fact]
        public void RelayCommand1_WithValidParameters_ShouldCreateInstance()
        {
            // Arrange
            // Act
            var instance = new RelayCommandAsync(MockExecute, null, (Action<Exception>) null);

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void RelayCommand2_WithValidParameters_ShouldCreateInstance()
        {
            // Arrange
            // Act
            var instance = new RelayCommandAsync(MockExecute, null, (IExceptionHandler) null);

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void RelayCommand2_WithValidParameters2_ShouldCreateInstance()
        {
            // Arrange
            // Act
            var instance = new RelayCommandAsync(MockExecute, null, new ExceptionHandlerMock());

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void CanExecute_WithDefaultValue_ShouldReturnsTrue()
        {
            // Arrange
            var instance = new RelayCommandAsync(MockExecute);

            // Act
            bool actual = instance.CanExecute();

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void CanExecute_WithAlwaysFalseFunc_ShouldReturnsFalse()
        {
            // Arrange
            var instance = new RelayCommandAsync(MockExecute, () => false);

            // Act
            bool actual = instance.CanExecute();

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public async Task ExecuteAsync_WithValidExecuteAction_ShouldExecuteAction()
        {
            // Arrange
            bool isInvoked = false;
            var instance = new RelayCommandAsync(() =>
            {
                isInvoked = true;
                return Task.CompletedTask;
            });

            // Act
            await instance.ExecuteAsync();

            // Assert
            Assert.True(isInvoked);
        }

        [Fact]
        public void RaiseCanExecuteChanged_WithValidInstance_ShouldRaiseCanExecuteChangedEvent()
        {
            // Arrange
            var instance = new RelayCommandAsync(MockExecute);

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
            ICommand instance = new RelayCommandAsync(MockExecute);

            // Act
            bool actual = instance.CanExecute(null);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void ICommandCanExecute_WithAlwaysFalseFunc_ShouldReturnsFalse()
        {
            // Arrange
            ICommand instance = new RelayCommandAsync(MockExecute, () => false);

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
            ICommand instance = new RelayCommandAsync(() =>
            {
                isInvoked = true;
                return Task.CompletedTask;
            });

            // Act
            instance.Execute(null);

            // Assert
            Assert.True(isInvoked);
        }

        private static Task MockExecute()
        {
            return Task.CompletedTask;
        }

        private class ExceptionHandlerMock : IExceptionHandler
        {
            public void HandleException(Exception exception)
            {
            }
        }
    }

    public class GenericRelayCommandAsyncTests
    {
        [Fact]
        public void RelayCommand_WithValidParameters_ShouldCreateInstance()
        {
            // Arrange
            // Act
            var instance = new RelayCommandAsync<int>(MockExecute);

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void RelayCommand_WithInvalidExecuteFunction_ShouldThrowArgumentNullException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = new RelayCommandAsync<int>(null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>("executeFunction", TestCode);
        }

        [Fact]
        public void RelayCommand1_WithValidParameters_ShouldCreateInstance()
        {
            // Arrange
            // Act
            var instance = new RelayCommandAsync<int>(MockExecute, null, (Action<Exception>) null);

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void RelayCommand2_WithValidParameters_ShouldCreateInstance()
        {
            // Arrange
            // Act
            var instance = new RelayCommandAsync<int>(MockExecute, null, (IExceptionHandler) null);

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void RelayCommand2_WithValidParameters2_ShouldCreateInstance()
        {
            // Arrange
            // Act
            var instance = new RelayCommandAsync<int>(MockExecute, null, new ExceptionHandlerMock());

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void CanExecute_WithDefaultValue_ShouldReturnsTrue()
        {
            // Arrange
            var instance = new RelayCommandAsync<int>(MockExecute);

            // Act
            bool actual = instance.CanExecute(0);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void CanExecute_WithAlwaysFalseFunc_ShouldReturnsFalse()
        {
            // Arrange
            var instance = new RelayCommandAsync<int>(MockExecute, _ => false);

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
            var instance = new RelayCommandAsync<int>(MockExecute, CanExecuteHandler);

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
        public async Task ExecuteAsync_WithValidExecuteAction_ShouldExecuteAction()
        {
            // Arrange
            bool isInvoked = false;
            var instance = new RelayCommandAsync<int>(_ =>
            {
                isInvoked = true;
                return Task.CompletedTask;
            });

            // Act
            await instance.ExecuteAsync(0);

            // Assert
            Assert.True(isInvoked);
        }

        [Fact]
        public async Task ExecuteAsync_WithParameter_ShouldPassParameterToFunction()
        {
            // Arrange
            const int expected = 1;
            var instance = new RelayCommandAsync<int>(ExecuteHandler);

            int actual = -1;

            Task ExecuteHandler(int parameter)
            {
                actual = parameter;
                return Task.CompletedTask;
            }

            // Act
            await instance.ExecuteAsync(1);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RaiseCanExecuteChanged_WithValidInstance_ShouldRaiseCanExecuteChangedEvent()
        {
            // Arrange
            var instance = new RelayCommandAsync<int>(MockExecute);

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
            ICommand instance = new RelayCommandAsync<int>(MockExecute);

            // Act
            bool actual = instance.CanExecute(0);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void ICommandCanExecute_WithAlwaysFalseFunc_ShouldReturnsFalse()
        {
            // Arrange
            ICommand instance = new RelayCommandAsync<int>(MockExecute, _ => false);

            // Act
            bool actual = instance.CanExecute(0);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void ICommandCanExecute_WithParameter_ShouldPassParameterToFunction()
        {
            // Arrange
            const int expected = 1;
            ICommand instance = new RelayCommandAsync<int>(MockExecute, CanExecuteHandler);

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
        public void ICommandExecute_WithValidExecuteAction_ShouldExecuteAction()
        {
            // Arrange
            bool isInvoked = false;
            ICommand instance = new RelayCommandAsync<int>(_ =>
            {
                isInvoked = true;
                return Task.CompletedTask;
            });

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
            ICommand instance = new RelayCommandAsync<int>(ExecuteHandler);

            int actual = -1;

            Task ExecuteHandler(int parameter)
            {
                actual = parameter;
                return Task.CompletedTask;
            }

            // Act
            instance.Execute(1);

            // Assert
            Assert.Equal(expected, actual);
        }

        private static Task MockExecute(int parameter)
        {
            return Task.CompletedTask;
        }

        private class ExceptionHandlerMock : IExceptionHandler
        {
            public void HandleException(Exception exception)
            {
            }
        }
    }
}
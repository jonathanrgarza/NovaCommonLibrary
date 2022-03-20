using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ncl.Common.Core.Infrastructure;
using Xunit;

namespace Ncl.Common.Core.Tests.Infrastructure
{
    public class ActionServiceTests
    {
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
        public void MaxActionCount_WithPositiveMaxActionCount_ShouldCreateInstance()
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
        public void MaxActionCount_WithZeroMaxActionCount_ShouldCreateInstance()
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
        public void MaxActionCount_WithUnlimitedMaxActionCount_ShouldCreateInstance()
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
                var instance = new ActionService
                {
                    MaxUndoActions = -2
                };
            }

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(TestCode);
        }
    }
}

using System.Collections.ObjectModel;
using Ncl.Common.Core.Extensions;
using Xunit;

namespace Ncl.Common.Core.Tests.Extensions
{
    public class CollectionExtensionsTests
    {
        [Fact]
        public void GetValueOrDefault_WithPopulatedCollectionAndValidIndex_ShouldReturnValue()
        {
            // Arrange
            const int expected = 1;
            var instance = new Collection<int> { expected, 2, 3 };

            // Act
            int actual = instance.GetValueOrDefault(0);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetValueOrDefault_WithInvalidIndex_ShouldReturnDefault()
        {
            // Arrange
            const int expected = 0;
            var instance = new Collection<int> { 1, 2, 3 };

            // Act
            int actual = instance.GetValueOrDefault(5);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetValueOrDefault_WithNegativeIndex_ShouldReturnDefault()
        {
            // Arrange
            const int expected = 0;
            var instance = new Collection<int> { 1, 2, 3 };

            // Act
            int actual = instance.GetValueOrDefault(-5);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryGetValue_WithPopulatedCollectionAndValidIndex_ShouldReturnTrue()
        {
            // Arrange
            var instance = new Collection<int> { 1, 2, 3 };

            // Act
            bool actual = instance.TryGetValue(0, out _);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void TryGetValue_WithInvalidIndex_ShouldReturnFalse()
        {
            // Arrange
            var instance = new Collection<int> { 1, 2, 3 };

            // Act
            bool actual = instance.TryGetValue(5, out _);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryGetValue_WithNegativeIndex_ShouldReturnFalse()
        {
            // Arrange
            var instance = new Collection<int> { 1, 2, 3 };

            // Act
            bool actual = instance.TryGetValue(-5, out _);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryGetValue_WithPopulatedCollectionAndValidIndex_ShouldSetValue()
        {
            // Arrange
            const int expected = 1;
            var instance = new Collection<int> { expected, 2, 3 };

            // Act
            _ = instance.TryGetValue(0, out int actual);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryGetValue_WithInvalidIndex_ShouldSetAsDefault()
        {
            // Arrange
            const int expected = 0;
            var instance = new Collection<int> { 1, 2, 3 };

            // Act
            _ = instance.TryGetValue(5, out int actual);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
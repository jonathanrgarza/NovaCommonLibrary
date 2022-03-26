using System;
using Ncl.Common.Core.Extensions;
using Xunit;

namespace Ncl.Common.Core.Tests.Extensions
{
    public class TypeExtensionsTests
    {
        [Fact]
        public void IsNullable_WithNullableType_ShouldReturnTrue()
        {
            // Arrange
            Type type = typeof(bool?);

            // Act
            bool actual = type.IsNullableType();

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void IsNullable_WithNonNullableType_ShouldReturnFalse()
        {
            // Arrange
            Type type = typeof(string);

            // Act
            bool actual = type.IsNullableType();

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void IsNullable_WithNullValue_ShouldThrowArgumentNullException()
        {
            // Arrange
            void TestCode()
            {
                Type type = null;

                // Act
                bool actual = type.IsNullableType();
            }

            // Assert
            Assert.Throws<ArgumentNullException>(TestCode);
        }
    }
}
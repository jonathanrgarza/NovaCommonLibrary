using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ncl.Common.Core.Collections;
using Xunit;

namespace Ncl.Common.Core.Tests.Collections
{
    public class LimitedStackTests
    {
        [Fact]
        public void Constructor_WithDefault_ShouldCreateInstanceWithDefault()
        {
            // Act
            var instance = new LimitedStack<int>();

            // Assert
            Assert.Equal(LimitedStack<int>.DefaultMaxCapacity, instance.MaxCapacity);
        }

        [Fact]
        public void Constructor1_WithMaxCapacityValue_ShouldCreateInstanceWithValue()
        {
            //Arrange
            const int expected = 10;

            // Act
            var instance = new LimitedStack<int>(expected);

            // Assert
            Assert.Equal(expected, instance.MaxCapacity);
        }

        [Fact]
        public void Constructor1_WithNegativeMaxCapacity_ShouldThrowArgumentOutOfRangeException()
        {
            //Arrange

            static void TestCode()
            {
                // Act
                var instance = new LimitedStack<int>(-10);
            }

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(TestCode);
        }
    }
}

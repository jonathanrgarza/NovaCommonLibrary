using System;
using Xunit;

namespace Ncl.Common.Core.Measurement.Tests
{
    public class DistanceTests
    {
        [Fact]
        public void Distance_ShouldCreateDefaultInstance()
        {
            //Act
            var instance = new Distance();
            //Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void Distance1_ShouldCreateInstance()
        {
            //Act
            var instance = new Distance(12.5, DistanceUoM.Inch);
            //Assert
            Assert.NotNull(instance);
            Assert.Equal(12.5, instance.Value, 1);
            Assert.Equal(DistanceUoM.Inch, instance.Unit);
        }

        [Fact]
        public void Distance2_ShouldCreateCopyInstance()
        {
            //Arrange
            var original = new Distance(12.5, DistanceUoM.Inch);
            //Act
            var instance = new Distance(original);
            //Assert
            Assert.NotNull(instance);
            Assert.Equal(12.5, instance.Value, 1);
            Assert.Equal(DistanceUoM.Inch, instance.Unit);
        }

        [Fact]
        public void Distance2_NullArgument_ShouldThrowException()
        {
            //Arrange
            Distance original = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var instance = new Distance(original);
            });
        }

        [Theory]
        [InlineData(DistanceUoM.Meter)]
        [InlineData(DistanceUoM.Kilometer)]
        [InlineData(DistanceUoM.Foot)]
        [InlineData(DistanceUoM.Mile)]
        public void Convert_SameUoMShouldReturnSameValue(DistanceUoM uom)
        {
            //Arrange
            const double expected = 5.25;
            var instance = new Distance(expected, uom);

            //Act
            var actual = instance.Convert(uom);

            //Assert
            Assert.Equal(uom, actual.Unit);
            Assert.Equal(expected, actual.Value, 3);
        }

        [Theory]
        [InlineData(1.0, DistanceUoM.Meter, DistanceUoM.Kilometer, 0.001)]
        [InlineData(1.0, DistanceUoM.Meter, DistanceUoM.Foot, 3.281)]
        [InlineData(1.0, DistanceUoM.Millimeter, DistanceUoM.Centimeter, 0.1)]
        [InlineData(1.0, DistanceUoM.Centimeter, DistanceUoM.Meter, 0.01)]
        [InlineData(1.0, DistanceUoM.Inch, DistanceUoM.Foot, 0.083)]
        [InlineData(1.0, DistanceUoM.Foot, DistanceUoM.Yard, 0.333)]
        [InlineData(100.0, DistanceUoM.Yard, DistanceUoM.Mile, 0.057)]
        [InlineData(1.0, DistanceUoM.Foot, DistanceUoM.Inch, 12.0)]
        public void Convert_DifferentUoMShouldReturnConvertedValue(double initialValue, DistanceUoM from, DistanceUoM to, double expected)
        {
            //Arrange
            var instance = new Distance(initialValue, from);

            //Act
            var actual = instance.Convert(to);

            //Assert
            Assert.Equal(to, actual.Unit);
            Assert.Equal(expected, actual.Value, 3);
        }

        [Fact]
        public void Add_SameUom_ShouldReturnAdditionResult()
        {
            //Sanity check test - should be handled by MeasurementTests anyway

            const double expectedValue = 5.0;
            const DistanceUoM expectedUoM = DistanceUoM.Meter;

            double otherValue = 1.5;
            DistanceUoM otherUnit = DistanceUoM.Meter;
            var mock = new Distance(3.5, DistanceUoM.Meter);
            var mock2 = new Distance(otherValue, otherUnit);

            var actual = mock.Add(mock2);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }
    }
}
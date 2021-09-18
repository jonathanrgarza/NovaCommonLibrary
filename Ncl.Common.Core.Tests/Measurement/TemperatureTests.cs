using System;
using Xunit;

namespace Ncl.Common.Core.Measurement.Tests
{
    public class TemperatureTests
    {
        [Fact]
        public void Temperature_ShouldCreateDefaultInstance()
        {
            //Act
            var instance = new Temperature();
            //Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void Temperature1_ShouldCreateInstance()
        {
            //Act
            var instance = new Temperature(12.5, TemperatureUoM.Celsius);
            //Assert
            Assert.NotNull(instance);
            Assert.Equal(12.5, instance.Value, 1);
            Assert.Equal(TemperatureUoM.Celsius, instance.Unit);
        }

        [Fact]
        public void Temperature2_ShouldCreateCopyInstance()
        {
            //Arrange
            var original = new Temperature(12.5, TemperatureUoM.Celsius);
            //Act
            var instance = new Temperature(original);
            //Assert
            Assert.NotNull(instance);
            Assert.Equal(12.5, instance.Value, 1);
            Assert.Equal(TemperatureUoM.Celsius, instance.Unit);
        }

        [Fact]
        public void Temperature2_NullArgument_ShouldThrowException()
        {
            //Arrange
            Temperature original = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var instance = new Temperature(original);
            });
        }

        [Theory]
        [InlineData(TemperatureUoM.Kelvin)]
        [InlineData(TemperatureUoM.Celsius)]
        [InlineData(TemperatureUoM.Fahrenheit)]
        public void Convert_SameUoMShouldReturnSameValue(TemperatureUoM uom)
        {
            //Arrange
            const double expected = 5.25;
            var instance = new Temperature(expected, uom);

            //Act
            Temperature actual = instance.Convert(uom);

            //Assert
            Assert.Equal(uom, actual.Unit);
            Assert.Equal(expected, actual.Value, 3);
        }

        [Theory]
        [InlineData(1.0, TemperatureUoM.Kelvin, TemperatureUoM.Celsius, -272.15)]
        [InlineData(1.0, TemperatureUoM.Kelvin, TemperatureUoM.Fahrenheit, -457.87)]
        [InlineData(1.0, TemperatureUoM.Celsius, TemperatureUoM.Kelvin, 274.15)]
        [InlineData(1.0, TemperatureUoM.Celsius, TemperatureUoM.Fahrenheit, 33.8)]
        [InlineData(1.0, TemperatureUoM.Fahrenheit, TemperatureUoM.Kelvin, 255.928)]
        [InlineData(1.0, TemperatureUoM.Fahrenheit, TemperatureUoM.Celsius, -17.2222)]
        public void Convert_DifferentUoMShouldReturnConvertedValue(double initialValue, TemperatureUoM from, TemperatureUoM to, double expected)
        {
            //Arrange
            var instance = new Temperature(initialValue, from);

            //Act
            Temperature actual = instance.Convert(to);

            //Assert
            Assert.Equal(to, actual.Unit);
            Assert.Equal(expected, actual.Value, 3);
        }

        [Fact]
        public void Add_SameUom_ShouldReturnAdditionResult()
        {
            //Sanity check test - should be handled by MeasurementTests anyway

            const double expectedValue = 5.0;
            const TemperatureUoM expectedUoM = TemperatureUoM.Celsius;

            double otherValue = 1.5;
            var otherUnit = TemperatureUoM.Celsius;
            var mock = new Temperature(3.5, TemperatureUoM.Celsius);
            var mock2 = new Temperature(otherValue, otherUnit);

            Temperature actual = mock.Add(mock2);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }
    }
}
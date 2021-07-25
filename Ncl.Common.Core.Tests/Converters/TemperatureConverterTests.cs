using Ncl.Common.Core.Measurement;
using Xunit;

namespace Ncl.Common.Core.Converters.Tests
{
    public class TemperatureConverterTests
    {
        private const int precison = 3;

        [Fact]
        public void TemperatureConverter_ShouldConstructInstance()
        {
            //Act
            var instance = new TemperatureConverter();
            //Assert
            Assert.NotNull(instance);
        }

        [Theory]
        [InlineData(TemperatureUoM.Kelvin)]
        [InlineData(TemperatureUoM.Celsius)]
        [InlineData(TemperatureUoM.Fahrenheit)]
        public void Convert_SameUoMShouldReturnSameValue(TemperatureUoM uom)
        {
            //Arrange
            const double expected = 5.0;
            var converter = new TemperatureConverter();

            //Act
            double actual = converter.Convert(expected, uom, uom);

            //Assert
            Assert.Equal(expected, actual);
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
            var converter = new TemperatureConverter();

            //Act
            double actual = converter.Convert(initialValue, from, to);

            //Assert
            Assert.Equal(expected, actual, precison);
        }

        [Fact]
        public void ToKelvin_SameUoMShouldReturnSameValue()
        {
            //Arrange
            const double expected = 225.25;
            var converter = new TemperatureConverter();

            //Act
            double actual = converter.ToKelvin(expected, TemperatureUoM.Kelvin);

            //Assert
            Assert.Equal(expected, actual, precison);
        }

        [Theory]
        [InlineData(1.0, TemperatureUoM.Celsius, 274.15)]
        [InlineData(1.0, TemperatureUoM.Fahrenheit, 255.928)]
        public void ToKelvin_DifferentUoMShouldReturnConvertedValue(double initialValue, TemperatureUoM from, double expected)
        {
            //Arrange
            var converter = new TemperatureConverter();

            //Act
            double actual = converter.ToKelvin(initialValue, from);

            //Assert
            Assert.Equal(expected, actual, precison);
        }

        [Fact]
        public void ToCelsius_SameUoMShouldReturnSameValue()
        {
            //Arrange
            const double expected = 225.25;
            var converter = new TemperatureConverter();

            //Act
            double actual = converter.ToCelsius(expected, TemperatureUoM.Celsius);

            //Assert
            Assert.Equal(expected, actual, precison);
        }

        [Theory]
        [InlineData(1.0, TemperatureUoM.Kelvin, -272.15)]
        [InlineData(1.0, TemperatureUoM.Fahrenheit, -17.2222)]
        public void ToCelsius_DifferentUoMShouldReturnConvertedValue(double initialValue, TemperatureUoM from, double expected)
        {
            //Arrange
            var converter = new TemperatureConverter();

            //Act
            double actual = converter.ToCelsius(initialValue, from);

            //Assert
            Assert.Equal(expected, actual, precison);
        }

        [Fact]
        public void ToFahrenheit_SameUoMShouldReturnSameValue()
        {
            //Arrange
            const double expected = 225.25;
            var converter = new TemperatureConverter();

            //Act
            double actual = converter.ToFahrenheit(expected, TemperatureUoM.Fahrenheit);

            //Assert
            Assert.Equal(expected, actual, precison);
        }

        [Theory]
        [InlineData(1.0, TemperatureUoM.Kelvin, -457.87)]
        [InlineData(1.0, TemperatureUoM.Celsius, 33.8)]
        public void ToFahrenheit_DifferentUoMShouldReturnConvertedValue(double initialValue, TemperatureUoM from, double expected)
        {
            //Arrange
            var converter = new TemperatureConverter();

            //Act
            double actual = converter.ToFahrenheit(initialValue, from);

            //Assert
            Assert.Equal(expected, actual, precison);
        }
    }
}

using Ncl.Common.Core.Converters;
using Ncl.Common.Core.Measurement;
using Xunit;

namespace Ncl.Common.Core.Tests.Converters
{
    public class AngleConverterTests
    {
        private const int Precision = 5;

        [Fact]
        public void AngleConverter_ShouldConstructInstance()
        {
            //Act
            var instance = new AngleConverter();
            //Assert
            Assert.NotNull(instance);
        }

        [Theory]
        [InlineData(AngleUoM.Radian)]
        [InlineData(AngleUoM.Degree)]
        [InlineData(AngleUoM.Revolution)]
        public void Convert_SameUoMShouldReturnSameValue(AngleUoM uom)
        {
            //Arrange
            const double expected = 5.0;
            var converter = new AngleConverter();

            //Act
            double actual = converter.Convert(expected, uom, uom);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1.0, AngleUoM.Radian, AngleUoM.Degree, 57.29578)]
        [InlineData(1.0, AngleUoM.Radian, AngleUoM.Revolution, 0.15915)]
        [InlineData(1.0, AngleUoM.Degree, AngleUoM.Radian, 0.01745)]
        [InlineData(1.0, AngleUoM.Degree, AngleUoM.Revolution, 0.00278)]
        [InlineData(1.0, AngleUoM.Revolution, AngleUoM.Radian, 6.28319)]
        [InlineData(1.0, AngleUoM.Revolution, AngleUoM.Degree, 360.0)]
        public void Convert_DifferentUoMShouldReturnConvertedValue(double initialValue, AngleUoM from, AngleUoM to, double expected)
        {
            //Arrange
            var converter = new AngleConverter();

            //Act
            double actual = converter.Convert(initialValue, from, to);

            //Assert
            Assert.Equal(expected, actual, Precision);
        }

        [Fact]
        public void ToRadian_SameUoMShouldReturnSameValue()
        {
            //Arrange
            const double expected = 225.25;
            var converter = new AngleConverter();

            //Act
            double actual = converter.ToRadian(expected, AngleUoM.Radian);

            //Assert
            Assert.Equal(expected, actual, Precision);
        }

        [Theory]
        [InlineData(1.0, AngleUoM.Degree, 0.01745)]
        [InlineData(1.0, AngleUoM.Revolution, 6.28319)]
        public void ToRadian_DifferentUoMShouldReturnConvertedValue(double initialValue, AngleUoM from, double expected)
        {
            //Arrange
            var converter = new AngleConverter();

            //Act
            double actual = converter.ToRadian(initialValue, from);

            //Assert
            Assert.Equal(expected, actual, Precision);
        }

        [Fact]
        public void ToDegree_SameUoMShouldReturnSameValue()
        {
            //Arrange
            const double expected = 225.25;
            var converter = new AngleConverter();

            //Act
            double actual = converter.ToDegree(expected, AngleUoM.Degree);

            //Assert
            Assert.Equal(expected, actual, Precision);
        }

        [Theory]
        [InlineData(1.0, AngleUoM.Radian, 57.29578)]
        [InlineData(1.0, AngleUoM.Revolution, 360.0)]
        public void ToDegree_DifferentUoMShouldReturnConvertedValue(double initialValue, AngleUoM from, double expected)
        {
            //Arrange
            var converter = new AngleConverter();

            //Act
            double actual = converter.ToDegree(initialValue, from);

            //Assert
            Assert.Equal(expected, actual, Precision);
        }

        [Fact]
        public void ToRevolution_SameUoMShouldReturnSameValue()
        {
            //Arrange
            const double expected = 225.25;
            var converter = new AngleConverter();

            //Act
            double actual = converter.ToRevolution(expected, AngleUoM.Revolution);

            //Assert
            Assert.Equal(expected, actual, Precision);
        }

        [Theory]
        [InlineData(1.0, AngleUoM.Radian, 0.15915)]
        [InlineData(1.0, AngleUoM.Degree, 0.00278)]
        public void ToRevolution_DifferentUoMShouldReturnConvertedValue(double initialValue, AngleUoM from, double expected)
        {
            //Arrange
            var converter = new AngleConverter();

            //Act
            double actual = converter.ToRevolution(initialValue, from);

            //Assert
            Assert.Equal(expected, actual, Precision);
        }
    }
}
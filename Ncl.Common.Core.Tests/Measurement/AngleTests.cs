using System;
using Xunit;

namespace Ncl.Common.Core.Measurement.Tests
{
    public class AngleTests
    {
        [Fact]
        public void Angle_ShouldCreateDefaultInstance()
        {
            //Act
            var instance = new Angle();
            //Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void Angle1_ShouldCreateInstance()
        {
            //Act
            var instance = new Angle(12.5, AngleUoM.Radian);
            //Assert
            Assert.NotNull(instance);
            Assert.Equal(12.5, instance.Value, 1);
            Assert.Equal(AngleUoM.Radian, instance.Unit);
        }

        [Fact]
        public void Angle2_ShouldCreateCopyInstance()
        {
            //Arrange
            var original = new Angle(12.5, AngleUoM.Degree);
            //Act
            var instance = new Angle(original);
            //Assert
            Assert.NotNull(instance);
            Assert.Equal(12.5, instance.Value, 1);
            Assert.Equal(AngleUoM.Degree, instance.Unit);
        }

        [Fact]
        public void Angle2_NullArgument_ShouldThrowException()
        {
            //Arrange
            Angle original = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var instance = new Angle(original);
            });
        }

        [Theory]
        [InlineData(AngleUoM.Radian)]
        [InlineData(AngleUoM.Degree)]
        [InlineData(AngleUoM.Revolution)]
        public void Convert_SameUoMShouldReturnSameValue(AngleUoM uom)
        {
            //Arrange
            const double expected = 5.25;
            var instance = new Angle(expected, uom);

            //Act
            var actual = instance.Convert(uom);

            //Assert
            Assert.Equal(uom, actual.Unit);
            Assert.Equal(expected, actual.Value, 3);
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
            var instance = new Angle(initialValue, from);

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
            const AngleUoM expectedUoM = AngleUoM.Radian;

            double otherValue = 1.5;
            AngleUoM otherUnit = AngleUoM.Radian;
            var mock = new Angle(3.5, AngleUoM.Radian);
            var mock2 = new Angle(otherValue, otherUnit);

            var actual = mock.Add(mock2);

            Assert.Equal(expectedValue, actual.Value, 2);
            Assert.Equal(expectedUoM, actual.Unit);
        }

        [Fact]
        public void ToDegree_SameUoMAndWithinRangeShouldReturnSameValue()
        {
            //Arrange
            const double expected = 5.25;
            var instance = new Angle(expected, AngleUoM.Degree);

            //Act
            var actual = instance.ToDegree(true);

            //Assert
            Assert.Equal(AngleUoM.Degree, actual.Unit);
            Assert.Equal(expected, actual.Value, 3);
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(1.0)]
        [InlineData(360.0)]
        [InlineData(720.0)]
        [InlineData(365.0)]
        [InlineData(-365.0)]
        public void ToDegree_SameUoMAndFalseShouldReturnSameValue(double initial)
        {
            //Arrange
            var instance = new Angle(initial, AngleUoM.Degree);

            //Act
            var actual = instance.ToDegree(false);

            //Assert
            Assert.Equal(AngleUoM.Degree, actual.Unit);
            Assert.Equal(initial, actual.Value, 3);
        }

        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(1.0, 1.0)]
        [InlineData(360.0, 0.0)]
        [InlineData(720.0, 0.0)]
        [InlineData(365.0, 5.0)]
        [InlineData(-365.0, 355.0)]
        [InlineData(-360.0, 0.0)]
        public void ToDegree_SameUoMShouldReturnNormalizedValue(double initial, double expected)
        {
            //Arrange
            var instance = new Angle(initial, AngleUoM.Degree);

            //Act
            var actual = instance.ToDegree(true);

            //Assert
            Assert.Equal(AngleUoM.Degree, actual.Unit);
            Assert.Equal(expected, actual.Value, 3);
        }

        [Theory]
        [InlineData(1.0, AngleUoM.Radian, 57.29578)]
        [InlineData(1.0, AngleUoM.Revolution, 0.0)]

        public void ToDegree_DifferentUoMShouldReturnNormalizedValue(double initialValue, AngleUoM from, double expected)
        {
            //Arrange
            var instance = new Angle(initialValue, from);

            //Act
            var actual = instance.ToDegree(true);

            //Assert
            Assert.Equal(AngleUoM.Degree, actual.Unit);
            Assert.Equal(expected, actual.Value, 3);
        }
    }
}
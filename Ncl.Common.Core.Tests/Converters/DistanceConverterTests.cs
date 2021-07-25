using Ncl.Common.Core.Measurement;
using Xunit;

namespace Ncl.Common.Core.Converters.Tests
{
    public class DistanceConverterTests
    {
        private const int precison = 3;

        [Fact]
        public void DistanceConverter_ShouldConstructInstance()
        {
            //Act
            var instance = new DistanceConverter();
            //Assert
            Assert.NotNull(instance);
        }

        [Theory]
        [InlineData(DistanceUoM.Meter)]
        [InlineData(DistanceUoM.Kilometer)]
        [InlineData(DistanceUoM.Foot)]
        [InlineData(DistanceUoM.Mile)]
        public void Convert_SameUoMShouldReturnSameValue(DistanceUoM uom)
        {
            //Arrange
            var converter = new DistanceConverter();
            const double expected = 5.0;

            //Act
            double actual = converter.Convert(expected, uom, uom);

            //Assert
            Assert.Equal(expected, actual);
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
            var converter = new DistanceConverter();

            //Act
            double actual = converter.Convert(initialValue, from, to);

            //Assert
            Assert.Equal(expected, actual, precison);
        }
    }
}

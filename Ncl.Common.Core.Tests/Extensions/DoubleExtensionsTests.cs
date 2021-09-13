using Xunit;

namespace Ncl.Common.Core.Extensions.Tests
{
    public class DoubleExtensionsTests
    {
        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(1.576, 1.576)]
        [InlineData(500.57985, 500.57985)]
        [InlineData(double.NaN, double.NaN)]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity, double.NegativeInfinity)]
        public void IsEqual_SameValuesShouldReturnTrue(double first, double second)
        {
            Assert.True(first.IsEqual(second));
        }

        [Theory]
        [InlineData(0.0, 1.576)]
        [InlineData(1.576, 1.6)]
        [InlineData(1.576, 1.5)]
        [InlineData(double.NaN, 1.5)]
        [InlineData(double.PositiveInfinity, 1.5)]
        [InlineData(double.NegativeInfinity, 1.5)]
        [InlineData(double.NegativeInfinity, double.PositiveInfinity)]
        public void IsEqual_DifferentValuesShouldReturnFalse(double first, double second)
        {
            Assert.False(first.IsEqual(second));
        }

        [Theory]
        [InlineData(0.0, 1.576, 0.0)]
        [InlineData(1.576, 2.657, 4.187)]
        [InlineData(150, 0.333333, 49.99995)]
        public void IsEqual_CalculatedValuesShouldReturnTrue(double first, double second, double expected)
        {
            var result = first * second;
            Assert.True(result.IsEqual(expected));
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(0.0, 0.0)]
        [InlineData(1.576, 1.576)]
        [InlineData(500.57985, 500.57985)]
        [InlineData(double.NaN, double.NaN)]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity, double.NegativeInfinity)]
        public void IsEqual1_SameValuesShouldReturnTrue(double? first, double? second)
        {
            Assert.True(first.IsEqual(second));
        }

        [Theory]
        [InlineData(null, 1.05)]
        [InlineData(2.50, null)]
        [InlineData(0.0, 1.576)]
        [InlineData(1.576, 1.6)]
        [InlineData(1.576, 1.5)]
        [InlineData(double.NaN, 1.5)]
        [InlineData(double.PositiveInfinity, 1.5)]
        [InlineData(double.NegativeInfinity, 1.5)]
        [InlineData(double.NegativeInfinity, double.PositiveInfinity)]
        public void IsEqual1_DifferentValuesShouldReturnFalse(double? first, double? second)
        {
            Assert.False(first.IsEqual(second));
        }

        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(1.576, 1.576)]
        [InlineData(500.57985, 500.57985)]
        [InlineData(double.NaN, double.NaN)]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity, double.NegativeInfinity)]
        public void IsEqualTo_SameValuesShouldReturnTrue(double first, double second)
        {
            Assert.True(first.IsEqualTo(second));
        }

        [Theory]
        [InlineData(0.0, 1.576)]
        [InlineData(1.576, 1.6)]
        [InlineData(1.576, 1.5)]
        [InlineData(double.NaN, 1.5)]
        [InlineData(double.PositiveInfinity, 1.5)]
        [InlineData(double.NegativeInfinity, 1.5)]
        [InlineData(double.NegativeInfinity, double.PositiveInfinity)]
        public void IsEqualTo_DifferentValuesShouldReturnFalse(double first, double second)
        {
            Assert.False(first.IsEqualTo(second));
        }

        [Theory]
        [InlineData(0.0, 1.576, 0.0)]
        [InlineData(1.576, 2.657, 4.187)]
        [InlineData(150, 0.333333, 49.99995)]
        public void IsEqualTo_CalculatedValuesShouldReturnTrue(double first, double second, double expected)
        {
            var result = first * second;
            Assert.True(result.IsEqualTo(expected));
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(0.0, 0.0)]
        [InlineData(1.576, 1.576)]
        [InlineData(500.57985, 500.57985)]
        [InlineData(double.NaN, double.NaN)]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity, double.NegativeInfinity)]
        public void IsEqualTo1_SameValuesShouldReturnTrue(double? first, double? second)
        {
            Assert.True(first.IsEqualTo(second));
        }

        [Theory]
        [InlineData(null, 1.05)]
        [InlineData(2.50, null)]
        [InlineData(0.0, 1.576)]
        [InlineData(1.576, 1.6)]
        [InlineData(1.576, 1.5)]
        [InlineData(double.NaN, 1.5)]
        [InlineData(double.PositiveInfinity, 1.5)]
        [InlineData(double.NegativeInfinity, 1.5)]
        [InlineData(double.NegativeInfinity, double.PositiveInfinity)]
        public void IsEqualTo1_DifferentValuesShouldReturnFalse(double? first, double? second)
        {
            Assert.False(first.IsEqualTo(second));
        }
    }
}
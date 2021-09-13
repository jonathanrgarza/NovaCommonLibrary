using Xunit;

namespace Ncl.Common.Core.Extensions.Tests
{
    public class FloatExtensionsTests
    {
        [Theory]
        [InlineData(0.0f, 0.0f)]
        [InlineData(1.576f, 1.576f)]
        [InlineData(500.57985f, 500.57985f)]
        [InlineData(float.NaN, float.NaN)]
        [InlineData(float.PositiveInfinity, float.PositiveInfinity)]
        [InlineData(float.NegativeInfinity, float.NegativeInfinity)]
        public void IsEqual_SameValuesShouldReturnTrue(float first, float second)
        {
            Assert.True(first.IsEqual(second));
        }

        [Theory]
        [InlineData(0.0f, 1.576f)]
        [InlineData(1.576f, 1.6f)]
        [InlineData(1.576f, 1.5f)]
        [InlineData(float.NaN, 1.5f)]
        [InlineData(float.PositiveInfinity, 1.5f)]
        [InlineData(float.NegativeInfinity, 1.5f)]
        [InlineData(float.NegativeInfinity, float.PositiveInfinity)]
        public void IsEqual_DifferentValuesShouldReturnFalse(float first, float second)
        {
            Assert.False(first.IsEqual(second));
        }

        [Theory]
        [InlineData(0.0f, 1.576f, 0.0f)]
        [InlineData(1.576f, 2.657f, 4.187f)]
        [InlineData(150f, 0.333333f, 49.99995f)]
        public void IsEqual_CalculatedValuesShouldReturnTrue(float first, float second, float expected)
        {
            var result = first * second;
            Assert.True(result.IsEqual(expected));
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(0.0f, 0.0f)]
        [InlineData(1.576f, 1.576f)]
        [InlineData(500.57985f, 500.57985f)]
        [InlineData(float.NaN, float.NaN)]
        [InlineData(float.PositiveInfinity, float.PositiveInfinity)]
        [InlineData(float.NegativeInfinity, float.NegativeInfinity)]
        public void IsEqual1_SameValuesShouldReturnTrue(float? first, float? second)
        {
            Assert.True(first.IsEqual(second));
        }

        [Theory]
        [InlineData(null, 1.05f)]
        [InlineData(2.50f, null)]
        [InlineData(0.0f, 1.576f)]
        [InlineData(1.576f, 1.6f)]
        [InlineData(1.576f, 1.5f)]
        [InlineData(float.NaN, 1.5f)]
        [InlineData(float.PositiveInfinity, 1.5f)]
        [InlineData(float.NegativeInfinity, 1.5f)]
        [InlineData(float.NegativeInfinity, float.PositiveInfinity)]
        public void IsEqual1_DifferentValuesShouldReturnFalse(float? first, float? second)
        {
            Assert.False(first.IsEqual(second));
        }

        [Theory]
        [InlineData(0.0f, 0.0f)]
        [InlineData(1.576f, 1.576f)]
        [InlineData(500.57985f, 500.57985f)]
        [InlineData(float.NaN, float.NaN)]
        [InlineData(float.PositiveInfinity, float.PositiveInfinity)]
        [InlineData(float.NegativeInfinity, float.NegativeInfinity)]
        public void IsEqualTo_SameValuesShouldReturnTrue(float first, float second)
        {
            Assert.True(first.IsEqualTo(second));
        }

        [Theory]
        [InlineData(0.0f, 1.576f)]
        [InlineData(1.576f, 1.6f)]
        [InlineData(1.576f, 1.5f)]
        [InlineData(float.NaN, 1.5f)]
        [InlineData(float.PositiveInfinity, 1.5f)]
        [InlineData(float.NegativeInfinity, 1.5f)]
        [InlineData(float.NegativeInfinity, float.PositiveInfinity)]
        public void IsEqualTo_DifferentValuesShouldReturnFalse(float first, float second)
        {
            Assert.False(first.IsEqualTo(second));
        }

        [Theory]
        [InlineData(0.0f, 1.576f, 0.0f)]
        [InlineData(1.576f, 2.657f, 4.187f)]
        [InlineData(150f, 0.333333f, 49.99995f)]
        public void IsEqualTo_CalculatedValuesShouldReturnTrue(float first, float second, float expected)
        {
            var result = first * second;
            Assert.True(result.IsEqualTo(expected));
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(0.0f, 0.0f)]
        [InlineData(1.576f, 1.576f)]
        [InlineData(500.57985f, 500.57985f)]
        [InlineData(float.NaN, float.NaN)]
        [InlineData(float.PositiveInfinity, float.PositiveInfinity)]
        [InlineData(float.NegativeInfinity, float.NegativeInfinity)]
        public void IsEqualTo1_SameValuesShouldReturnTrue(float? first, float? second)
        {
            Assert.True(first.IsEqualTo(second));
        }

        [Theory]
        [InlineData(null, 1.05f)]
        [InlineData(2.50f, null)]
        [InlineData(0.0f, 1.576f)]
        [InlineData(1.576f, 1.6f)]
        [InlineData(1.576f, 1.5f)]
        [InlineData(float.NaN, 1.5f)]
        [InlineData(float.PositiveInfinity, 1.5f)]
        [InlineData(float.NegativeInfinity, 1.5f)]
        [InlineData(float.NegativeInfinity, float.PositiveInfinity)]
        public void IsEqualTo1_DifferentValuesShouldReturnFalse(float? first, float? second)
        {
            Assert.False(first.IsEqualTo(second));
        }
    }
}
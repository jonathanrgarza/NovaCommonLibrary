using Xunit;

namespace Ncl.Common.Core.Extensions.Tests
{
    public class FloatExtensionsTests
    {
        [Theory]
        [InlineData(0.0f, 0.0f)]
        [InlineData(1.576f, 1.576f)]
        [InlineData(500.57985f, 500.57985f)]
        public void IsEqual_SameValuesShouldReturnTrue(float first, float second)
        {
            Assert.True(first.IsEqual(second));
        }

        [Theory]
        [InlineData(0.0f, 1.576f)]
        [InlineData(1.576f, 1.6f)]
        [InlineData(1.576f, 1.5f)]
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
        public void IsEqual1_DifferentValuesShouldReturnFalse(float? first, float? second)
        {
            Assert.False(first.IsEqual(second));
        }
    }
}
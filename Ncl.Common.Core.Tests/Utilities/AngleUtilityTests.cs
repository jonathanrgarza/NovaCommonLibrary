using Ncl.Common.Core.Utilities;
using Xunit;

namespace Ncl.Common.Core.Tests.Utilities
{
    public class AngleUtilityTests
    {
        [Theory]
        [InlineData(0.0)]
        [InlineData(90.0)]
        [InlineData(359.9999)]
        public void IsWithinNormalizeDegreeRange_WithValidValues_ShouldReturnTrue(double value)
        {
            // Act
            bool actual = AngleUtility.IsWithinNormalizeDegreeRange(value);

            // Assert
            Assert.True(actual);
        }

        [Theory]
        [InlineData(-90.0)]
        [InlineData(-0.1)]
        [InlineData(360.0)]
        [InlineData(720.0)]
        public void IsWithinNormalizeDegreeRange_WithInvalidValues_ShouldReturnFalse(double value)
        {
            // Act
            bool actual = AngleUtility.IsWithinNormalizeDegreeRange(value);

            // Assert
            Assert.False(actual);
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(90.0)]
        [InlineData(359.9999)]
        public void GetNormalizeDegreeValue_WithValidValues_ShouldReturnSameValue(double value)
        {
            // Act
            double actual = AngleUtility.GetNormalizeDegreeValue(value);

            // Assert
            Assert.Equal(value, actual, 3);
        }

        [Theory]
        [InlineData(-90.0, 270.0)]
        [InlineData(450.0, 90.0)]
        [InlineData(360.0, 0.0)]
        public void GetNormalizeDegreeValue_WithInvalidValues_ShouldReturnNormalizedValue(double value, double expected)
        {
            // Act
            double actual = AngleUtility.GetNormalizeDegreeValue(value);

            // Assert
            Assert.Equal(expected, actual, 3);
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(90.0)]
        [InlineData(359.9999)]
        public void GetNormalizeDegreeValue1_WithValidValues_ShouldReturnFalse(double value)
        {
            // Act
            bool actual = AngleUtility.GetNormalizeDegreeValue(value, out _);

            // Assert
            Assert.False(actual);
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(90.0)]
        [InlineData(359.9999)]
        public void GetNormalizeDegreeValue1_WithValidValues_ShouldHaveOutAsSameValue(double value)
        {
            // Act
            _ = AngleUtility.GetNormalizeDegreeValue(value, out double actual);

            // Assert
            Assert.Equal(value, actual, 3);
        }

        [Theory]
        [InlineData(-90.0)]
        [InlineData(450.0)]
        [InlineData(360.0)]
        public void GetNormalizeDegreeValue1_WithInvalidValues_ShouldReturnTrue(double value)
        {
            // Act
            bool actual = AngleUtility.GetNormalizeDegreeValue(value, out _);

            // Assert
            Assert.True(actual);
        }

        [Theory]
        [InlineData(-90.0, 270.0)]
        [InlineData(450.0, 90.0)]
        [InlineData(360.0, 0.0)]
        public void GetNormalizeDegreeValue1_WithInvalidValues_ShouldHaveOutAsNormalizedValue(double value, double expected)
        {
            // Act
            _ = AngleUtility.GetNormalizeDegreeValue(value, out double actual);

            // Assert
            Assert.Equal(expected, actual, 3);
        }
    }
}
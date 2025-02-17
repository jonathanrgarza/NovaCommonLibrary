using System;
using Ncl.Common.Core.Models;
using Xunit;

namespace Ncl.Common.Core.Tests.Models;

public class RangeTests
{
    [Fact]
    public void Range_ShouldCreateInfiniteInstance()
    {
        // Act
        var range = new Range<int>();

        // Assert
        Assert.NotNull(range);
        Assert.True(range.IsInfinite);
    }

    [Fact]
    public void Range_ShouldBeInfiniteInstance()
    {
        // Act
        var range = Range<int>.Infinite;

        // Assert
        Assert.NotNull(range);
        Assert.True(range.IsInfinite);
    }

    [Fact]
    public void Range_ShouldCreateEmptyInstance()
    {
        // Act
        var range = new Range<int>(0, 0, false, false);

        // Assert
        Assert.NotNull(range);
        Assert.True(range.IsEmpty);
    }

    [Fact]
    public void Range_ShouldBeEmptyInstance()
    {
        // Act
        var range = Range<int>.Empty;

        // Assert
        Assert.NotNull(range);
        Assert.True(range.IsEmpty);
    }

    [Fact]
    public void Range_ShouldCreateInstanceWithValues()
    {
        // Act
        var range = new Range<int>(1, 10);

        // Assert
        Assert.NotNull(range);
        Assert.Equal(1, range.Min);
        Assert.Equal(10, range.Max);
        Assert.True(range.InclusiveMin);
        Assert.True(range.InclusiveMax);
    }

    [Fact]
    public void Range_CopyConstructor_ShouldCreateIdenticalInstance()
    {
        // Arrange
        var original = new Range<int>(1, 10);

        // Act
        var copy = new Range<int>(original);

        // Assert
        Assert.NotNull(copy);
        Assert.Equal(original.Min, copy.Min);
        Assert.Equal(original.Max, copy.Max);
        Assert.Equal(original.InclusiveMin, copy.InclusiveMin);
        Assert.Equal(original.InclusiveMax, copy.InclusiveMax);
        Assert.Equal(original.IsInfinite, copy.IsInfinite);
        Assert.Equal(original.IsEmpty, copy.IsEmpty);
    }

    [Fact]
    public void Range_ShouldThrowExceptionForInvalidRange()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Range<int>(10, 1));
    }

    [Fact]
    public void Range_ShouldThrowExceptionForEqualMinMaxWithDifferentInclusivity()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Range<int>(5, 5, true, false));
    }

    [Fact]
    public void Range_ShouldContainValue()
    {
        // Arrange
        var range = new Range<int>(1, 10);

        // Act
        bool result = range.Contains(5);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Range_ShouldNotContainValue()
    {
        // Arrange
        var range = new Range<int>(1, 10);

        // Act
        bool result = range.Contains(11);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void InfiniteRange_ShouldContainAllValues()
    {
        // Arrange
        var range = Range<int>.Infinite;

        // Act & Assert
        Assert.True(range.Contains(int.MinValue));
        Assert.True(range.Contains(0));
        Assert.True(range.Contains(int.MaxValue));
    }

    [Fact]
    public void EmptyRange_ShouldContainNoValues()
    {
        // Arrange
        var range = Range<int>.Empty;

        // Act & Assert
        Assert.False(range.Contains(int.MinValue));
        Assert.False(range.Contains(0));
        Assert.False(range.Contains(int.MaxValue));
    }

    [Fact]
    public void Range_ShouldContainRange()
    {
        // Arrange
        var range = new Range<int>(1, 10);
        var subRange = new Range<int>(2, 5);

        // Act
        bool result = range.Contains(subRange);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Range_ShouldNotContainRange()
    {
        // Arrange
        var range = new Range<int>(1, 10);
        var subRange = new Range<int>(0, 5);

        // Act
        bool result = range.Contains(subRange);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void InfiniteRange_ShouldContainAnyRange()
    {
        // Arrange
        var infiniteRange = Range<int>.Infinite;
        var subRange = new Range<int>(1, 10);

        // Act
        bool result = infiniteRange.Contains(subRange);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void InfiniteRange_ShouldNotContainInfiniteRange()
    {
        // Arrange
        var infiniteRange = Range<int>.Infinite;
        var subRange = Range<int>.Infinite;

        // Act
        bool result = infiniteRange.Contains(subRange);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void EmptyRange_ShouldContainNoRange()
    {
        // Arrange
        var emptyRange = Range<int>.Empty;
        var subRange = new Range<int>(1, 10);

        // Act
        bool result = emptyRange.Contains(subRange);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void EmptyRange_ShouldNotContainEmptyRange()
    {
        // Arrange
        var emptyRange = Range<int>.Empty;
        var subRange = Range<int>.Empty;

        // Act
        bool result = emptyRange.Contains(subRange);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Range_Equals_ShouldReturnTrueForEqualRanges()
    {
        // Arrange
        var range1 = new Range<int>(1, 10);
        var range2 = new Range<int>(1, 10);

        // Act
        bool result = range1.Equals(range2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Range_Equals_ShouldReturnFalseForDifferentRanges()
    {
        // Arrange
        var range1 = new Range<int>(1, 10);
        var range2 = new Range<int>(1, 5);

        // Act
        bool result = range1.Equals(range2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Range_GetHashCode_ShouldReturnSameValueForEqualRanges()
    {
        // Arrange
        var range1 = new Range<int>(1, 10);
        var range2 = new Range<int>(1, 10);

        // Act
        int hashCode1 = range1.GetHashCode();
        int hashCode2 = range2.GetHashCode();

        // Assert
        Assert.Equal(hashCode1, hashCode2);
    }

    [Fact]
    public void Range_GetHashCode_ShouldReturnDifferentValuesForDifferentRanges()
    {
        // Arrange
        var range1 = new Range<int>(1, 10);
        var range2 = new Range<int>(1, 5);

        // Act
        int hashCode1 = range1.GetHashCode();
        int hashCode2 = range2.GetHashCode();

        // Assert
        Assert.NotEqual(hashCode1, hashCode2);
    }

    [Fact]
    public void Range_ToString_ShouldReturnCorrectString()
    {
        // Arrange
        var range = new Range<int>(1, 10);

        // Act
        string result = range.ToString();

        // Assert
        Assert.Equal("[1,10]", result);
    }

    [Fact]
    public void Range_ToString_ShouldReturnExclusiveString()
    {
        // Arrange
        var range = new Range<int>(1, 10, false, false);

        // Act
        string result = range.ToString();

        // Assert
        Assert.Equal("(1,10)", result);
    }

    [Fact]
    public void Range_ToString_ShouldReturnLeftInclusiveString()
    {
        // Arrange
        var range = new Range<int>(1, 10, true, false);

        // Act
        string result = range.ToString();

        // Assert
        Assert.Equal("[1,10)", result);
    }

    [Fact]
    public void Range_ToString_ShouldReturnRightInclusiveString()
    {
        // Arrange
        var range = new Range<int>(1, 10, false);

        // Act
        string result = range.ToString();

        // Assert
        Assert.Equal("(1,10]", result);
    }

    [Fact]
    public void Range_ToString_ShouldReturnEmptyString()
    {
        // Arrange
        var range = Range<int>.Empty;

        // Act
        string result = range.ToString();

        // Assert
        Assert.Equal("∅", result);
    }

    [Fact]
    public void Range_ToString_ShouldReturnInfiniteString()
    {
        // Arrange
        var range = Range<int>.Infinite;

        // Act
        string result = range.ToString();

        // Assert
        Assert.Equal("(-∞,∞)", result);
    }
}
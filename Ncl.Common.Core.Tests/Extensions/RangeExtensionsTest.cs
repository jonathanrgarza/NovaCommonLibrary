using Ncl.Common.Core.Extensions;
using Ncl.Common.Core.Models;
using Xunit;

namespace Ncl.Common.Core.Tests.Extensions;

public class RangeExtensionsTest
{
    [Fact]
    public void ToDoubleRange_IntRange_ConvertsCorrectly()
    {
        var range = new Range<int>(1, 10);
        var result = range.ToDoubleRange();
        Assert.Equal(1.0, result.Min);
        Assert.Equal(10.0, result.Max);
        Assert.True(result.InclusiveMin);
        Assert.True(result.InclusiveMax);
    }

    [Fact]
    public void ToDoubleRange_ShortRange_ConvertsCorrectly()
    {
        var range = new Range<short>(1, 10);
        var result = range.ToDoubleRange();
        Assert.Equal(1.0, result.Min);
        Assert.Equal(10.0, result.Max);
        Assert.True(result.InclusiveMin);
        Assert.True(result.InclusiveMax);
    }

    [Fact]
    public void ToDoubleRange_UShortRange_ConvertsCorrectly()
    {
        var range = new Range<ushort>(1, 10);
        var result = range.ToDoubleRange();
        Assert.Equal(1.0, result.Min);
        Assert.Equal(10.0, result.Max);
        Assert.True(result.InclusiveMin);
        Assert.True(result.InclusiveMax);
    }

    [Fact]
    public void ToDoubleRange_UIntRange_ConvertsCorrectly()
    {
        var range = new Range<uint>(1, 10);
        var result = range.ToDoubleRange();
        Assert.Equal(1.0, result.Min);
        Assert.Equal(10.0, result.Max);
        Assert.True(result.InclusiveMin);
        Assert.True(result.InclusiveMax);
    }

    [Fact]
    public void ToDoubleRange_LongRange_ConvertsCorrectly()
    {
        var range = new Range<long>(1, 10);
        var result = range.ToDoubleRange();
        Assert.Equal(1.0, result.Min);
        Assert.Equal(10.0, result.Max);
        Assert.True(result.InclusiveMin);
        Assert.True(result.InclusiveMax);
    }

    [Fact]
    public void ToDoubleRange_ULongRange_ConvertsCorrectly()
    {
        var range = new Range<ulong>(1, 10);
        var result = range.ToDoubleRange();
        Assert.Equal(1.0, result.Min);
        Assert.Equal(10.0, result.Max);
        Assert.True(result.InclusiveMin);
        Assert.True(result.InclusiveMax);
    }

    [Fact]
    public void ToDoubleRange_FloatRange_ConvertsCorrectly()
    {
        var range = new Range<float>(1.0f, 10.0f);
        var result = range.ToDoubleRange();
        Assert.Equal(1.0, result.Min);
        Assert.Equal(10.0, result.Max);
        Assert.True(result.InclusiveMin);
        Assert.True(result.InclusiveMax);
    }

    [Fact]
    public void ToSByteRange_DoubleRange_ConvertsCorrectly()
    {
        var range = new Range<double>(1.0, 10.0);
        var result = range.ToSByteRange();
        Assert.Equal((sbyte)1, result.Min);
        Assert.Equal((sbyte)10, result.Max);
        Assert.True(result.InclusiveMin);
        Assert.True(result.InclusiveMax);
    }

    [Fact]
    public void ToByteRange_DoubleRange_ConvertsCorrectly()
    {
        var range = new Range<double>(1.0, 10.0);
        var result = range.ToByteRange();
        Assert.Equal((byte)1, result.Min);
        Assert.Equal((byte)10, result.Max);
        Assert.True(result.InclusiveMin);
        Assert.True(result.InclusiveMax);
    }

    [Fact]
    public void ToShortRange_DoubleRange_ConvertsCorrectly()
    {
        var range = new Range<double>(1.0, 10.0);
        var result = range.ToShortRange();
        Assert.Equal((short)1, result.Min);
        Assert.Equal((short)10, result.Max);
        Assert.True(result.InclusiveMin);
        Assert.True(result.InclusiveMax);
    }

    [Fact]
    public void ToUShortRange_DoubleRange_ConvertsCorrectly()
    {
        var range = new Range<double>(1.0, 10.0);
        var result = range.ToUShortRange();
        Assert.Equal((ushort)1, result.Min);
        Assert.Equal((ushort)10, result.Max);
        Assert.True(result.InclusiveMin);
        Assert.True(result.InclusiveMax);
    }

    [Fact]
    public void ToIntRange_DoubleRange_ConvertsCorrectly()
    {
        var range = new Range<double>(1.0, 10.0);
        var result = range.ToIntRange();
        Assert.Equal(1, result.Min);
        Assert.Equal(10, result.Max);
        Assert.True(result.InclusiveMin);
        Assert.True(result.InclusiveMax);
    }

    [Fact]
    public void ToUIntRange_DoubleRange_ConvertsCorrectly()
    {
        var range = new Range<double>(1.0, 10.0);
        var result = range.ToUIntRange();
        Assert.Equal(1U, result.Min);
        Assert.Equal(10U, result.Max);
        Assert.True(result.InclusiveMin);
        Assert.True(result.InclusiveMax);
    }

    [Fact]
    public void ToLongRange_DoubleRange_ConvertsCorrectly()
    {
        var range = new Range<double>(1.0, 10.0);
        var result = range.ToLongRange();
        Assert.Equal(1L, result.Min);
        Assert.Equal(10L, result.Max);
        Assert.True(result.InclusiveMin);
        Assert.True(result.InclusiveMax);
    }

    [Fact]
    public void ToULongRange_DoubleRange_ConvertsCorrectly()
    {
        var range = new Range<double>(1.0, 10.0);
        var result = range.ToULongRange();
        Assert.Equal((ulong)1, result.Min);
        Assert.Equal((ulong)10, result.Max);
        Assert.True(result.InclusiveMin);
        Assert.True(result.InclusiveMax);
    }

    [Fact]
    public void ToFloatRange_DoubleRange_ConvertsCorrectly()
    {
        var range = new Range<double>(1.0, 10.0);
        var result = range.ToFloatRange();
        Assert.Equal((float)1.0, result.Min);
        Assert.Equal((float)10.0, result.Max);
        Assert.True(result.InclusiveMin);
        Assert.True(result.InclusiveMax);
    }
}
using System;
using Ncl.Common.Core.Models;

namespace Ncl.Common.Core.Extensions
{
    /// <summary>
    ///     Extensions for <see cref="Range{T}" />.
    /// </summary>
    public static class RangeExtensions
    {
        /// <summary>
        ///     Converts the Range&lt;int&gt; to a Range&lt;double&gt;.
        /// </summary>
        /// <param name="range">The range to convert.</param>
        /// <returns>The equivalent Range&lt;double&gt; range.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="range" /> is <see langword="null" />.
        /// </exception>
        public static Range<double> ToDoubleRange(this Range<int> range)
        {
            if (range == null) throw new ArgumentNullException(nameof(range));
            return new Range<double>(range.Min, range.Max, range.InclusiveMin, range.InclusiveMax);
        }

        /// <summary>
        ///     Converts the Range&lt;uint&gt; to a Range&lt;double&gt;.
        /// </summary>
        /// <param name="range">The range to convert.</param>
        /// <returns>The equivalent Range&lt;double&gt; range.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="range" /> is <see langword="null" />.
        /// </exception>
        public static Range<double> ToDoubleRange(this Range<uint> range)
        {
            if (range == null) throw new ArgumentNullException(nameof(range));
            return new Range<double>(range.Min, range.Max, range.InclusiveMin, range.InclusiveMax);
        }

        /// <summary>
        ///     Converts the Range&lt;short&gt; to a Range&lt;double&gt;.
        /// </summary>
        /// <param name="range">The range to convert.</param>
        /// <returns>The equivalent Range&lt;double&gt; range.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="range" /> is <see langword="null" />.
        /// </exception>
        public static Range<double> ToDoubleRange(this Range<short> range)
        {
            if (range == null) throw new ArgumentNullException(nameof(range));
            return new Range<double>(range.Min, range.Max, range.InclusiveMin, range.InclusiveMax);
        }

        /// <summary>
        ///     Converts the Range&lt;ushort&gt; to a Range&lt;double&gt;.
        /// </summary>
        /// <param name="range">The range to convert.</param>
        /// <returns>The equivalent Range&lt;double&gt; range.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="range" /> is <see langword="null" />.
        /// </exception>
        public static Range<double> ToDoubleRange(this Range<ushort> range)
        {
            if (range == null) throw new ArgumentNullException(nameof(range));
            return new Range<double>(range.Min, range.Max, range.InclusiveMin, range.InclusiveMax);
        }

        /// <summary>
        ///     Converts the Range&lt;long&gt; to a Range&lt;double&gt;.
        /// </summary>
        /// <param name="range">The range to convert.</param>
        /// <returns>The equivalent Range&lt;double&gt; range.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="range" /> is <see langword="null" />.
        /// </exception>
        public static Range<double> ToDoubleRange(this Range<long> range)
        {
            if (range == null) throw new ArgumentNullException(nameof(range));
            return new Range<double>(range.Min, range.Max, range.InclusiveMin, range.InclusiveMax);
        }

        /// <summary>
        ///     Converts the Range&lt;ulong&gt; to a Range&lt;double&gt;.
        /// </summary>
        /// <param name="range">The range to convert.</param>
        /// <returns>The equivalent Range&lt;double&gt; range.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="range" /> is <see langword="null" />.
        /// </exception>
        public static Range<double> ToDoubleRange(this Range<ulong> range)
        {
            if (range == null) throw new ArgumentNullException(nameof(range));
            return new Range<double>(range.Min, range.Max, range.InclusiveMin, range.InclusiveMax);
        }

        /// <summary>
        ///     Converts the Range&lt;float&gt; to a Range&lt;double&gt;.
        /// </summary>
        /// <param name="range">The range to convert.</param>
        /// <returns>The equivalent Range&lt;double&gt; range.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="range" /> is <see langword="null" />.
        /// </exception>
        public static Range<double> ToDoubleRange(this Range<float> range)
        {
            if (range == null) throw new ArgumentNullException(nameof(range));
            return new Range<double>(range.Min, range.Max, range.InclusiveMin, range.InclusiveMax);
        }

        /// <summary>
        ///     Converts the Range&lt;double&gt; to a Range&lt;sbyte&gt;.
        /// </summary>
        /// <param name="range">The range to convert.</param>
        /// <param name="round">
        ///     If <see langword="true" />, rounds the min and max values using <see cref="Math.Round(double)" />.
        /// </param>
        /// <returns>The equivalent Range&lt;sbyte&gt; range.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="range" /> is <see langword="null" />.
        /// </exception>
        public static Range<sbyte> ToSByteRange(this Range<double> range, bool round = false)
        {
            if (range == null) throw new ArgumentNullException(nameof(range));
            double? min = !range.Min.HasValue ? null : round ? Math.Round(range.Min.Value) : range.Min;
            double? max = !range.Max.HasValue ? null : round ? Math.Round(range.Max.Value) : range.Max;
            return new Range<sbyte>((sbyte?)min, (sbyte?)max, range.InclusiveMin, range.InclusiveMax);
        }

        /// <summary>
        ///     Converts the Range&lt;double&gt; to a Range&lt;byte&gt;.
        /// </summary>
        /// <param name="range">The range to convert.</param>
        /// <param name="round">
        ///     If <see langword="true" />, rounds the min and max values using <see cref="Math.Round(double)" />.
        /// </param>
        /// <returns>The equivalent Range&lt;byte&gt; range.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="range" /> is <see langword="null" />.
        /// </exception>
        public static Range<byte> ToByteRange(this Range<double> range, bool round = false)
        {
            if (range == null) throw new ArgumentNullException(nameof(range));
            double? min = !range.Min.HasValue ? null : round ? Math.Round(range.Min.Value) : range.Min;
            double? max = !range.Max.HasValue ? null : round ? Math.Round(range.Max.Value) : range.Max;
            return new Range<byte>((byte?)min, (byte?)max, range.InclusiveMin, range.InclusiveMax);
        }

        /// <summary>
        ///     Converts the Range&lt;double&gt; to a Range&lt;short&gt;.
        /// </summary>
        /// <param name="range">The range to convert.</param>
        /// <param name="round">
        ///     If <see langword="true" />, rounds the min and max values using <see cref="Math.Round(double)" />.
        /// </param>
        /// <returns>The equivalent Range&lt;short&gt; range.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="range" /> is <see langword="null" />.
        /// </exception>
        public static Range<short> ToShortRange(this Range<double> range, bool round = false)
        {
            if (range == null) throw new ArgumentNullException(nameof(range));
            double? min = !range.Min.HasValue ? null : round ? Math.Round(range.Min.Value) : range.Min;
            double? max = !range.Max.HasValue ? null : round ? Math.Round(range.Max.Value) : range.Max;
            return new Range<short>((short?)min, (short?)max, range.InclusiveMin, range.InclusiveMax);
        }

        /// <summary>
        ///     Converts the Range&lt;double&gt; to a Range&lt;ushort&gt;.
        /// </summary>
        /// <param name="range">The range to convert.</param>
        /// <param name="round">
        ///     If <see langword="true" />, rounds the min and max values using <see cref="Math.Round(double)" />.
        /// </param>
        /// <returns>The equivalent Range&lt;ushort&gt; range.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="range" /> is <see langword="null" />.
        /// </exception>
        public static Range<ushort> ToUShortRange(this Range<double> range, bool round = false)
        {
            if (range == null) throw new ArgumentNullException(nameof(range));
            double? min = !range.Min.HasValue ? null : round ? Math.Round(range.Min.Value) : range.Min;
            double? max = !range.Max.HasValue ? null : round ? Math.Round(range.Max.Value) : range.Max;
            return new Range<ushort>((ushort?)min, (ushort?)max, range.InclusiveMin, range.InclusiveMax);
        }

        /// <summary>
        ///     Converts the Range&lt;double&gt; to a Range&lt;int&gt;.
        /// </summary>
        /// <param name="range">The range to convert.</param>
        /// <param name="round">
        ///     If <see langword="true" />, rounds the min and max values using <see cref="Math.Round(double)" />.
        /// </param>
        /// <returns>The equivalent Range&lt;int&gt; range.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="range" /> is <see langword="null" />.
        /// </exception>
        public static Range<int> ToIntRange(this Range<double> range, bool round = false)
        {
            if (range == null) throw new ArgumentNullException(nameof(range));
            double? min = !range.Min.HasValue ? null : round ? Math.Round(range.Min.Value) : range.Min;
            double? max = !range.Max.HasValue ? null : round ? Math.Round(range.Max.Value) : range.Max;
            return new Range<int>((int?)min, (int?)max, range.InclusiveMin, range.InclusiveMax);
        }

        /// <summary>
        ///     Converts the Range&lt;double&gt; to a Range&lt;uint&gt;.
        /// </summary>
        /// <param name="range">The range to convert.</param>
        /// <param name="round">
        ///     If <see langword="true" />, rounds the min and max values using <see cref="Math.Round(double)" />.
        /// </param>
        /// <returns>The equivalent Range&lt;uint&gt; range.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="range" /> is <see langword="null" />.
        /// </exception>
        public static Range<uint> ToUIntRange(this Range<double> range, bool round = false)
        {
            if (range == null) throw new ArgumentNullException(nameof(range));
            double? min = !range.Min.HasValue ? null : round ? Math.Round(range.Min.Value) : range.Min;
            double? max = !range.Max.HasValue ? null : round ? Math.Round(range.Max.Value) : range.Max;
            return new Range<uint>((uint?)min, (uint?)max, range.InclusiveMin, range.InclusiveMax);
        }

        /// <summary>
        ///     Converts the Range&lt;double&gt; to a Range&lt;long&gt;.
        /// </summary>
        /// <param name="range">The range to convert.</param>
        /// <param name="round">
        ///     If <see langword="true" />, rounds the min and max values using <see cref="Math.Round(double)" />.
        /// </param>
        /// <returns>The equivalent Range&lt;long&gt; range.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="range" /> is <see langword="null" />.
        /// </exception>
        public static Range<long> ToLongRange(this Range<double> range, bool round = false)
        {
            if (range == null) throw new ArgumentNullException(nameof(range));
            double? min = !range.Min.HasValue ? null : round ? Math.Round(range.Min.Value) : range.Min;
            double? max = !range.Max.HasValue ? null : round ? Math.Round(range.Max.Value) : range.Max;
            return new Range<long>((long?)min, (long?)max, range.InclusiveMin, range.InclusiveMax);
        }

        /// <summary>
        ///     Converts the Range&lt;double&gt; to a Range&lt;ulong&gt;.
        /// </summary>
        /// <param name="range">The range to convert.</param>
        /// <param name="round">
        ///     If <see langword="true" />, rounds the min and max values using <see cref="Math.Round(double)" />.
        /// </param>
        /// <returns>The equivalent Range&lt;ulong&gt; range.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="range" /> is <see langword="null" />.
        /// </exception>
        public static Range<ulong> ToULongRange(this Range<double> range, bool round = false)
        {
            if (range == null) throw new ArgumentNullException(nameof(range));
            double? min = !range.Min.HasValue ? null : round ? Math.Round(range.Min.Value) : range.Min;
            double? max = !range.Max.HasValue ? null : round ? Math.Round(range.Max.Value) : range.Max;
            return new Range<ulong>((ulong?)min, (ulong?)max, range.InclusiveMin, range.InclusiveMax);
        }

        /// <summary>
        ///     Converts the Range&lt;double&gt; to a Range&lt;float&gt;.
        /// </summary>
        /// <param name="range">The range to convert.</param>
        /// <returns>The equivalent Range&lt;float&gt; range.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="range" /> is <see langword="null" />.
        /// </exception>
        public static Range<float> ToFloatRange(this Range<double> range)
        {
            if (range == null) throw new ArgumentNullException(nameof(range));
            return new Range<float>((float?)range.Min, (float?)range.Max, range.InclusiveMin, range.InclusiveMax);
        }
    }
}
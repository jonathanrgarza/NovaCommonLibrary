using System;

namespace Ncl.Common.Core.Models
{
    /// <summary>
    /// Represents a range with a minimum and maximum value. The range can be inclusive or exclusive.
    /// </summary>
    /// <typeparam name="T">The value's type.</typeparam>
    public class Range<T> : IEquatable<Range<T>> where T : struct, IComparable<T>
    {
        /// <summary>
        /// Represents an infinite range where all value are contained within.
        /// </summary>
        public static readonly Range<T> Infinite = new Range<T>();

        /// <summary>
        /// Represents an empty range where no value are contained within.
        /// </summary>
        public static readonly Range<T> Empty =
            new Range<T>(default(T), default(T), false, false);

        /// <summary>
        /// Initializes a new instance of the <see cref="Range{T}"/> class.
        /// </summary>
        /// <param name="min">The minimum value. A <see langword="null"/> value means infinite.</param>
        /// <param name="max">The maximum value. A <see langword="null"/> value means infinite.</param>
        /// <param name="inclusiveMin">Is the minimum value inclusive.</param>
        /// <param name="inclusiveMax">Is the maximum value inclusive.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="min"/> value must be less than or equal to <paramref name="max"/> value -or-
        /// <paramref name="min"/> and <paramref name="max"/> values are equal, so both must be inclusive or exclusive.
        /// </exception>
        public Range(T? min = null, T? max = null, bool inclusiveMin = true, bool inclusiveMax = true)
        {
            if (min != null && max != null && min.Value.CompareTo(max.Value) > 0)
            {
                throw new ArgumentException("Min value must be less than or equal to max value.", nameof(min));
            }

            if (min?.Equals(max) == true && inclusiveMin != inclusiveMax)
            {
                throw new ArgumentException("Min and max values are equal, so both must be inclusive or exclusive.",
                    nameof(inclusiveMin));
            }

            Min = min;
            Max = max;
            InclusiveMin = inclusiveMin;
            InclusiveMax = inclusiveMax;
            // We are immutable, so we can calculate these values once.
            IsInfinite = min == null && max == null;
            IsEmpty = min?.Equals(max) == true && !inclusiveMin;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Range{T}"/> class from another range.
        /// </summary>
        /// <param name="other">The other range to copy.</param>
        public Range(Range<T> other)
        {
            Min = other.Min;
            Max = other.Max;
            InclusiveMin = other.InclusiveMin;
            InclusiveMax = other.InclusiveMax;
            IsInfinite = other.IsInfinite;
            IsEmpty = other.IsEmpty;
        }

        /// <summary>
        /// Gets the minimum value of the range.
        /// </summary>
        public T? Min { get; }

        /// <summary>
        /// Gets the maximum value of the range.
        /// </summary>
        public T? Max { get; }

        /// <summary>
        /// Gets a value indicating whether the minimum value is inclusive.
        /// </summary>
        public bool InclusiveMin { get; }

        /// <summary>
        /// Gets a value indicating whether the maximum value is inclusive.
        /// </summary>
        public bool InclusiveMax { get; }

        /// <summary>
        /// Gets a value indicating whether the range is infinite.
        /// </summary>
        public bool IsInfinite { get; }

        /// <summary>
        /// Gets a value indicating whether the range is empty.
        /// </summary>
        public bool IsEmpty { get; }

        /// <inheritdoc/>
        public bool Equals(Range<T> other)
        {
            if (other is null) return false;

            if (ReferenceEquals(this, other)) return true;

            return Nullable.Equals(Min, other.Min) && Nullable.Equals(Max, other.Max) &&
                   InclusiveMin == other.InclusiveMin && InclusiveMax == other.InclusiveMax;
        }

        /// <summary>
        /// Determines whether the specified value is within the range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        /// <see langword="true"/> if the value is within the range; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Contains(T value)
        {
            if (IsEmpty) return false;

            if (IsInfinite) return true;

            if (Min != null)
            {
                int minCompareTo = value.CompareTo(Min.Value);
                if (minCompareTo < 0 || (minCompareTo == 0 && !InclusiveMin)) return false;
            }

            if (Max == null) return true;

            int maxCompareTo = value.CompareTo(Max.Value);
            if (maxCompareTo > 0 || (maxCompareTo == 0 && !InclusiveMax)) return false;

            return true;
        }

        /// <summary>
        /// Determines whether the specified range is within the current range.
        /// </summary>
        /// <param name="range">The range to check.</param>
        /// <returns>
        /// <see langword="true"/> if the specified range is within the current range; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        public bool Contains(Range<T> range)
        {
            if (IsEmpty || range.IsEmpty) return false;

            if (IsInfinite && !range.IsInfinite) return true;

            if (range.IsInfinite) return false;

            // We can't contain a range that is larger than us
            if (Min != null && range.Min != null)
            {
                int minCompareTo = range.Min.Value.CompareTo(Min.Value);
                if (minCompareTo < 0 || (minCompareTo == 0 && (!range.InclusiveMin || InclusiveMin))) return false;
            }

            if (Max != null && range.Max != null)
            {
                int maxCompareTo = range.Max.Value.CompareTo(Max.Value);
                if (maxCompareTo > 0 || (maxCompareTo == 0 && (!range.InclusiveMax || InclusiveMax))) return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            if (ReferenceEquals(this, obj)) return true;

            return Equals(obj as Range<T>);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            // Show infinity if Min or Max is null
            if (IsInfinite) return "(-∞,∞)";

            if (IsEmpty) return "∅";

            char minBracket = InclusiveMin ? '[' : '(';
            char maxBracket = InclusiveMax ? ']' : ')';
            string min = Min?.ToString() ?? "-∞";
            string max = Max?.ToString() ?? "∞";
            return $"{minBracket}{min},{max}{maxBracket}";
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Min.GetHashCode();
                hashCode = (hashCode * 397) ^ Max.GetHashCode();
                hashCode = (hashCode * 397) ^ InclusiveMin.GetHashCode();
                hashCode = (hashCode * 397) ^ InclusiveMax.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Determines whether two ranges are equal.
        /// </summary>
        /// <param name="left">The left range.</param>
        /// <param name="right">The right range.</param>
        /// <returns><see langword="true"/> if the ranges are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Range<T> left, Range<T> right)
        {
            if (left is null && right is null) return true;

            if (left is null || right is null) return false;

            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two ranges are not equal.
        /// </summary>
        /// <param name="left">The left range.</param>
        /// <param name="right">The right range.</param>
        /// <returns><see langword="true"/> if the ranges are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Range<T> left, Range<T> right)
        {
            return !(left == right);
        }
    }
}
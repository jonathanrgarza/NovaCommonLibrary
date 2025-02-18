using System;
using System.Globalization;

namespace Ncl.Common.Core.Extensions
{
    /// <summary>
    /// Extensions methods for the <see cref="double"/> type.
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// The default tolerance for comparing double equality.
        /// </summary>
        public const double DefaultTolerance = 0.001;

        /// <summary>
        /// The default decimal tolerance for comparing float equality.
        /// </summary>
        public const int DefaultDecimalTolerance = 3;

        /// <summary>
        /// Checks if two doubles are equal, within a given tolerance.
        /// </summary>
        /// <param name="left">The left/first double.</param>
        /// <param name="right">The right/second double.</param>
        /// <param name="tolerance">The tolerance for the comparison.</param>
        /// <returns>
        /// <see langword="true"/> if the doubles are considered equal, otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsEqual(this double left, double right, double tolerance = DefaultTolerance)
        {
            //Check NaN case
            bool currentIsNaN = double.IsNaN(left);
            bool valueIsNaN = double.IsNaN(right);
            if (currentIsNaN && valueIsNaN) return true;

            if (currentIsNaN || valueIsNaN) return false;

            //Check infinity case
            if (!double.IsInfinity(left) && !double.IsInfinity(right))
            {
                return Math.Abs(left - right) < tolerance; //Normal value case
            }

            if (double.IsPositiveInfinity(left) && double.IsPositiveInfinity(right)) return true;

            if (double.IsNegativeInfinity(left) && double.IsNegativeInfinity(right)) return true;

            return false;
        }

        /// <summary>
        /// Checks if two nullable doubles are equal, within a given tolerance.
        /// </summary>
        /// <param name="left">The left/first double.</param>
        /// <param name="right">The right/second double.</param>
        /// <param name="tolerance">The tolerance for the comparison.</param>
        /// <returns>
        /// <see langword="true"/> if the doubles are considered equal, otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsEqual(this double? left, double? right, double tolerance = DefaultTolerance)
        {
            if (left.HasValue == false && right.HasValue == false) return true;

            if (left.HasValue == false || right.HasValue == false) return false;

            return left.Value.IsEqual(right.Value, tolerance);
        }

        /// <summary>
        /// Determines if the <paramref name="current"/> is considered equal to
        /// the <paramref name="value"/> based on precision given by <paramref name="decimals"/> value.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="value">The other value to compare against.</param>
        /// <param name="decimals">The decimal precision.</param>
        /// <returns>
        /// <see langword="true"/> if the two values are considered equal, otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsEqualTo(this double current, double value, int decimals = DefaultDecimalTolerance)
        {
            //Check NaN case
            bool currentIsNaN = double.IsNaN(current);
            bool valueIsNaN = double.IsNaN(value);
            if (currentIsNaN && valueIsNaN) return true;

            if (currentIsNaN || valueIsNaN) return false;

            //Check infinity case
            if (double.IsInfinity(current) || double.IsInfinity(value))
            {
                if (double.IsPositiveInfinity(current) && double.IsPositiveInfinity(value)) return true;

                if (double.IsNegativeInfinity(current) && double.IsNegativeInfinity(value)) return true;

                return false;
            }

            //Check normal value case
            decimals = decimals >= 0 ? decimals : 0;
            double tolerance = Math.Pow(10, -decimals);

            if (Math.Abs(current - value) < tolerance) return true;

            return false;
        }

        /// <summary>
        /// Determines if the <paramref name="current"/> is considered equal to
        /// the <paramref name="value"/> based on precision given by <paramref name="decimals"/> value.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="value">The other value to compare against.</param>
        /// <param name="decimals">The decimal precision.</param>
        /// <returns>
        /// <see langword="true"/> if the two values are considered equal, otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsEqualTo(this double? current, double? value, int decimals = DefaultDecimalTolerance)
        {
            //Check Null case
            if (current.HasValue == false && value.HasValue == false) return true;

            if (current.HasValue == false || value.HasValue == false) return false;

            //Check all other cases
            return IsEqualTo(current.Value, value.Value, decimals);
        }

        /// <summary>
        /// Normalizes a value into a new range.
        /// </summary>
        /// <param name="value">The value to normalize.</param>
        /// <param name="valueMin">The minimum value of the current range.</param>
        /// <param name="valueMax">The maximum value of the current range.</param>
        /// <param name="newMin">The minimum value of the new range.</param>
        /// <param name="newMax">The maximum value of the new range.</param>
        /// <returns>The normalized value.</returns>
        public static double Normalize(this double value, double valueMin, double valueMax, double newMin,
            double newMax)
        {
            /*
             * Where m = value, rMin = valueMin, rMax = valueMax, tMin = newMin, and tMax = newMin
             * Formula = ( (m - rMin) / (rMax - rMin) ) * (tMax - tMin) + tMin
             */

            return (value - valueMin) / (valueMax - valueMin) * (newMax - newMin) + newMin;
        }

        /// <summary>
        /// Gets the number of digits in a <see cref="double"/> value.
        /// </summary>
        /// <remarks>
        /// This works of the string representation of the <see cref="double"/>.
        /// </remarks>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The number of digits in a <see cref="double"/> value. If the value is infinity or NaN, returns 0.
        /// </returns>
        public static int DigitCount(this double value)
        {
            if (value == 0.0) return 1;

            if (double.IsInfinity(value) || double.IsNaN(value)) return 0;

            string valueString = value.ToString(CultureInfo.InvariantCulture);
            int count = 0;
            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            // This was faster and less memory than using LINQ.
            foreach (char c in valueString)
            {
                if (c != '-' && c != '.')
                {
                    count++;
                }
            }

            return count;
        }
    }
}
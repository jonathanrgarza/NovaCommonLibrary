using System;

namespace Ncl.Common.Core.Extensions
{
    /// <summary>
    ///     Extensions methods for the <see cref="double"/> type.
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        ///     The default tolerance for comparing double equality.
        /// </summary>
        public const double DefaultTolerance = 0.001;

        /// <summary>
        ///     Checks if two doubles are equal, within a given tolerance.
        /// </summary>
        /// <param name="left">The left/first double.</param>
        /// <param name="right">The right/second double.</param>
        /// <param name="tolerance">The tolerance for the comparison.</param>
        /// <returns>True if the doubles are considered equal, false otherwise.</returns>
        public static bool IsEqual(this double left, double right, double tolerance = DefaultTolerance)
        {
            return Math.Abs(left - right) < tolerance;
        }

        /// <summary>
        ///     Checks if two nullable doubles are equal, within a given tolerance.
        /// </summary>
        /// <param name="left">The left/first double.</param>
        /// <param name="right">The right/second double.</param>
        /// <param name="tolerance">The tolerance for the comparison.</param>
        /// <returns>True if the doubles are considered equal, false otherwise.</returns>
        public static bool IsEqual(this double? left, double? right, double tolerance = DefaultTolerance)
        {
            if (left.HasValue == false && right.HasValue == false)
                return true;
            if (left.HasValue == false || right.HasValue == false)
                return false;

            return left.Value.IsEqual(right.Value, tolerance);
        }

        /// <summary>
        ///     Normalizes a value into a new range.
        /// </summary>
        /// <param name="value">The value to normalize.</param>
        /// <param name="valueMin">The minimum value of the current range.</param>
        /// <param name="valueMax">The maximum value of the current range.</param>
        /// <param name="newMin">The minimum value of the new range.</param>
        /// <param name="newMax">The maximum value of the new range.</param>
        /// <returns>The normalized value.</returns>
        public static double Normalize(this double value, double valueMin, double valueMax, double newMin, double newMax)
        {
            /*
             * Where m = value, rMin = valueMin, rMax = valueMax, tMin = newMin, and tMax = newMin
             * Formula = ( (m - rMin) / (rMax - rMin) ) * (tMax - tMin) + tMin
             */

            return (value - valueMin) / (valueMax - valueMin) * (newMax - newMin) + newMin;
        }
    }
}

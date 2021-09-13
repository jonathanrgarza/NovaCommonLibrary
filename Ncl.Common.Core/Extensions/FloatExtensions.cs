using System;

namespace Ncl.Common.Core.Extensions
{
    /// <summary>
    ///     Extensions methods for the <see cref="float"/> type.
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        ///     The default tolerance for comparing float equality.
        /// </summary>
        public const float DefaultTolerance = 0.001f;

        /// <summary>
        ///     The default decimal tolerance for comparing float equality.
        /// </summary>
        public const int DefaultDecimalTolerance = 3;

        /// <summary>
        ///     Checks if two floats are equal, within a given tolerance.
        /// </summary>
        /// <param name="left">The left/first float.</param>
        /// <param name="right">The right/second float.</param>
        /// <param name="tolerance">The tolerance for the comparison.</param>
        /// <returns>True if the floats are considered equal, false otherwise.</returns>
        public static bool IsEqual(this float left, float right, float tolerance = DefaultTolerance)
        {
            //Check NaN case
            bool currentIsNaN = float.IsNaN(left);
            bool valueIsNaN = float.IsNaN(right);
            if (currentIsNaN && valueIsNaN)
                return true;
            if (currentIsNaN || valueIsNaN)
            {
                return false;
            }

            //Check infinity case
            if (float.IsInfinity(left) || float.IsInfinity(right))
            {
                if (float.IsPositiveInfinity(left) && float.IsPositiveInfinity(right))
                {
                    return true;
                }

                if (float.IsNegativeInfinity(left) && float.IsNegativeInfinity(right))
                {
                    return true;
                }

                return false;
            }

            return Math.Abs(left - right) < tolerance;
        }

        /// <summary>
        ///     Checks if two nullable floats are equal, within a given tolerance.
        /// </summary>
        /// <param name="left">The left/first float.</param>
        /// <param name="right">The right/second float.</param>
        /// <param name="tolerance">The tolerance for the comparison.</param>
        /// <returns>True if the nullable floats are considered equal, false otherwise.</returns>
        public static bool IsEqual(this float? left, float? right, float tolerance = DefaultTolerance)
        {
            if (left.HasValue == false && right.HasValue == false)
                return true;
            if (left.HasValue == false || right.HasValue == false)
                return false;

            return left.Value.IsEqual(right.Value, tolerance);
        }

        /// <summary>
        ///     Determines if the <paramref name="left"/> is considered equal to 
        ///     the <paramref name="right"/> based on precision given by <paramref name="decimals"/> value.
        /// </summary>
        /// <param name="left">The current value.</param>
        /// <param name="right">The other value to compare against.</param>
        /// <param name="decimals">The decimal precision.</param>
        /// <returns>True if the two values are considered equal, otherwise, false.</returns>
        public static bool IsEqualTo(this float left, float right, int decimals = DefaultDecimalTolerance)
        {
            //Check NaN case
            bool currentIsNaN = float.IsNaN(left);
            bool valueIsNaN = float.IsNaN(right);
            if (currentIsNaN && valueIsNaN)
                return true;
            if (currentIsNaN || valueIsNaN)
            {
                return false;
            }

            //Check infinity case
            if (float.IsInfinity(left) || float.IsInfinity(right))
            {
                if (float.IsPositiveInfinity(left) && float.IsPositiveInfinity(right))
                {
                    return true;
                }

                if (float.IsNegativeInfinity(left) && float.IsNegativeInfinity(right))
                {
                    return true;
                }

                return false;
            }

            //Check normal value case
            decimals = decimals >= 0 ? decimals : 0;
            float tolerance = (float)Math.Pow(10, -decimals);

            if (Math.Abs(left - right) < tolerance)
                return true;

            return false;
        }

        /// <summary>
        ///     Determines if the <paramref name="left"/> is considered equal to 
        ///     the <paramref name="right"/> based on precision given by <paramref name="decimals"/> value.
        /// </summary>
        /// <param name="left">The current value.</param>
        /// <param name="right">The other value to compare against.</param>
        /// <param name="decimals">The decimal precision.</param>
        /// <returns>True if the two values are considered equal, otherwise, false.</returns>
        public static bool IsEqualTo(this float? left, float? right, int decimals = DefaultDecimalTolerance)
        {
            //Check Null case
            if (left.HasValue == false && right.HasValue == false)
                return true;
            if (left.HasValue == false || right.HasValue == false)
                return false;

            //Check all other cases
            return IsEqualTo(left.Value, right.Value, decimals);
        }
    }
}

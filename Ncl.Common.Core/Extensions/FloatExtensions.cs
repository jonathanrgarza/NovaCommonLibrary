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
        ///     Checks if two floats are equal, within a given tolerance.
        /// </summary>
        /// <param name="left">The left/first float.</param>
        /// <param name="right">The right/second float.</param>
        /// <param name="tolerance">The tolerance for the comparison.</param>
        /// <returns>True if the floats are considered equal, false otherwise.</returns>
        public static bool IsEqual(this float left, float right, float tolerance = DefaultTolerance)
        {
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
    }
}

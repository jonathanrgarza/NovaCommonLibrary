using System;

namespace Ncl.Common.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for integers.
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Returns the number of digits in the specified integer value.
        /// Does not include the sign.
        /// </summary>
        /// <param name="value">The integer value.</param>
        /// <returns>The number of digits in the value.</returns>
        public static int DigitCount(this int value)
        {
            return value == 0 ? 1 : (int)Math.Floor(Math.Log10(Math.Abs(value)) + 1);
        }

        /// <summary>
        /// Returns the number of digits in the specified long value.
        /// Does not include the sign.
        /// </summary>
        /// <param name="value">The long value.</param>
        /// <returns>The number of digits in the value.</returns>
        public static int DigitCount(this long value)
        {
            return value == 0 ? 1 : (int)Math.Floor(Math.Log10(Math.Abs(value)) + 1);
        }
    }
}
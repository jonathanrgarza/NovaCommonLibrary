namespace Ncl.Common.Core.Utilities
{
    /// <summary>
    ///     Utility methods related to angles.
    /// </summary>
    public static class AngleUtility
    {
        /// <summary>
        ///     Gets whether the given value is within [0,360).
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>True if the value is within the range of [0, 360), false otherwise.</returns>
        public static bool IsWithinNormalizeDegreeRange(double value)
        {
            return value >= 0.0 && value < 360.0;
        }

        /// <summary>
        ///     Normalizes the given degree value to be within the range of [0,360).
        /// </summary>
        /// <param name="value">The value to normalize.</param>
        /// <returns>The normalized degree value.</returns>
        public static double GetNormalizeDegreeValue(double value)
        {
            if (IsWithinNormalizeDegreeRange(value))
                return value;

            double normalizedValue = value % 360.0;

            if (normalizedValue < 0)
            {
                normalizedValue += 360.0;
            }

            return normalizedValue;
        }

        /// <summary>
        ///     Normalizes the given degree value to be within the range of [0,360).
        /// </summary>
        /// <param name="value">The value to normalize.</param>
        /// <param name="normalizedValue">The out normalized value.</param>
        /// <returns>True if the value was normalized, false if value was already within the normalize range.</returns>
        public static bool GetNormalizeDegreeValue(double value, out double normalizedValue)
        {
            normalizedValue = value;
            if (IsWithinNormalizeDegreeRange(value))
                return false;

            normalizedValue = GetNormalizeDegreeValue(value);
            return true;
        }
    }
}
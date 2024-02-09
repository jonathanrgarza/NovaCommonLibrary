using System;
using System.Globalization;
using System.Windows.Data;

namespace Ncl.Common.Wpf.Converters
{
    /// <summary>
    /// Converts a boolean value to its inverse.
    /// </summary>
    public class InverseBoolConverter : IValueConverter
    {
        /// <summary>
        /// Converts a boolean value to its inverse.
        /// </summary>
        /// <param name="value">The boolean value to be converted.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">An optional parameter for the conversion.</param>
        /// <param name="culture">The culture to be used for the conversion.</param>
        /// <returns>The inverse of the boolean value.</returns>
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }

            return Binding.DoNothing;
        }

        /// <summary>
        /// Converts the inverse of a boolean value back to its original value.
        /// </summary>
        /// <param name="value">The inverse of the boolean value.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">An optional parameter for the conversion.</param>
        /// <param name="culture">The culture to be used for the conversion.</param>
        /// <returns>The original boolean value.</returns>
        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }

            return Binding.DoNothing;
        }
    }
}

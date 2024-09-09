using System;
using System.Globalization;
using System.Windows.Data;

namespace Ncl.Common.Wpf.Converters;

/// <summary>
/// Converts a string to a boolean based on if it's empty/null (false) or not (true).
/// </summary>
public class NonEmptyStringToBoolConverter : IValueConverter
{
    /// <summary>
    /// Converts a string to a boolean value.
    /// </summary>
    /// <param name="value">The string value to convert.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A boolean value indicating if the string is empty/null (false) or not (true).</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string str) return !string.IsNullOrEmpty(str);

        return false;
    }

    /// <summary>
    /// Converts a boolean value back to a string.
    /// </summary>
    /// <param name="value">The boolean value to convert back.</param>
    /// <param name="targetType">The type of the binding source property.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>NotImplementedException is thrown as this conversion is not supported.</returns>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

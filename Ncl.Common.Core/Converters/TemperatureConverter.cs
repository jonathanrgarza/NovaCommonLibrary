using Ncl.Common.Core.Infrastructure;
using Ncl.Common.Core.Measurement;

namespace Ncl.Common.Core.Converters
{
    /// <summary>
    ///     Utility class for converting temperatures.
    /// </summary>
    public class TemperatureConverter : IMeasurementConverter<TemperatureUoM>
    {
        /// <inheritdoc/>
        public double Convert(double value, TemperatureUoM fromUnit, TemperatureUoM toUnit)
        {
            if (fromUnit == toUnit)
                return value;

            double convertedValue;

            if (toUnit == TemperatureUoM.Kelvin)
            {
                convertedValue = ToKelvin(value, fromUnit);
                return convertedValue;
            }

            if (toUnit == TemperatureUoM.Celsius)
            {
                convertedValue = ToCelsius(value, fromUnit);
                return convertedValue;
            }

            //Must be a conversion to Fahrenheit
            convertedValue = ToFahrenheit(value, fromUnit);
            return convertedValue;
        }

        /// <summary>
        ///     Converts a value to Kelvin units.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="fromUnit">The current units of the value.</param>
        /// <returns>The converted value.</returns>
        public double ToKelvin(double value, TemperatureUoM fromUnit)
        {
            double convertedValue;

            if (fromUnit == TemperatureUoM.Celsius)
            {
                convertedValue = value + 273.15;
                return convertedValue;
            }

            if (fromUnit == TemperatureUoM.Fahrenheit)
            {
                convertedValue = (value - 32) / 1.8 + 273.15;
                return convertedValue;
            }

            //Must already be same unit.
            return value;
        }

        /// <summary>
        ///     Converts a value to Celsius units.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="fromUnit">The current units of the value.</param>
        /// <returns>The converted value.</returns>
        public double ToCelsius(double value, TemperatureUoM fromUnit)
        {
            double convertedValue;

            if (fromUnit == TemperatureUoM.Fahrenheit)
            {
                convertedValue = (value - 32) / 1.8;
                return convertedValue;
            }

            if (fromUnit == TemperatureUoM.Kelvin)
            {
                convertedValue = value - 273.15;
                return convertedValue;
            }

            //Must already be same unit.
            return value;
        }

        /// <summary>
        ///     Converts a value to Fahrenheit units.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="fromUnit">The current units of the value.</param>
        /// <returns>The converted value.</returns>
        public double ToFahrenheit(double value, TemperatureUoM fromUnit)
        {
            double convertedValue;

            if (fromUnit == TemperatureUoM.Celsius)
            {
                convertedValue = (value * 1.8) + 32;
                return convertedValue;
            }

            if (fromUnit == TemperatureUoM.Kelvin)
            {
                convertedValue = (value - 273.15) * 1.8 + 32;
                return convertedValue;
            }

            //Must already be same unit.
            return value;
        }
    }
}

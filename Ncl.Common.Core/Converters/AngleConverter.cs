using System;
using Ncl.Common.Core.Infrastructure;
using Ncl.Common.Core.Measurement;

namespace Ncl.Common.Core.Converters
{
    /// <summary>
    ///     Utility class for converting angles.
    /// </summary>
    public class AngleConverter : IMeasurementConverter<AngleUoM>
    {
        /// <inheritdoc/>
        public double Convert(double value, AngleUoM fromUnit, AngleUoM toUnit)
        {
            if (fromUnit == toUnit)
                return value;

            double convertedValue;

            if (toUnit == AngleUoM.Radian)
            {
                convertedValue = ToRadian(value, fromUnit);
                return convertedValue;
            }

            if (toUnit == AngleUoM.Degree)
            {
                convertedValue = ToDegree(value, fromUnit);
                return convertedValue;
            }

            //Must be a conversion to Revolution
            convertedValue = ToRevolution(value, fromUnit);
            return convertedValue;
        }

        /// <summary>
        ///     Converts a value to Radian units.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="fromUnit">The current units of the value.</param>
        /// <returns>The converted value.</returns>
        public double ToRadian(double value, AngleUoM fromUnit)
        {
            double convertedValue;

            if (fromUnit == AngleUoM.Degree)
            {
                //1° × π/180 = 0.01745rad
                convertedValue = value * (Math.PI / 180.0);
                return convertedValue;
            }

            if (fromUnit == AngleUoM.Revolution)
            {
                //1𝞽 × 2π = 6.283rad
                convertedValue = value * (2.0 * Math.PI);
                return convertedValue;
            }

            //Must already be same unit.
            return value;
        }

        /// <summary>
        ///     Converts a value to Degree units.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="fromUnit">The current units of the value.</param>
        /// <returns>The converted value.</returns>
        public double ToDegree(double value, AngleUoM fromUnit)
        {
            double convertedValue;

            if (fromUnit == AngleUoM.Radian)
            {
                //1rad × 180/π = 57.296°
                convertedValue = value * (180.0 / Math.PI);
                return convertedValue;
            }

            if (fromUnit == AngleUoM.Revolution)
            {
                convertedValue = value * 360.0;
                return convertedValue;
            }

            //Must already be same unit.
            return value;
        }

        /// <summary>
        ///     Converts a value to Revolution units.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="fromUnit">The current units of the value.</param>
        /// <returns>The converted value.</returns>
        public double ToRevolution(double value, AngleUoM fromUnit)
        {
            double convertedValue;

            if (fromUnit == AngleUoM.Degree)
            {
                convertedValue = value / 360.0;
                return convertedValue;
            }

            if (fromUnit == AngleUoM.Radian)
            {
                //1rad ÷ 2π = 0.1592𝞽
                convertedValue = value / (2.0 * Math.PI);
                return convertedValue;
            }

            //Must already be same unit.
            return value;
        }
    }
}

using System;

namespace Ncl.Common.Core.Infrastructure
{
    /// <summary>
    ///     Interface for a measurement converter.
    /// </summary>
    /// <typeparam name="UoM">The enum type representing the Unit of Measure.</typeparam>
    public interface IMeasurementConverter<UoM> where UoM : Enum
    {
        /// <summary>
        ///     Converts a value to another unit.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="fromUnit">The current unit for the value.</param>
        /// <param name="toUnit">The unit to convert the value to.</param>
        /// <returns>The converted value.</returns>
        double Convert(double value, UoM fromUnit, UoM toUnit);
    }
}

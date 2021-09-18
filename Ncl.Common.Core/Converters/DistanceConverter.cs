using System.Collections.Generic;
using Ncl.Common.Core.Infrastructure;
using Ncl.Common.Core.Measurement;

namespace Ncl.Common.Core.Converters
{
    /// <summary>
    ///     Utility class for converting distances.
    /// </summary>
    public class DistanceConverter : IMeasurementConverter<DistanceUoM>
    {
        /// <summary>
        ///     Contains a dictionary of conversion factors to the SI unit for distances.
        /// </summary>
        protected static readonly IReadOnlyDictionary<DistanceUoM, double> ConversionFactors = new Dictionary<DistanceUoM, double>
        {
            {DistanceUoM.Millimeter, 0.001},
            {DistanceUoM.Centimeter, 0.01},
            {DistanceUoM.Meter, 1.0}, //SI unit
            {DistanceUoM.Kilometer, 1000.0},

            //U.S. customary units
            {DistanceUoM.Inch, 0.0254},
            {DistanceUoM.Foot, 0.3048},
            {DistanceUoM.Yard, 0.9144},
            {DistanceUoM.Mile, 1609.344}
        };

        /// <inheritdoc />
        public double Convert(double value, DistanceUoM fromUnit, DistanceUoM toUnit)
        {
            if (fromUnit == toUnit)
                return value;

            double convertedValue = value;

            //Check if need to convert to SI unit
            if (fromUnit != DistanceUoM.Meter)
            {
                convertedValue *= ConversionFactors[fromUnit];
            }

            //Convert from SI unit to destination unit
            convertedValue /= ConversionFactors[toUnit];

            return convertedValue;
        }
    }
}
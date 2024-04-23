using System;
using Ncl.Common.Core.Infrastructure;
using Ncl.Common.Core.Measurement.Converters;

namespace Ncl.Common.Core.Measurement
{
    /// <summary>
    ///     Represents a distance measurement.
    /// </summary>
    public class Distance : Measurement<Distance, DistanceUoM>
    {
        //Static member only initialized when type is first referenced or instance first created so this should be fine
        private static readonly IMeasurementConverter<DistanceUoM> _converter = new DistanceConverter();

        /// <summary>
        ///     Initializes a new instance of <see cref="Distance" />.
        /// </summary>
        public Distance()
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="Distance" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit.</param>
        public Distance(double value, DistanceUoM unit) : base(value, unit)
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="Distance" />.
        /// </summary>
        /// <param name="instance">The instance to copy.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="instance" /> is null.</exception>
        public Distance(Distance instance) : base(instance)
        {
        }

        /// <inheritdoc />
        protected override Distance NewInstance()
        {
            return new Distance();
        }

        /// <inheritdoc />
        protected override Distance NewInstance(double value, DistanceUoM unit)
        {
            return new Distance(value, unit);
        }

        /// <inheritdoc />
        public override Distance Convert(DistanceUoM newUnit)
        {
            DistanceUoM curUnit = Unit;
            if (curUnit == newUnit)
                return this;

            double convertedValue = _converter.Convert(Value, curUnit, newUnit);
            return new Distance(convertedValue, newUnit);
        }
    }
}
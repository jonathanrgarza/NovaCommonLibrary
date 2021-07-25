using System;
using Ncl.Common.Core.Converters;
using Ncl.Common.Core.Infrastructure;

namespace Ncl.Common.Core.Measurement
{
    /// <summary>
    ///     Represents a temperature measurement.
    /// </summary>
    public class Temperature : Measurement<Temperature, TemperatureUoM>
    {
        //Static member only initialized when type is first referenced or instance first created so this should be fine
        private static readonly IMeasurementConverter<TemperatureUoM> _converter = new TemperatureConverter();

        /// <summary>
        ///     Initializes a new instance of <see cref="Temperature"/>.
        /// </summary>
        public Temperature() : base()
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="Temperature"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit.</param>
        public Temperature(double value, TemperatureUoM unit) : base(value, unit)
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="Temperature"/>.
        /// </summary>
        /// <param name="instance">The instance to copy.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="instance"/> is null.</exception>
        public Temperature(Temperature instance) : base(instance)
        {
        }

        /// <inheritdoc/>
        protected override Temperature NewInstance()
        {
            return new Temperature();
        }

        /// <inheritdoc/>
        protected override Temperature NewInstance(double value, TemperatureUoM unit)
        {
            return new Temperature(value, unit);
        }

        /// <inheritdoc/>
        public override Temperature Convert(TemperatureUoM newUnit)
        {
            var curUnit = Unit;
            if (curUnit == newUnit)
                return this;

            var convertedValue = _converter.Convert(Value, curUnit, newUnit);
            return new Temperature(convertedValue, newUnit);
        }
    }
}

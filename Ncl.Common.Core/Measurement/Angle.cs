using System;
using Ncl.Common.Core.Converters;
using Ncl.Common.Core.Infrastructure;
using Ncl.Common.Core.Utilities;

namespace Ncl.Common.Core.Measurement
{
    /// <summary>
    ///     Represents a angle measurement.
    /// </summary>
    public class Angle : Measurement<Angle, AngleUoM>
    {
        //Static member only initialized when type is first referenced or instance first created so this should be fine
        private static readonly IMeasurementConverter<AngleUoM> _converter = new AngleConverter();

        /// <summary>
        ///     Initializes a new instance of <see cref="Temperature" />.
        /// </summary>
        public Angle()
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="Temperature" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit.</param>
        public Angle(double value, AngleUoM unit) : base(value, unit)
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="Temperature" />.
        /// </summary>
        /// <param name="instance">The instance to copy.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="instance" /> is null.</exception>
        public Angle(Angle instance) : base(instance)
        {
        }

        /// <inheritdoc />
        protected override Angle NewInstance()
        {
            return new Angle();
        }

        /// <inheritdoc />
        protected override Angle NewInstance(double value, AngleUoM unit)
        {
            return new Angle(value, unit);
        }

        /// <inheritdoc />
        public override Angle Convert(AngleUoM newUnit)
        {
            AngleUoM curUnit = Unit;
            if (curUnit == newUnit)
                return this;

            double convertedValue = _converter.Convert(Value, curUnit, newUnit);
            return new Angle(convertedValue, newUnit);
        }

        /// <summary>
        ///     Converts the current <see cref="Angle" /> to degrees.
        /// </summary>
        /// <param name="normalizeValue">Should the converted value be normalized to [0,360).</param>
        /// <returns>A new converted instance</returns>
        public Angle ToDegree(bool normalizeValue)
        {
            const AngleUoM newUnit = AngleUoM.Degree;
            AngleUoM curUnit = Unit;
            double curValue = Value;
            if (curUnit == newUnit && (normalizeValue == false || AngleUtility.IsWithinNormalizeDegreeRange(curValue)))
                return this;

            double convertedValue;
            if (curUnit == newUnit)
            {
                //Not normalized yet
                convertedValue = AngleUtility.GetNormalizeDegreeValue(curValue);
                return new Angle(convertedValue, newUnit);
            }

            convertedValue = _converter.Convert(Value, curUnit, newUnit);

            if (normalizeValue)
            {
                convertedValue = AngleUtility.GetNormalizeDegreeValue(convertedValue);
            }

            return new Angle(convertedValue, newUnit);
        }
    }
}
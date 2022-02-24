﻿using System;
using System.Collections.Generic;
using Ncl.Common.Core.Extensions;

namespace Ncl.Common.Core.Measurement
{
    /// <summary>
    ///     Base class representing a measurement.
    /// </summary>
    /// <typeparam name="T">The type of the derived class.</typeparam>
    /// <typeparam name="TUoM">The enum type representing the Unit of Measure.</typeparam>
    public abstract class Measurement<T, TUoM> : IEquatable<T>
        where T : Measurement<T, TUoM> where TUoM : Enum
    {
        /// <summary>
        ///     The default value for a <see cref="Measurement{T, TUoM}" />.
        /// </summary>
        /// <remarks>
        ///     The default value is not considered as having a value by <see cref="Measurement{T, TUoM}" /> and thus
        ///     <see cref="HasValue" /> would return false.
        /// </remarks>
        public const double DefaultValue = double.NaN;

        /// <summary>
        ///     Initializes a new instance of <see cref="Measurement{T, TUoM}" />.
        /// </summary>
        protected Measurement()
        {
            Value = DefaultValue;
            Unit = default;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="Measurement{T, TUoM}" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit.</param>
        protected Measurement(double value, TUoM unit)
        {
            Value = value;
            Unit = unit;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="Measurement{T, TUoM}" />.
        /// </summary>
        /// <param name="instance">The instance to copy.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="instance" /> is null.</exception>
        protected Measurement(Measurement<T, TUoM> instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            Value = instance.Value;
            Unit = instance.Unit;
        }

        /// <summary>
        ///     Gets if this measurement has a set value.
        /// </summary>
        public bool HasValue => double.IsNaN(Value);

        /// <summary>
        ///     Gets the units for this measurement.
        /// </summary>
        public TUoM Unit { get; }

        /// <summary>
        ///     Gets the value of this measurement.
        /// </summary>
        public double Value { get; }

        /// <inheritdoc />
        public bool Equals(T other)
        {
            return other != null &&
                   EqualityComparer<TUoM>.Default.Equals(Unit, other.Unit) &&
                   Value.IsEqual(other.Value);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Equals(obj as T);
        }

        /// <summary>
        ///     Gets a hash code for the current instance.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            int hashCode = -177567199;
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<TUoM>.Default.GetHashCode(Unit);
            return hashCode;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Value} {Unit.GetAbbreviation()}";
        }

        /// <summary>
        ///     Returns a string that represents the current object with
        ///     the <see cref="Value" /> rounded to the given number of decimals.
        /// </summary>
        /// <param name="decimals">The number of decimals to round the <see cref="Value" /> to.</param>
        /// <returns>A string that represents the current object.</returns>
        public virtual string ToString(uint decimals)
        {
            string format = $"F{decimals}";
            return $"{Value.ToString(format)} {Unit.GetAbbreviation()}";
        }

        /// <summary>
        ///     Gets a new default instance of this class.
        /// </summary>
        /// <returns>A new instance of this measurement.</returns>
        protected abstract T NewInstance();

        /// <summary>
        ///     Gets a new instance of this class with a given value and unit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="unit">The units.</param>
        /// <returns>A new instance of this measurement.</returns>
        protected abstract T NewInstance(double value, TUoM unit);

        /// <summary>
        ///     Converts this measurement's value into one with the given units.
        /// </summary>
        /// <param name="newUnit">The units to convert to.</param>
        /// <returns>A new converted instance.</returns>
        public abstract T Convert(TUoM newUnit);

        /// <summary>
        ///     Adds this measurement with a given value in a given unit.
        /// </summary>
        /// <param name="value">The value to add.</param>
        /// <param name="unit">The unit of the given value.</param>
        /// <returns>A new measurement with the resulting value.</returns>
        public T Add(double value, TUoM unit)
        {
            TUoM thisUnit = Unit;

            double convertedValue = value;
            if (thisUnit.Equals(unit) == false)
            {
                T instance = NewInstance(value, unit).Convert(thisUnit);
                convertedValue = instance.Value;
            }

            double result = Value + convertedValue;
            return NewInstance(result, thisUnit);
        }

        /// <summary>
        ///     Adds this measurement with another measurement.
        /// </summary>
        /// <param name="other">The other measurement to add with.</param>
        /// <returns>A new measurement with the resulting value.</returns>
        /// <exception cref="ArgumentNullException">If other is null.</exception>
        public T Add(T other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            TUoM thisUnit = Unit;

            double convertedValue = other.Convert(thisUnit).Value;
            double result = Value + convertedValue;
            return NewInstance(result, thisUnit);
        }

        /// <summary>
        ///     Subtracts this measurement with a given value in a given unit.
        /// </summary>
        /// <param name="value">The value to subtract.</param>
        /// <param name="unit">The unit of the given value.</param>
        /// <returns>A new measurement with the resulting value.</returns>
        public T Subtract(double value, TUoM unit)
        {
            TUoM thisUnit = Unit;

            double convertedValue = value;
            if (thisUnit.Equals(unit) == false)
            {
                T instance = NewInstance(value, unit).Convert(thisUnit);
                convertedValue = instance.Value;
            }

            double result = Value - convertedValue;
            return NewInstance(result, thisUnit);
        }

        /// <summary>
        ///     Subtracts this measurement with another measurement.
        /// </summary>
        /// <param name="other">The other measurement to subtract with.</param>
        /// <returns>A new measurement with the resulting value.</returns>
        /// <exception cref="ArgumentNullException">If other is null.</exception>
        public T Subtract(T other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            TUoM thisUnit = Unit;

            double convertedValue = other.Convert(thisUnit).Value;
            double result = Value - convertedValue;
            return NewInstance(result, thisUnit);
        }

        /// <summary>
        ///     Multiples this measurement with a given value in a given unit.
        /// </summary>
        /// <param name="value">The value to multiply.</param>
        /// <param name="unit">The unit of the given value.</param>
        /// <returns>A new measurement with the resulting value.</returns>
        public T Multiply(double value, TUoM unit)
        {
            TUoM thisUnit = Unit;

            double convertedValue = value;
            if (thisUnit.Equals(unit) == false)
            {
                T instance = NewInstance(value, unit).Convert(thisUnit);
                convertedValue = instance.Value;
            }

            double result = Value * convertedValue;
            return NewInstance(result, thisUnit);
        }

        /// <summary>
        ///     Multiplies this measurement with another measurement.
        /// </summary>
        /// <param name="other">The other measurement to multiply with.</param>
        /// <returns>A new measurement with the resulting value.</returns>
        /// <exception cref="ArgumentNullException">If other is null.</exception>
        public T Multiply(T other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            TUoM thisUnit = Unit;

            double convertedValue = other.Convert(thisUnit).Value;
            double result = Value * convertedValue;
            return NewInstance(result, thisUnit);
        }

        /// <summary>
        ///     Divides this measurement with a given value in a given unit.
        /// </summary>
        /// <param name="value">The value to divide by.</param>
        /// <param name="unit">The unit of the given value.</param>
        /// <returns>A new measurement with the resulting value.</returns>
        /// <exception cref="DivideByZeroException">If the given value is zero or when converted is zero.</exception>
        public T Divide(double value, TUoM unit)
        {
            TUoM thisUnit = Unit;

            double convertedValue = value;
            if (thisUnit.Equals(unit) == false)
            {
                T instance = NewInstance(value, unit).Convert(thisUnit);
                convertedValue = instance.Value;
            }

            if (convertedValue == 0.0)
                throw new DivideByZeroException();

            double result = Value / convertedValue;
            return NewInstance(result, thisUnit);
        }

        /// <summary>
        ///     Divides this measurement with another measurement.
        /// </summary>
        /// <param name="other">The other measurement to divide with.</param>
        /// <returns>A new measurement with the resulting value.</returns>
        /// <exception cref="ArgumentNullException">If other is null.</exception>
        /// <exception cref="DivideByZeroException">If the given measurement's value is zero or when converted is zero.</exception>
        public T Divide(T other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            TUoM thisUnit = Unit;

            double convertedValue = other.Convert(thisUnit).Value;

            if (convertedValue == 0.0)
                throw new DivideByZeroException();

            double result = Value / convertedValue;
            return NewInstance(result, thisUnit);
        }

        /// <summary>
        ///     Modulus this measurement with a given value in a given unit.
        /// </summary>
        /// <param name="value">The value to divide by.</param>
        /// <param name="unit">The unit of the given value.</param>
        /// <returns>A new measurement with the resulting value.</returns>
        /// <exception cref="DivideByZeroException">If the given value is zero or when converted is zero.</exception>
        public T Modulus(double value, TUoM unit)
        {
            TUoM thisUnit = Unit;

            double convertedValue = value;
            if (thisUnit.Equals(unit) == false)
            {
                T instance = NewInstance(value, unit).Convert(thisUnit);
                convertedValue = instance.Value;
            }

            if (convertedValue == 0.0)
                throw new DivideByZeroException();

            double result = Value % convertedValue;
            return NewInstance(result, thisUnit);
        }

        /// <summary>
        ///     Modulus this measurement with another measurement.
        /// </summary>
        /// <param name="other">The other measurement to divide with.</param>
        /// <returns>A new measurement with the resulting value.</returns>
        /// <exception cref="ArgumentNullException">If other is null.</exception>
        /// <exception cref="DivideByZeroException">If the given measurement's value is zero or when converted is zero.</exception>
        public T Modulus(T other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            TUoM thisUnit = Unit;

            double convertedValue = other.Convert(thisUnit).Value;

            if (convertedValue == 0.0)
                throw new DivideByZeroException();

            double result = Value % convertedValue;
            return NewInstance(result, thisUnit);
        }

        public static bool operator ==(Measurement<T, TUoM> left, Measurement<T, TUoM> right)
        {
            return EqualityComparer<Measurement<T, TUoM>>.Default.Equals(left, right);
        }

        public static bool operator !=(Measurement<T, TUoM> left, Measurement<T, TUoM> right)
        {
            return !(left == right);
        }

        public static T operator +(Measurement<T, TUoM> left, Measurement<T, TUoM> right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));

            return left.Add((T) right);
        }

        public static T operator -(Measurement<T, TUoM> left, Measurement<T, TUoM> right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));

            return left.Subtract((T) right);
        }

        public static T operator *(Measurement<T, TUoM> left, Measurement<T, TUoM> right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));

            return left.Multiply((T) right);
        }

        public static T operator /(Measurement<T, TUoM> left, Measurement<T, TUoM> right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));

            return left.Divide((T) right);
        }

        public static T operator %(Measurement<T, TUoM> left, Measurement<T, TUoM> right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));

            return left.Modulus((T) right);
        }
    }
}
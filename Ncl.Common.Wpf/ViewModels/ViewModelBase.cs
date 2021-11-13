namespace Ncl.Common.Wpf.ViewModels
{
    /// <summary>
    ///     The base class for a view model.
    /// </summary>
    public abstract class ViewModelBase : System.ComponentModel.INotifyPropertyChanged
    {
        //You can make this class's code into a snippet to be used for classes that
        //use the INotifyPropertyChanged interface; if that is preferred over extending from this base class.

        /// <inheritdoc />
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Raises the PropertyChanged event for the given property name.
        ///     The calling member's name will be used as the parameter, by default.
        /// </summary>
        /// <param name="name">
        ///     The property's name.
        ///     Can specify <see cref="string.Empty" /> or null to signal all properties have changed.
        /// </param>
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(name));
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        /// </summary>
        /// <typeparam name="T">The field's type.</typeparam>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool Set<T>(ref T current, T value, [System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            var equalityComparer = System.Collections.Generic.EqualityComparer<T>.Default;
            if (equalityComparer.Equals(current, value))
                return false;

            current = value;
            OnPropertyChanged(name);
            return true;
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        ///     Calls the given <see cref="System.Action" /> when the value is set.
        /// </summary>
        /// <typeparam name="T">The field's type.</typeparam>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="onSetAction">The action to call when the field is set.</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool Set<T>(ref T current, T value, System.Action onSetAction,
            [System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            if (Set(ref current, value, name) == false)
                return false;

            onSetAction?.Invoke();
            return true;
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        ///     Calls the given <see cref="System.Action" /> when the value is set before raising the
        ///     <see cref="PropertyChanged" /> event.
        /// </summary>
        /// <typeparam name="T">The field's type.</typeparam>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="onSetAction">The action to call when the field is set.</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool SetAndCallFirst<T>(ref T current, T value, System.Action onSetAction,
            [System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            var equalityComparer = System.Collections.Generic.EqualityComparer<T>.Default;
            if (equalityComparer.Equals(current, value))
                return false;

            current = value;

            onSetAction?.Invoke();
            OnPropertyChanged(name);
            return true;
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        /// </summary>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool SetDouble(ref double current, double value, int decimals = 3,
            [System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            if (IsDoubleEqual(current, value, decimals))
                return false;

            current = value;
            OnPropertyChanged(name);
            return true;
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        ///     Calls the given <see cref="System.Action" /> when the value is set.
        /// </summary>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="onSetAction">The action to call when the field is set.</param>
        /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool SetDouble(ref double current, double value, System.Action onSetAction, int decimals = 3,
            [System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            if (SetDouble(ref current, value, decimals, name) == false)
                return false;

            onSetAction?.Invoke();
            return true;
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        ///     Calls the given <see cref="System.Action" /> when the value is set before raising the
        ///     <see cref="PropertyChanged" /> event.
        /// </summary>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="onSetAction">The action to call when the field is set.</param>
        /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool SetDoubleAndCallFirst(ref double current, double value, System.Action onSetAction, int decimals = 3,
            [System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            if (IsDoubleEqual(current, value, decimals))
                return false;

            current = value;
            onSetAction?.Invoke();
            OnPropertyChanged(name);
            return true;
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        /// </summary>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool SetFloat(ref float current, float value, int decimals = 3,
            [System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            if (IsFloatEqual(current, value, decimals))
                return false;

            current = value;
            OnPropertyChanged(name);
            return true;
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        ///     Calls the given <see cref="System.Action" /> when the value is set.
        /// </summary>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="onSetAction">The action to call when the field is set.</param>
        /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool SetFloat(ref float current, float value, System.Action onSetAction, int decimals = 3,
            [System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            if (SetFloat(ref current, value, decimals, name) == false)
                return false;

            onSetAction?.Invoke();
            return true;
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        ///     Calls the given <see cref="System.Action" /> when the value is set before raising the
        ///     <see cref="PropertyChanged" /> event.
        /// </summary>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="onSetAction">The action to call when the field is set.</param>
        /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool SetFloatAndCallFirst(ref float current, float value, System.Action onSetAction, int decimals = 3,
            [System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            if (IsFloatEqual(current, value, decimals))
                return false;

            current = value;
            onSetAction?.Invoke();
            OnPropertyChanged(name);
            return true;
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        /// </summary>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool SetDouble(ref double? current, double? value, int decimals = 3,
            [System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            if (IsDoubleEqual(current, value, decimals))
                return false;

            current = value;
            OnPropertyChanged(name);
            return true;
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        ///     Calls the given <see cref="System.Action" /> when the value is set.
        /// </summary>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="onSetAction">The action to call when the field is set.</param>
        /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool SetDouble(ref double? current, double? value, System.Action onSetAction, int decimals = 3,
            [System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            if (SetDouble(ref current, value, decimals, name) == false)
                return false;

            onSetAction?.Invoke();
            return true;
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        ///     Calls the given <see cref="System.Action" /> when the value is set before raising the
        ///     <see cref="PropertyChanged" /> event.
        /// </summary>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="onSetAction">The action to call when the field is set.</param>
        /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool SetDoubleAndCallFirst(ref double? current, double? value, System.Action onSetAction, int decimals = 3,
            [System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            if (IsDoubleEqual(current, value, decimals))
                return false;

            current = value;
            onSetAction?.Invoke();
            OnPropertyChanged(name);
            return true;
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        /// </summary>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool SetFloat(ref float? current, float? value, int decimals = 3,
            [System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            if (IsFloatEqual(current, value, decimals))
                return false;

            current = value;
            OnPropertyChanged(name);
            return true;
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        ///     Calls the given <see cref="System.Action" /> when the value is set.
        /// </summary>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="onSetAction">The action to call when the field is set.</param>
        /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool SetFloat(ref float? current, float? value, System.Action onSetAction, int decimals = 3,
            [System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            if (SetFloat(ref current, value, decimals, name) == false)
                return false;

            onSetAction?.Invoke();
            return true;
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        ///     Calls the given <see cref="System.Action" /> when the value is set before raising the
        ///     <see cref="PropertyChanged" /> event.
        /// </summary>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="onSetAction">The action to call when the field is set.</param>
        /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool SetFloatAndCallFirst(ref float? current, float? value, System.Action onSetAction, int decimals = 3,
            [System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            if (IsFloatEqual(current, value, decimals))
                return false;

            current = value;
            onSetAction?.Invoke();
            OnPropertyChanged(name);
            return true;
        }

        /// <summary>
        ///     Determines if the <paramref name="current" /> is considered equal to
        ///     the <paramref name="value" /> based on precision given by <paramref name="decimals" /> value.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="value">The other value to compare against.</param>
        /// <param name="decimals">The decimal precision.</param>
        /// <returns>True if the two values are considered equal, otherwise, false.</returns>
        protected bool IsDoubleEqual(double current, double value, int decimals)
        {
            //Check NaN case
            bool currentIsNaN = double.IsNaN(current);
            bool valueIsNaN = double.IsNaN(value);
            if (currentIsNaN && valueIsNaN)
                return true;
            if (currentIsNaN || valueIsNaN)
                return false;

            //Check infinity case
            if (double.IsInfinity(current) || double.IsInfinity(value))
            {
                if (double.IsPositiveInfinity(current) && double.IsPositiveInfinity(value))
                    return true;

                if (double.IsNegativeInfinity(current) && double.IsNegativeInfinity(value))
                    return true;

                return false;
            }

            //Check normal value case
            decimals = decimals >= 0 ? decimals : 0;
            double tolerance = System.Math.Pow(10, -decimals);

            if (System.Math.Abs(current - value) < tolerance)
                return true;

            return false;
        }

        /// <summary>
        ///     Determines if the <paramref name="current" /> is considered equal to
        ///     the <paramref name="value" /> based on precision given by <paramref name="decimals" /> value.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="value">The other value to compare against.</param>
        /// <param name="decimals">The decimal precision.</param>
        /// <returns>True if the two values are considered equal, otherwise, false.</returns>
        protected bool IsFloatEqual(float current, float value, int decimals)
        {
            //Check NaN case
            bool currentIsNaN = float.IsNaN(current);
            bool valueIsNaN = float.IsNaN(value);
            if (currentIsNaN && valueIsNaN)
                return true;
            if (currentIsNaN || valueIsNaN)
                return false;

            //Check infinity case
            if (float.IsInfinity(current) || float.IsInfinity(value))
            {
                if (float.IsPositiveInfinity(current) && float.IsPositiveInfinity(value))
                    return true;

                if (float.IsNegativeInfinity(current) && float.IsNegativeInfinity(value))
                    return true;

                return false;
            }

            //Check normal value case
            decimals = decimals >= 0 ? decimals : 0;
            float tolerance = (float)System.Math.Pow(10, -decimals);

            if (System.Math.Abs(current - value) < tolerance)
                return true;

            return false;
        }

        /// <summary>
        ///     Determines if the <paramref name="current" /> is considered equal to
        ///     the <paramref name="value" /> based on precision given by <paramref name="decimals" /> value.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="value">The other value to compare against.</param>
        /// <param name="decimals">The decimal precision.</param>
        /// <returns>True if the two values are considered equal, otherwise, false.</returns>
        protected bool IsDoubleEqual(double? current, double? value, int decimals)
        {
            //Check Null case
            if (current.HasValue == false && value.HasValue == false)
                return true;
            if (current.HasValue == false || value.HasValue == false)
                return false;

            //Check all other cases
            return IsDoubleEqual(current.Value, value.Value, decimals);
        }

        /// <summary>
        ///     Determines if the <paramref name="current" /> is considered equal to
        ///     the <paramref name="value" /> based on precision given by <paramref name="decimals" /> value.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="value">The other value to compare against.</param>
        /// <param name="decimals">The decimal precision.</param>
        /// <returns>True if the two values are considered equal, otherwise, false.</returns>
        protected bool IsFloatEqual(float? current, float? value, int decimals)
        {
            //Check Null case
            if (current.HasValue == false && value.HasValue == false)
                return true;
            if (current.HasValue == false || value.HasValue == false)
                return false;

            //Check all other cases
            return IsFloatEqual(current.Value, value.Value, decimals);
        }
    }
}
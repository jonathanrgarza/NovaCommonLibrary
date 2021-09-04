using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ncl.Common.Core.Infrastructure
{
    /// <summary>
    ///     Base class for a class which notifies listeners when all, or certain, properties change.
    /// </summary>
    public abstract class NotifyingObject : System.ComponentModel.INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Raises the PropertyChanged event for the given property name.
        ///     The calling member's name will be used as the parameter, by default.
        /// </summary>
        /// <param name="name">The property's name.</param>
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        /// </summary>
        /// <typeparam name="T">The field's type.</typeparam>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool Set<T>(ref T current, T value, [CallerMemberName] string name = null)
        {
            var equalityComparer = EqualityComparer<T>.Default;
            if (equalityComparer.Equals(current, value))
                return false;

            current = value;
            OnPropertyChanged(name);
            return true;
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        ///     Calls the given <see cref="Action"/> when the value is set.
        /// </summary>
        /// <typeparam name="T">The field's type.</typeparam>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="onSetAction">The action to call when the field is set.</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool Set<T>(ref T current, T value, Action onSetAction, [CallerMemberName] string name = null)
        {
            if (Set(ref current, value, name) == false)
                return false;

            onSetAction?.Invoke();
            return true;
        }

        /// <summary>
        ///     Sets a field to a given value if its different than the current value.
        ///     Calls the given <see cref="Action"/> when the value is set before raising the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <typeparam name="T">The field's type.</typeparam>
        /// <param name="current">The reference to the field.</param>
        /// <param name="value">The value to set, if different.</param>
        /// <param name="onSetAction">The action to call when the field is set.</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the field's value changed, otherwise, false.</returns>
        protected bool SetAndCallFirst<T>(ref T current, T value, Action onSetAction, [CallerMemberName] string name = null)
        {
            var equalityComparer = EqualityComparer<T>.Default;
            if (equalityComparer.Equals(current, value))
                return false;

            current = value;

            onSetAction?.Invoke();
            OnPropertyChanged(name);
            return true;
        }
    }
}

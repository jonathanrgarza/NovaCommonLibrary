using System;

namespace Ncl.Common.Core.Infrastructure
{
    /// <summary>
    ///     Event arguments for a value changed event.
    /// </summary>
    /// <typeparam name="T">The type for the value that changed.</typeparam>
    public class ValueChangedEventArgs<T> : EventArgs
    {
        protected readonly T _oldValue;
        protected readonly T _newValue;

        /// <summary>
        ///     Initializes a new instance of <see cref="ValueChangedEventArgs{T}" />.
        /// </summary>
        protected ValueChangedEventArgs()
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="ValueChangedEventArgs{T}" />.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        public ValueChangedEventArgs(T oldValue, T newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        /// <summary>
        ///     Gets the new value.
        /// </summary>
        public virtual T NewValue => _newValue;

        /// <summary>
        ///     Gets the old value.
        /// </summary>
        public virtual T OldValue => _oldValue;
    }
}
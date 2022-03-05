using System;
using Ncl.Common.Core.Infrastructure;

namespace Ncl.Common.Core.Preferences
{
    /// <summary>
    ///     Event arguments for a preference changed event.
    /// </summary>
    public class PreferenceChangedEventArgs : ValueChangedEventArgs<IPreference>
    {
        protected readonly Type _type;

        /// <summary>
        ///     Initializes a new instance of <see cref="PreferenceChangedEventArgs" />.
        /// </summary>
        protected PreferenceChangedEventArgs()
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="PreferenceChangedEventArgs" />.
        /// </summary>
        /// <param name="type">The <see cref="System.Type" /> for the preference.</param>
        /// <param name="oldValue">The old version of the preference.</param>
        /// <param name="newValue">The new version of the preference.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type" /> is <see langword="null" />.</exception>
        public PreferenceChangedEventArgs(Type type, IPreference oldValue, IPreference newValue) : base(oldValue,
            newValue)
        {
            _type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        ///     Gets the new version of the preference that changed.
        /// </summary>
        /// <remarks>
        ///     Calling this property returns a new cloned instance.
        /// </remarks>
        public override IPreference NewValue => _newValue?.Clone();

        /// <summary>
        ///     Gets the old version of the preference that changed.
        /// </summary>
        /// <remarks>
        ///     Calling this property returns a new cloned instance.
        /// </remarks>
        public override IPreference OldValue => _oldValue?.Clone();

        /// <summary>
        ///     Gets the type for the preference that changed.
        /// </summary>
        public virtual Type Type => _type;
    }
}
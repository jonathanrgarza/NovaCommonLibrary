using System;

namespace Ncl.Common.Core.Preferences
{
    /// <summary>
    ///     Interface for a preference model class.
    /// </summary>
    public interface IPreference : IEquatable<IPreference>
    {
        /// <summary>
        ///     Gets a clone instance of the instance.
        /// </summary>
        /// <returns>A clone instance.</returns>
        IPreference Clone();

        /// <summary>
        ///     Gets the version for this preference instance (for comparing against other instances of this type).
        /// </summary>
        /// <returns>The version.</returns>
        Version GetVersion();

        /// <summary>
        ///     Called when this instance has been deserialized.
        /// </summary>
        /// <remarks>
        ///     This allows the preference class to perform any post deserialized checks
        ///     and ensure its been properly deserialized.
        /// </remarks>
        /// <returns>
        ///     The same instance or a corrected preference instance, if needed.
        /// </returns>
        IPreference OnDeserialization();
    }
}
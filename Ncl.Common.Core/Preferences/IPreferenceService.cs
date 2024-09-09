using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Security;

namespace Ncl.Common.Core.Preferences
{
    /// <summary>
    ///     Interface for a preference service that does not require customizations.
    /// </summary>
    public interface IPreferenceService
    {
        /// <summary>
        ///     Event that occurs when a preference value is changed via a set call.
        /// </summary>
        event EventHandler<PreferenceChangedEventArgs> PreferenceChanged;

        /// <summary>
        ///     Gets the save directory path for a given preference type.
        /// </summary>
        /// <typeparam name="T">The preference type.</typeparam>
        /// <returns>The save directory path for the given preference type or <see langword="null" /> when none.</returns>
        string GetPreferenceDirectoryPath<T>() where T : class, IPreference;

        /// <summary>
        ///     Gets the file name for the given preference type.
        /// </summary>
        /// <typeparam name="T">The preference type.</typeparam>
        /// <returns>The file name for the given preference type.</returns>
        string GetPreferenceFileName<T>() where T : class, IPreference;

        /// <summary>
        ///     Gets the save path for the given preference type.
        /// </summary>
        /// <typeparam name="T">The preference type.</typeparam>
        /// <returns>The save path for the given preference type or <see langword="null" /> when none.</returns>
        string GetPreferenceFilePath<T>() where T : class, IPreference;

        /// <summary>
        ///     Gets the default preference for the given preference type.
        /// </summary>
        /// <typeparam name="T">The preference type.</typeparam>
        /// <returns>The default preference or <see langword="null" /> when none has been registered.</returns>
        T GetDefaultPreference<T>() where T : class, IPreference;

        /// <summary>
        ///     Gets the preference for the given preference type.
        /// </summary>
        /// <typeparam name="T">The preference type.</typeparam>
        /// <param name="forceLoadNew">Force loading this preference from save path. Default: <see langword="false" />.</param>
        /// <returns>The preference, default preference or <see langword="null" /> if neither exists.</returns>
        T GetPreference<T>(bool forceLoadNew = false) where T : class, IPreference;

        /// <summary>
        ///     Sets the value for the given preference type.
        ///     A value of <see langword="null" /> will remove existing value for the given preference type.
        /// </summary>
        /// <typeparam name="T">The preference type.</typeparam>
        /// <param name="preference">The preference value.</param>
        void SetPreference<T>(T preference) where T : class, IPreference;

        /// <summary>
        ///     Saves the given preference type to its associated file.
        /// </summary>
        /// <typeparam name="T">The preference type.</typeparam>
        /// <exception cref="ArgumentException">
        ///     The preference file path is <see langword="null" />, empty or white space,
        ///     or contains one or more invalid characters.-or-
        ///     The preference file path refers to a non-file device,
        ///     such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The preference file path refers to a non-file device,
        ///     such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.
        /// </exception>
        /// <exception cref="SecurityException">
        ///     The caller does not have the required permission.
        /// </exception>
        /// <exception cref="PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined maximum length.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        ///     The specified path is invalid, (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     The preference file path specifies a file that is read-only.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the file.
        /// </exception>
        void SavePreference<T>() where T : class, IPreference;

        /// <summary>
        ///     Saves all the currently unsaved preferences.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///     A preference file path is <see langword="null" />, empty or white space,
        ///     or contains one or more invalid characters.-or-
        ///     A preference file path refers to a non-file device,
        ///     such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     A preference file path refers to a non-file device,
        ///     such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.
        /// </exception>
        /// <exception cref="SecurityException">
        ///     The caller does not have the required permission.
        /// </exception>
        /// <exception cref="PathTooLongException">
        ///     A specified path, file name, or both exceed the system-defined maximum length.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        ///     A specified path is invalid, (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     A preference file path specifies a file that is read-only.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to a file.
        /// </exception>
        void SaveAllPreferences();
    }

    /// <summary>
    ///     Interface for a preference service that allows customizing storage location.
    /// </summary>
    public interface ICustomizablePreferenceService : IPreferenceService
    {
        /// <summary>
        ///     The default preference directory path.
        /// </summary>
        /// <remarks>
        ///     This directory must already exist or the <see cref="FallbackDirectory" /> path will be used instead, if allowed.
        /// </remarks>
        string DefaultDirectory { get; }

        /// <summary>
        ///     The fallback preference directory path.
        ///     A <see langword="null" /> value means this option is disabled.
        /// </summary>
        string FallbackDirectory { get; }

        /// <summary>
        ///     Sets the preference save directory path for the given preference type.
        /// </summary>
        /// <typeparam name="T">The preference type.</typeparam>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="invalidateCachedPreferences">
        ///     Should currently cached preferences of this type be invalidated. Default: <see langword="false" />.
        /// </param>
        void SetPreferenceDirectoryPath<T>(string directoryPath,
            bool invalidateCachedPreferences = false) where T : class, IPreference;

        /// <summary>
        ///     Sets the preference save file path for the given preference type.
        /// </summary>
        /// <typeparam name="T">The preference type.</typeparam>
        /// <param name="filePath">The file path.</param>
        /// <param name="invalidateCachedPreferences">
        ///     Should currently cached preferences of this type be invalidated. Default: <see langword="false" />.
        /// </param>
        void SetPreferenceFilePath<T>(string filePath,
            bool invalidateCachedPreferences = false) where T : class, IPreference;

        /// <summary>
        ///     Registers an enumerable of default preferences.
        /// </summary>
        /// <param name="preferenceEnumerable">The preference enumerable.</param>
        /// <exception cref="ArgumentNullException"><paramref name="preferenceEnumerable" /> is <see langword="null" />.</exception>
        void RegisterDefaultPreferences(IEnumerable<IPreference> preferenceEnumerable);

        /// <summary>
        ///     Registers default preferences found in a given directory.
        /// </summary>
        /// <param name="directoryPath">The directory path to search for.</param>
        /// <param name="preferenceTypes">The list of preference types to search for.</param>
        /// <param name="namePrefix">
        ///     The name prefix for default preference files. Default: <see langword="null" /> which means
        ///     none.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="directoryPath" /> or <paramref name="preferenceTypes" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="directoryPath" /> is a zero-length string,
        ///     contains only white space, or contains invalid characters. You can query for invalid characters
        ///     by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">The directory specified does not exist.</exception>
        /// <exception cref="PathTooLongException">
        ///     The specified path, file name, or combined exceed the system-defined maximum
        ///     length.
        /// </exception>
        /// <exception cref="IOException"><paramref name="directoryPath" /> is a file name.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permission.</exception>
        int RegisterDefaultPreferences(string directoryPath, IEnumerable<Type> preferenceTypes,
            string namePrefix = null);
    }
}
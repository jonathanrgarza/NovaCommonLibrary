using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using Ncl.Common.Core.Utilities;
using Ncl.Common.Core.Xml;

namespace Ncl.Common.Core.Preferences
{
    /// <summary>
    /// The base class for a preference service.
    /// </summary>
    public abstract class PreferenceServiceBase : IPreferenceService, ICustomizablePreferenceService
    {
        /// <summary>
        /// Holds the dictionary of factory preference instances (default preferences).
        /// </summary>
        protected readonly Dictionary<Type, IPreference> _factoryPreferences = new Dictionary<Type, IPreference>();

        /// <summary>
        /// Holds the dictionary of preference instances (current preferences).
        /// </summary>
        protected readonly Dictionary<Type, PreferenceSaveInfo> _preferences =
            new Dictionary<Type, PreferenceSaveInfo>();

        /// <summary>
        /// Holds override save locations for a given preference type.
        /// </summary>
        protected readonly Dictionary<Type, SaveLocation> _preferenceSaveLocations =
            new Dictionary<Type, SaveLocation>();

        /// <summary>
        /// Gets the XML serialization service.
        /// </summary>
        protected abstract IXmlSerializationService XmlSerializationService { get; }

        /// <inheritdoc/>
        public abstract string DefaultDirectory { get; }

        /// <inheritdoc/>
        public abstract string FallbackDirectory { get; }

        /// <inheritdoc/>
        public void SetPreferenceDirectoryPath<T>(string directoryPath,
            bool invalidateCachedPreferences = false) where T : class, IPreference
        {
            var prefType = typeof(T);

            UpdatePreferenceSaveLocation(prefType, directoryPath, true);

            if (!invalidateCachedPreferences)
            {
                return;
            }

            UpdatePreferenceCacheSaveLocation(prefType, GetPreferenceFilePath(prefType));
        }

        /// <inheritdoc/>
        public void SetPreferenceFilePath<T>(string filePath,
            bool invalidateCachedPreferences = false) where T : class, IPreference
        {
            var prefType = typeof(T);

            if (filePath.EndsWith(".xml", StringComparison.InvariantCultureIgnoreCase)) filePath += ".xml";

            UpdatePreferenceSaveLocation(prefType, filePath, false);

            if (!invalidateCachedPreferences)
            {
                return;
            }

            UpdatePreferenceCacheSaveLocation(prefType, GetPreferenceFilePath(prefType));
        }

        /// <inheritdoc/>
        public void RegisterDefaultPreferences(IEnumerable<IPreference> preferenceEnumerable)
        {
            if (preferenceEnumerable is null)
            {
                throw new ArgumentNullException(nameof(preferenceEnumerable));
            }

            foreach (var preference in preferenceEnumerable)
            {
                if (preference is null)
                {
                    continue;
                }

                var prefType = preference.GetType();
                _factoryPreferences[prefType] = preference.Clone();
            }
        }

        /// <inheritdoc/>
        public virtual int RegisterDefaultPreferences(string directoryPath, IEnumerable<Type> preferenceTypes,
            string namePrefix = null)
        {
            if (directoryPath is null)
            {
                throw new ArgumentNullException(nameof(directoryPath));
            }

            if (preferenceTypes is null)
            {
                throw new ArgumentNullException(nameof(preferenceTypes));
            }

            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"The directory does not exist: '{directoryPath}'");
            }

            int count = 0;
            var xmlFiles = Directory.EnumerateFiles(directoryPath, "*.xml").ToList();

            if (xmlFiles.Count == 0)
            {
                return 0;
            }

            foreach (var prefType in preferenceTypes)
            {
                if (prefType is null)
                {
                    continue;
                }

                string name = namePrefix is null
                    ? GetPreferenceFileName(prefType)
                    : namePrefix + GetPreferenceFileName(prefType);
                string matchedPath = null;

                foreach (string xmlFile in xmlFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(xmlFile);

                    if (!name.Equals(fileName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }

                    var preference = LoadFromFilePath(prefType, xmlFile);

                    if (preference == null)
                    {
                        continue;
                    }

                    matchedPath = xmlFile;
                    _factoryPreferences[prefType] = preference;
                    break;
                }

                if (matchedPath == null)
                {
                    continue;
                }

                xmlFiles.Remove(matchedPath);
                count++;
            }

            return count;
        }

        /// <inheritdoc cref="IPreferenceService.GetPreferenceFileName{T}"/>
        public event EventHandler<PreferenceChangedEventArgs> PreferenceChanged;

        /// <inheritdoc cref="IPreferenceService.GetPreferenceDirectoryPath{T}"/>
        public string GetPreferenceDirectoryPath<T>() where T : class, IPreference
        {
            var prefType = typeof(T);
            return GetPreferenceDirectoryPath(prefType);
        }

        /// <inheritdoc cref="IPreferenceService.GetPreferenceFileName{T}"/>
        public string GetPreferenceFileName<T>() where T : class, IPreference
        {
            var prefType = typeof(T);
            return GetPreferenceFileName(prefType);
        }

        /// <inheritdoc cref="IPreferenceService.GetPreferenceFilePath{T}"/>
        public string GetPreferenceFilePath<T>() where T : class, IPreference
        {
            string directory = GetPreferenceDirectoryPath<T>();
            if (directory == null)
            {
                return null;
            }

            string fileName = GetPreferenceFileName<T>();
            return Path.Combine(directory, fileName);
        }

        /// <inheritdoc cref="IPreferenceService.GetDefaultPreference{T}"/>
        public T GetDefaultPreference<T>() where T : class, IPreference
        {
            var prefType = typeof(T);
            if (_factoryPreferences.TryGetValue(prefType, out var preference))
            {
                return (T)preference.Clone();
            }

            return null;
        }

        /// <inheritdoc cref="IPreferenceService.GetPreference{T}"/>
        public T GetPreference<T>(bool forceLoadNew = false) where T : class, IPreference
        {
            var prefType = typeof(T);

            return (T)GetPreference(prefType, forceLoadNew);
        }

        /// <inheritdoc cref="IPreferenceService.SetPreference{T}"/>
        public void SetPreference<T>(T preference) where T : class, IPreference
        {
            var prefType = typeof(T);

            if (preference == null)
            {
                //Remove any existing preference for this type
                SetPreferenceCache(prefType, null);
                return;
            }

            SetPreferenceCache(prefType, preference);
        }

        /// <inheritdoc cref="IPreferenceService.SavePreference{T}"/>
        public void SavePreference<T>() where T : class, IPreference
        {
            var prefType = typeof(T);

            SavePreference(prefType);
        }

        /// <inheritdoc cref="IPreferenceService.SaveAllPreferences"/>
        public void SaveAllPreferences()
        {
            foreach (var preferenceSaveInfo in _preferences)
            {
                if (!preferenceSaveInfo.Value.IsDirty)
                {
                    continue;
                }

                SavePreference(preferenceSaveInfo.Key);
            }
        }

        /// <summary>
        /// Gets the save directory path for a given preference type.
        /// </summary>
        /// <param name="prefType">The preference type.</param>
        /// <returns>The save directory path for the given preference type or <see langword="null"/> when none.</returns>
        protected string GetPreferenceDirectoryPath(Type prefType)
        {
            if (_preferenceSaveLocations.TryGetValue(prefType, out var saveLocation))
            {
                if (saveLocation != null)
                {
                    return saveLocation.IsDirectoryPath
                        ? saveLocation.DirectoryPath
                        : Path.GetDirectoryName(saveLocation.FilePath);
                }

                _preferenceSaveLocations.Remove(prefType);
            }

            string defaultDirectory = DefaultDirectory;
            if (defaultDirectory != null && Directory.Exists(defaultDirectory))
            {
                return defaultDirectory;
            }

            return FallbackDirectory;
        }

        /// <summary>
        /// Gets the file name for the given preference type.
        /// </summary>
        /// <param name="prefType">The preference type.</param>
        /// <returns>The file name for the given preference type.</returns>
        protected virtual string GetPreferenceFileName(Type prefType)
        {
            if (!_preferenceSaveLocations.TryGetValue(prefType, out var saveLocation))
            {
                return $"{prefType.Name}.xml";
            }

            if (saveLocation != null)
            {
                return saveLocation.IsFilePath
                    ? Path.GetFileName(saveLocation.FilePath)
                    : $"{prefType.Name}.xml";
            }

            _preferenceSaveLocations.Remove(prefType);

            return $"{prefType.Name}.xml";
        }

        /// <summary>
        /// Gets the save path for the given preference type.
        /// </summary>
        /// <param name="prefType">The preference type.</param>
        /// <returns>The save path for the given preference type or <see langword="null"/> when none.</returns>
        protected string GetPreferenceFilePath(Type prefType)
        {
            string directory = GetPreferenceDirectoryPath(prefType);
            if (directory == null)
            {
                return null;
            }

            string fileName = GetPreferenceFileName(prefType);
            return Path.Combine(directory, fileName);
        }

        /// <summary>
        /// Gets the default preference for the given preference type.
        /// </summary>
        /// <param name="prefType">The preference type.</param>
        /// <returns>The default preference or <see langword="null"/> when none has been registered.</returns>
        protected IPreference GetDefaultPreference(Type prefType)
        {
            return _factoryPreferences.TryGetValue(prefType, out var preference) ? preference.Clone() : null;
        }

        /// <summary>
        /// Gets the preference for the given preference type.
        /// </summary>
        /// <param name="prefType">The preference type.</param>
        /// <param name="forceLoadNew">Force loading this preference from save path. Default: <see langword="false"/>.</param>
        /// <returns>The preference, default preference or <see langword="null"/> if neither exists.</returns>
        protected IPreference GetPreference(Type prefType, bool forceLoadNew = false)
        {
            if (forceLoadNew) return GetPreferenceFromDefaultPath(prefType);

            if (!_preferences.TryGetValue(prefType, out var currentPreferencesInfo))
            {
                return GetPreferenceFromDefaultPath(prefType);
            }

            //Check save path
            if (currentPreferencesInfo == null ||
                (currentPreferencesInfo.SavePath != null &&
                 currentPreferencesInfo.SavePath != GetPreferenceFilePath(prefType)))
            {
                return GetPreferenceFromDefaultPath(prefType);
            }

            return currentPreferencesInfo.Preference.Clone() ?? GetDefaultPreference(prefType);
        }

        /// <summary>
        /// Gets the preference from the default file path for the given preference type.
        /// </summary>
        /// <param name="prefType">The preference type.</param>
        /// <returns>The preference, default preference or <see langword="null"/> if neither exists.</returns>
        protected IPreference GetPreferenceFromDefaultPath(Type prefType)
        {
            string preferenceSavePath = GetPreferenceFilePath(prefType);
            var preference = LoadFromFilePath(prefType, preferenceSavePath);

            SetPreferenceCache(prefType, preference, preferenceSavePath);

            if (preference == null)
            {
                return GetDefaultPreference(prefType);
            }

            return preference.Clone();
        }

        /// <summary>
        /// Saves the given preference type to its associated file.
        /// </summary>
        /// <param name="prefType">The preference type.</param>
        /// <exception cref="ArgumentException">
        /// The preference file path is <see langword="null"/>, empty or white space,
        /// or contains one or more invalid characters.-or-
        /// The preference file path refers to a non-file device,
        /// such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        /// The type being serialized does not conform to data contract rules.
        /// For example, the <see cref="DataContractAttribute"/> attribute
        /// has not been applied to the type.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// The preference file path refers to a non-file device,
        /// such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.
        /// </exception>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// The specified path, file name, or both exceed the system-defined maximum length.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The specified path is invalid, (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The preference file path specifies a file that is read-only.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred while writing to the file.
        /// </exception>
        protected void SavePreference(Type prefType)
        {
            var preferences = GetPreference(prefType);

            if (preferences is null)
            {
                return;
            }

            string filePath = GetPreferenceFilePath(prefType);

            SaveToFilePath(prefType, preferences, filePath);
        }

        /// <summary>
        /// Gets the preference from a given path or <see langword="null"/> on error.
        /// </summary>
        /// <param name="prefType">The preference type.</param>
        /// <param name="path">The file path.</param>
        /// <returns>The preference instance or <see langword="null"/> on error.</returns>
        protected virtual IPreference LoadFromFilePath(Type prefType, string path)
        {
            if (path == null)
            {
                return null;
            }

            if (prefType == null)
            {
                return null;
            }

            if (!File.Exists(path))
            {
                return null;
            }

            if (!XmlSerializationService.TryReadObject(path, prefType, out object result))
            {
                return null;
            }

            var preferences = result as IPreference;
            preferences = preferences?.OnDeserialization();

            return preferences;
        }

        /// <summary>
        /// Saves the given preference type to the given file path.
        /// </summary>
        /// <param name="prefType">The preference type.</param>
        /// <param name="preference">The preference to save.</param>
        /// <param name="path">The file path.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is <see langword="null"/>, empty or white space,
        /// or contains one or more invalid characters.-or-
        /// <paramref name="path"/> refers to a non-file device,
        /// such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        /// The type being serialized does not conform to data contract rules.
        /// For example, the <see cref="DataContractAttribute"/> attribute
        /// has not been applied to the type.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="path"/> refers to a non-file device,
        /// such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.
        /// </exception>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// The specified path, file name, or both exceed the system-defined maximum length.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The specified path is invalid, (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <paramref name="path"/> specifies a file that is read-only.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred while writing to the file.
        /// </exception>
        protected virtual void SaveToFilePath(Type prefType, IPreference preference, string path)
        {
            XmlSerializationService.WriteObject(path, preference, prefType);
            var cache = SetPreferenceCache(prefType, preference, path);
            cache.IsDirty = false;
        }

        /// <summary>
        /// Sets the preference cache value for the given preference type.
        /// </summary>
        /// <param name="prefType">The preference type.</param>
        /// <param name="preference">The preference value.</param>
        /// <param name="filePath">
        /// The preference file path. Not used if <paramref name="preference"/> is <see langword="null"/>.
        /// </param>
        /// <returns>The cache info instance.</returns>
        protected virtual PreferenceSaveInfo SetPreferenceCache(Type prefType, IPreference preference, string filePath)
        {
            if (preference == null)
            {
                _preferences.Remove(prefType);
                RaisePreferenceChanged(prefType, GetPreferenceFromCache(prefType), null);
                return null;
            }

            if (_preferences.TryGetValue(prefType, out var currentSaveInfo) && currentSaveInfo != null)
            {
                var oldPreference = currentSaveInfo.Preference;

                currentSaveInfo.Preference = preference;
                currentSaveInfo.SavePath = filePath;

                RaisePreferenceChanged(prefType, oldPreference, preference);
                return currentSaveInfo;
            }

            var newSaveInfo = new PreferenceSaveInfo(preference, filePath)
            {
                IsDirty = false
            };
            _preferences[prefType] = newSaveInfo;
            RaisePreferenceChanged(prefType, null, preference);
            return newSaveInfo;
        }

        /// <summary>
        /// Sets the preference cache value for the given preference type.
        /// </summary>
        /// <param name="prefType">The preference type.</param>
        /// <param name="preference">The preference value.</param>
        /// <returns>The cache info instance.</returns>
        protected virtual PreferenceSaveInfo SetPreferenceCache(Type prefType, IPreference preference)
        {
            if (preference == null)
            {
                _preferences.Remove(prefType);
                RaisePreferenceChanged(prefType, GetPreferenceFromCache(prefType), null);
                return null;
            }

            var clonedPreference = preference.Clone();
            if (_preferences.TryGetValue(prefType, out var currentSaveInfo) && currentSaveInfo != null)
            {
                var oldPreference = currentSaveInfo.Preference;

                // Update the current preference with a cloned one to avoid changes to the cached preference
                currentSaveInfo.Preference = clonedPreference.Clone();

                RaisePreferenceChanged(prefType, oldPreference, clonedPreference);
                return currentSaveInfo;
            }

            var newSaveInfo = new PreferenceSaveInfo(clonedPreference, null)
            {
                IsDirty = true
            };
            _preferences[prefType] = newSaveInfo;
            RaisePreferenceChanged(prefType, null, clonedPreference);
            return newSaveInfo;
        }

        /// <summary>
        /// Gets a preference from cache. Will not try to load the preference if it isn't in the cache.
        /// </summary>
        /// <param name="prefType">The preference type.</param>
        /// <returns>The preference from cache or <see langword="null"/>.</returns>
        protected virtual IPreference GetPreferenceFromCache(Type prefType)
        {
            return _preferences.TryGetValue(prefType, out var preferenceInfo)
                ? preferenceInfo?.Preference
                : null;
        }

        /// <summary>
        /// Updates the preference cache save locations.
        /// </summary>
        /// <param name="prefType">The preference type.</param>
        /// <param name="filePath">The new file path.</param>
        protected void UpdatePreferenceCacheSaveLocation(Type prefType, string filePath)
        {
            if (!_preferences.TryGetValue(prefType, out var currentSaveInfo))
            {
                return;
            }

            if (currentSaveInfo == null)
            {
                _preferences.Remove(prefType);
                return;
            }

            currentSaveInfo.SavePath = filePath;
        }

        /// <summary>
        /// Updates the preference location information.
        /// </summary>
        /// <param name="prefType">The preference type.</param>
        /// <param name="path">The path.</param>
        /// <param name="isDirectoryPath">
        /// Is the <paramref name="path"/> a directory path (<see langword="true"/>) or
        /// a file path (<see langword="false"/>).
        /// </param>
        protected void UpdatePreferenceSaveLocation(Type prefType, string path, bool isDirectoryPath)
        {
            if (path == null)
            {
                _preferenceSaveLocations.Remove(prefType);
                return;
            }

            if (_preferenceSaveLocations.TryGetValue(prefType, out var currentLocation))
            {
                if (isDirectoryPath)
                {
                    currentLocation.DirectoryPath = path;
                    return;
                }

                currentLocation.FilePath = path;
                return;
            }

            _preferenceSaveLocations[prefType] = new SaveLocation(path, isDirectoryPath);
        }

        /// <summary>
        /// Raises a preference changed event with the given arguments, if difference in values.
        /// </summary>
        /// <param name="prefType">The preference type.</param>
        /// <param name="oldValue">The previous value.</param>
        /// <param name="newValue">The new value.</param>
        protected void RaisePreferenceChanged(Type prefType, IPreference oldValue, IPreference newValue)
        {
            Guard.AgainstNullArgument(nameof(prefType), prefType);

            if (EqualityComparer<IPreference>.Default.Equals(oldValue, newValue))
            {
                return;
            }

            var eventArgs = new PreferenceChangedEventArgs(prefType, oldValue, newValue);
            PreferenceChanged?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// Represents a directory path or a file path.
        /// </summary>
        protected class SaveLocation
        {
            private string _path;

            /// <summary>
            /// Initializes a new instance of <see cref="SaveLocation"/>.
            /// </summary>
            /// <param name="path">The path.</param>
            /// <param name="isDirectoryPath">Is <paramref name="path"/> a directory path.</param>
            public SaveLocation(string path, bool isDirectoryPath)
            {
                _path = path ?? throw new ArgumentNullException(nameof(path));
                IsDirectoryPath = isDirectoryPath;
            }

            /// <summary>
            /// Get the directory path for a save location.
            /// </summary>
            public string DirectoryPath
            {
                get => IsDirectoryPath ? _path : null;
                set
                {
                    _path = value;
                    IsDirectoryPath = true;
                }
            }

            /// <summary>
            /// Gets the file path for a save location.
            /// </summary>
            public string FilePath
            {
                get => !IsDirectoryPath ? _path : null;
                set
                {
                    _path = value;
                    IsDirectoryPath = false;
                }
            }

            /// <summary>
            /// Gets if this is a directory path.
            /// </summary>
            public bool IsDirectoryPath { get; private set; }

            /// <summary>
            /// Gets if this is a file path.
            /// </summary>
            public bool IsFilePath => !IsDirectoryPath;
        }

        /// <summary>
        /// Holds the save information for a preference.
        /// </summary>
        protected class PreferenceSaveInfo : IEquatable<PreferenceSaveInfo>
        {
            private IPreference _preference;
            private string _savePath;

            /// <summary>
            /// Initializes a new instance of <see cref="PreferenceSaveInfo"/>.
            /// </summary>
            /// <param name="preference">The preference value.</param>
            /// <param name="savePath">The save path.</param>
            public PreferenceSaveInfo(IPreference preference, string savePath)
            {
                _preference = preference;
                _savePath = savePath;
                IsDirty = false;
            }

            /// <summary>
            /// Initializes a new instance of <see cref="PreferenceSaveInfo"/>.
            /// </summary>
            /// <param name="instance">The instance to copy.</param>
            public PreferenceSaveInfo(PreferenceSaveInfo instance)
            {
                Guard.AgainstNullArgument(nameof(instance), instance);
                _preference = instance._preference;
                _savePath = instance._savePath;
                IsDirty = instance.IsDirty;
            }

            /// <summary>
            /// Gets/Sets if this preference dirty/unsaved.
            /// </summary>
            public bool IsDirty { get; set; }

            /// <summary>
            /// Gets/Sets the preference value.
            /// </summary>
            public IPreference Preference
            {
                get => _preference;
                set
                {
                    if (EqualityComparer<IPreference>.Default.Equals(_preference, value))
                    {
                        return;
                    }

                    _preference = value;
                    IsDirty = true;
                }
            }

            /// <summary>
            /// Gets/Sets the current save path of this preference.
            /// </summary>
            public string SavePath
            {
                get => _savePath;
                set
                {
                    if (_savePath == value)
                    {
                        return;
                    }

                    _savePath = value;
                    IsDirty = true;
                }
            }

            /// <inheritdoc/>
            public bool Equals(PreferenceSaveInfo other)
            {
                if (ReferenceEquals(null, other))
                {
                    return false;
                }

                if (ReferenceEquals(this, other))
                {
                    return true;
                }

                return EqualityComparer<IPreference>.Default.Equals(_preference, other._preference) &&
                       _savePath == other._savePath &&
                       IsDirty == other.IsDirty;
            }

            /// <inheritdoc/>
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }

                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                if (obj.GetType() != GetType())
                {
                    return false;
                }

                return Equals((PreferenceSaveInfo)obj);
            }

            /// <summary>
            /// Gets a hash code for the current instance.
            /// </summary>
            /// <returns>A hash code for the current instance.</returns>
            public override int GetHashCode()
            {
                unchecked
                {
                    // ReSharper disable NonReadonlyMemberInGetHashCode
                    int hashCode = EqualityComparer<IPreference>.Default.GetHashCode(_preference);
                    hashCode = (hashCode * 397) ^ (_savePath != null ? _savePath.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ IsDirty.GetHashCode();
                    // ReSharper restore NonReadonlyMemberInGetHashCode
                    return hashCode;
                }
            }

            public static bool operator ==(PreferenceSaveInfo left, PreferenceSaveInfo right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(PreferenceSaveInfo left, PreferenceSaveInfo right)
            {
                return !Equals(left, right);
            }
        }
    }
}
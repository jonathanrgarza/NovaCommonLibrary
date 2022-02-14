using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ncl.Common.Core.Infrastructure
{
    /// <summary>
    ///     Utility class for getting certain empty collection type instances.
    /// </summary>
    public static class Empty<T>
    {
        private static T[] _emptyArray;
        private static ReadOnlyCollection<T> _readOnlyCollection;

        /// <summary>
        ///     Gets an empty <see cref="System.Array" /> instance.
        /// </summary>
        /// <returns>An empty <see cref="System.Array" />.</returns>
        public static T[] Array()
        {
            if (_emptyArray != null)
                return _emptyArray;

            // ReSharper disable once UseArrayEmptyMethod
            _emptyArray = new T[0];
            return _emptyArray;
        }

        /// <summary>
        ///     Gets an empty <see cref="IList{T}" /> instance.
        /// </summary>
        /// <returns>The empty <see cref="IList{T}" />.</returns>
        public static IList<T> List()
        {
            return Array();
        }

        /// <summary>
        ///     Gets an empty <see cref="IEnumerable{T}" /> instance.
        /// </summary>
        /// <returns>The empty <see cref="IEnumerable{T}" />.</returns>
        public static IEnumerable<T> Enumerable()
        {
            return Array();
        }

        /// <summary>
        ///     Gets an empty <see cref="ReadOnlyCollection{T}" /> instance.
        /// </summary>
        /// <returns>The empty <see cref="ReadOnlyCollection{T}" />.</returns>
        public static ReadOnlyCollection<T> ReadOnlyCollection()
        {
            if (_readOnlyCollection != null)
                return _readOnlyCollection;

            _readOnlyCollection = new ReadOnlyCollection<T>(Array());
            return _readOnlyCollection;
        }
    }

    /// <summary>
    ///     Utility class for getting certain empty dictionary type instances.
    /// </summary>
    public static class Empty<TKey, TValue>
    {
        private static ReadOnlyDictionary<TKey, TValue> _emptyReadOnlyDictionary;

        /// <summary>
        ///     Gets an empty <see cref="ReadOnlyDictionary{TKey, TValue}" /> instance.
        /// </summary>
        /// <returns>The empty <see cref="ReadOnlyDictionary{TKey, TValue}" />.</returns>
        public static ReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary()
        {
            if (_emptyReadOnlyDictionary != null)
                return _emptyReadOnlyDictionary;

            _emptyReadOnlyDictionary = new ReadOnlyDictionary<TKey, TValue>(new Dictionary<TKey, TValue>());
            return _emptyReadOnlyDictionary;
        }
    }
}
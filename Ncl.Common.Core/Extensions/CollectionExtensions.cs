using System;
using System.Collections.ObjectModel;

namespace Ncl.Common.Core.Extensions
{
    /// <summary>
    ///     Extensions for <see cref="Collection{T}" />.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        ///     Gets the value at the given <paramref name="index" /> or the default value for
        ///     <typeparamref name="T" /> if <paramref name="index" /> is out of range for the collection.
        /// </summary>
        /// <typeparam name="T">The type for the value.</typeparam>
        /// <param name="collection">The collection to get value from.</param>
        /// <param name="index">The index to get the value for.</param>
        /// <returns>
        ///     The value at the <paramref name="index"/> or default value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="collection" /> is <see langword="null" />.</exception>
        public static T GetValueOrDefault<T>(this Collection<T> collection, int index)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (index < 0)
                return default;

            if (index >= collection.Count)
                return default;

            return collection[index];
        }

        /// <summary>
        ///     Gets the value at the given <paramref name="index" /> or the default value for
        ///     <typeparamref name="T" /> if <paramref name="index" /> is out of range for the collection.
        /// </summary>
        /// <typeparam name="T">The type for the value.</typeparam>
        /// <param name="collection">The collection to get value from.</param>
        /// <param name="index">The index to get the value for.</param>
        /// <param name="value">Out: The value at index, or, default value if return value is false.</param>
        /// <returns>
        ///     <see langword="true"/> if index is a valid index and value was set,
        ///     otherwise, <see langword="false"/> if index is not valid and value is default.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="collection" /> is <see langword="null" />.</exception>
        public static bool TryGetValue<T>(this Collection<T> collection, int index, out T value)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (index < 0)
            {
                value = default;
                return false;
            }

            if (index >= collection.Count)
            {
                value = default;
                return false;
            }

            value = collection[index];
            return true;
        }
    }
}
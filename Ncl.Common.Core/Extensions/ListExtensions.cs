using System;
using System.Collections.Generic;
using Ncl.Common.Core.Utilities;

namespace Ncl.Common.Core.Extensions
{
    /// <summary>
    ///     Extensions for <see cref="List{T}" />.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        ///     Gets the value at the given <paramref name="index" /> or the default value for
        ///     <typeparamref name="T" /> if <paramref name="index" /> is out of range for the list.
        /// </summary>
        /// <typeparam name="T">The type for the value.</typeparam>
        /// <param name="list">The list to get value from.</param>
        /// <param name="index">The index to get the value for.</param>
        /// <returns>
        ///     The value at the <paramref name="index" /> or default value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="list" /> is <see langword="null" />.</exception>
        public static T GetValueOrDefault<T>(this List<T> list, int index)
        {
            Guard.AgainstNullArgument(nameof(list), list);

            if (index < 0)
                return default;

            if (index >= list.Count)
                return default;

            return list[index];
        }

        /// <summary>
        ///     Gets the value at the given <paramref name="index" /> or the default value for
        ///     <typeparamref name="T" /> if <paramref name="index" /> is out of range for the list.
        /// </summary>
        /// <typeparam name="T">The type for the value.</typeparam>
        /// <param name="list">The list to get value from.</param>
        /// <param name="index">The index to get the value for.</param>
        /// <param name="value">Out: The value at index, or, default value if return value is false.</param>
        /// <returns>
        ///     <see langword="true" /> if index is a valid index and value was set,
        ///     otherwise, <see langword="false" /> if index is not valid and value is default.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="list" /> is <see langword="null" />.</exception>
        public static bool TryGetValue<T>(this List<T> list, int index, out T value)
        {
            Guard.AgainstNullArgument(nameof(list), list);

            if (index < 0)
            {
                value = default;
                return false;
            }

            if (index >= list.Count)
            {
                value = default;
                return false;
            }

            value = list[index];
            return true;
        }
    }
}
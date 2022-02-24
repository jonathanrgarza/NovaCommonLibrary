using System;

namespace Ncl.Common.Core.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="Type" />.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        ///     Gets if the given type is a <see cref="Nullable{T}" /> type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><see langword="true" /> if the given type is a nullable type, otherwise, <see langword="false" />.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type" /> is <see langword="null" />.</exception>
        public static bool IsNullableType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            
            return Nullable.GetUnderlyingType(type) != null;
        }
    }
}
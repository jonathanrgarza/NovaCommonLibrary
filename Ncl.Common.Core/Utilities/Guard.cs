using System;

namespace Ncl.Common.Core.Utilities
{
    /// <summary>
    ///     Utility class with common guard functions.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        ///     Guard against a null argument/parameter value.
        /// </summary>
        /// <typeparam name="T">The type of the argument/parameter.</typeparam>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">The parameter's name.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="argument" /> is <see langword="null" />.
        /// </exception>
        public static void AgainstNullArgument<T>(T argument, string paramName) where T : class
        {
            if (argument is null)
                throw new ArgumentNullException(paramName);
        }

        /// <summary>
        ///     Guard against a null or empty <see cref="string" /> argument/parameter value.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">The parameter's name.</param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="argument" /> is <see langword="null" /> or an empty string.
        /// </exception>
        public static void AgainstNullOrEmptyArgument(string argument, string paramName)
        {
            if (string.IsNullOrEmpty(argument))
                throw new ArgumentException("Argument is null or an empty.", paramName);
        }

        /// <summary>
        ///     Guard against a null, empty or all white space <see cref="string" />
        ///     argument/parameter value.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">The parameter's name.</param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="argument" /> is <see langword="null" /> or an empty string.
        /// </exception>
        public static void AgainstNullOrWhiteSpaceArgument(string argument, string paramName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException("Argument is null, empty or whitespace.",
                    paramName);
            }
        }
    }
}
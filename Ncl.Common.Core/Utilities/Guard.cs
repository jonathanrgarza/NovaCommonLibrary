using System;

namespace Ncl.Common.Core.Utilities
{
    /// <summary>
    /// Utility class with common guard functions.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        ///     Guard against a null argument/parameter value.
        /// </summary>
        /// <typeparam name="T">The type of the argument/parameter.</typeparam>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">The parameter's name.</param>
        /// <exception cref="ArgumentNullException"><paramref name="argument"/> is <see langword="null"/>.</exception>
        public static void AgainstNullArgument<T>(T argument, string paramName)
        {
            if (argument == null)
                throw new ArgumentNullException(paramName);
        }
    }
}

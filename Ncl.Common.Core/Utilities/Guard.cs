using System;
using Ncl.Common.Core.Extensions;

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
        /// <param name="paramName">The parameter's name.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="argument" /> is <see langword="null" />.
        /// </exception>
        public static void AgainstNullArgument<T>(string paramName, T argument)
        {
            var type = typeof(T);
            if (type.IsValueType && !type.IsNullableType()) //Check to prevent need to box value types
                return;

            if (ReferenceEquals(argument, null))
                throw new ArgumentNullException(paramName);
        }

        /// <summary>
        ///     Guard against a null or empty <see cref="string" /> argument/parameter value.
        /// </summary>
        /// <param name="paramName">The parameter's name.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="argument" /> is <see langword="null" /> or an empty string.
        /// </exception>
        public static void AgainstNullOrEmptyArgument(string paramName, string argument)
        {
            if (string.IsNullOrEmpty(argument))
                throw new ArgumentException("Argument is null or an empty.", paramName);
        }

        /// <summary>
        ///     Guard against a null, empty or all white space <see cref="string" />
        ///     argument/parameter value.
        /// </summary>
        /// <param name="paramName">The parameter's name.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="argument" /> is <see langword="null" /> or an empty string.
        /// </exception>
        public static void AgainstNullOrWhiteSpaceArgument(string paramName, string argument)
        {
            if (!string.IsNullOrWhiteSpace(argument))
                return;
            throw new ArgumentException("Argument is null, empty or whitespace.",
                paramName);
        }

        /// <summary>
        ///    Guard against a disposed object.
        /// </summary>
        /// <param name="disposedValue">The disposed state.</param>
        /// <exception cref="ObjectDisposedException"><paramref name="disposedValue" /> is <see langword="true" />.</exception>
        public static void AgainstDisposed(bool disposedValue)
        {
            if (!disposedValue)
                return;
            throw new ObjectDisposedException("Object has been disposed.");
        }

        /// <summary>
        ///     Guard against a negative <see cref="int" /> argument/parameter value.
        /// </summary>
        /// <param name="paramName">The parameter's name.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstNegativeArgument(string paramName, int argument)
        {
            if (argument >= 0)
                return;
            throw new ArgumentOutOfRangeException(paramName,
                "Value can not be negative");
        }

        /// <summary>
        ///     Guard against a negative <see cref="long" /> argument/parameter value.
        /// </summary>
        /// <param name="paramName">The parameter's name.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstNegativeArgument(string paramName, long argument)
        {
            if (argument >= 0L)
                return;
            throw new ArgumentOutOfRangeException(paramName,
                "Value can not be negative");
        }

        /// <summary>
        ///     Guard against a negative <see cref="float" /> argument/parameter value.
        /// </summary>
        /// <param name="paramName">The parameter's name.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstNegativeArgument(string paramName, float argument)
        {
            if (!(argument < 0.0f))
                return;
            throw new ArgumentOutOfRangeException(paramName,
                "Value can not be negative");
        }

        /// <summary>
        ///     Guard against a negative <see cref="double" /> argument/parameter value.
        /// </summary>
        /// <param name="paramName">The parameter's name.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstNegativeArgument(string paramName, double argument)
        {
            if (!(argument < 0.0))
                return;
            throw new ArgumentOutOfRangeException(paramName,
                "Value can not be negative");
        }

        /// <summary>
        ///     Guard against a negative <see cref="int" /> argument/parameter value, except for -1.
        /// </summary>
        /// <param name="paramName">The parameter's name.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstNegativeArgumentExceptOne(string paramName, int argument)
        {
            if (argument >= -1)
                return;
            throw new ArgumentOutOfRangeException(paramName,
                "Value can not be negative, except -1");
        }

        /// <summary>
        ///     Guard against a negative <see cref="int" /> argument/parameter value, except for -1.
        /// </summary>
        /// <param name="paramName">The parameter's name.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstNegativeArgumentExceptOne(string paramName, long argument)
        {
            if (argument >= -1L)
                return;
            throw new ArgumentOutOfRangeException(paramName,
                "Value can not be negative, except -1");
        }

        /// <summary>
        ///     Guard against a negative <see cref="int" /> argument/parameter value, except for -1.
        /// </summary>
        /// <param name="paramName">The parameter's name.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstNegativeArgumentExceptOne(string paramName, float argument)
        {
            if (argument >= -1.0f)
                return;
            throw new ArgumentOutOfRangeException(paramName,
                "Value can not be negative, except -1");
        }

        /// <summary>
        ///     Guard against a negative <see cref="int" /> argument/parameter value, except for -1.
        /// </summary>
        /// <param name="paramName">The parameter's name.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstNegativeArgumentExceptOne(string paramName, double argument)
        {
            if (argument >= -1.0)
                return;
            throw new ArgumentOutOfRangeException(paramName,
                "Value can not be negative, except -1");
        }

        /// <summary>
        ///     Guard against a zero or negative <see cref="int" /> argument/parameter value.
        /// </summary>
        /// <param name="paramName">The parameter's name.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstZeroOrLessArgument(string paramName, int argument)
        {
            if (argument > 0)
                return;
            throw new ArgumentOutOfRangeException(paramName,
                "Value can not be zero or negative");
        }

        /// <summary>
        ///     Guard against a zero or negative <see cref="long" /> argument/parameter value.
        /// </summary>
        /// <param name="paramName">The parameter's name.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstZeroOrLessArgument(string paramName, long argument)
        {
            if (argument > 0L)
                return;
            throw new ArgumentOutOfRangeException(paramName,
                "Value can not be zero or negative");
        }

        /// <summary>
        ///     Guard against a zero or negative <see cref="float" /> argument/parameter value.
        /// </summary>
        /// <param name="paramName">The parameter's name.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstZeroOrLessArgument(string paramName, float argument)
        {
            if (!(argument <= 0.0f))
                return;
            throw new ArgumentOutOfRangeException(paramName,
                "Value can not be zero or negative");
        }

        /// <summary>
        ///     Guard against a zero or negative <see cref="double" /> argument/parameter value.
        /// </summary>
        /// <param name="paramName">The parameter's name.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstZeroOrLessArgument(string paramName, double argument)
        {
            if (!(argument <= 0.0))
                return;
            throw new ArgumentOutOfRangeException(paramName,
                "Value can not be zero or negative");
        }
    }
}
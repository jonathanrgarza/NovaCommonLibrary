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
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">The parameter's name.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="argument" /> is <see langword="null" />.
        /// </exception>
        public static void AgainstNullArgument<T>(T argument, string paramName)
        {
            Type type = typeof(T);
            if (type.IsValueType && !type.IsNullableType()) //Check to prevent need to box value types
                return;

            if (ReferenceEquals(argument, null))
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

        /// <summary>
        ///     Guard against a negative <see cref="int" /> argument/parameter value.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">The parameter's name.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstNegativeArgument(int argument, string paramName)
        {
            if (argument < 0)
            {
                throw new ArgumentOutOfRangeException(paramName,
                    "Value can not be negative");
            }
        }

        /// <summary>
        ///     Guard against a negative <see cref="long" /> argument/parameter value.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">The parameter's name.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstNegativeArgument(long argument, string paramName)
        {
            if (argument < 0)
            {
                throw new ArgumentOutOfRangeException(paramName,
                    "Value can not be negative");
            }
        }

        /// <summary>
        ///     Guard against a negative <see cref="float" /> argument/parameter value.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">The parameter's name.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstNegativeArgument(float argument, string paramName)
        {
            if (argument < 0)
            {
                throw new ArgumentOutOfRangeException(paramName,
                    "Value can not be negative");
            }
        }

        /// <summary>
        ///     Guard against a negative <see cref="double" /> argument/parameter value.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">The parameter's name.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstNegativeArgument(double argument, string paramName)
        {
            if (argument < 0)
            {
                throw new ArgumentOutOfRangeException(paramName,
                    "Value can not be negative");
            }
        }

        /// <summary>
        ///     Guard against a zero or negative <see cref="int" /> argument/parameter value.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">The parameter's name.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstZeroOrLessArgument(int argument, string paramName)
        {
            if (argument <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName,
                    "Value can not be zero or negative");
            }
        }

        /// <summary>
        ///     Guard against a zero or negative <see cref="long" /> argument/parameter value.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">The parameter's name.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstZeroOrLessArgument(long argument, string paramName)
        {
            if (argument <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName,
                    "Value can not be zero or negative");
            }
        }

        /// <summary>
        ///     Guard against a zero or negative <see cref="float" /> argument/parameter value.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">The parameter's name.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstZeroOrLessArgument(float argument, string paramName)
        {
            if (argument <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName,
                    "Value can not be zero or negative");
            }
        }

        /// <summary>
        ///     Guard against a zero or negative <see cref="double" /> argument/parameter value.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">The parameter's name.</param>
        /// <exception cref="ArgumentOutOfRangeException ">
        ///     <paramref name="argument" /> is less than zero.
        /// </exception>
        public static void AgainstZeroOrLessArgument(double argument, string paramName)
        {
            if (argument <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName,
                    "Value can not be zero or negative");
            }
        }
    }
}
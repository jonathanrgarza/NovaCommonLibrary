using System;
using System.ComponentModel;
using Ncl.Common.Core.Infrastructure;

namespace Ncl.Common.Core.Extensions
{
    /// <summary>
    ///     Extensions methods for the <see cref="Enum"/> type.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        ///     Gets an attribute on an enum field value.
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve.</typeparam>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>The attribute of type <typeparamref name="T"/> that exists on the enum value or null if it doesn't exist.</returns>
        public static T GetAttributeOfType<T>(this Enum enumValue) where T : Attribute
        {
            var type = enumValue.GetType();
            var memInfo = type.GetMember(enumValue.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        /// <summary>
        ///     Gets the description attribute for an enum field value.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>The description for the enum value or the value's ToString() if their is no description.</returns>
        public static string GetDescription(this Enum enumValue)
        {
            return GetAttributeOfType<DescriptionAttribute>(enumValue)?.Description ?? enumValue.ToString();
        }

        /// <summary>
        ///     Gets the description attribute for an enum field value.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <param name="result">The out resulting value.</param>
        /// <returns>
        ///     true if the result contains the description for the enum value, 
        ///     false if their is no description for the enum value 
        ///     (result will be set to the ToString() for the enum value in this case).
        /// </returns>
        public static bool GetDescription(this Enum enumValue, out string result)
        {
            var value = GetAttributeOfType<DescriptionAttribute>(enumValue);

            if (value == null)
            {
                result = enumValue.ToString();
                return false;
            }

            result = value.Description;
            return true;
        }

        /// <summary>
        ///     Gets the abbreviation attribute for an enum field value.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>The abbreviation for the enum value or the value's ToString() if their is no abbreviation.</returns>
        public static string GetAbbreviation(this Enum enumValue)
        {
            return GetAttributeOfType<AbbreviationAttribute>(enumValue)?.Abbreviation ?? enumValue.ToString();
        }

        /// <summary>
        ///     Gets the abbreviation attribute for an enum field value.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <param name="result">The out resulting value.</param>
        /// <returns>
        ///     true if the result contains the abbreviation for the enum value, 
        ///     false if their is no abbreviation for the enum value 
        ///     (result will be set to the ToString() for the enum value in this case).
        /// </returns>
        public static bool GetAbbreviation(this Enum enumValue, out string result)
        {
            var value = GetAttributeOfType<AbbreviationAttribute>(enumValue);

            if (value == null)
            {
                result = enumValue.ToString();
                return false;
            }

            result = value.Abbreviation;
            return true;
        }
    }
}

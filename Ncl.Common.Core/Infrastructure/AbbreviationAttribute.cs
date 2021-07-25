using System;
using System.Collections.Generic;

namespace Ncl.Common.Core.Infrastructure
{
    /// <summary>
    ///     An attribute which represents an abbreviation.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class AbbreviationAttribute : Attribute, IEquatable<AbbreviationAttribute>
    {
        /// <summary>
        ///     Specifies the default value for the <see cref="AbbreviationAttribute"/>, which is an
        ///     empty string ("").
        /// </summary>
        public static readonly AbbreviationAttribute Default = new AbbreviationAttribute();

        /// <summary>
        ///     Gets the abbreviation.
        /// </summary>
        public string Abbreviation { get; }

        /// <summary>
        ///     Initializes a new instance of <see cref="AbbreviationAttribute"/>.
        /// </summary>
        public AbbreviationAttribute() : this(string.Empty)
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="AbbreviationAttribute"/>.
        /// </summary>
        /// <param name="abbreviation">The abbreviation value.</param>
        public AbbreviationAttribute(string abbreviation)
        {
            Abbreviation = abbreviation;
        }

        /// <inheritdoc/>
        public override bool IsDefaultAttribute()
        {
            return Equals(Default);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals(obj as AbbreviationAttribute);
        }

        /// <inheritdoc/>
        public bool Equals(AbbreviationAttribute other)
        {
            return other != null &&
                   Abbreviation == other.Abbreviation;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 1272196887;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Abbreviation);
            return hashCode;
        }

        public static bool operator ==(AbbreviationAttribute left, AbbreviationAttribute right)
        {
            return EqualityComparer<AbbreviationAttribute>.Default.Equals(left, right);
        }

        public static bool operator !=(AbbreviationAttribute left, AbbreviationAttribute right)
        {
            return !(left == right);
        }
    }
}

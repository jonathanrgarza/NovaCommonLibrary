using System;

namespace Ncl.Common.Csv
{
    /// <summary>
    ///     Represents a CSV field read result.
    /// </summary>
    public class FieldReadResult : IEquatable<FieldReadResult>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="FieldReadResult" />.
        /// </summary>
        /// <param name="field">The field read.</param>
        /// <param name="newLineEncountered">The status of a new line sequence being encountered.</param>
        public FieldReadResult(string field, bool newLineEncountered)
        {
            Field = field;
            NewLineEncountered = newLineEncountered;
        }

        /// <summary>
        ///     Gets the field read from the stream.
        /// </summary>
        public string Field { get; }

        /// <summary>
        ///     Gets if a new line sequence was encountered during the read operation.
        /// </summary>
        public bool NewLineEncountered { get; }

        /// <inheritdoc />
        public bool Equals(FieldReadResult other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Field == other.Field && NewLineEncountered == other.NewLineEncountered;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return Equals(obj as FieldReadResult);
        }

        /// <summary>
        ///     Gets a hash code representing this object.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Field != null ? Field.GetHashCode() : 0) * 397) ^ NewLineEncountered.GetHashCode();
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Field;
        }

        public static bool operator ==(FieldReadResult left, FieldReadResult right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FieldReadResult left, FieldReadResult right)
        {
            return !Equals(left, right);
        }
    }
}
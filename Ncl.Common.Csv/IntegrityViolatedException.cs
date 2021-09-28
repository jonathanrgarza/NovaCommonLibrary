using System;
using System.Runtime.Serialization;

namespace Ncl.Common.Csv
{
    /// <summary>
    ///     The exception that is thrown when a method call would violate the integrity of the CSV stream.
    /// </summary>
    public class IntegrityViolatedException : InvalidOperationException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="IntegrityViolatedException" /> class.
        /// </summary>
        public IntegrityViolatedException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="IntegrityViolatedException" /> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected IntegrityViolatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="IntegrityViolatedException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public IntegrityViolatedException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="IntegrityViolatedException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        ///     The exception that is the cause of the current exception.
        ///     If the <paramref name="innerException" /> parameter is not a null reference
        ///     (<see langword="Nothing" /> in Visual Basic), the current exception is raised
        ///     in a <see langword="catch" /> block that handles the inner exception.
        /// </param>
        public IntegrityViolatedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
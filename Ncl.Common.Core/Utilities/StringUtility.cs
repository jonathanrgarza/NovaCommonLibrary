using System;
using System.IO;
using System.Text;

namespace Ncl.Common.Core.Utilities
{
    /// <summary>
    ///     Utility function related to <see cref="string"/>.
    /// </summary>
    public static class StringUtility
    {
        /// <summary>
        ///     Gets a string from the whole content of the given stream.
        ///     Will reset the stream to the starting position, if needed, read to end and
        ///     put position back to initial position.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>The <see cref="string"/> read from the stream.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <see langword="null"/>.</exception>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="NotSupportedException">The stream does not support seeking.</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after the stream was closed.</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        public static string GetStringFromStream(Stream stream)
        {
            Guard.AgainstNullArgument(nameof(stream), stream);

            long initialPosition = stream.Position;

            //Reset to beginning
            bool resetPosition = initialPosition > 0L;
            if (resetPosition)
            {
                stream.Position = 0;
            }

            string text;
            using (var streamReader = new StreamReader(stream, Encoding.UTF8, true, 1024, true))
            {
                text = streamReader.ReadToEnd();
            }

            if (resetPosition)
            {
                stream.Position = initialPosition;
            }

            return text;
        }
    }
}

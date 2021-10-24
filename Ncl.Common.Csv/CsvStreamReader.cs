using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Ncl.Common.Csv
{
    /// <summary>
    ///     A CSV stream writer.
    ///     Handles reading escaped fields.
    /// </summary>
    public class CsvStreamReader : IDisposable
    {
        //CSV Standard: https://datatracker.ietf.org/doc/html/rfc4180

        /// <summary>
        ///     The new line string based on the CSV standard.
        /// </summary>
        public const string DefaultNewLine = "\r\n";

        /// <summary>
        ///     The default separator character, a comma.
        /// </summary>
        public const char DefaultSeparator = ',';

        /// <summary>
        ///     The double quote (") character.
        /// </summary>
        protected const char DoubleQuoteChar = '"';

        /// <summary>
        ///     The exception message when given an invalid separator character.
        /// </summary>
        protected const string InvalidSeparatorCharacterMsg =
            "Separator value can not be a double quotation mark (\"), return feed (\\r) or newline character (\\n)";

        /// <summary>
        ///     The default buffer size for any StreamReader instance and the internal buffer.
        /// </summary>
        protected const int DefaultBufferSize = 1024;

        /// <summary>
        ///     The default encoding used when no encoding is specified.
        /// </summary>
        protected static readonly Encoding DefaultEncoding = Encoding.UTF8;

        /// <summary>
        ///     Should the stream be left open when this reader is closed/disposed.
        /// </summary>
        protected readonly bool _leaveOpen;

        /// <summary>
        ///     The stream.
        /// </summary>
        protected readonly TextReader _stream;

        /// <summary>
        ///     The buffer holding characters read from the stream.
        /// </summary>
        protected char[] _buffer = new char[DefaultBufferSize];

        /// <summary>
        ///     The length of the current buffer. This can be smaller than the array's length.
        /// </summary>
        protected int _bufferLength;

        /// <summary>
        ///     The current position in the buffer.
        /// </summary>
        protected int _bufferPosition;

        /// <summary>
        ///     The new line sequence used to determine when a row ends.
        /// </summary>
        protected readonly string _newLine;

        /// <summary>
        ///     Single character buffer used for when need to move back a character.
        ///     Needed in case a newline sequence crosses the end of a buffer.
        /// </summary>
        protected char? _previousCharBuffer;

        /// <summary>
        ///     The separator character used to delineate fields.
        /// </summary>
        protected readonly char _separator;

        private IFormatProvider _formatProvider;

        private bool _isDisposed;

        /// <summary>
        ///     Initializes a new instance of <see cref="CsvStreamReader" />.
        /// </summary>
        /// <param name="stream">The underlying stream to use.</param>
        /// <param name="encoding">
        ///     The encoding to use.
        ///     A null value will result in the default encoding being used. Defaults to null.
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        ///     <see langword="true" /> to look for byte order marks at the beginning of the file;
        ///     otherwise, <see langword="false" />.
        /// </param>
        /// <param name="leaveOpen">
        ///     Should the given stream be left open when this instance is disposed/closed.
        ///     Defaults to <see langword="false" />.
        /// </param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="newLine">The new line character(s) for the CSV stream. Defaults to CRLF.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream" /> or <paramref name="newLine" /> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     The <paramref name="stream" /> is not readable -or-
        ///     <paramref name="separator" /> is a double quote, return feed or a new line character -or-
        ///     <paramref name="newLine" /> is an empty string, contains a double quotation mark (") or
        ///     is longer than 2 characters.
        /// </exception>
        public CsvStreamReader(Stream stream, Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true,
            bool leaveOpen = false, char separator = DefaultSeparator, string newLine = DefaultNewLine,
            IFormatProvider formatProvider = null)
        {
            if (separator == DoubleQuoteChar || separator == '\r' || separator == '\n')
                throw new ArgumentException(InvalidSeparatorCharacterMsg, nameof(separator));

            if (newLine == null)
                throw new ArgumentNullException(nameof(newLine));
            if (newLine.Length == 0)
                throw new ArgumentException("New line sequence can't be an empty string", nameof(newLine));
            if (newLine.Length > 2)
            {
                throw new ArgumentException("New line sequence can't not be more than two characters",
                    nameof(newLine));
            }

            if (newLine.Contains("\""))
            {
                throw new ArgumentException("New line sequence can't contain a double quotation mark (\")",
                    nameof(newLine));
            }

            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (encoding == null)
            {
                encoding = DefaultEncoding;
            }

            _stream = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, DefaultBufferSize,
                leaveOpen);
            _leaveOpen = leaveOpen;
            _separator = separator;
            _newLine = newLine;
            _formatProvider = formatProvider ?? Thread.CurrentThread.CurrentCulture;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="CsvStreamReader" />.
        /// </summary>
        /// <param name="stream">The underlying stream to use.</param>
        /// <param name="leaveOpen">
        ///     Should the given stream be left open when this instance is disposed/closed.
        ///     Defaults to <see langword="false" />.
        /// </param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="newLine">The new line character(s) for the CSV stream. Defaults to CRLF.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream" /> or <paramref name="newLine" /> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="separator" /> is a double quote, return feed or a new line character -or-
        ///     <paramref name="newLine" /> is an empty string, contains a double quotation mark (") or
        ///     is longer than 2 characters.
        /// </exception>
        public CsvStreamReader(TextReader stream, bool leaveOpen = false,
            char separator = DefaultSeparator, string newLine = DefaultNewLine, IFormatProvider formatProvider = null)
        {
            if (separator == DoubleQuoteChar || separator == '\r' || separator == '\n')
                throw new ArgumentException(InvalidSeparatorCharacterMsg, nameof(separator));

            if (newLine == null)
                throw new ArgumentNullException(nameof(newLine));
            if (newLine.Length == 0)
                throw new ArgumentException("New line sequence can't be an empty string", nameof(newLine));
            if (newLine.Length > 2)
            {
                throw new ArgumentException("New line sequence can't not be more than two characters",
                    nameof(newLine));
            }

            if (newLine.Contains("\""))
            {
                throw new ArgumentException("New line sequence can't contain a double quotation mark (\")",
                    nameof(newLine));
            }

            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _leaveOpen = leaveOpen;
            _separator = separator;
            _newLine = newLine;
            _formatProvider = formatProvider ?? Thread.CurrentThread.CurrentCulture;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="CsvStreamReader" />.
        /// </summary>
        /// <param name="path">The file path to read from.</param>
        /// <param name="encoding">
        ///     The encoding to use.
        ///     A null value will result in the default encoding being used. Defaults to null.
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        ///     <see langword="true" /> to look for byte order marks at the beginning of the file;
        ///     otherwise, <see langword="false" />.
        /// </param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="newLine">The new line character(s) for the CSV stream. Defaults to CRLF.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="newLine" /> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path" /> is an empty string ("") -or-
        ///     <paramref name="separator" /> is a double quote, return feed or a new line character -or-
        ///     <paramref name="newLine" /> is an empty string, contains a double quotation mark (") or
        ///     is longer than 2 characters.
        /// </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified path is invalid, such as being on an unmapped drive.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="path" /> includes an incorrect or invalid syntax for file name,
        ///     directory name, or volume label.
        /// </exception>
        public CsvStreamReader(string path, Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true,
            char separator = DefaultSeparator, string newLine = DefaultNewLine,
            IFormatProvider formatProvider = null)
        {
            if (separator == DoubleQuoteChar || separator == '\r' || separator == '\n')
                throw new ArgumentException(InvalidSeparatorCharacterMsg, nameof(separator));

            if (newLine == null)
                throw new ArgumentNullException(nameof(newLine));
            if (newLine.Length == 0)
                throw new ArgumentException("New line sequence can't be an empty string", nameof(newLine));
            if (newLine.Length > 2)
            {
                throw new ArgumentException("New line sequence can't not be more than two characters",
                    nameof(newLine));
            }

            if (newLine.Contains("\""))
            {
                throw new ArgumentException("New line sequence can't contain a double quotation mark (\")",
                    nameof(newLine));
            }

            //Moved before "if (path == null)" only for unit tests
            if (encoding == null)
            {
                encoding = DefaultEncoding;
            }

            if (path == null)
                throw new ArgumentNullException(nameof(path));

            _stream = new StreamReader(path, encoding, detectEncodingFromByteOrderMarks, DefaultBufferSize);
            _leaveOpen = false;
            _separator = separator;
            _newLine = newLine;
            _formatProvider = formatProvider ?? Thread.CurrentThread.CurrentCulture;
        }

        /// <summary>
        ///     Gets if the end of the stream has been reached.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        ///     The <see cref="CsvStreamReader" /> or underlying stream is disposed.
        /// </exception>
        public bool EndOfStream
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(nameof(CsvStreamReader));

                return Peek() == null;
            }
        }

        /// <summary>
        ///     Gets the field position in the current row.
        /// </summary>
        public int FieldPosition { get; protected set; }
        
        /// <summary>
        ///     Gets the total number of fields read from the stream.
        /// </summary>
        public long FieldsRead { get; protected set; }

        /// <summary>
        ///     Gets if the first row has been read. Special as it can optionally be a header row.
        /// </summary>
        public bool FirstRowRead => RowsRead > 0;

        /// <summary>
        ///     Gets/Sets the format provider.
        ///     Setting a null value will reset to default value instead.
        /// </summary>
        public IFormatProvider FormatProvider
        {
            get => _formatProvider;
            set
            {
                if (_formatProvider != null && _formatProvider.Equals(value))
                    return;

                if (value == null)
                {
                    _formatProvider = Thread.CurrentThread.CurrentCulture;
                    return;
                }

                _formatProvider = value;
            }
        }

        /// <summary>
        ///     Gets the maximum field count read for the a row.
        ///     This property is only updated when a row is finished.
        /// </summary>
        public int MaxFieldCount { get; protected set; }

        /// <summary>
        ///     Gets the new line character sequence.
        /// </summary>
        public string NewLine => _newLine;

        /// <summary>
        ///     Gets the rows written for the current stream.
        /// </summary>
        public long RowsRead { get; protected set; }

        /// <summary>
        ///     Gets the separator character.
        /// </summary>
        public char Separator => _separator;

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Creates a new <see cref="CsvStreamReader" /> instance using the given stream.
        /// </summary>
        /// <param name="stream">The underlying stream to use.</param>
        /// <param name="encoding">
        ///     The encoding to use.
        ///     A null value will result in the default encoding being used. Defaults to null.
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        ///     <see langword="true" /> to look for byte order marks at the beginning of the file;
        ///     otherwise, <see langword="false" />.
        /// </param>
        /// <param name="leaveOpen">
        ///     Should the given stream be left open when this instance is disposed/closed.
        ///     Defaults to <see langword="false" />.
        /// </param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="newLine">The new line character(s) for the CSV stream. Defaults to CRLF.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <returns>The new instance of <see cref="CsvStreamReader" />.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream" /> or <paramref name="newLine" /> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     The <paramref name="stream" /> is not readable -or-
        ///     <paramref name="separator" /> is a double quote, return feed or a new line character -or-
        ///     <paramref name="newLine" /> is an empty string, contains a double quotation mark (") or
        ///     is longer than 2 characters.
        /// </exception>
        public static CsvStreamReader Create(Stream stream, Encoding encoding = null,
            bool detectEncodingFromByteOrderMarks = true, bool leaveOpen = false, char separator = DefaultSeparator,
            string newLine = DefaultNewLine, IFormatProvider formatProvider = null)
        {
            bool result = TryCreate(stream, encoding, detectEncodingFromByteOrderMarks, leaveOpen, separator, newLine,
                formatProvider, out Exception ex, out CsvStreamReader cvsStream);
            if (result)
                return cvsStream;

            if (ex == null)
            {
                //Shouldn't happen
                ex = new Exception("An unknown error has occurred");
            }

            throw ex;
        }

        /// <summary>
        ///     Creates a new <see cref="CsvStreamReader" /> instance using the given stream.
        /// </summary>
        /// <param name="stream">The underlying stream to use.</param>
        /// <param name="ex">Out: The <see cref="Exception" />, if any occurs.</param>
        /// <param name="encoding">
        ///     The encoding to use.
        ///     A null value will result in the default encoding being used. Defaults to null.
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        ///     <see langword="true" /> to look for byte order marks at the beginning of the file;
        ///     otherwise, <see langword="false" />.
        /// </param>
        /// <param name="leaveOpen">
        ///     Should the given stream be left open when this instance is disposed/closed.
        ///     Defaults to <see langword="false" />.
        /// </param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="newLine">The new line character(s) for the CSV stream. Defaults to CRLF.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <returns>The new instance of <see cref="CsvStreamReader" /> or null on error.</returns>
        public static CsvStreamReader Create(Stream stream, out Exception ex, Encoding encoding = null,
            bool detectEncodingFromByteOrderMarks = true, bool leaveOpen = false, char separator = DefaultSeparator,
            string newLine = DefaultNewLine, IFormatProvider formatProvider = null)
        {
            bool result = TryCreate(stream, encoding, detectEncodingFromByteOrderMarks, leaveOpen, separator, newLine,
                formatProvider, out ex, out CsvStreamReader cvsStream);

            return result ? cvsStream : null;
        }

        /// <summary>
        ///     Tries to create a new <see cref="CsvStreamReader" /> instance using the given stream.
        /// </summary>
        /// <param name="stream">The stream to use.</param>
        /// <param name="ex">Out: The <see cref="Exception" />, if any occurs.</param>
        /// <param name="csvStream">Out: The new instance.</param>
        /// <param name="encoding">
        ///     The encoding to use.
        ///     A null value will result in the default encoding being used. Defaults to null.
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        ///     <see langword="true" /> to look for byte order marks at the beginning of the file;
        ///     otherwise, <see langword="false" />.
        /// </param>
        /// <param name="leaveOpen">
        ///     Should the given stream be left open when this instance is disposed/closed.
        ///     Defaults to <see langword="false" />.
        /// </param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="newLine">The new line character(s) for the CSV stream. Defaults to CRLF.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <returns>True if the instance of <see cref="CsvStreamReader" /> was created, otherwise, false.</returns>
        public static bool TryCreate(Stream stream, out Exception ex, out CsvStreamReader csvStream,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, bool leaveOpen = false,
            char separator = DefaultSeparator, string newLine = DefaultNewLine, IFormatProvider formatProvider = null)
        {
            return TryCreate(stream, encoding, detectEncodingFromByteOrderMarks, leaveOpen, separator, newLine,
                formatProvider, out ex, out csvStream);
        }

        /// <summary>
        ///     Tries to create a new <see cref="CsvStreamReader" /> instance using the given stream.
        /// </summary>
        /// <param name="stream">The stream to use.</param>
        /// <param name="encoding">
        ///     The encoding to use.
        ///     A null value will result in the default encoding being used.
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        ///     <see langword="true" /> to look for byte order marks at the beginning of the file;
        ///     otherwise, <see langword="false" />.
        /// </param>
        /// <param name="leaveOpen">
        ///     Should the given stream be left open when this instance is disposed/closed.
        /// </param>
        /// <param name="separator">The separator for the CSV stream.</param>
        /// <param name="newLine">The new line character(s) for the CSV stream.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        /// </param>
        /// <param name="ex">Out: The <see cref="Exception" />, if any occurs.</param>
        /// <param name="csvStream">Out: The new instance.</param>
        /// <returns>True if the instance of <see cref="CsvStreamReader" /> was created, otherwise, false.</returns>
        public static bool TryCreate(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks,
            bool leaveOpen, char separator, string newLine, IFormatProvider formatProvider,
            out Exception ex, out CsvStreamReader csvStream)
        {
            ex = null;
            csvStream = null;

            try
            {
                csvStream = new CsvStreamReader(stream, encoding, detectEncodingFromByteOrderMarks, leaveOpen,
                    separator, newLine, formatProvider);
                return true;
            }
            catch (Exception e)
            {
                ex = e;
            }

            return false;
        }

        /// <summary>
        ///     Creates a new <see cref="CsvStreamReader" /> instance using the given stream.
        /// </summary>
        /// <param name="stream">The underlying stream to use.</param>
        /// <param name="leaveOpen">
        ///     Should the given stream be left open when this instance is disposed/closed.
        ///     Defaults to <see langword="false" />.
        /// </param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="newLine">The new line character(s) for the CSV stream. Defaults to CRLF.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <returns>The new instance of <see cref="CsvStreamReader" />.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream" /> or <paramref name="newLine" /> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="separator" /> is a double quote, return feed or a new line character -or-
        ///     <paramref name="newLine" /> is an empty string, contains a double quotation mark (") or
        ///     is longer than 2 characters.
        /// </exception>
        public static CsvStreamReader Create(TextReader stream, bool leaveOpen = false,
            char separator = DefaultSeparator, string newLine = DefaultNewLine, IFormatProvider formatProvider = null)
        {
            bool result = TryCreate(stream, leaveOpen, separator, newLine, formatProvider,
                out Exception ex, out CsvStreamReader cvsStream);
            if (result)
                return cvsStream;

            if (ex == null)
            {
                //Shouldn't happen
                ex = new Exception("An unknown error has occurred");
            }

            throw ex;
        }

        /// <summary>
        ///     Creates a new <see cref="CsvStreamReader" /> instance using the given stream.
        /// </summary>
        /// <param name="stream">The underlying stream to use.</param>
        /// <param name="ex">Out: The <see cref="Exception" />, if any occurs.</param>
        /// <param name="leaveOpen">
        ///     Should the given stream be left open when this instance is disposed/closed.
        ///     Defaults to <see langword="false" />.
        /// </param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="newLine">The new line character(s) for the CSV stream. Defaults to CRLF.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <returns>The new instance of <see cref="CsvStreamReader" /> or null on error.</returns>
        public static CsvStreamReader Create(TextReader stream, out Exception ex, bool leaveOpen = false,
            char separator = DefaultSeparator, string newLine = DefaultNewLine, IFormatProvider formatProvider = null)
        {
            bool result = TryCreate(stream, leaveOpen, separator, newLine, formatProvider,
                out ex, out CsvStreamReader cvsStream);

            return result ? cvsStream : null;
        }

        /// <summary>
        ///     Tries to create a new <see cref="CsvStreamReader" /> instance using the given stream.
        /// </summary>
        /// <param name="stream">The stream to use.</param>
        /// <param name="ex">Out: The <see cref="Exception" />, if any occurs.</param>
        /// <param name="csvStream">Out: The new instance.</param>
        /// <param name="leaveOpen">
        ///     Should the given stream be left open when this instance is disposed/closed.
        ///     Defaults to <see langword="false" />.
        /// </param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="newLine">The new line character(s) for the CSV stream. Defaults to CRLF.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <returns>True if the instance of <see cref="CsvStreamReader" /> was created, otherwise, false.</returns>
        public static bool TryCreate(TextReader stream, out Exception ex, out CsvStreamReader csvStream,
            bool leaveOpen = false, char separator = DefaultSeparator, string newLine = DefaultNewLine,
            IFormatProvider formatProvider = null)
        {
            return TryCreate(stream, leaveOpen, separator, newLine, formatProvider, out ex, out csvStream);
        }

        /// <summary>
        ///     Tries to create a new <see cref="CsvStreamReader" /> instance using the given stream.
        /// </summary>
        /// <param name="stream">The stream to use.</param>
        /// <param name="leaveOpen">
        ///     Should the given stream be left open when this instance is disposed/closed.
        /// </param>
        /// <param name="separator">The separator for the CSV stream.</param>
        /// <param name="newLine">The new line character(s) for the CSV stream.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        /// </param>
        /// <param name="ex">Out: The <see cref="Exception" />, if any occurs.</param>
        /// <param name="csvStream">Out: The new instance.</param>
        /// <returns>True if the instance of <see cref="CsvStreamReader" /> was created, otherwise, false.</returns>
        public static bool TryCreate(TextReader stream, bool leaveOpen, char separator, string newLine,
            IFormatProvider formatProvider, out Exception ex, out CsvStreamReader csvStream)
        {
            ex = null;
            csvStream = null;

            try
            {
                csvStream = new CsvStreamReader(stream, leaveOpen, separator, newLine, formatProvider);
                return true;
            }
            catch (Exception e)
            {
                ex = e;
            }

            return false;
        }

        /// <summary>
        ///     Creates a new <see cref="CsvStreamReader" /> instance using the given file path.
        /// </summary>
        /// <param name="path">The file path to read from.</param>
        /// <param name="encoding">
        ///     The encoding to use.
        ///     A null value will result in the default encoding being used. Defaults to null.
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        ///     <see langword="true" /> to look for byte order marks at the beginning of the file;
        ///     otherwise, <see langword="false" />.
        /// </param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="newLine">The new line character(s) for the CSV stream. Defaults to CRLF.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <returns>The new instance of <see cref="CsvStreamReader" />.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="newLine" /> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path" /> is an empty string ("") -or-
        ///     <paramref name="separator" /> is a double quote, return feed or a new line character -or-
        ///     <paramref name="newLine" /> is an empty string, contains a double quotation mark (") or
        ///     is longer than 2 characters.
        /// </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified path is invalid, such as being on an unmapped drive.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="path" /> includes an incorrect or invalid syntax for file name,
        ///     directory name, or volume label.
        /// </exception>
        public static CsvStreamReader Create(string path, Encoding encoding = null,
            bool detectEncodingFromByteOrderMarks = true,
            char separator = DefaultSeparator, string newLine = DefaultNewLine,
            IFormatProvider formatProvider = null)
        {
            bool result = TryCreate(path, encoding, detectEncodingFromByteOrderMarks, separator, newLine,
                formatProvider, out Exception ex, out CsvStreamReader cvsStream);
            if (result)
                return cvsStream;

            if (ex == null)
            {
                //Shouldn't happen
                ex = new Exception("An unknown error has occurred");
            }

            throw ex;
        }

        /// <summary>
        ///     Creates a new <see cref="CsvStreamReader" /> instance using the given file path.
        /// </summary>
        /// <param name="path">The file path to read from.</param>
        /// <param name="ex">Out: The <see cref="Exception" />, if any occurs.</param>
        /// <param name="encoding">
        ///     The encoding to use.
        ///     A null value will result in the default encoding being used. Defaults to null.
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        ///     <see langword="true" /> to look for byte order marks at the beginning of the file;
        ///     otherwise, <see langword="false" />.
        /// </param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="newLine">The new line character(s) for the CSV stream. Defaults to CRLF.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <returns>The new instance of <see cref="CsvStreamReader" /> or null on error.</returns>
        public static CsvStreamReader Create(string path, out Exception ex, Encoding encoding = null,
            bool detectEncodingFromByteOrderMarks = true, char separator = DefaultSeparator,
            string newLine = DefaultNewLine, IFormatProvider formatProvider = null)
        {
            bool result = TryCreate(path, encoding, detectEncodingFromByteOrderMarks, separator, newLine,
                formatProvider, out ex, out CsvStreamReader cvsStream);

            return result ? cvsStream : null;
        }

        /// <summary>
        ///     Tries to create a new <see cref="CsvStreamReader" /> instance using the given file path.
        /// </summary>
        /// <param name="path">The file path to read from.</param>
        /// <param name="ex">Out: The <see cref="Exception" />, if any occurs.</param>
        /// <param name="csvStream">Out: The new instance.</param>
        /// <param name="encoding">
        ///     The encoding to use.
        ///     A null value will result in the default encoding being used. Defaults to null.
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        ///     <see langword="true" /> to look for byte order marks at the beginning of the file;
        ///     otherwise, <see langword="false" />.
        /// </param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="newLine">The new line character(s) for the CSV stream. Defaults to CRLF.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <returns>True if the instance of <see cref="CsvStreamReader" /> was created, otherwise, false.</returns>
        public static bool TryCreate(string path, out Exception ex, out CsvStreamReader csvStream,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true,
            char separator = DefaultSeparator, string newLine = DefaultNewLine, IFormatProvider formatProvider = null)
        {
            return TryCreate(path, encoding, detectEncodingFromByteOrderMarks, separator, newLine, formatProvider,
                out ex, out csvStream);
        }

        /// <summary>
        ///     Tries to create a new <see cref="CsvStreamReader" /> instance using the given file path.
        /// </summary>
        /// <param name="path">The file path to read from.</param>
        /// <param name="encoding">
        ///     The encoding to use.
        ///     A null value will result in the default encoding being used.
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        ///     <see langword="true" /> to look for byte order marks at the beginning of the file;
        ///     otherwise, <see langword="false" />.
        /// </param>
        /// <param name="separator">The separator for the CSV stream.</param>
        /// <param name="newLine">The new line character(s) for the CSV stream.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        /// </param>
        /// <param name="ex">Out: The <see cref="Exception" />, if any occurs.</param>
        /// <param name="csvStream">Out: The new instance.</param>
        /// <returns>True if the instance of <see cref="CsvStreamReader" /> was created, otherwise, false.</returns>
        public static bool TryCreate(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks,
            char separator, string newLine, IFormatProvider formatProvider,
            out Exception ex, out CsvStreamReader csvStream)
        {
            ex = null;
            csvStream = null;

            try
            {
                csvStream = new CsvStreamReader(path, encoding, detectEncodingFromByteOrderMarks, separator, newLine,
                    formatProvider);
                return true;
            }
            catch (Exception e)
            {
                ex = e;
            }

            return false;
        }

        /// <summary>
        ///     Resets the reader's properties as a new/next row.
        /// </summary>
        protected virtual void ResetAsNextRow()
        {
            //Empty rows don't count
            if (FieldPosition == 0)
                return;

            if (FieldPosition > MaxFieldCount)
            {
                MaxFieldCount = FieldPosition;
            }
            
            FieldPosition = 0;
            RowsRead++;
        }

        /// <summary>
        ///     Checks for the end of a row in the stream.
        /// </summary>
        /// <param name="firstCharacter">
        ///     The character to check. The character MUST be removed/read from the stream already.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the given character, and next character if new line sequence is
        ///     made up of two characters, matches the new line sequence, otherwise, <see langword="false" />.
        ///     Will also return <see langword="true" /> if the first character,
        ///     of a two character new line sequence, matches and EOF is reached.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        ///     The <see cref="CsvStreamReader" /> or underlying stream is disposed.
        /// </exception>
        protected virtual bool IsEndRow(char? firstCharacter)
        {
            if (firstCharacter == null)
                return false;

            string newLineSequence = NewLine;
            if (firstCharacter.Value != newLineSequence[0])
                return false;

            if (newLineSequence.Length <= 1)
                return true;

            char? secondChar = Peek();

            //Check if at EOF, if so, then consider it an end of row
            if (secondChar == null)
                return true;

            return secondChar.Value == newLineSequence[1];
        }

        /// <summary>
        ///     Checks for the end of a row in the stream and processes it.
        /// </summary>
        /// <param name="firstCharacter">
        ///     The first character to check; MUST be removed/read from the stream already.
        ///     If null, will read the first character from the stream instead.
        /// </param>
        /// <exception cref="ObjectDisposedException">
        ///     The <see cref="CsvStreamReader" /> or underlying stream is disposed.
        /// </exception>
        protected virtual void CheckForEndRow(char? firstCharacter = null)
        {
            char? nextChar = firstCharacter;
            if (firstCharacter == null)
            {
                nextChar = Read();
            }

            if (nextChar == null)
                return;

            if (!IsEndRow(nextChar))
            {
                if (firstCharacter == null)
                {
                    //Ensure the character goes back on the stream/buffer
                    _previousCharBuffer = nextChar;
                }

                return;
            }

            if (_newLine.Length == 2)
            {
                Read(); //Remove the next character from stream
            }

            ResetAsNextRow();
        }

        /// <summary>
        ///     Reads in the next block of characters into the buffer.
        /// </summary>
        /// <returns>The number of characters read into the buffer.</returns>
        /// <exception cref="ObjectDisposedException">
        ///     The <see cref="CsvStreamReader" /> or underlying stream is disposed.
        /// </exception>
        protected virtual int ReadBuffer()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(CsvStreamReader));

            _bufferPosition = 0;
            _bufferLength = 0;

            int length = _stream.Read(_buffer, 0, _buffer.Length);

            _bufferLength = length;
            return length;
        }

        /// <summary>
        ///     Peeks at the next character from the stream.
        /// </summary>
        /// <returns>The next character or null if there are no more characters in the stream.</returns>
        /// <exception cref="ObjectDisposedException">
        ///     The <see cref="CsvStreamReader" /> or underlying stream is disposed.
        /// </exception>
        protected virtual char? Peek()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(CsvStreamReader));

            if (_previousCharBuffer.HasValue)
                return _previousCharBuffer;

            if (_bufferPosition != _bufferLength)
                return _buffer[_bufferPosition];

            if (ReadBuffer() == 0)
                return null; //Reached EOF

            return _buffer[_bufferPosition];
        }

        /// <summary>
        ///     Reads the next character from the stream.
        /// </summary>
        /// <returns>The next character or null if there are no more characters in the stream.</returns>
        /// <exception cref="ObjectDisposedException">
        ///     The <see cref="CsvStreamReader" /> or underlying stream is disposed.
        /// </exception>
        protected virtual char? Read()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(CsvStreamReader));

            if (_previousCharBuffer.HasValue)
            {
                char previousChar = _previousCharBuffer.Value;
                _previousCharBuffer = null;
                return previousChar;
            }

            if (_bufferPosition == _bufferLength)
            {
                if (ReadBuffer() == 0)
                    return null; //Reached EOF
            }

            char nextChar = _buffer[_bufferPosition];
            _bufferPosition++;
            return nextChar;
        }

        /// <summary>
        ///     Reads the next field from the stream.
        /// </summary>
        /// <returns>
        ///     The next field, as a string, or null if there are no more fields in the stream.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        ///     The <see cref="CsvStreamReader" /> or underlying stream is disposed.
        /// </exception>
        public virtual string ReadField()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(CsvStreamReader));
            
            //Check if we are at the end of a row
            CheckForEndRow();

            bool escaped = false;
            char? nextCharBuffer = Read();

            //Check for EOF
            if (nextCharBuffer == null)
                return null;

            char separator = Separator;

            //Check if an empty field
            if (nextCharBuffer.Value == separator)
            {
                FieldPosition++;
                return string.Empty;
            }

            var sb = new StringBuilder();

            //Get the next field from the stream
            while (nextCharBuffer != null)
            {
                char nextChar = nextCharBuffer.Value;

                //Are we within an escape sequence
                if (escaped)
                {
                    if (nextChar == DoubleQuoteChar)
                    {
                        char? peekedChar = Peek();
                        if (peekedChar == null)
                            break;
                        if (peekedChar.Value == DoubleQuoteChar)
                        {
                            //Escaped quote
                            sb.Append(nextChar);
                            Read();

                            nextCharBuffer = Read();
                            continue;
                        }

                        escaped = false;

                        nextCharBuffer = Read();
                        continue;
                    }

                    //Just append whatever character it is
                    sb.Append(nextChar);

                    nextCharBuffer = Read();
                    continue;
                }

                //Does the next char match the start of an escape sequence
                if (nextChar == DoubleQuoteChar)
                {
                    escaped = true;

                    nextCharBuffer = Read();
                    continue;
                }

                //Does the next char match the start of a newline character
                if (IsEndRow(nextChar))
                {
                    //Ensure the character goes back on the stream/buffer
                    _previousCharBuffer = nextChar;
                    break;
                }

                //Does the next char match the separator, i.e. the end of the field
                if (nextChar == separator)
                {
                    //No need to add character back on the stream/buffer
                    break;
                }

                //Normal character, append it
                sb.Append(nextChar);

                nextCharBuffer = Read();
            }

            FieldPosition++;
            FieldsRead++;
            
            //Check if we are at the end of a row
            CheckForEndRow();
            return sb.ToString();
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting
        ///     unmanaged resources.
        /// </summary>
        /// <param name="disposing">Is the instance being disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
            }

            _stream.Dispose();

            _isDisposed = true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ncl.Common.Csv
{
    /// <summary>
    ///     A CSV stream writer.
    ///     Handles escaping fields.
    /// </summary>
    public class CsvStreamWriter : IDisposable
    {
        //CSV Standard: https://datatracker.ietf.org/doc/html/rfc4180

        /// <summary>
        ///     The new line string based on the CSV standard.
        /// </summary>
        public const string NewLine = "\r\n";

        private bool _isDisposed;
        private IFormatProvider _formatProvider;
        protected readonly TextWriter _stream;
        protected readonly bool _leaveOpen;
        protected int _fieldPosition;
        protected int _rowsWritten;
        protected char separator = ',';

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
        ///     Gets/Sets the separator character.
        /// </summary>
        /// <exception cref="ArgumentException">value is equal to a quotation mark ("), return feed (\r) or newline character (\n).</exception>
        public char Separator
        {
            get => separator;
            set
            {
                if (value == '"' || value == '\r' || value == '\n')
                {
                    throw new ArgumentException(
                        "Separator value can not be a quotation mark (\"), return feed (\\r) or newline character (\\n)");
                }

                separator = value;
            }
        }

        /// <summary>
        ///     Gets the field position in the current row.
        /// </summary>
        public int FieldPosition { get => _fieldPosition; protected set => _fieldPosition = value; }

        /// <summary>
        ///     Gets the rows written for the current stream.
        /// </summary>
        public int RowsWritten { get => _rowsWritten; protected set => _rowsWritten = value; }

        /// <summary>
        ///     Initializes a new instance of <see cref="CsvStreamWriter"/>.
        /// </summary>
        /// <param name="stream">The underlying stream to use.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is null.</exception>
        public CsvStreamWriter(TextWriter stream) : this(stream, false)
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="CsvStreamWriter"/>.
        /// </summary>
        /// <param name="stream">The underlying stream to use.</param>
        /// <param name="leaveOpen">
        ///     Should the given stream be left open when this instance is disposed/closed.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is null.</exception>
        public CsvStreamWriter(TextWriter stream, bool leaveOpen)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _leaveOpen = leaveOpen;
            _formatProvider = Thread.CurrentThread.CurrentCulture;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="CsvStreamWriter"/>.
        /// </summary>
        /// <param name="path">
        ///     The file path to write to.
        ///     Will create the file if it does not exist.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
        public CsvStreamWriter(string path) : this(path, false)
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="CsvStreamWriter"/>.
        /// </summary>
        /// <param name="path">
        ///     The file path to write to.
        ///     Will create the file if it does not exist.
        /// </param>
        /// <param name="append">Should the stream append content to the file.</param>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
        public CsvStreamWriter(string path, bool append)
        {
            _stream = new StreamWriter(path, append);
            _formatProvider = Thread.CurrentThread.CurrentCulture;
        }

        /// <summary>
        ///     Creates a new <see cref="CsvStreamWriter"/> instance using the given stream.
        /// </summary>
        /// <param name="stream">The stream to use.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is null.</exception>
        public static CsvStreamWriter Create(TextWriter stream)
        {
            var result = TryCreate(stream, out Exception ex, out CsvStreamWriter cvsStream);
            if (result)
                return cvsStream;

            System.Diagnostics.Debug.Assert(ex != null, "Had a failed result but no exception");
            if (ex == null)
            {
                //Shouldn't happen
                ex = new Exception("An unknown error has occurred");
            }

            throw ex;
        }

        /// <summary>
        ///     Creates a new <see cref="CsvStreamWriter"/> instance using the given stream.
        /// </summary>
        /// <param name="stream">The stream to use.</param>
        /// <param name="ex">Out: The <see cref="Exception"/>, if any occurs.</param>
        /// <returns>The new instance or null on error.</returns>
        public static CsvStreamWriter Create(TextWriter stream, out Exception ex)
        {
            var result = TryCreate(stream, out ex, out CsvStreamWriter cvsStream);

            if (result)
                return cvsStream;

            return null;
        }

        /// <summary>
        ///     Tries to create a new <see cref="CsvStreamWriter"/> instance using the given stream.
        /// </summary>
        /// <param name="stream">The stream to use.</param>
        /// <param name="ex">Out: The <see cref="Exception"/>, if any occurs.</param>
        /// <param name="cvsStream">Out: The new instance.</param>
        /// <returns>True if a new instance was created, otherwise, false.</returns>
        public static bool TryCreate(TextWriter stream, out Exception ex, out CsvStreamWriter cvsStream)
        {
            ex = null;
            cvsStream = null;

            try
            {
                cvsStream = new CsvStreamWriter(stream);
                return true;
            }
            catch (Exception e)
            {
                ex = e;
            }

            return false;
        }

        public static CsvStreamWriter Create(string path)
        {
            var result = TryCreate(path, out Exception ex, out CsvStreamWriter cvsStream);
            if (result)
                return cvsStream;

            if (ex != null)
                throw ex;

            return null;
        }

        public static CsvStreamWriter Create(string path, out Exception ex)
        {
            var result = TryCreate(path, out ex, out CsvStreamWriter cvsStream);
            if (result)
                return cvsStream;
            return null;
        }

        public static bool TryCreate(string path, out Exception ex, out CsvStreamWriter cvsStream)
        {
            ex = null;
            cvsStream = null;

            try
            {
                cvsStream = new CsvStreamWriter(path);
                return true;
            }
            catch (Exception e)
            {
                ex = e;
            }

            return false;
        }

        public static CsvStreamWriter Create(string path, bool append)
        {
            var result = TryCreate(path, append, out Exception ex, out CsvStreamWriter cvsStream);
            if (result)
                return cvsStream;

            if (ex != null)
                throw ex;

            return null;
        }

        public static CsvStreamWriter Create(string path, bool append, out Exception ex)
        {
            var result = TryCreate(path, append, out ex, out CsvStreamWriter cvsStream);
            if (result)
                return cvsStream;
            return null;
        }

        public static bool TryCreate(string path, bool append, out Exception ex, out CsvStreamWriter cvsStream)
        {
            ex = null;
            cvsStream = null;

            try
            {
                cvsStream = new CsvStreamWriter(path, append);
                return true;
            }
            catch (Exception e)
            {
                ex = e;
            }

            return false;
        }

        /// <summary>
        ///     Escapes a field value, if necessary.
        /// </summary>
        /// <param name="value">The value being escaped, if necessary.</param>
        /// <returns>The escaped field value, or the original value.</returns>
        public virtual string EscapeField(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            bool needsEscaping = false;
            foreach (char character in value)
            {
                if (character != '\n' && character != '\r' && character != '"' && character != Separator)
                    continue;

                needsEscaping = true;
                break;
            }

            if (needsEscaping == false)
                return value;

            var sb = new StringBuilder(value.Length + 2);

            //Escape character
            sb.Append('"');

            //Field's content
            sb.Append(value);

            //Escape character
            sb.Append('"');

            return sb.ToString();
        }

        /// <summary>
        ///     Escapes a field value, if necessary.
        /// </summary>
        /// <param name="buffer">The buffer being escaped, if necessary.</param>
        /// <returns>The escaped field value, or the original value.</returns>
        public virtual string EscapeField(StringBuilder buffer)
        {
            if (buffer == null)
                return null;

            if (buffer.Length == 0)
                return string.Empty;

            bool needsEscaping = false;
            for (int i = 0; i < buffer.Length; i++)
            {
                char character = buffer[i];
                if (character != '\n' && character != '\r' && character != '"' && character != Separator)
                    continue;

                needsEscaping = true;
                break;
            }

            if (needsEscaping == false)
                return buffer.ToString();

            //Escape character
            buffer.Insert(0, '"');

            //Escape character
            buffer.Append('"');

            return buffer.ToString();
        }

        /// <summary>
        ///     Writes a <see cref="string"/> to the stream as a header entry.
        ///     The <paramref name="header"/> will be escaped, if necessary.
        ///     If <paramref name="header"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="header">The header to write.</param>
        /// <returns>The <see cref="CsvStreamWriter"/> instance.</returns>
        public CsvStreamWriter WriteHeader(string header)
        {
            //TODO: Do any special header handling/tracking
            return WriteUnescapedField(header);
        }

        /// <summary>
        ///     Writes a <see cref="string"/> to the stream as a header entry asynchronously.
        ///     The <paramref name="header"/> will be escaped, if necessary.
        ///     If <paramref name="header"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="header">The header to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter"/> result, that represents 
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteHeaderAsync(string header)
        {
            //TODO: Do any special header handling/tracking
            return WriteUnescapedFieldAsync(header);
        }

        /// <summary>
        ///     Writes a <see cref="IEnumerable{T}"/> to the stream as the header entries
        ///     then moves to the start of the next row.
        ///     The headers will be escaped, if necessary.
        ///     If <paramref name="headers"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="headers">The header row to write.</param>
        /// <returns>The <see cref="CsvStreamWriter"/> instance.</returns>
        public CsvStreamWriter WriteHeaderRow(IEnumerable<string> headers)
        {
            if (headers == null)
                return this;

            //TODO: Do any special header handling/tracking
            foreach (var header in headers)
            {
                WriteUnescapedField(header);
            }

            WriteRowEnd();
            return this;
        }

        /// <summary>
        ///     Writes a <see cref="IEnumerable{T}"/> to the stream as the header entries
        ///     then moves to the start of the next row; asynchronously
        ///     The headers will be escaped, if necessary.
        ///     If <paramref name="headers"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="headers">The header row to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter"/> result, that represents 
        ///     the asynchronous write operation.
        /// </returns>
        public async Task<CsvStreamWriter> WriteHeaderRowAsync(IEnumerable<string> headers)
        {
            if (headers == null)
                return this;

            //TODO: Do any special header handling/tracking
            foreach (var header in headers)
            {
                await WriteUnescapedFieldAsync(header).ConfigureAwait(false);
            }

            await WriteRowEndAsync().ConfigureAwait(false);
            return this;
        }

        /// <summary>
        ///     Writes the row terminating characters to the stream.
        /// </summary>
        /// <returns>The <see cref="CsvStreamWriter"/> instance.</returns>
        public CsvStreamWriter WriteRowEnd()
        {
            _stream.Write(NewLine);
            _fieldPosition = 0;
            _rowsWritten++;
            return this;
        }

        /// <summary>
        ///     Writes the row terminating characters to the stream asynchronously.
        /// </summary>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter"/> result, that represents 
        ///     the asynchronous write operation.
        /// </returns>
        public async Task<CsvStreamWriter> WriteRowEndAsync()
        {
            await _stream.WriteAsync(NewLine).ConfigureAwait(false);
            _fieldPosition = 0;
            _rowsWritten++;
            return this;
        }

        /// <summary>
        ///     Writes a <see cref="string"/> to the stream as a field entry.
        ///     The <paramref name="value"/> will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter"/> instance.</returns>
        public CsvStreamWriter WriteField(string value)
        {
            return WriteUnescapedField(value);
        }

        /// <summary>
        ///     Writes a <see cref="string"/> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter"/> result, that represents 
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(string value)
        {
            return WriteUnescapedFieldAsync(value);
        }

        /// <summary>
        ///     Writes a <see cref="string"/> to the stream as a field entry.
        ///     The resulting string will be escaped, if necessary.
        ///     If <paramref name="format"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="format">The format to write.</param>
        /// <param name="args">The format arguments.</param>
        /// <returns>The <see cref="CsvStreamWriter"/> instance.</returns>
        public CsvStreamWriter WriteField(string format, params object[] args)
        {
            if (format == null)
                return this;

            string valueStr = string.Format(FormatProvider, format, args);
            return WriteUnescapedField(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="string"/> to the stream as a field entry asynchronously.
        ///     The resulting string will be escaped, if necessary.
        ///     If <paramref name="format"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="format">The format to write.</param>
        /// <param name="args">The format arguments.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter"/> result, that represents 
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(string format, params object[] args)
        {
            if (format == null)
                return Task.FromResult(this);

            string valueStr = string.Format(FormatProvider, format, args);
            return WriteUnescapedFieldAsync(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="object"/> to the stream as a field entry.
        ///     The <paramref name="value"/> will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter"/> instance.</returns>
        public CsvStreamWriter WriteField(object value)
        {
            if (value == null)
                return this;

            if (value is IFormattable formattable)
                return WriteUnescapedField(formattable.ToString(null, FormatProvider));

            return WriteUnescapedField(value.ToString());
        }

        /// <summary>
        ///     Writes a <see cref="object"/> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter"/> result, that represents 
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(object value)
        {
            if (value == null)
                return Task.FromResult(this);

            if (value is IFormattable formattable)
                return WriteUnescapedFieldAsync(formattable.ToString(null, FormatProvider));

            return WriteUnescapedFieldAsync(value.ToString());
        }

        /// <summary>
        ///     Writes a <see cref="char"/>[] to the stream as a field entry.
        ///     The <paramref name="buffer"/> will be escaped, if necessary.
        ///     If <paramref name="buffer"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="buffer">The buffer to write.</param>
        /// <param name="index">The index to start at.</param>
        /// <param name="count">The number of entries to get.</param>
        /// <returns>The <see cref="CsvStreamWriter"/> instance.</returns>
        public CsvStreamWriter WriteField(char[] buffer, int index, int count)
        {
            //Check index and count ranges
            if (buffer == null)
                return this;

            if (index < 0 || index > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (count < 0 || index + count > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(count));

            var sb = new StringBuilder(buffer.Length + 2);

            sb.Append(buffer, index, count);

            return WriteUnescapedField(sb);
        }

        /// <summary>
        ///     Writes a subset of a <see cref="char"/>[] to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="buffer"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="buffer">The buffer to write.</param>
        /// <param name="index">The index to start at.</param>
        /// <param name="count">The number of entries to get.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter"/> result, that represents 
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(char[] buffer, int index, int count)
        {
            //Check index and count ranges
            if (buffer == null)
                return Task.FromResult(this);

            if (index < 0 || index > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (count < 0 || index + count > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(count));

            var sb = new StringBuilder(buffer.Length + 2);

            sb.Append(buffer, index, count);

            return WriteUnescapedFieldAsync(sb);
        }

        /// <summary>
        ///     Writes a <see cref="char"/>[] to the stream as a field entry.
        ///     The <paramref name="buffer"/> will be escaped, if necessary.
        ///     If <paramref name="buffer"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="buffer">The buffer to write.</param>
        /// <returns>The <see cref="CsvStreamWriter"/> instance.</returns>
        public CsvStreamWriter WriteField(char[] buffer)
        {
            //Check index and count ranges
            if (buffer == null)
                return this;

            if (buffer.Length == 0)
                return WriteUnescapedField(string.Empty);

            var sb = new StringBuilder(buffer.Length + 2);

            sb.Append(buffer);

            return WriteUnescapedField(sb);
        }

        /// <summary>
        ///     Writes a <see cref="char"/>[] to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="buffer"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="buffer">The buffer to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter"/> result, that represents 
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(char[] buffer)
        {
            //Check index and count ranges
            if (buffer == null)
                return Task.FromResult(this);

            if (buffer.Length == 0)
                return WriteUnescapedFieldAsync(string.Empty);

            var sb = new StringBuilder(buffer.Length + 2);
            sb.Append(buffer);

            return WriteUnescapedFieldAsync(sb);
        }

        /// <summary>
        ///     Writes a <see cref="char"/> to the stream as a field entry.
        ///     The <paramref name="value"/> will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter"/> instance.</returns>
        public CsvStreamWriter WriteField(char value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedField(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="char"/> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter"/> result, that represents 
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(char value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedFieldAsync(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="float"/> to the stream as a field entry.
        ///     The <paramref name="value"/> will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter"/> instance.</returns>
        public CsvStreamWriter WriteField(float value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedField(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="float"/> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter"/> result, that represents 
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(float value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedFieldAsync(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="bool"/> to the stream as a field entry.
        ///     The <paramref name="value"/> will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter"/> instance.</returns>
        public CsvStreamWriter WriteField(bool value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedField(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="bool"/> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter"/> result, that represents 
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(bool value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedFieldAsync(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="ulong"/> to the stream as a field entry.
        ///     The <paramref name="value"/> will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter"/> instance.</returns>
        public CsvStreamWriter WriteField(ulong value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedField(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="ulong"/> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter"/> result, that represents 
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(ulong value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedFieldAsync(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="uint"/> to the stream as a field entry.
        ///     The <paramref name="value"/> will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter"/> instance.</returns>
        public CsvStreamWriter WriteField(uint value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedField(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="uint"/> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter"/> result, that represents 
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(uint value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedFieldAsync(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="long"/> to the stream as a field entry.
        ///     The <paramref name="value"/> will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter"/> instance.</returns>
        public CsvStreamWriter WriteField(long value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedField(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="long"/> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter"/> result, that represents 
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(long value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedFieldAsync(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="int"/> to the stream as a field entry.
        ///     The <paramref name="value"/> will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter"/> instance.</returns>
        public CsvStreamWriter WriteField(int value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedField(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="int"/> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter"/> result, that represents 
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(int value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedFieldAsync(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="double"/> to the stream as a field entry.
        ///     The <paramref name="value"/> will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter"/> instance.</returns>
        public CsvStreamWriter WriteField(double value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedField(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="double"/> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter"/> result, that represents 
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(double value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedFieldAsync(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="decimal"/> to the stream as a field entry.
        ///     The <paramref name="value"/> will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter"/> instance.</returns>
        public CsvStreamWriter WriteField(decimal value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedField(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="decimal"/> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value"/> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter"/> result, that represents 
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(decimal value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedFieldAsync(valueStr);
        }

        /// <summary>
        ///     Writes a string to the text string or stream.
        /// </summary>
        /// <param name="value">The string to write.</param>
        /// <returns>This instance.</returns>
        protected virtual CsvStreamWriter WriteUnescapedField(string value)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(CsvStreamWriter));

            string escapedText = EscapeField(value);

            if (escapedText == null)
                return this;

            _stream.Write(escapedText);
            _fieldPosition++;
            return this;
        }

        /// <summary>
        ///     Writes a string to the stream asynchronously.
        /// </summary>
        /// <param name="value">
        ///     The string to write. If value is null, nothing is written to the text stream.
        /// </param>
        /// <returns>
        ///     A task with this instance as the result and 
        ///     that represents the asynchronous write operation.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        ///     The <see cref="CsvStreamWriter"/> or underlying stream is disposed.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The <see cref="CsvStreamWriter"/> is currently in use by a previous write operation.
        /// </exception>
        protected virtual async Task<CsvStreamWriter> WriteUnescapedFieldAsync(string value)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(CsvStreamWriter));

            string escapedText = EscapeField(value);

            if (escapedText == null)
                return this;

            await _stream.WriteAsync(escapedText).ConfigureAwait(false);
            _fieldPosition++;
            return this;
        }

        /// <summary>
        ///     Writes a <see cref="StringBuilder"/> to the text string or stream.
        /// </summary>
        /// <param name="buffer">
        ///     The buffer to write. If buffer is null, nothing is written to the text stream.
        /// </param>
        /// <returns>This instance.</returns>
        protected virtual CsvStreamWriter WriteUnescapedField(StringBuilder buffer)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(CsvStreamWriter));

            string escapedText = EscapeField(buffer);

            if (escapedText == null)
                return this;

            if (_fieldPosition != 0)
            {
                _stream.Write(Separator);
            }
            _stream.Write(escapedText);

            _fieldPosition++;
            return this;
        }

        /// <summary>
        ///     Writes a <see cref="StringBuilder"/> to the stream asynchronously.
        /// </summary>
        /// <param name="buffer">
        ///     The buffer to write. If buffer is null, nothing is written to the text stream.
        /// </param>
        /// <returns>
        ///     A task with this instance as the result and 
        ///     that represents the asynchronous write operation.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        ///     The <see cref="CsvStreamWriter"/> or underlying stream is disposed.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The <see cref="CsvStreamWriter"/> is currently in use by a previous write operation.
        /// </exception>
        protected virtual async Task<CsvStreamWriter> WriteUnescapedFieldAsync(StringBuilder buffer)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(CsvStreamWriter));

            string escapedText = EscapeField(buffer);

            if (escapedText == null)
                return this;

            if (_fieldPosition != 0)
            {
                await _stream.WriteAsync(Separator).ConfigureAwait(false);
            }
            await _stream.WriteAsync(escapedText).ConfigureAwait(false);

            _fieldPosition++;
            return this;
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
                // dispose managed state (managed objects)
            }

            // free unmanaged resources (unmanaged objects) and override finalizer
            // set large fields to null
            if (_leaveOpen == false)
            {
                _stream.Dispose();
            }

            _isDisposed = true;
        }

        // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~CsvStreamWriter()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

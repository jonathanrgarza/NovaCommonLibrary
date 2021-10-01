﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        /// <summary>
        ///     The default separator character, a comma.
        /// </summary>
        public const char DefaultSeparator = ',';

        /// <summary>
        ///     The exception message for attempting to write a header after the first row.
        /// </summary>
        protected const string HeaderRowWrittenMsg =
            "The first row has already been written. Can not write a header entry";

        /// <summary>
        ///     The exception message when given an invalid separator character.
        /// </summary>
        protected const string InvalidSeparatorCharacterMsg =
            "Separator value can not be a quotation mark (\"), return feed (\\r) or newline character (\\n)";

        /// <summary>
        ///     The double quote (") character.
        /// </summary>
        protected const char DoubleQuoteChar = '"';

        protected readonly bool _leaveOpen;
        protected readonly TextWriter _stream;
        protected List<string> _headers;
        protected char _separator;

        private IFormatProvider _formatProvider;
        private bool _isDisposed;

        /// <summary>
        ///     Initializes a new instance of <see cref="CsvStreamWriter" />.
        /// </summary>
        /// <param name="stream">The underlying stream to use.</param>
        /// <param name="leaveOpen">
        ///     Should the given stream be left open when this instance is disposed/closed. Defaults to false.
        /// </param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <param name="integrityMode">The integrity mode for this stream. Defaults to IntegrityMode.Strict.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream" /> is null.</exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="separator" /> is a double quote, return feed or a new line character.
        /// </exception>
        public CsvStreamWriter(TextWriter stream, bool leaveOpen = false, char separator = DefaultSeparator,
            IFormatProvider formatProvider = null, IntegrityMode integrityMode = IntegrityMode.Strict)
        {
            if (separator == DoubleQuoteChar || separator == '\r' || separator == '\n')
            {
                throw new ArgumentException(InvalidSeparatorCharacterMsg, nameof(separator));
            }

            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _leaveOpen = leaveOpen;
            _separator = separator;
            _formatProvider = formatProvider ?? Thread.CurrentThread.CurrentCulture;
            IntegrityMode = integrityMode;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="CsvStreamWriter" />.
        /// </summary>
        /// <param name="path">
        ///     The file path to write to.
        ///     Will create the file if it does not exist.
        /// </param>
        /// <param name="append">Should the stream append content to the file. Defaults to false.</param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <param name="integrityMode">The integrity mode for this stream. Defaults to IntegrityMode.Strict.</param>
        /// <exception cref="ArgumentNullException"><paramref name="path" /> is null.</exception>
        public CsvStreamWriter(string path, bool append = false, char separator = DefaultSeparator,
            IFormatProvider formatProvider = null, IntegrityMode integrityMode = IntegrityMode.Strict)
        {
            if (separator == DoubleQuoteChar || separator == '\r' || separator == '\n')
            {
                throw new ArgumentException(InvalidSeparatorCharacterMsg, nameof(separator));
            }

            _stream = new StreamWriter(path, append);
            _formatProvider = Thread.CurrentThread.CurrentCulture;
            _separator = separator;
            _formatProvider = formatProvider ?? Thread.CurrentThread.CurrentCulture;
            IntegrityMode = integrityMode;
        }

        /// <summary>
        ///     Gets the field position in the current row.
        /// </summary>
        public int FieldPosition { get; protected set; }

        /// <summary>
        ///     Gets if the first row has been written. Special as it can optionally be a header row.
        /// </summary>
        public bool FirstRowWritten => RowsWritten > 0;

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
        ///     Gets if the a header row has been written.
        /// </summary>
        public bool HeaderRowWritten => RowsWritten > 0 && (_headers?.Count ?? 0) > 0;

        /// <summary>
        ///     The headers written by this stream. Default value is null when no headers have been written.
        /// </summary>
        public IReadOnlyList<string> Headers => _headers?.AsReadOnly();

        /// <summary>
        ///     Gets/Sets the integrity mode.
        /// </summary>
        public IntegrityMode IntegrityMode { get; set; }

        /// <summary>
        ///     Gets the maximum field count written for the rows.
        ///     This property is only updated when a row is finished.
        /// </summary>
        public int MaxFieldCount { get; protected set; }

        /// <summary>
        ///     Gets the rows written for the current stream.
        /// </summary>
        public int RowsWritten { get; protected set; }

        /// <summary>
        ///     Gets/Sets the separator character.
        /// </summary>
        /// <exception cref="ArgumentException">value is equal to a quotation mark ("), return feed (\r) or newline character (\n).</exception>
        public char Separator
        {
            get => _separator;
            protected set
            {
                if (value == DoubleQuoteChar || value == '\r' || value == '\n')
                {
                    throw new ArgumentException(
                        "Separator value can not be a quotation mark (\"), return feed (\\r) or newline character (\\n)");
                }

                _separator = value;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Creates a new <see cref="CsvStreamWriter" /> instance using the given stream.
        /// </summary>
        /// <param name="stream">The stream to use.</param>
        /// <param name="leaveOpen">
        ///     Should the given stream be left open when this instance is disposed/closed. Defaults to false.
        /// </param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <param name="integrityMode">The integrity mode for this stream. Defaults to IntegrityMode.Strict.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream" /> is null.</exception>
        public static CsvStreamWriter Create(TextWriter stream, bool leaveOpen = false,
            char separator = DefaultSeparator,
            IFormatProvider formatProvider = null, IntegrityMode integrityMode = IntegrityMode.Strict)
        {
            bool result = TryCreate(stream, leaveOpen, separator, formatProvider, integrityMode, out Exception ex,
                out CsvStreamWriter cvsStream);
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
        ///     Creates a new <see cref="CsvStreamWriter" /> instance using the given stream.
        /// </summary>
        /// <param name="stream">The stream to use.</param>
        /// <param name="ex">Out: The <see cref="Exception" />, if any occurs.</param>
        /// <param name="leaveOpen">
        ///     Should the given stream be left open when this instance is disposed/closed. Defaults to false.
        /// </param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <param name="integrityMode">The integrity mode for this stream. Defaults to IntegrityMode.Strict.</param>
        /// <returns>The new instance or null on error.</returns>
        public static CsvStreamWriter Create(TextWriter stream, out Exception ex, bool leaveOpen = false,
            char separator = DefaultSeparator, IFormatProvider formatProvider = null,
            IntegrityMode integrityMode = IntegrityMode.Strict)
        {
            bool result = TryCreate(stream, leaveOpen, separator, formatProvider, integrityMode, out ex,
                out CsvStreamWriter cvsStream);

            return result ? cvsStream : null;
        }

        /// <summary>
        ///     Tries to create a new <see cref="CsvStreamWriter" /> instance using the given stream.
        /// </summary>
        /// <param name="stream">The stream to use.</param>
        /// <param name="ex">Out: The <see cref="Exception" />, if any occurs.</param>
        /// <param name="csvStream">Out: The new instance.</param>
        /// <param name="leaveOpen">Should the given stream be left open when this instance is disposed/closed.</param>
        /// <param name="separator">The separator for the CSV stream.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <param name="integrityMode">The integrity mode for this stream. Defaults to IntegrityMode.Strict.</param>
        /// <returns>The new instance of <see cref="CsvStreamWriter" /> or null on error.</returns>
        public static bool TryCreate(TextWriter stream, out Exception ex, out CsvStreamWriter csvStream,
            bool leaveOpen = false, char separator = DefaultSeparator, IFormatProvider formatProvider = null,
            IntegrityMode integrityMode = IntegrityMode.Strict)
        {
            return TryCreate(stream, leaveOpen, separator, formatProvider, integrityMode, out ex, out csvStream);
        }

        /// <summary>
        ///     Tries to create a new <see cref="CsvStreamWriter" /> instance using the given stream.
        /// </summary>
        /// <param name="stream">The stream to use.</param>
        /// <param name="leaveOpen">Should the given stream be left open when this instance is disposed/closed.</param>
        /// <param name="separator">The separator for the CSV stream.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     A null value will result in the Thread's current culture being used.
        /// </param>
        /// <param name="integrityMode">The integrity mode for this stream.</param>
        /// <param name="ex">Out: The <see cref="Exception" />, if any occurs.</param>
        /// <param name="csvStream">Out: The new instance.</param>
        /// <returns>The new instance of <see cref="CsvStreamWriter" /> or null on error.</returns>
        public static bool TryCreate(TextWriter stream, bool leaveOpen, char separator,
            IFormatProvider formatProvider, IntegrityMode integrityMode, out Exception ex,
            out CsvStreamWriter csvStream)
        {
            ex = null;
            csvStream = null;

            try
            {
                csvStream = new CsvStreamWriter(stream, leaveOpen, separator, formatProvider, integrityMode);
                return true;
            }
            catch (Exception e)
            {
                ex = e;
            }

            return false;
        }

        /// <summary>
        ///     Creates a new <see cref="CsvStreamWriter" /> instance using the given file path.
        /// </summary>
        /// <param name="path">
        ///     The file path to write to.
        ///     Will create the file if it does not exist.
        /// </param>
        /// <param name="append">Should the stream append content to the file. Defaults to false.</param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <param name="integrityMode">The integrity mode for this stream. Defaults to IntegrityMode.Strict.</param>
        /// <returns>The new instance of <see cref="CsvStreamWriter" />.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="path" /> is null.</exception>
        /// <exception cref="Exception">Thrown if an unexpected error occurs.</exception>
        public static CsvStreamWriter Create(string path, bool append = false,
            char separator = DefaultSeparator, IFormatProvider formatProvider = null,
            IntegrityMode integrityMode = IntegrityMode.Strict)
        {
            bool result = TryCreate(path, append, separator, formatProvider, integrityMode, out Exception ex,
                out CsvStreamWriter csvStream);

            if (result)
                return csvStream;

            if (ex != null)
                throw ex;

            throw new Exception("Unexpected error occurred while trying to create a new CsvStreamWriter");
        }

        /// <summary>
        ///     Creates a new <see cref="CsvStreamWriter" /> instance using the given file path.
        /// </summary>
        /// <param name="path">
        ///     The file path to write to.
        ///     Will create the file if it does not exist.
        /// </param>
        /// <param name="ex">Out: The <see cref="Exception" />, if any occurs.</param>
        /// <param name="append">Should the stream append content to the file. Defaults to false.</param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <param name="integrityMode">The integrity mode for this stream. Defaults to IntegrityMode.Strict.</param>
        /// <returns>The new instance of <see cref="CsvStreamWriter" /> or null on error.</returns>
        public static CsvStreamWriter Create(string path, out Exception ex, bool append = false,
            char separator = DefaultSeparator, IFormatProvider formatProvider = null,
            IntegrityMode integrityMode = IntegrityMode.Strict)
        {
            bool result = TryCreate(path, append, separator, formatProvider, integrityMode, out ex,
                out CsvStreamWriter csvStream);
            return result ? csvStream : null;
        }

        /// <summary>
        ///     Tries to create a new <see cref="CsvStreamWriter" /> instance using the given file path.
        /// </summary>
        /// <param name="path">
        ///     The file path to write to.
        ///     Will create the file if it does not exist.
        /// </param>
        /// <param name="ex">Out: The <see cref="Exception" />, if any occurs.</param>
        /// <param name="csvStream">Out: The new instance of <see cref="CsvStreamWriter" /> or null on error.</param>
        /// <param name="append">Should the stream append content to the file. Defaults to false.</param>
        /// <param name="separator">The separator for the CSV stream. Defaults to a comma character.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     Defaults to null which will result in the Thread's current culture being used.
        /// </param>
        /// <param name="integrityMode">The integrity mode for this stream. Defaults to IntegrityMode.Strict.</param>
        /// <returns>True if the instance of <see cref="CsvStreamWriter" /> was created, otherwise, false.</returns>
        public static bool TryCreate(string path, out Exception ex, out CsvStreamWriter csvStream, bool append = false,
            char separator = DefaultSeparator, IFormatProvider formatProvider = null,
            IntegrityMode integrityMode = IntegrityMode.Strict)
        {
            return TryCreate(path, append, separator, formatProvider, integrityMode, out ex, out csvStream);
        }

        /// <summary>
        ///     Tries to create a new <see cref="CsvStreamWriter" /> instance using the given file path.
        /// </summary>
        /// <param name="path">
        ///     The file path to write to.
        ///     Will create the file if it does not exist.
        /// </param>
        /// <param name="append">Should the stream append content to the file.</param>
        /// <param name="separator">The separator for the CSV stream.</param>
        /// <param name="formatProvider">
        ///     The format provider for numeric types.
        ///     A null value will result in the Thread's current culture being used.
        /// </param>
        /// <param name="integrityMode">The integrity mode for this stream.</param>
        /// <param name="ex">Out: The <see cref="Exception" />, if any occurs.</param>
        /// <param name="csvStream">Out: The new instance of <see cref="CsvStreamWriter" /> or null on error.</param>
        /// <returns>True if the instance of <see cref="CsvStreamWriter" /> was created, otherwise, false.</returns>
        public static bool TryCreate(string path, bool append, char separator,
            IFormatProvider formatProvider, IntegrityMode integrityMode, out Exception ex,
            out CsvStreamWriter csvStream)
        {
            ex = null;
            csvStream = null;

            try
            {
                csvStream = new CsvStreamWriter(path, append, separator, formatProvider, integrityMode);
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

            bool needsEscaping = value.Any(
                character => character == '\n' || character == '\r' || character == DoubleQuoteChar ||
                             character == Separator);

            if (needsEscaping == false)
                return value;

            var sb = new StringBuilder(value.Length + 4);

            //Escape character
            sb.Append(DoubleQuoteChar);

            //Field's content
            foreach (char character in value)
            {
                if (character == DoubleQuoteChar)
                {
                    sb.Append(DoubleQuoteChar);
                }

                sb.Append(character);
            }

            //Escape character
            sb.Append(DoubleQuoteChar);

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
                if (character != '\n' && character != '\r' && character != DoubleQuoteChar && character != Separator)
                    continue;

                needsEscaping = true;
                break;
            }

            if (needsEscaping == false)
                return buffer.ToString();

            //Replace all double quotes in buffer as two double quotes
            buffer.Replace("\"", "\"\"");

            //Escape character
            buffer.Insert(0, DoubleQuoteChar);

            //Escape character
            buffer.Append(DoubleQuoteChar);

            return buffer.ToString();
        }

        /// <summary>
        ///     Writes a <see cref="string" /> to the stream as a header entry.
        ///     The <paramref name="header" /> will be escaped, if necessary.
        ///     If <paramref name="header" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="header">The header to write.</param>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
        /// <exception cref="InvalidOperationException">
        ///     <see cref="FirstRowWritten" /> is true and thus no further headers can be
        ///     written.
        /// </exception>
        public CsvStreamWriter WriteHeader(string header)
        {
            if (header == null)
                return this;

            if (FirstRowWritten)
                throw new InvalidOperationException(HeaderRowWrittenMsg);

            if (_headers == null)
            {
                _headers = new List<string>();
            }

            _headers.Add(header);

            return WriteUnescapedEntry(header, true);
        }

        /// <summary>
        ///     Writes a <see cref="string" /> to the stream as a header entry asynchronously.
        ///     The <paramref name="header" /> will be escaped, if necessary.
        ///     If <paramref name="header" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="header">The header to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
        ///     the asynchronous write operation.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     <see cref="FirstRowWritten" /> is true and thus no further headers can be
        ///     written.
        /// </exception>
        public Task<CsvStreamWriter> WriteHeaderAsync(string header)
        {
            if (header == null)
                return Task.FromResult(this);

            if (FirstRowWritten)
                throw new InvalidOperationException(HeaderRowWrittenMsg);

            if (_headers == null)
            {
                _headers = new List<string>();
            }

            _headers.Add(header);

            return WriteUnescapedEntryAsync(header, true);
        }

        /// <summary>
        ///     Writes a <see cref="IEnumerable{T}" /> to the stream as the header entries
        ///     then moves to the start of the next row.
        ///     The headers will be escaped, if necessary.
        ///     If <paramref name="headers" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="headers">The header row to write.</param>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
        /// <exception cref="InvalidOperationException">
        ///     <see cref="FirstRowWritten" /> is true and thus no further headers can be
        ///     written.
        /// </exception>
        public CsvStreamWriter WriteHeaderRow(IEnumerable<string> headers)
        {
            if (headers == null)
                return this;

            if (FirstRowWritten)
                throw new InvalidOperationException(HeaderRowWrittenMsg);

            if (_headers == null)
            {
                _headers = new List<string>();
            }

            foreach (string header in headers)
            {
                if (header == null)
                    continue;

                _headers.Add(header);
                WriteUnescapedEntry(header, true);
            }

            return WriteRowEnd();
        }

        /// <summary>
        ///     Writes a <see cref="IEnumerable{T}" /> to the stream as the header entries
        ///     then moves to the start of the next row; asynchronously
        ///     The headers will be escaped, if necessary.
        ///     If <paramref name="headers" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="headers">The header row to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
        ///     the asynchronous write operation.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     <see cref="FirstRowWritten" /> is true and thus no further headers can be
        ///     written.
        /// </exception>
        public async Task<CsvStreamWriter> WriteHeaderRowAsync(IEnumerable<string> headers)
        {
            if (headers == null)
                return this;

            if (FirstRowWritten)
                throw new InvalidOperationException(HeaderRowWrittenMsg);

            if (_headers == null)
            {
                _headers = new List<string>();
            }

            foreach (string header in headers)
            {
                if (header == null)
                    continue;

                _headers.Add(header);
                await WriteUnescapedEntryAsync(header, true).ConfigureAwait(false);
            }

            return await WriteRowEndAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///     Writes headers to the stream as the header entries
        ///     then moves to the start of the next row.
        ///     The headers will be escaped, if necessary.
        ///     If <paramref name="header" /> and <paramref name="headers" /> is null or empty, nothing is written to the stream.
        /// </summary>
        /// <param name="header">The first header to write.</param>
        /// <param name="headers">The other headers to write.</param>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
        /// <exception cref="InvalidOperationException">
        ///     <see cref="FirstRowWritten" /> is true and thus no further headers can be
        ///     written.
        /// </exception>
        public CsvStreamWriter WriteHeaderRow(string header, params string[] headers)
        {
            if (header == null && headers == null)
                return this;

            if (FirstRowWritten)
                throw new InvalidOperationException(HeaderRowWrittenMsg);

            if (_headers == null)
            {
                _headers = new List<string>();
            }

            if (header != null)
            {
                _headers.Add(header);
                WriteUnescapedEntry(header, true);
            }

            if (headers == null || headers.Length == 0)
            {
                return WriteRowEnd();
            }

            foreach (string otherHeader in headers)
            {
                if (otherHeader == null)
                    continue;

                _headers.Add(otherHeader);
                WriteUnescapedEntry(otherHeader, true);
            }

            return WriteRowEnd();
        }

        /// <summary>
        ///     Writes headers to the stream as the header entries
        ///     then moves to the start of the next row.
        ///     The headers will be escaped, if necessary.
        ///     If <paramref name="header" /> and <paramref name="headers" /> is null or empty, nothing is written to the stream.
        /// </summary>
        /// <param name="header">The first header to write.</param>
        /// <param name="headers">The other headers to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
        ///     the asynchronous write operation.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     <see cref="FirstRowWritten" /> is true and thus no further headers can be
        ///     written.
        /// </exception>
        public async Task<CsvStreamWriter> WriteHeaderRowAsync(string header, params string[] headers)
        {
            if (header == null && headers == null)
                return this;

            if (FirstRowWritten)
                throw new InvalidOperationException(HeaderRowWrittenMsg);

            if (_headers == null)
            {
                _headers = new List<string>();
            }

            if (header != null)
            {
                _headers.Add(header);
                await WriteUnescapedEntryAsync(header, true);
            }

            if (headers == null || headers.Length == 0)
            {
                return await WriteRowEndAsync().ConfigureAwait(false);
            }

            foreach (string otherHeader in headers)
            {
                if (otherHeader == null)
                    continue;

                _headers.Add(otherHeader);
                await WriteUnescapedEntryAsync(otherHeader, true).ConfigureAwait(false);
            }

            return await WriteRowEndAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///     Writes the row terminating characters to the stream.
        /// </summary>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
        public CsvStreamWriter WriteRowEnd()
        {
            VerifyRowEndIntegrity(out string fieldsToAdd);

            if (fieldsToAdd != null)
            {
                _stream.Write(fieldsToAdd);
            }

            _stream.Write(NewLine);
            MaxFieldCount = FieldPosition;
            FieldPosition = 0;
            RowsWritten++;
            return this;
        }

        /// <summary>
        ///     Writes the row terminating characters to the stream asynchronously.
        /// </summary>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
        ///     the asynchronous write operation.
        /// </returns>
        public async Task<CsvStreamWriter> WriteRowEndAsync()
        {
            VerifyRowEndIntegrity(out string fieldsToAdd);

            if (fieldsToAdd != null)
            {
                await _stream.WriteAsync(fieldsToAdd).ConfigureAwait(false);
            }

            await _stream.WriteAsync(NewLine).ConfigureAwait(false);
            MaxFieldCount = FieldPosition;
            FieldPosition = 0;
            RowsWritten++;
            return this;
        }

        /// <summary>
        ///     Writes a <see cref="string" /> to the stream as a field entry.
        ///     The <paramref name="value" /> will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
        public CsvStreamWriter WriteField(string value)
        {
            return WriteUnescapedEntry(value);
        }

        /// <summary>
        ///     Writes a <see cref="string" /> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(string value)
        {
            return WriteUnescapedEntryAsync(value);
        }

        /// <summary>
        ///     Writes a <see cref="string" /> to the stream as a field entry.
        ///     The resulting string will be escaped, if necessary.
        ///     If <paramref name="format" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="format">The format to write.</param>
        /// <param name="args">The format arguments.</param>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
        public CsvStreamWriter WriteField(string format, params object[] args)
        {
            if (format == null)
                return this;

            string valueStr = string.Format(FormatProvider, format, args);
            return WriteUnescapedEntry(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="string" /> to the stream as a field entry asynchronously.
        ///     The resulting string will be escaped, if necessary.
        ///     If <paramref name="format" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="format">The format to write.</param>
        /// <param name="args">The format arguments.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(string format, params object[] args)
        {
            if (format == null)
                return Task.FromResult(this);

            string valueStr = string.Format(FormatProvider, format, args);
            return WriteUnescapedEntryAsync(valueStr);
        }

        /// <summary>
        ///     Writes an <see cref="object" /> to the stream as a field entry.
        ///     The <paramref name="value" /> will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
        public CsvStreamWriter WriteField(object value)
        {
            if (value == null)
                return this;

            if (value is IFormattable formattable)
                return WriteUnescapedEntry(formattable.ToString(null, FormatProvider));

            return WriteUnescapedEntry(value.ToString());
        }

        /// <summary>
        ///     Writes an <see cref="object" /> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(object value)
        {
            if (value == null)
                return Task.FromResult(this);

            if (value is IFormattable formattable)
                return WriteUnescapedEntryAsync(formattable.ToString(null, FormatProvider));

            return WriteUnescapedEntryAsync(value.ToString());
        }

        /// <summary>
        ///     Writes a <see cref="char" />[] to the stream as a field entry.
        ///     The <paramref name="buffer" /> will be escaped, if necessary.
        ///     If <paramref name="buffer" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="buffer">The buffer to write.</param>
        /// <param name="index">The index to start at.</param>
        /// <param name="count">The number of entries to get.</param>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
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

            return WriteUnescapedEntry(sb);
        }

        /// <summary>
        ///     Writes a subset of a <see cref="char" />[] to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="buffer" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="buffer">The buffer to write.</param>
        /// <param name="index">The index to start at.</param>
        /// <param name="count">The number of entries to get.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
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

            return WriteUnescapedEntryAsync(sb);
        }

        /// <summary>
        ///     Writes a <see cref="char" />[] to the stream as a field entry.
        ///     The <paramref name="buffer" /> will be escaped, if necessary.
        ///     If <paramref name="buffer" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="buffer">The buffer to write.</param>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
        public CsvStreamWriter WriteField(char[] buffer)
        {
            //Check index and count ranges
            if (buffer == null)
                return this;

            if (buffer.Length == 0)
                return WriteUnescapedEntry(string.Empty);

            var sb = new StringBuilder(buffer.Length + 2);

            sb.Append(buffer);

            return WriteUnescapedEntry(sb);
        }

        /// <summary>
        ///     Writes a <see cref="char" />[] to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="buffer" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="buffer">The buffer to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(char[] buffer)
        {
            //Check index and count ranges
            if (buffer == null)
                return Task.FromResult(this);

            if (buffer.Length == 0)
                return WriteUnescapedEntryAsync(string.Empty);

            var sb = new StringBuilder(buffer.Length + 2);
            sb.Append(buffer);

            return WriteUnescapedEntryAsync(sb);
        }

        /// <summary>
        ///     Writes a <see cref="char" /> to the stream as a field entry.
        ///     The <paramref name="value" /> will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
        public CsvStreamWriter WriteField(char value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntry(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="char" /> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(char value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntryAsync(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="float" /> to the stream as a field entry.
        ///     The <paramref name="value" /> will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
        public CsvStreamWriter WriteField(float value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntry(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="float" /> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(float value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntryAsync(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="bool" /> to the stream as a field entry.
        ///     The <paramref name="value" /> will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
        public CsvStreamWriter WriteField(bool value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntry(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="bool" /> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(bool value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntryAsync(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="ulong" /> to the stream as a field entry.
        ///     The <paramref name="value" /> will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
        public CsvStreamWriter WriteField(ulong value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntry(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="ulong" /> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(ulong value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntryAsync(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="uint" /> to the stream as a field entry.
        ///     The <paramref name="value" /> will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
        public CsvStreamWriter WriteField(uint value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntry(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="uint" /> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(uint value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntryAsync(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="long" /> to the stream as a field entry.
        ///     The <paramref name="value" /> will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
        public CsvStreamWriter WriteField(long value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntry(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="long" /> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(long value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntryAsync(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="int" /> to the stream as a field entry.
        ///     The <paramref name="value" /> will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
        public CsvStreamWriter WriteField(int value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntry(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="int" /> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(int value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntryAsync(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="double" /> to the stream as a field entry.
        ///     The <paramref name="value" /> will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
        public CsvStreamWriter WriteField(double value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntry(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="double" /> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(double value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntryAsync(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="decimal" /> to the stream as a field entry.
        ///     The <paramref name="value" /> will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The <see cref="CsvStreamWriter" /> instance.</returns>
        public CsvStreamWriter WriteField(decimal value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntry(valueStr);
        }

        /// <summary>
        ///     Writes a <see cref="decimal" /> to the stream as a field entry asynchronously.
        ///     The value will be escaped, if necessary.
        ///     If <paramref name="value" /> is null, nothing is written to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>
        ///     A task, with a <see cref="CsvStreamWriter" /> result, that represents
        ///     the asynchronous write operation.
        /// </returns>
        public Task<CsvStreamWriter> WriteFieldAsync(decimal value)
        {
            string valueStr = value.ToString(_formatProvider);
            return WriteUnescapedEntryAsync(valueStr);
        }

        /// <summary>
        ///     Writes a string to the text string or stream.
        /// </summary>
        /// <param name="value">The string to write.</param>
        /// <param name="isHeader">Is the value being written a header.</param>
        /// <returns>This instance.</returns>
        protected virtual CsvStreamWriter WriteUnescapedEntry(string value, bool isHeader = false)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(CsvStreamWriter));

            if (isHeader == false && FirstRowWritten == false && _headers?.Count > 0)
            {
                //Move to the next row since this is a field entry and the header row isn't complete
                WriteRowEnd();
            }

            string escapedText = EscapeField(value);

            if (escapedText == null)
                return this;

            if (FieldPosition != 0)
            {
                _stream.Write(Separator);
            }

            _stream.Write(escapedText);
            FieldPosition++;
            return this;
        }

        /// <summary>
        ///     Writes a string to the stream asynchronously.
        /// </summary>
        /// <param name="value">
        ///     The string to write. If value is null, nothing is written to the text stream.
        /// </param>
        /// <param name="isHeader">Is the value being written a header.</param>
        /// <returns>
        ///     A task with this instance as the result and
        ///     that represents the asynchronous write operation.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        ///     The <see cref="CsvStreamWriter" /> or underlying stream is disposed.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The <see cref="CsvStreamWriter" /> is currently in use by a previous write operation.
        /// </exception>
        protected virtual async Task<CsvStreamWriter> WriteUnescapedEntryAsync(string value, bool isHeader = false)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(CsvStreamWriter));

            if (isHeader == false && FirstRowWritten == false && _headers?.Count > 0)
            {
                //Move to the next row since this is a field entry and the header row isn't complete
                await WriteRowEndAsync().ConfigureAwait(false);
            }

            string escapedText = EscapeField(value);

            if (escapedText == null)
                return this;

            if (FieldPosition != 0)
            {
                await _stream.WriteAsync(Separator).ConfigureAwait(false);
            }

            await _stream.WriteAsync(escapedText).ConfigureAwait(false);
            FieldPosition++;
            return this;
        }

        /// <summary>
        ///     Writes a <see cref="StringBuilder" /> to the text string or stream.
        /// </summary>
        /// <param name="buffer">
        ///     The buffer to write. If buffer is null, nothing is written to the text stream.
        /// </param>
        /// <param name="isHeader">Is the value being written a header.</param>
        /// <returns>This instance.</returns>
        protected virtual CsvStreamWriter WriteUnescapedEntry(StringBuilder buffer, bool isHeader = false)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(CsvStreamWriter));

            if (isHeader == false && FirstRowWritten == false && _headers?.Count > 0)
            {
                //Move to the next row since this is a field entry and the header row isn't complete
                WriteRowEnd();
            }

            string escapedText = EscapeField(buffer);

            if (escapedText == null)
                return this;

            if (FieldPosition != 0)
            {
                _stream.Write(Separator);
            }

            _stream.Write(escapedText);

            FieldPosition++;
            return this;
        }

        /// <summary>
        ///     Writes a <see cref="StringBuilder" /> to the stream asynchronously.
        /// </summary>
        /// <param name="buffer">
        ///     The buffer to write. If buffer is null, nothing is written to the text stream.
        /// </param>
        /// <param name="isHeader">Is the value being written a header.</param>
        /// <returns>
        ///     A task with this instance as the result and
        ///     that represents the asynchronous write operation.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        ///     The <see cref="CsvStreamWriter" /> or underlying stream is disposed.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The <see cref="CsvStreamWriter" /> is currently in use by a previous write operation.
        /// </exception>
        protected virtual async Task<CsvStreamWriter> WriteUnescapedEntryAsync(StringBuilder buffer,
            bool isHeader = false)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(CsvStreamWriter));

            if (isHeader == false && FirstRowWritten == false && _headers?.Count > 0)
            {
                //Move to the next row since this is a field entry and the header row isn't complete
                await WriteRowEndAsync();
            }

            string escapedText = EscapeField(buffer);

            if (escapedText == null)
                return this;

            if (FieldPosition != 0)
            {
                await _stream.WriteAsync(Separator).ConfigureAwait(false);
            }

            await _stream.WriteAsync(escapedText).ConfigureAwait(false);

            FieldPosition++;
            return this;
        }

        protected virtual void VerifyRowEndIntegrity(out string fieldsToAdd)
        {
            fieldsToAdd = null;

            if (IntegrityMode == IntegrityMode.None)
                return;

            //Perform integrity checks

            //Check that something was written for the first row; else its a failure since the first row can't be empty
            if (RowsWritten == 0 && FieldPosition == 0)
                throw new IntegrityViolatedException("Can not write an empty first row");

            //Writing the first row; must be valid since this row determines the field length
            if (RowsWritten == 0)
                return;

            if (FieldPosition == MaxFieldCount)
                return;

            bool isFieldsLessThanPrevious = FieldPosition < MaxFieldCount;

            if (IntegrityMode == IntegrityMode.Strict)
            {
                //Throw exception if strict mode
                string exceptionMessage = isFieldsLessThanPrevious
                    ? $"Row's field count ({FieldPosition}) is less than the previous row's field count {MaxFieldCount}"
                    : $"Row's field count ({FieldPosition}) is greater than the previous row's field count {MaxFieldCount}";
                throw new IntegrityViolatedException(exceptionMessage);
            }

            if (IntegrityMode != IntegrityMode.Loose || !isFieldsLessThanPrevious)
                return;

            int difference = MaxFieldCount - FieldPosition;
            fieldsToAdd = new string(',', difference);
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
            Dispose(false);
        }
    }
}
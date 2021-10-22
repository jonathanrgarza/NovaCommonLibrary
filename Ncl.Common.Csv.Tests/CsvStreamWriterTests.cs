using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ncl.Common.Csv.Tests
{
    public class CsvStreamWriterTests
    {
        private const string ValidHeader = "header";
        private const string NeedsEscapingHeader = "header,\" more";
        private const string NeedsEscapingHeaderExpected = "\"header,\"\" more\"";

        private const string ValidField = "field";
        private const string NeedsEscapingField = "field,\" more";
        private const string NeedsEscapingFieldExpected = "\"field,\"\" more\"";

        private const string SimpleFormat = "{0}";

        private static readonly CultureInfo _englishUsCulture = new("en-US");
        private static readonly CultureInfo _spanishCulture = new("es-ES");

        [Fact]
        public void CsvStreamWriter_WithValidArguments_ShouldCreateInstance()
        {
            // Arrange
            using var stream = new MemoryStream();

            // Act
            var actual = new CsvStreamWriter(stream);

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void CsvStreamWriter_WithNullStream_ShouldThrowException()
        {
            // Arrange

            // Act
            void TestCode()
            {
                _ = new CsvStreamWriter((Stream) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(TestCode);
        }

        [Fact]
        public void CsvStreamWriter_WithInvalidSeparator_ShouldThrowException()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            void TestCode()
            {
                _ = new CsvStreamWriter(stream, separator: '\n');
            }

            // Assert
            Assert.Throws<ArgumentException>(TestCode);
        }

        [Fact]
        public void CsvStreamWriter1_WithValidArguments_ShouldCreateInstance()
        {
            // Arrange
            using var stream = new StreamWriter(new MemoryStream());

            // Act
            var actual = new CsvStreamWriter(stream);

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void CsvStreamWriter1_WithNullStream_ShouldThrowException()
        {
            // Arrange

            // Act
            void TestCode()
            {
                _ = new CsvStreamWriter((TextWriter) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(TestCode);
        }

        [Fact]
        public void CsvStreamWriter1_WithInvalidSeparator_ShouldThrowException()
        {
            // Arrange
            using StreamWriter stream = GetDefaultStreamWriter();

            // Act
            void TestCode()
            {
                _ = new CsvStreamWriter(stream, separator: '\n');
            }

            // Assert
            Assert.Throws<ArgumentException>(TestCode);
        }

        [Fact]
        public void CsvStreamWriter2_WithNullString_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = new CsvStreamWriter((string) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(TestCode);
        }

        [Fact]
        public void CsvStreamWriter2_WithInvalidSeparator_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = new CsvStreamWriter((string) null, separator: '\n');
            }

            // Assert
            Assert.Throws<ArgumentException>(TestCode);
        }

        [Fact]
        public void Create_WithValidArguments_ShouldReturnInstance()
        {
            // Arrange
            using Stream stream = GetDefaultStream();

            // Act
            var actual = CsvStreamWriter.Create(stream);

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void Create_WithNullStream_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = CsvStreamWriter.Create((Stream) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(TestCode);
        }

        [Fact]
        public void Create_WithInvalidSeparator_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = CsvStreamWriter.Create((Stream) null, separator: '\n');
            }

            // Assert
            Assert.Throws<ArgumentException>(TestCode);
        }

        [Fact]
        public void Create1_WithValidArguments_ShouldReturnInstance()
        {
            // Arrange
            using Stream stream = GetDefaultStream();

            // Act
            var actual = CsvStreamWriter.Create(stream, out Exception _);

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void Create1_WithNullStream_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            _ = CsvStreamWriter.Create((Stream) null, out Exception actual);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }

        [Fact]
        public void Create1_WithInvalidSeparator_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            _ = CsvStreamWriter.Create((Stream) null, out Exception actual, separator: '\n');

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }

        [Fact]
        public void TryCreate_WithValidArguments_ShouldReturnTrue()
        {
            // Arrange
            using Stream stream = GetDefaultStream();

            // Act
            bool actual = CsvStreamWriter.TryCreate(stream, out Exception _, out CsvStreamWriter _);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void TryCreate_WithValidArguments_ShouldSetInstanceArgument()
        {
            // Arrange
            using Stream stream = GetDefaultStream();

            // Act
            bool _ = CsvStreamWriter.TryCreate(stream, out Exception _, out CsvStreamWriter actual);

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void TryCreate_WithValidArguments_ShouldSetExceptionArgumentToNull()
        {
            // Arrange
            using Stream stream = GetDefaultStream();

            // Act
            bool _ = CsvStreamWriter.TryCreate(stream, out Exception actual, out CsvStreamWriter _);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate_WithNullStream_ShouldReturnFalse()
        {
            // Arrange
            // Act
            bool actual = CsvStreamWriter.TryCreate((Stream) null, out Exception _, out CsvStreamWriter _);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate_WithNullStream_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((Stream) null, out Exception _, out CsvStreamWriter actual);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate_WithNullStream_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((Stream) null, out Exception actual, out CsvStreamWriter _);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }

        [Fact]
        public void TryCreate_WithInvalidSeparator_ShouldReturnFalse()
        {
            // Arrange
            // Act
            bool actual = CsvStreamWriter.TryCreate((Stream) null, out Exception _, out CsvStreamWriter _,
                separator: '\n');

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate_WithInvalidSeparator_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((Stream) null, out Exception _, out CsvStreamWriter actual,
                separator: '\n');

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate_WithInvalidSeparator_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((Stream) null, out Exception actual, out CsvStreamWriter _,
                separator: '\n');

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }
        
        [Fact]
        public void Create2_WithValidArguments_ShouldReturnInstance()
        {
            // Arrange
            using StreamWriter stream = GetDefaultStreamWriter();

            // Act
            var actual = CsvStreamWriter.Create(stream);

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void Create2_WithNullStream_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = CsvStreamWriter.Create((TextWriter) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(TestCode);
        }

        [Fact]
        public void Create2_WithInvalidSeparator_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = CsvStreamWriter.Create((TextWriter) null, separator: '\n');
            }

            // Assert
            Assert.Throws<ArgumentException>(TestCode);
        }

        [Fact]
        public void Create3_WithValidArguments_ShouldReturnInstance()
        {
            // Arrange
            using StreamWriter stream = GetDefaultStreamWriter();

            // Act
            var actual = CsvStreamWriter.Create(stream, out Exception _);

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void Create3_WithNullStream_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            _ = CsvStreamWriter.Create((TextWriter) null, out Exception actual);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }

        [Fact]
        public void Create3_WithInvalidSeparator_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            _ = CsvStreamWriter.Create((TextWriter) null, out Exception actual, separator: '\n');

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }

        [Fact]
        public void TryCreate1_WithValidArguments_ShouldReturnTrue()
        {
            // Arrange
            using StreamWriter stream = GetDefaultStreamWriter();

            // Act
            bool actual = CsvStreamWriter.TryCreate(stream, out Exception _, out CsvStreamWriter _);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void TryCreate1_WithValidArguments_ShouldSetInstanceArgument()
        {
            // Arrange
            using StreamWriter stream = GetDefaultStreamWriter();

            // Act
            bool _ = CsvStreamWriter.TryCreate(stream, out Exception _, out CsvStreamWriter actual);

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void TryCreate1_WithValidArguments_ShouldSetExceptionArgumentToNull()
        {
            // Arrange
            using StreamWriter stream = GetDefaultStreamWriter();

            // Act
            bool _ = CsvStreamWriter.TryCreate(stream, out Exception actual, out CsvStreamWriter _);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate1_WithNullStream_ShouldReturnFalse()
        {
            // Arrange
            // Act
            bool actual = CsvStreamWriter.TryCreate((TextWriter) null, out Exception _, out CsvStreamWriter _);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate1_WithNullStream_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((TextWriter) null, out Exception _, out CsvStreamWriter actual);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate1_WithNullStream_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((TextWriter) null, out Exception actual, out CsvStreamWriter _);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }

        [Fact]
        public void TryCreate1_WithInvalidSeparator_ShouldReturnFalse()
        {
            // Arrange
            // Act
            bool actual = CsvStreamWriter.TryCreate((TextWriter) null, out Exception _, out CsvStreamWriter _,
                separator: '\n');

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate1_WithInvalidSeparator_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((TextWriter) null, out Exception _, out CsvStreamWriter actual,
                separator: '\n');

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate1_WithInvalidSeparator_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((TextWriter) null, out Exception actual, out CsvStreamWriter _,
                separator: '\n');

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }

        [Fact]
        public void Create3_WithNullString_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = CsvStreamWriter.Create((string) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(TestCode);
        }

        [Fact]
        public void Create3_WithInvalidSeparator_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = CsvStreamWriter.Create((string) null, separator: '\n');
            }

            // Assert
            Assert.Throws<ArgumentException>(TestCode);
        }

        [Fact]
        public void Create4_WithNullString_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            _ = CsvStreamWriter.Create((string) null, out Exception actual);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }

        [Fact]
        public void Create4_WithInvalidSeparator_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            _ = CsvStreamWriter.Create((string) null, out Exception actual, separator: '\n');

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }

        [Fact]
        public void TryCreate2_WithNullString_ShouldReturnFalse()
        {
            // Arrange
            // Act
            bool actual = CsvStreamWriter.TryCreate((string) null, out Exception _, out CsvStreamWriter _);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate2_WithNullString_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((string) null, out Exception _, out CsvStreamWriter actual);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate2_WithNullString_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((string) null, out Exception actual, out CsvStreamWriter _);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }

        [Fact]
        public void TryCreate2_WithInvalidSeparator_ShouldReturnFalse()
        {
            // Arrange
            // Act
            bool actual = CsvStreamWriter.TryCreate((string) null, out Exception _, out CsvStreamWriter _,
                separator: '\n');

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate2_WithInvalidSeparator_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((string) null, out Exception _, out CsvStreamWriter actual,
                separator: '\n');

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate2_WithInvalidSeparator_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((string) null, out Exception actual, out CsvStreamWriter _,
                separator: '\n');

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }

        [Fact]
        public void HeaderRowWritten_WithHeaderRowWritten_ShouldReturnTrue()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteHeader(ValidHeader);
            csvStream.WriteRowEnd();

            // Assert
            Assert.True(csvStream.HeaderRowWritten);
        }

        [Fact]
        public void HeaderRowWritten_WithHeaderWrittenButRowSame_ShouldReturnFalse()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteHeader(ValidHeader);

            // Assert
            Assert.False(csvStream.HeaderRowWritten);
        }

        [Fact]
        public void HeaderRowWritten_WithNoHeaderWrittenAndNextRow_ShouldReturnFalse()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteField(ValidHeader);
            csvStream.WriteRowEnd();

            // Assert
            Assert.False(csvStream.HeaderRowWritten);
        }

        [Fact]
        public void HeaderRowWritten_WithNothingWritten_ShouldReturnFalse()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act

            // Assert
            Assert.False(csvStream.HeaderRowWritten);
        }

        [Fact]
        public void Headers_WithNoHeadersWritten_ShouldBeNull()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act

            // Assert
            Assert.Null(csvStream.Headers);
        }

        [Fact]
        public void Headers_WithOneHeaderWritten_ShouldContainOneHeader()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteHeader(ValidHeader);

            // Assert
            Assert.NotNull(csvStream.Headers);
            Assert.Equal(ValidHeader, csvStream.Headers[0]);
        }

        [Fact]
        public void RowsWritten_WithNothingWritten_ShouldReturnZero()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act

            // Assert
            Assert.Equal(0, csvStream.RowsWritten);
        }

        [Fact]
        public void RowsWritten_WithARowWritten_ShouldReturnOne()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteHeader(ValidHeader);
            csvStream.WriteRowEnd();

            // Assert
            Assert.Equal(1, csvStream.RowsWritten);
        }

        [Fact]
        public void FormatProvider_WithNullValue_ShouldSetToThreadsCurrentCulture()
        {
            IFormatProvider expected = Thread.CurrentThread.CurrentCulture;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();
            csvStream.FormatProvider = expected.Equals(_englishUsCulture) ? _spanishCulture : _englishUsCulture;

            // Act
            csvStream.FormatProvider = null;

            IFormatProvider actual = csvStream.FormatProvider;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatProvider_WithSameValue_ShouldRemainSameValue()
        {
            IFormatProvider expected = _englishUsCulture;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.FormatProvider = _englishUsCulture;

            IFormatProvider actual = csvStream.FormatProvider;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatProvider_WithValue_ShouldSetToValue()
        {
            IFormatProvider expected = _englishUsCulture;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();
            csvStream.FormatProvider = _spanishCulture;

            // Act
            csvStream.FormatProvider = _englishUsCulture;

            IFormatProvider actual = csvStream.FormatProvider;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Flush_WithContentWritten_ShouldWriteContentToUnderlyingStream()
        {
            // Arrange
            using MemoryStream memoryStream = new();
            using StreamWriter streamWriter = new(memoryStream);
            using CsvStreamWriter csvStream = new(streamWriter);

            // Act
            csvStream.WriteHeader(ValidHeader);
            csvStream.Flush();

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(ValidHeader, actual);
        }

        [Fact]
        public void Flush_WithDisposedStream_ShouldThrowException()
        {
            void TestCode()
            {
                // Arrange
                CsvStreamWriter csvStream = GetDefaultInstance();

                csvStream.Dispose();
                csvStream.Dispose(); //Twice to cover Dispose

                // Act
                csvStream.Flush();
            }

            // Assert
            Assert.Throws<ObjectDisposedException>(TestCode);
        }

        [Fact]
        public async Task FlushAsync_WithContentWritten_ShouldWriteContentToUnderlyingStream()
        {
            // Arrange
            await using MemoryStream memoryStream = new();
            await using StreamWriter streamWriter = new(memoryStream);
            using CsvStreamWriter csvStream = new(streamWriter);

            // Act
            await csvStream.WriteHeaderAsync(ValidHeader).ConfigureAwait(false);
            await csvStream.FlushAsync().ConfigureAwait(false);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(ValidHeader, actual);
        }

        [Fact]
        public async Task FlushAsync_WithDisposedStream_ShouldThrowException()
        {
            async Task TestCode()
            {
                // Arrange
                CsvStreamWriter csvStream = GetDefaultInstance();

                csvStream.Dispose();
                csvStream.Dispose(); //Twice to cover Dispose

                // Act
                await csvStream.FlushAsync().ConfigureAwait(false);
            }

            // Assert
            await Assert.ThrowsAsync<ObjectDisposedException>(TestCode);
        }

        [Fact]
        public void WriteHeader_WithValidString_ShouldWriteHeader()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteHeader(ValidHeader);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(ValidHeader, actual);
        }

        [Fact]
        public void WriteHeader_WithUnescapedString_ShouldWriteEscapedHeader()
        {
            const string expected = NeedsEscapingHeaderExpected;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteHeader(NeedsEscapingHeader);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeader_WithNullString_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteHeader(null);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeader_WithValidHeaderAfterFirstRow_ShouldThrowException()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();
            csvStream.WriteHeader(ValidHeader);
            csvStream.WriteRowEnd();

            // Act
            void TestCode()
            {
                csvStream.WriteHeader(ValidHeader);
            }

            // Assert
            Assert.Throws<InvalidOperationException>(TestCode);
        }

        [Fact]
        public async Task WriteHeaderAsync_WithValidString_ShouldWriteHeader()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteHeaderAsync(ValidHeader).ConfigureAwait(false);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(ValidHeader, actual);
        }

        [Fact]
        public async Task WriteHeaderAsync_WithUnescapedString_ShouldWriteEscapedHeader()
        {
            const string expected = NeedsEscapingHeaderExpected;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteHeaderAsync(NeedsEscapingHeader).ConfigureAwait(false);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeaderAsync_WithNullString_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteHeaderAsync(null).ConfigureAwait(false);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeaderAsync_WithValidHeaderAfterFirstRow_ShouldThrowException()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();
            await csvStream.WriteHeaderAsync(ValidHeader).ConfigureAwait(false);
            await csvStream.WriteRowEndAsync().ConfigureAwait(false);

            // Act
            async Task TestCode()
            {
                // ReSharper disable once AccessToDisposedClosure
                await csvStream.WriteHeaderAsync(ValidHeader).ConfigureAwait(false);
            }

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(TestCode).ConfigureAwait(false);
        }

        [Fact]
        public void WriteHeaders_WithValidStrings_ShouldWriteHeaderEntries()
        {
            string expected = $"{ValidHeader},{ValidHeader}";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteHeaders(new[] { ValidHeader, ValidHeader });

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaders_WithNullEnumerable_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteHeaders((IEnumerable<string>) null);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaders_WithEnumerableWithOnlyANullEntry_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteHeaders(new string[] { null });
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaders_WithValidHeaderAfterFirstRow_ShouldThrowException()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();
            csvStream.WriteHeaderRow(new[] { ValidHeader, ValidHeader });

            // Act
            void TestCode()
            {
                csvStream.WriteHeaders(new[] { ValidHeader, ValidHeader });
            }

            // Assert
            Assert.Throws<InvalidOperationException>(TestCode);
        }

        [Fact]
        public async Task WriteHeadersAsync_WithValidStrings_ShouldWriteHeaderEntries()
        {
            string expected = $"{ValidHeader},{ValidHeader}";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteHeadersAsync(new[] { ValidHeader, ValidHeader }).ConfigureAwait(false);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeadersAsync_WithNullEnumerable_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteHeadersAsync((IEnumerable<string>) null).ConfigureAwait(false);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeadersAsync_WithEnumerableWithOnlyANullEntry_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteHeadersAsync(new string[] { null }).ConfigureAwait(false);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeadersAsync_WithValidHeaderAfterFirstRow_ShouldThrowException()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();
            await csvStream.WriteHeaderRowAsync(new[] { ValidHeader, ValidHeader }).ConfigureAwait(false);

            // Act
            async Task TestCode()
            {
                // ReSharper disable once AccessToDisposedClosure
                await csvStream.WriteHeadersAsync(new[] { ValidHeader, ValidHeader }).ConfigureAwait(false);
            }

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(TestCode).ConfigureAwait(false);
        }

        [Fact]
        public void WriteHeaders2_WithValidStrings_ShouldWriteHeaderEntries()
        {
            string expected = $"{ValidHeader},{ValidHeader}";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteHeaders(ValidHeader, ValidHeader);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaders2_WithFirstNullValueAndValidValue_ShouldWriteHeaderEntries()
        {
            string expected = $"{ValidHeader}";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteHeaders(null, ValidHeader);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaders2_WithValidValueAndSecondNullValue_ShouldWriteHeaderEntries()
        {
            string expected = $"{ValidHeader}";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteHeaders(ValidHeader);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaders2_WithValidValueAndSecondNullValueAndValidValue_ShouldWriteHeaderEntries()
        {
            string expected = $"{ValidHeader},{ValidHeader}";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteHeaders(ValidHeader, null, ValidHeader);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaders2_WithNullValues_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteHeaders(null, null);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaders2_WithNullStringValues_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteHeaders(null, (string) null);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaders2_WithThreeNullValues_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteHeaders(null, null, null);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaders2_WithValidHeaderAfterFirstRow_ShouldThrowException()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();
            csvStream.WriteHeaderRow(ValidHeader, ValidHeader);

            // Act
            void TestCode()
            {
                csvStream.WriteHeaders(ValidHeader, ValidHeader);
            }

            // Assert
            Assert.Throws<InvalidOperationException>(TestCode);
        }

        [Fact]
        public async Task WriteHeadersAsync2_WithValidStrings_ShouldWriteHeaderEntries()
        {
            string expected = $"{ValidHeader},{ValidHeader}";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteHeadersAsync(ValidHeader, ValidHeader).ConfigureAwait(false);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeadersAsync2_WithFirstNullValueAndValidValue_ShouldWriteHeaderEntries()
        {
            string expected = $"{ValidHeader}";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteHeadersAsync(null, ValidHeader).ConfigureAwait(false);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeadersAsync2_WithValidValueAndSecondNullValue_ShouldWriteHeaderEntries()
        {
            string expected = $"{ValidHeader}";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteHeadersAsync(ValidHeader).ConfigureAwait(false);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeadersAsync2_WithValidValueAndSecondNullValueAndValidValue_ShouldWriteHeaderEntries()
        {
            string expected = $"{ValidHeader},{ValidHeader}";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteHeadersAsync(ValidHeader, null, ValidHeader).ConfigureAwait(false);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeadersAsync2_WithNullValues_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteHeadersAsync(null, null).ConfigureAwait(false);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeadersAsync2_WithNullStringValues_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteHeadersAsync(null, (string) null).ConfigureAwait(false);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeadersAsync2_WithThreeNullValues_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteHeadersAsync(null, null, null).ConfigureAwait(false);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeadersAsync2_WithValidHeaderAfterFirstRow_ShouldThrowException()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();
            await csvStream.WriteHeaderRowAsync(ValidHeader, ValidHeader).ConfigureAwait(false);

            // Act
            async Task TestCode()
            {
                // ReSharper disable once AccessToDisposedClosure
                await csvStream.WriteHeadersAsync(ValidHeader, ValidHeader).ConfigureAwait(false);
            }

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(TestCode).ConfigureAwait(false);
        }

        [Fact]
        public void WriteHeaderRow_WithValidStrings_ShouldWriteHeaderRow()
        {
            string expected = $"{ValidHeader},{ValidHeader}\r\n";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteHeaderRow(new[] { ValidHeader, ValidHeader });

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaderRow_WithNullEnumerable_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteHeaderRow((IEnumerable<string>) null);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaderRow_WithEnumerableWithOnlyANullEntry_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteHeaderRow(new string[] { null });
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaderRow_WithValidHeaderAfterFirstRow_ShouldThrowException()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();
            csvStream.WriteHeaderRow(new[] { ValidHeader, ValidHeader });

            // Act
            void TestCode()
            {
                csvStream.WriteHeaderRow(new[] { ValidHeader, ValidHeader });
            }

            // Assert
            Assert.Throws<InvalidOperationException>(TestCode);
        }

        [Fact]
        public async Task WriteHeaderRowAsync_WithValidStrings_ShouldWriteHeaderRow()
        {
            string expected = $"{ValidHeader},{ValidHeader}\r\n";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteHeaderRowAsync(new[] { ValidHeader, ValidHeader }).ConfigureAwait(false);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeaderRowAsync_WithNullEnumerable_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteHeaderRowAsync((IEnumerable<string>) null).ConfigureAwait(false);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeaderRowAsync_WithEnumerableWithOnlyANullEntry_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteHeaderRowAsync(new string[] { null }).ConfigureAwait(false);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeaderRowAsync_WithValidHeaderAfterFirstRow_ShouldThrowException()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();
            await csvStream.WriteHeaderRowAsync(new[] { ValidHeader, ValidHeader }).ConfigureAwait(false);

            // Act
            async Task TestCode()
            {
                // ReSharper disable once AccessToDisposedClosure
                await csvStream.WriteHeaderRowAsync(new[] { ValidHeader, ValidHeader }).ConfigureAwait(false);
            }

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(TestCode).ConfigureAwait(false);
        }

        [Fact]
        public void WriteHeaderRow2_WithValidStrings_ShouldWriteHeaderRow()
        {
            string expected = $"{ValidHeader},{ValidHeader}\r\n";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteHeaderRow(ValidHeader, ValidHeader);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaderRow2_WithFirstNullValueAndValidValue_ShouldWriteHeaderRow()
        {
            string expected = $"{ValidHeader}\r\n";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteHeaderRow(null, ValidHeader);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaderRow2_WithValidValueAndSecondNullValue_ShouldWriteHeaderRow()
        {
            string expected = $"{ValidHeader}\r\n";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteHeaderRow(ValidHeader);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaderRow2_WithValidValueAndSecondNullValueAndValidValue_ShouldWriteHeaderRow()
        {
            string expected = $"{ValidHeader},{ValidHeader}\r\n";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteHeaderRow(ValidHeader, null, ValidHeader);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaderRow2_WithNullValues_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteHeaderRow(null, null);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaderRow2_WithNullStringValues_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteHeaderRow(null, (string) null);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaderRow2_WithThreeNullValues_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteHeaderRow(null, null, null);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteHeaderRow2_WithValidHeaderAfterFirstRow_ShouldThrowException()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();
            csvStream.WriteHeaderRow(ValidHeader, ValidHeader);

            // Act
            void TestCode()
            {
                csvStream.WriteHeaderRow(ValidHeader, ValidHeader);
            }

            // Assert
            Assert.Throws<InvalidOperationException>(TestCode);
        }

        [Fact]
        public async Task WriteHeaderRowAsync2_WithValidStrings_ShouldWriteHeaderRow()
        {
            string expected = $"{ValidHeader},{ValidHeader}\r\n";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteHeaderRowAsync(ValidHeader, ValidHeader).ConfigureAwait(false);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeaderRowAsync2_WithFirstNullValueAndValidValue_ShouldWriteHeaderRow()
        {
            string expected = $"{ValidHeader}\r\n";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteHeaderRowAsync(null, ValidHeader).ConfigureAwait(false);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeaderRowAsync2_WithValidValueAndSecondNullValue_ShouldWriteHeaderRow()
        {
            string expected = $"{ValidHeader}\r\n";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteHeaderRowAsync(ValidHeader).ConfigureAwait(false);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeaderRowAsync2_WithValidValueAndSecondNullValueAndValidValue_ShouldWriteHeaderRow()
        {
            string expected = $"{ValidHeader},{ValidHeader}\r\n";
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteHeaderRowAsync(ValidHeader, null, ValidHeader).ConfigureAwait(false);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeaderRowAsync2_WithNullValues_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteHeaderRowAsync(null, null).ConfigureAwait(false);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeaderRowAsync2_WithNullStringValues_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteHeaderRowAsync(null, (string) null).ConfigureAwait(false);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeaderRowAsync2_WithThreeNullValues_ShouldDoNothing()
        {
            const int expected = 0;
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteHeaderRowAsync(null, null, null).ConfigureAwait(false);
            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteHeaderRowAsync2_WithValidHeaderAfterFirstRow_ShouldThrowException()
        {
            // Arrange
            using CsvStreamWriter csvStream = GetDefaultInstance();
            await csvStream.WriteHeaderRowAsync(ValidHeader, ValidHeader).ConfigureAwait(false);

            // Act
            async Task TestCode()
            {
                // ReSharper disable once AccessToDisposedClosure
                await csvStream.WriteHeaderRowAsync(ValidHeader, ValidHeader).ConfigureAwait(false);
            }

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(TestCode).ConfigureAwait(false);
        }

        [Fact]
        public void WriteRowEnd_WithNoFieldsWritten_ShouldThrowIntegrityException()
        {
            // Arrange
            // ReSharper disable once RedundantArgumentDefaultValue
            using CsvStreamWriter csvStream = GetDefaultInstance(IntegrityMode.Strict);

            // Act
            void TestCode()
            {
                csvStream.WriteRowEnd();
            }

            // Assert
            Assert.Throws<IntegrityViolatedException>(TestCode);
        }

        [Fact]
        public void WriteRowEnd_WithDisposedStream_ShouldThrowException()
        {
            // Act
            void TestCode()
            {
                // Arrange
                CsvStreamWriter csvStream = GetDefaultInstance();
                csvStream.Dispose();

                //Act
                csvStream.WriteRowEnd();
            }

            // Assert
            Assert.Throws<ObjectDisposedException>(TestCode);
        }

        [Fact]
        public void WriteRowEnd_WithUnmatchedFieldsAndStrictMode_ShouldThrowIntegrityException()
        {
            // Arrange
            // ReSharper disable once RedundantArgumentDefaultValue
            using CsvStreamWriter csvStream = GetDefaultInstance(IntegrityMode.Strict);

            csvStream.WriteHeaderRow(ValidHeader, ValidHeader);
            csvStream.WriteField(ValidField);

            // Act
            void TestCode()
            {
                csvStream.WriteRowEnd();
            }

            // Assert
            Assert.Throws<IntegrityViolatedException>(TestCode);
        }

        [Fact]
        public void WriteRowEnd_WithUnmatchedAndLessFieldsAndLooseMode_ShouldFillMissingComma()
        {
            // Arrange
            string expected = $"{ValidHeader},{ValidHeader}\r\n{ValidField},\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream, IntegrityMode.Loose);

            csvStream.WriteHeaderRow(ValidHeader, ValidHeader);
            csvStream.WriteField(ValidField);

            // Act
            csvStream.WriteRowEnd();

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteRowEnd_WithUnmatchedAndMoreFieldsAndLooseMode_ShouldWriteField()
        {
            // Arrange
            string expected = $"{ValidHeader}\r\n{ValidField},{ValidField}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream, IntegrityMode.Loose);

            csvStream.WriteHeaderRow(ValidHeader);
            csvStream.WriteFields(ValidField, ValidField);

            // Act
            csvStream.WriteRowEnd();

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteRowEnd_WithUnmatchedFieldsAndNoneMode_ShouldOnlyWriteRowEnd()
        {
            // Arrange
            string expected = $"{ValidHeader},{ValidHeader}\r\n{ValidField}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream, IntegrityMode.None);

            csvStream.WriteHeaderRow(ValidHeader, ValidHeader);
            csvStream.WriteField(ValidField);

            // Act
            csvStream.WriteRowEnd();

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteRowEnd_WithMatchedFieldsAndStrictMode_ShouldWriteRowEnd()
        {
            // Arrange
            string expected = $"{ValidHeader},{ValidHeader}\r\n{ValidField},{ValidField}\r\n";
            // ReSharper disable once RedundantArgumentDefaultValue
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream, IntegrityMode.Strict);

            csvStream.WriteHeaderRow(ValidHeader, ValidHeader);
            csvStream.WriteFields(ValidField, ValidField);

            // Act
            csvStream.WriteRowEnd();

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteRowEnd_WithMatchedFieldsAndLooseMode_ShouldWriteRowEnd()
        {
            // Arrange
            string expected = $"{ValidHeader},{ValidHeader}\r\n{ValidField},{ValidField}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream, IntegrityMode.Loose);

            csvStream.WriteHeaderRow(ValidHeader, ValidHeader);
            csvStream.WriteFields(ValidField, ValidField);

            // Act
            csvStream.WriteRowEnd();

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteRowEnd_WithMatchedFieldsAndNoneMode_ShouldWriteRowEnd()
        {
            // Arrange
            string expected = $"{ValidHeader},{ValidHeader}\r\n{ValidField},{ValidField}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream, IntegrityMode.None);

            csvStream.WriteHeaderRow(ValidHeader, ValidHeader);
            csvStream.WriteFields(ValidField, ValidField);

            // Act
            csvStream.WriteRowEnd();

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteRowEndAsync_WithNoFieldsWritten_ShouldThrowIntegrityException()
        {
            // Act
            async Task TestCode()
            {
                // Arrange
                // ReSharper disable once RedundantArgumentDefaultValue
                using CsvStreamWriter csvStream = GetDefaultInstance(IntegrityMode.Strict);

                // Act
                await csvStream.WriteRowEndAsync();
            }

            // Assert
            await Assert.ThrowsAsync<IntegrityViolatedException>(TestCode);
        }

        [Fact]
        public async Task WriteRowEndAsync_WithDisposedStream_ShouldThrowException()
        {
            // Act
            async Task TestCode()
            {
                // Arrange
                CsvStreamWriter csvStream = GetDefaultInstance();
                csvStream.Dispose();

                //Act
                await csvStream.WriteRowEndAsync();
            }

            // Assert
            await Assert.ThrowsAsync<ObjectDisposedException>(TestCode);
        }

        [Fact]
        public async Task WriteRowEndAsync_WithUnmatchedFieldsAndStrictMode_ShouldThrowIntegrityException()
        {
            // Arrange
            // ReSharper disable once RedundantArgumentDefaultValue
            using CsvStreamWriter csvStream = GetDefaultInstance(IntegrityMode.Strict);

            await csvStream.WriteHeaderRowAsync(ValidHeader, ValidHeader);
            await csvStream.WriteFieldAsync(ValidField);

            // Act
            void TestCode()
            {
                csvStream.WriteRowEnd();
            }

            // Assert
            Assert.Throws<IntegrityViolatedException>(TestCode);
        }

        [Fact]
        public async Task WriteRowEndAsync_WithUnmatchedAndLessFieldsAndLooseMode_ShouldFillMissingComma()
        {
            // Arrange
            string expected = $"{ValidHeader},{ValidHeader}\r\n{ValidField},\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream, IntegrityMode.Loose);

            await csvStream.WriteHeaderRowAsync(ValidHeader, ValidHeader);
            await csvStream.WriteFieldAsync(ValidField);

            // Act
            await csvStream.WriteRowEndAsync();

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteRowEndAsync_WithUnmatchedAndMoreFieldsAndLooseMode_ShouldWriteField()
        {
            // Arrange
            string expected = $"{ValidHeader}\r\n{ValidField},{ValidField}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream, IntegrityMode.Loose);

            await csvStream.WriteHeaderRowAsync(ValidHeader);
            await csvStream.WriteFieldsAsync(ValidField, ValidField);

            // Act
            await csvStream.WriteRowEndAsync();

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteRowEndAsync_WithUnmatchedFieldsAndNoneMode_ShouldOnlyWriteRowEnd()
        {
            // Arrange
            string expected = $"{ValidHeader},{ValidHeader}\r\n{ValidField}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream, IntegrityMode.None);

            await csvStream.WriteHeaderRowAsync(ValidHeader, ValidHeader);
            await csvStream.WriteFieldAsync(ValidField);

            // Act
            await csvStream.WriteRowEndAsync();

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteRowEndAsync_WithMatchedFieldsAndStrictMode_ShouldWriteRowEnd()
        {
            // Arrange
            string expected = $"{ValidHeader},{ValidHeader}\r\n{ValidField},{ValidField}\r\n";
            // ReSharper disable once RedundantArgumentDefaultValue
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream, IntegrityMode.Strict);

            await csvStream.WriteHeaderRowAsync(ValidHeader, ValidHeader);
            await csvStream.WriteFieldsAsync(ValidField, ValidField);

            // Act
            await csvStream.WriteRowEndAsync();

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteRowEndAsync_WithMatchedFieldsAndLooseMode_ShouldWriteRowEnd()
        {
            // Arrange
            string expected = $"{ValidHeader},{ValidHeader}\r\n{ValidField},{ValidField}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream, IntegrityMode.Loose);

            await csvStream.WriteHeaderRowAsync(ValidHeader, ValidHeader);
            await csvStream.WriteFieldsAsync(ValidField, ValidField);

            // Act
            await csvStream.WriteRowEndAsync();

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteRowEndAsync_WithMatchedFieldsAndNoneMode_ShouldWriteRowEnd()
        {
            // Arrange
            string expected = $"{ValidHeader},{ValidHeader}\r\n{ValidField},{ValidField}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream, IntegrityMode.None);

            await csvStream.WriteHeaderRowAsync(ValidHeader, ValidHeader);
            await csvStream.WriteFieldsAsync(ValidField, ValidField);

            // Act
            await csvStream.WriteRowEndAsync();

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = ValidField;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteField(ValidField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField_WithFieldValueNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            const string expected = NeedsEscapingFieldExpected;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteField(NeedsEscapingField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteField((string) null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField_WithValidFieldValueAndUnfinishedHeaderRow_ShouldWriteRowEndAndField()
        {
            // Arrange
            string expected = $"{ValidHeader}\r\n{ValidField}";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            csvStream.WriteHeader(ValidHeader);

            // Act
            csvStream.WriteField(ValidField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField_WithValidHeaderButStreamDisposed_ShouldThrowException()
        {
            //Act
            void TestCode()
            {
                // Arrange
                CsvStreamWriter csvStream = GetDefaultInstance();
                csvStream.Dispose();

                //Act
                csvStream.WriteField(ValidField);
            }

            // Assert
            Assert.Throws<ObjectDisposedException>(TestCode);
        }

        [Fact]
        public async Task WriteFieldAsync_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = ValidField;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldAsync(ValidField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync_WithFieldValueNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            const string expected = NeedsEscapingFieldExpected;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldAsync(NeedsEscapingField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteFieldAsync((string) null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync_WithValidFieldValueAndUnfinishedHeaderRow_ShouldWriteRowEndAndField()
        {
            // Arrange
            string expected = $"{ValidHeader}\r\n{ValidField}";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            await csvStream.WriteHeaderAsync(ValidHeader);

            // Act
            await csvStream.WriteFieldAsync(ValidField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync_WithValidHeaderButStreamDisposed_ShouldThrowException()
        {
            //Act
            async Task TestCode()
            {
                // Arrange
                CsvStreamWriter csvStream = GetDefaultInstance();
                csvStream.Dispose();

                //Act
                await csvStream.WriteFieldAsync(ValidField);
            }

            // Assert
            await Assert.ThrowsAsync<ObjectDisposedException>(TestCode);
        }

        [Fact]
        public void WriteField2_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = ValidField;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteField(SimpleFormat, ValidField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField2_WithFieldValueNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            const string expected = NeedsEscapingFieldExpected;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteField(SimpleFormat, NeedsEscapingField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField2_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteField(null, null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync2_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = ValidField;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldAsync(SimpleFormat, ValidField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync2_WithFieldValueNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            const string expected = NeedsEscapingFieldExpected;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldAsync(SimpleFormat, NeedsEscapingField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync2_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteFieldAsync(null, null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField3_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = ValidField;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteField((object) ValidField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField3_WithValidFormattableFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "1";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteField((object) 1);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField3_WithFieldValueNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            const string expected = NeedsEscapingFieldExpected;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteField((object) NeedsEscapingField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField3_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteField((object) null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync3_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = ValidField;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldAsync((object) ValidField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync3_WithValidFormattableFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "1";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldAsync((object) 1);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync3_WithFieldValueNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            const string expected = NeedsEscapingFieldExpected;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldAsync((object) NeedsEscapingField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync3_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteFieldAsync((object) null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField4_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = ValidField;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);
            char[] charArray = ValidField.ToCharArray();

            // Act
            csvStream.WriteField(charArray, 0, charArray.Length);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField4_WithFieldValueNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            const string expected = NeedsEscapingFieldExpected;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);
            char[] charArray = NeedsEscapingField.ToCharArray();

            // Act
            csvStream.WriteField(charArray, 0, charArray.Length);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField4_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteField(null, 0, 0);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField4_WithInvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            void TestCode()
            {
                // Arrange
                using CsvStreamWriter csvStream = GetDefaultInstance();

                //Act
                char[] charArray = NeedsEscapingField.ToCharArray();
                csvStream.WriteField(charArray, -1, charArray.Length);
            }

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(TestCode);
        }

        [Fact]
        public void WriteField4_WithInvalidCount_ShouldThrowArgumentOutOfRangeException()
        {
            void TestCode()
            {
                // Arrange
                using CsvStreamWriter csvStream = GetDefaultInstance();

                //Act
                char[] charArray = NeedsEscapingField.ToCharArray();
                csvStream.WriteField(charArray, 0, charArray.Length + 1);
            }

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(TestCode);
        }

        [Fact]
        public async Task WriteFieldAsync4_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = ValidField;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);
            char[] charArray = ValidField.ToCharArray();

            // Act
            await csvStream.WriteFieldAsync(charArray, 0, charArray.Length);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync4_WithFieldValueNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            const string expected = NeedsEscapingFieldExpected;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);
            char[] charArray = NeedsEscapingField.ToCharArray();

            // Act
            await csvStream.WriteFieldAsync(charArray, 0, charArray.Length);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync4_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteFieldAsync(null, 0, 0);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync4_WithInvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            async Task TestCode()
            {
                // Arrange
                using CsvStreamWriter csvStream = GetDefaultInstance();

                //Act
                char[] charArray = NeedsEscapingField.ToCharArray();
                await csvStream.WriteFieldAsync(charArray, -1, charArray.Length);
            }

            // Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(TestCode);
        }

        [Fact]
        public async Task WriteFieldAsync4_WithInvalidCount_ShouldThrowArgumentOutOfRangeException()
        {
            async Task TestCode()
            {
                // Arrange
                using CsvStreamWriter csvStream = GetDefaultInstance();

                //Act
                char[] charArray = NeedsEscapingField.ToCharArray();
                await csvStream.WriteFieldAsync(charArray, 0, charArray.Length + 1);
            }

            // Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(TestCode);
        }

        [Fact]
        public void WriteField5_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = ValidField;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);
            char[] charArray = ValidField.ToCharArray();

            // Act
            csvStream.WriteField(charArray);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField5_WithFieldValueNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            const string expected = NeedsEscapingFieldExpected;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);
            char[] charArray = NeedsEscapingField.ToCharArray();

            // Act
            csvStream.WriteField(charArray);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField5_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteField((char[]) null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField5_WithEmptyFieldValue_ShouldWriteEmptyField()
        {
            // Arrange
            string expected = string.Empty;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);
            char[] charArray = string.Empty.ToCharArray();

            // Act
            csvStream.WriteField(charArray);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync5_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = ValidField;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);
            char[] charArray = ValidField.ToCharArray();

            // Act
            await csvStream.WriteFieldAsync(charArray);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync5_WithFieldValueNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            const string expected = NeedsEscapingFieldExpected;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);
            char[] charArray = NeedsEscapingField.ToCharArray();

            // Act
            await csvStream.WriteFieldAsync(charArray);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync5_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteFieldAsync((char[]) null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync5_WithEmptyFieldValue_ShouldWriteEmptyField()
        {
            // Arrange
            string expected = string.Empty;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);
            char[] charArray = string.Empty.ToCharArray();

            // Act
            await csvStream.WriteFieldAsync(charArray);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField6_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "a";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteField('a');

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField6_WithFieldValueNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            const string expected = "\",\"";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteField(',');

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync6_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "a";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldAsync('a');

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync6_WithFieldValueNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            const string expected = "\",\"";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldAsync(',');

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField7_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "1.5";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteField(1.5f);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync7_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "1.5";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldAsync(1.5f);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField8_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "True";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteField(true);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync8_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "True";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldAsync(true);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField9_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "1";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteField(1ul);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync9_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "1";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldAsync(1ul);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField10_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "1";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteField(1u);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync10_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "1";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldAsync(1u);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField11_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "1";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteField(1L);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync11_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "1";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldAsync(1L);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField12_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "1";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteField(1);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync12_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "1";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldAsync(1);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField13_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "1.5";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteField(1.5);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync13_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "1.5";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldAsync(1.5);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteField14_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "1.5";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteField((decimal) 1.5);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldAsync14_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            const string expected = "1.5";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldAsync((decimal) 1.5);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFields_WithValidFieldValues_ShouldWriteField()
        {
            // Arrange
            string expected = $"{ValidField},{ValidField}";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteFields(new[] { ValidField, ValidField });

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFields_WithFieldValuesNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            string expected = $"{NeedsEscapingFieldExpected},{NeedsEscapingFieldExpected}";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteFields(new[] { NeedsEscapingField, NeedsEscapingField });

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFields_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteFields((string[]) null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFields_WithNullValues_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteFields(new string[] { null, null });

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldsAsync_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            string expected = $"{ValidField},{ValidField}";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldsAsync(new[] { ValidField, ValidField });

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldsAsync_WithFieldValueNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            string expected = $"{NeedsEscapingFieldExpected},{NeedsEscapingFieldExpected}";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldsAsync(new[] { NeedsEscapingField, NeedsEscapingField });

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldsAsync_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteFieldsAsync((string[]) null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldsAsync_WithNullValues_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteFieldsAsync(new string[] { null, null });

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFields2_WithValidFieldValues_ShouldWriteField()
        {
            // Arrange
            string expected = $"{ValidField},{ValidField}";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteFields(ValidField, ValidField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFields2_WithFieldValuesNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            string expected = $"{NeedsEscapingFieldExpected},{NeedsEscapingFieldExpected}";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteFields(NeedsEscapingField, NeedsEscapingField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFields2_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteFields((string) null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFields2_WithNullValues_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteFields(null, null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFields2_WithNullStringValues_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteFields(null, null, null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFields2_WithValidFirstStringAndNullStringValue_ShouldWriteOneField()
        {
            // Arrange
            const string expected = ValidField;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteFields(ValidField, (string) null);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFields2Async_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            string expected = $"{ValidField},{ValidField}";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldsAsync(ValidField, ValidField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldsAsync2_WithFieldValueNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            string expected = $"{NeedsEscapingFieldExpected},{NeedsEscapingFieldExpected}";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldsAsync(NeedsEscapingField, NeedsEscapingField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldsAsync2_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteFieldsAsync((string) null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldsAsync2_WithNullValues_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteFieldsAsync(null, null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldsAsync2_WithNullStringValues_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteFieldsAsync(null, null, null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldsAsync2_WithValidFirstStringAndNullStringValue_ShouldWriteOneField()
        {
            // Arrange
            const string expected = ValidField;
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldsAsync(ValidField, (string) null);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFieldRow_WithValidFieldValues_ShouldWriteField()
        {
            // Arrange
            string expected = $"{ValidField},{ValidField}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteFieldRow(new[] { ValidField, ValidField });

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFieldRow_WithFieldValuesNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            string expected = $"{NeedsEscapingFieldExpected},{NeedsEscapingFieldExpected}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteFieldRow(new[] { NeedsEscapingField, NeedsEscapingField });

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFieldRow_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteFieldRow((string[]) null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFieldRow_WithNullValues_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteFieldRow(new string[] { null, null });

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldRowAsync_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            string expected = $"{ValidField},{ValidField}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldRowAsync(new[] { ValidField, ValidField });

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldRowAsync_WithFieldValueNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            string expected = $"{NeedsEscapingFieldExpected},{NeedsEscapingFieldExpected}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldRowAsync(new[] { NeedsEscapingField, NeedsEscapingField });

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldRowAsync_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteFieldRowAsync((string[]) null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldRowAsync_WithNullValues_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteFieldRowAsync(new string[] { null, null });

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFieldRow2_WithValidFieldValues_ShouldWriteField()
        {
            // Arrange
            string expected = $"{ValidField},{ValidField}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteFieldRow(ValidField, ValidField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFieldRow2_WithFieldValuesNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            string expected = $"{NeedsEscapingFieldExpected},{NeedsEscapingFieldExpected}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteFieldRow(NeedsEscapingField, NeedsEscapingField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFieldRow2_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteFieldRow((string) null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFieldRow2_WithNullValues_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteFieldRow(null, null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFieldRow2_WithNullStringValues_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            csvStream.WriteFieldRow(null, null, null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteFieldRow2_WithValidFirstStringAndNullStringValue_ShouldWriteOneField()
        {
            // Arrange
            string expected = $"{ValidField}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            csvStream.WriteFieldRow(ValidField, (string) null);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldRow2Async_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            string expected = $"{ValidField},{ValidField}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldRowAsync(ValidField, ValidField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldRowAsync2_WithFieldValueNeedingEscaping_ShouldWriteEscapedField()
        {
            // Arrange
            string expected = $"{NeedsEscapingFieldExpected},{NeedsEscapingFieldExpected}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldRowAsync(NeedsEscapingField, NeedsEscapingField);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldRowAsync2_WithNullValue_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteFieldRowAsync((string) null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldRowAsync2_WithNullValues_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteFieldRowAsync(null, null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldRowAsync2_WithNullStringValues_ShouldDoNothing()
        {
            // Arrange
            const int expected = 0;
            using CsvStreamWriter csvStream = GetDefaultInstance();

            // Act
            await csvStream.WriteFieldRowAsync(null, null, null);

            int actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WriteFieldRowAsync2_WithValidFirstStringAndNullStringValue_ShouldWriteOneField()
        {
            // Arrange
            string expected = $"{ValidField}\r\n";
            using CsvStreamWriter csvStream = GetDefaultInstance(out MemoryStream memoryStream);

            // Act
            await csvStream.WriteFieldRowAsync(ValidField, (string) null);

            string actual = GetString(memoryStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        //Utility functions

        private static string GetString(Stream stream)
        {
            long initialPosition = stream.Position;

            //Reset to beginning
            stream.Position = 0;

            using var streamReader = new StreamReader(stream, leaveOpen: true);
            string text = streamReader.ReadToEnd();

            stream.Position = initialPosition;

            return text;
        }

        private static CsvStreamWriter GetDefaultInstance(IntegrityMode integrityMode = IntegrityMode.Strict)
        {
            return new CsvStreamWriter(GetDefaultStream(),
                formatProvider: _englishUsCulture,
                integrityMode: integrityMode);
        }

        private static CsvStreamWriter GetDefaultInstance(out MemoryStream underlyingStream,
            IntegrityMode integrityMode = IntegrityMode.Strict)
        {
            StreamWriter streamWriter = GetDefaultStreamWriter(out underlyingStream);
            return new CsvStreamWriter(streamWriter,
                formatProvider: _englishUsCulture,
                integrityMode: integrityMode);
        }

        private static StreamWriter GetDefaultStreamWriter()
        {
            return new StreamWriter(GetDefaultStream()) { AutoFlush = true };
        }

        private static StreamWriter GetDefaultStreamWriter(out MemoryStream underlyingStream)
        {
            underlyingStream = GetDefaultStream();
            return new StreamWriter(underlyingStream) { AutoFlush = true };
        }

        private static MemoryStream GetDefaultStream()
        {
            return new MemoryStream();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Ncl.Common.Csv.Tests
{
    public class CsvStreamWriterTests
    {
        private const string ValidHeader = "header";
        private const string NeedsEscapingHeader = "header, more";

        private const string ValidField = "field";
        private const string NeedsEscapingField = "field, more";

        private static readonly CultureInfo _englishUsCulture = new("en-US");

        [Fact]
        public void CsvStreamWriter_WithValidArguments_ShouldCreateInstance()
        {
            // Arrange
            using var stream = new StreamWriter(new MemoryStream());

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
                _ = new CsvStreamWriter((TextWriter) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(TestCode);
        }

        [Fact]
        public void CsvStreamWriter_WithInvalidSeparator_ShouldThrowException()
        {
            // Arrange
            using StreamWriter stream = GetDefaultStream();

            // Act
            void TestCode()
            {
                _ = new CsvStreamWriter(stream, separator: '\n');
            }

            // Assert
            Assert.Throws<ArgumentException>(TestCode);
        }

        [Fact]
        public void CsvStreamWriter1_WithNullString_ShouldThrowException()
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
        public void CsvStreamWriter1_WithInvalidSeparator_ShouldThrowException()
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
            using StreamWriter stream = GetDefaultStream();

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
                _ = CsvStreamWriter.Create((TextWriter) null);
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
                _ = CsvStreamWriter.Create((TextWriter) null, separator: '\n');
            }

            // Assert
            Assert.Throws<ArgumentException>(TestCode);
        }

        [Fact]
        public void Create1_WithValidArguments_ShouldReturnInstance()
        {
            // Arrange
            using StreamWriter stream = GetDefaultStream();

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
            _ = CsvStreamWriter.Create((TextWriter) null, out Exception actual);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }

        [Fact]
        public void Create1_WithInvalidSeparator_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            _ = CsvStreamWriter.Create((TextWriter) null, out Exception actual, separator: '\n');

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }

        [Fact]
        public void TryCreate_WithValidArguments_ShouldReturnTrue()
        {
            // Arrange
            using StreamWriter stream = GetDefaultStream();

            // Act
            bool actual = CsvStreamWriter.TryCreate(stream, out Exception _, out CsvStreamWriter _);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void TryCreate_WithValidArguments_ShouldSetInstanceArgument()
        {
            // Arrange
            using StreamWriter stream = GetDefaultStream();

            // Act
            bool _ = CsvStreamWriter.TryCreate(stream, out Exception _, out CsvStreamWriter actual);

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void TryCreate_WithValidArguments_ShouldSetExceptionArgumentToNull()
        {
            // Arrange
            using StreamWriter stream = GetDefaultStream();

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
            bool actual = CsvStreamWriter.TryCreate((TextWriter) null, out Exception _, out CsvStreamWriter _);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate_WithNullStream_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((TextWriter) null, out Exception _, out CsvStreamWriter actual);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate_WithNullStream_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((TextWriter) null, out Exception actual, out CsvStreamWriter _);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }

        [Fact]
        public void TryCreate_WithInvalidSeparator_ShouldReturnFalse()
        {
            // Arrange
            // Act
            bool actual = CsvStreamWriter.TryCreate((TextWriter) null, out Exception _, out CsvStreamWriter _,
                separator: '\n');

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate_WithInvalidSeparator_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((TextWriter) null, out Exception _, out CsvStreamWriter actual,
                separator: '\n');

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate_WithInvalidSeparator_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((TextWriter) null, out Exception actual, out CsvStreamWriter _,
                separator: '\n');

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }

        [Fact]
        public void Create2_WithNullString_ShouldThrowException()
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
        public void Create2_WithInvalidSeparator_ShouldThrowException()
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
        public void Create3_WithNullString_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            _ = CsvStreamWriter.Create((string) null, out Exception actual);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }

        [Fact]
        public void Create3_WithInvalidSeparator_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            _ = CsvStreamWriter.Create((string) null, out Exception actual, separator: '\n');

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }

        [Fact]
        public void TryCreate1_WithNullString_ShouldReturnFalse()
        {
            // Arrange
            // Act
            bool actual = CsvStreamWriter.TryCreate((string) null, out Exception _, out CsvStreamWriter _);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate1_WithNullString_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((string) null, out Exception _, out CsvStreamWriter actual);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate1_WithNullString_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((string) null, out Exception actual, out CsvStreamWriter _);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }

        [Fact]
        public void TryCreate1_WithInvalidSeparator_ShouldReturnFalse()
        {
            // Arrange
            // Act
            bool actual = CsvStreamWriter.TryCreate((string) null, out Exception _, out CsvStreamWriter _,
                separator: '\n');

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate1_WithInvalidSeparator_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamWriter.TryCreate((string) null, out Exception _, out CsvStreamWriter actual,
                separator: '\n');

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate1_WithInvalidSeparator_ShouldSetExceptionArgument()
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
            string expected = $"\"{NeedsEscapingHeader}\"";
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
            string expected = $"\"{NeedsEscapingHeader}\"";
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
        public void WriteRowEnd_WithUnmatchedFieldsAndLooseMode_ShouldFillMissingComma()
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
        public async Task WriteRowEndAsync_WithUnmatchedFieldsAndLooseMode_ShouldFillMissingComma()
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
            string expected = $"{ValidField}";
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
            string expected = $"\"{NeedsEscapingField}\"";
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
        public async Task WriteFieldAsync_WithValidFieldValue_ShouldWriteField()
        {
            // Arrange
            string expected = $"{ValidField}";
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
            string expected = $"\"{NeedsEscapingField}\"";
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
            StreamWriter streamWriter = GetDefaultStream(out underlyingStream);
            return new CsvStreamWriter(streamWriter,
                formatProvider: _englishUsCulture,
                integrityMode: integrityMode);
        }

        private static StreamWriter GetDefaultStream()
        {
            return new StreamWriter(new MemoryStream()) { AutoFlush = true };
        }

        private static StreamWriter GetDefaultStream(out MemoryStream underlyingStream)
        {
            underlyingStream = new MemoryStream();
            return new StreamWriter(underlyingStream) { AutoFlush = true };
        }
    }
}
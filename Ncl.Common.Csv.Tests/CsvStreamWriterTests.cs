using System;
using System.Globalization;
using System.IO;
using Xunit;

namespace Ncl.Common.Csv.Tests
{
    public class CsvStreamWriterTests
    {
        private const string ValidHeader = "header";
        private const string NeedsEscapingHeader = "header, more";

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
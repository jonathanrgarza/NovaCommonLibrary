using System;
using System.IO;
using Xunit;

namespace Ncl.Common.Csv.Tests
{
    public class CsvStreamWriterTests
    {
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

        private static CsvStreamWriter GetDefaultInstance(IntegrityMode integrityMode = IntegrityMode.Strict)
        {
            return new CsvStreamWriter(GetDefaultStream(), integrityMode: integrityMode);
        }

        private static StreamWriter GetDefaultStream()
        {
            return new StreamWriter(new MemoryStream());
        }

        private static StreamWriter GetDefaultStream(out MemoryStream underlyingStream)
        {
            underlyingStream = new MemoryStream();
            return new StreamWriter(underlyingStream);
        }
    }
}
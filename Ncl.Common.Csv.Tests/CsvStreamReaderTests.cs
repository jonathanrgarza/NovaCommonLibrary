using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Xunit;

namespace Ncl.Common.Csv.Tests
{
    public class CsvStreamReaderTests
    {
        private const string ValidHeader = "header";
        private const string NeedsEscapingHeader = "header,\" more";
        private const string NeedsEscapingHeaderExpected = "\"header,\"\" more\"";

        private const string ValidField = "field";
        private const string NeedsEscapingField = "field,\" more";
        private const string NeedsEscapingFieldExpected = "\"field,\"\" more\"";

        private const string DefaultCsvContent = "field1,field2\r\nfield3, field4";

        private const string SimpleFormat = "{0}";

        private static readonly CultureInfo _englishUsCulture = new("en-US");
        private static readonly CultureInfo _spanishCulture = new("es-ES");

        [Fact]
        public void CsvStreamReader_WithValidArguments_ShouldCreateInstance()
        {
            // Arrange
            using var stream = new MemoryStream();

            // Act
            var actual = new CsvStreamReader(stream);

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void CsvStreamReader_WithNullStream_ShouldThrowException()
        {
            // Arrange

            // Act
            void TestCode()
            {
                _ = new CsvStreamReader((Stream) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>("stream", TestCode);
        }

        [Fact]
        public void CsvStreamReader_WithInvalidSeparator_ShouldThrowException()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            void TestCode()
            {
                _ = new CsvStreamReader(stream, separator: '\n');
            }

            // Assert
            Assert.Throws<ArgumentException>("separator", TestCode);
        }
        
        [Fact]
        public void CsvStreamReader_WithNullNewLine_ShouldThrowException()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            void TestCode()
            {
                _ = new CsvStreamReader(stream, newLine:null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>("newLine", TestCode);
        }
        
        [Fact]
        public void CsvStreamReader_WithEmptyNewLine_ShouldThrowException()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            void TestCode()
            {
                _ = new CsvStreamReader(stream, newLine:"");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }

        [Fact]
        public void CsvStreamReader_WithTooLongNewLine_ShouldThrowException()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            void TestCode()
            {
                _ = new CsvStreamReader(stream, newLine:"\r\n\n");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }
        
        [Fact]
        public void CsvStreamReader_WithDoubleQuoteNewLine_ShouldThrowException()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            void TestCode()
            {
                _ = new CsvStreamReader(stream, newLine:"\r\"");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }

        [Fact]
        public void CsvStreamReader1_WithValidArguments_ShouldCreateInstance()
        {
            // Arrange
            using var stream = new StreamReader(new MemoryStream());

            // Act
            var actual = new CsvStreamReader(stream);

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void CsvStreamReader1_WithNullStream_ShouldThrowException()
        {
            // Arrange

            // Act
            void TestCode()
            {
                _ = new CsvStreamReader((TextReader) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>("stream", TestCode);
        }

        [Fact]
        public void CsvStreamReader1_WithInvalidSeparator_ShouldThrowException()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            void TestCode()
            {
                _ = new CsvStreamReader(stream, separator: '\n');
            }

            // Assert
            Assert.Throws<ArgumentException>("separator", TestCode);
        }
        
        [Fact]
        public void CsvStreamReader1_WithNullNewLine_ShouldThrowException()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            void TestCode()
            {
                _ = new CsvStreamReader(stream, newLine:null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>("newLine", TestCode);
        }
        
        [Fact]
        public void CsvStreamReader1_WithEmptyNewLine_ShouldThrowException()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            void TestCode()
            {
                _ = new CsvStreamReader(stream, newLine:"");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }

        [Fact]
        public void CsvStreamReader1_WithTooLongNewLine_ShouldThrowException()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            void TestCode()
            {
                _ = new CsvStreamReader(stream, newLine:"\r\n\n");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }
        
        [Fact]
        public void CsvStreamReader1_WithDoubleQuoteNewLine_ShouldThrowException()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            void TestCode()
            {
                _ = new CsvStreamReader(stream, newLine:"\r\"");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }

        [Fact]
        public void CsvStreamReader2_WithNullString_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = new CsvStreamReader((string) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>("path", TestCode);
        }

        [Fact]
        public void CsvStreamReader2_WithInvalidSeparator_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = new CsvStreamReader((string) null, separator: '\n');
            }

            // Assert
            Assert.Throws<ArgumentException>("separator", TestCode);
        }
        
        [Fact]
        public void CsvStreamReader2_WithNullNewLine_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = new CsvStreamReader((string) null, newLine:null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>("newLine", TestCode);
        }
        
        [Fact]
        public void CsvStreamReader2_WithEmptyNewLine_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = new CsvStreamReader((string) null, newLine:"");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }

        [Fact]
        public void CsvStreamReader2_WithTooLongNewLine_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = new CsvStreamReader((string) null, newLine:"\r\n\n");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }
        
        [Fact]
        public void CsvStreamReader2_WithDoubleQuoteNewLine_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = new CsvStreamReader((string) null, newLine:"\r\"");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }

        [Fact]
        public void Create_WithValidArguments_ShouldReturnInstance()
        {
            // Arrange
            using Stream stream = GetDefaultStream();

            // Act
            var actual = CsvStreamReader.Create(stream);

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
                _ = CsvStreamReader.Create((Stream) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>("stream", TestCode);
        }

        [Fact]
        public void Create_WithInvalidSeparator_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = CsvStreamReader.Create((Stream) null, separator: '\n');
            }

            // Assert
            Assert.Throws<ArgumentException>("separator", TestCode);
        }
        
        [Fact]
        public void Create_WithNullNewLine_ShouldThrowException()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            void TestCode()
            {
                _ = CsvStreamReader.Create(stream, newLine:null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>("newLine", TestCode);
        }
        
        [Fact]
        public void Create_WithEmptyNewLine_ShouldThrowException()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            void TestCode()
            {
                _ = CsvStreamReader.Create(stream, newLine:"");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }

        [Fact]
        public void Create_WithTooLongNewLine_ShouldThrowException()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            void TestCode()
            {
                _ = CsvStreamReader.Create(stream, newLine:"\r\n\n");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }
        
        [Fact]
        public void Create_WithDoubleQuoteNewLine_ShouldThrowException()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            void TestCode()
            {
                _ = CsvStreamReader.Create(stream, newLine:"\r\"");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }

        [Fact]
        public void Create1_WithValidArguments_ShouldReturnInstance()
        {
            // Arrange
            using Stream stream = GetDefaultStream();

            // Act
            var actual = CsvStreamReader.Create(stream, out Exception _);

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void Create1_WithNullStream_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            _ = CsvStreamReader.Create((Stream) null, out Exception actual);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }

        [Fact]
        public void Create1_WithInvalidSeparator_ShouldSetExceptionArgument()
        {
            // Arrange
            using Stream stream = GetDefaultStream();
            
            // Act
            _ = CsvStreamReader.Create(stream, out Exception actual, separator: '\n');

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }
        
        [Fact]
        public void Create1_WithNullNewLine_ShouldThrowException()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            _ = CsvStreamReader.Create(stream, out Exception actual, newLine: null);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }
        
        [Fact]
        public void Create1_WithEmptyNewLine_ShouldThrowException()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            _ = CsvStreamReader.Create(stream, out Exception actual, newLine: "");

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }

        [Fact]
        public void Create1_WithTooLongNewLine_ShouldThrowException()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            _ = CsvStreamReader.Create(stream, out Exception actual, newLine: "\r\n\n");

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }
        
        [Fact]
        public void Create1_WithDoubleQuoteNewLine_ShouldThrowException()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            _ = CsvStreamReader.Create(stream, out Exception actual, newLine: "\r\"");

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }

        [Fact]
        public void TryCreate_WithValidArguments_ShouldReturnTrue()
        {
            // Arrange
            using Stream stream = GetDefaultStream();

            // Act
            bool actual = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader _);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void TryCreate_WithValidArguments_ShouldSetInstanceArgument()
        {
            // Arrange
            using Stream stream = GetDefaultStream();

            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader actual);

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void TryCreate_WithValidArguments_ShouldSetExceptionArgumentToNull()
        {
            // Arrange
            using Stream stream = GetDefaultStream();

            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception actual, out CsvStreamReader _);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate_WithNullStream_ShouldReturnFalse()
        {
            // Arrange
            // Act
            bool actual = CsvStreamReader.TryCreate((Stream) null, out Exception _, out CsvStreamReader _);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate_WithNullStream_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamReader.TryCreate((Stream) null, out Exception _, out CsvStreamReader actual);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate_WithNullStream_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            bool _ = CsvStreamReader.TryCreate((Stream) null, out Exception actual, out CsvStreamReader _);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }

        [Fact]
        public void TryCreate_WithInvalidSeparator_ShouldReturnFalse()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();
            
            // Act
            bool actual = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader _,
                separator: '\n');

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate_WithInvalidSeparator_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();
            
            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader actual,
                separator: '\n');

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate_WithInvalidSeparator_ShouldSetExceptionArgument()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();
            
            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception actual, out CsvStreamReader _,
                separator: '\n');

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }
        
        [Fact]
        public void TryCreate_WithNullNewLine_ShouldReturnFalse()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();
            
            // Act
            bool actual = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader _,
                newLine: null);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate_WithNullNewLine_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();
            
            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader actual,
                newLine: null);

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void TryCreate_WithNullNewLine_ShouldSetExceptionArgument()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception actual, out CsvStreamReader _,
                newLine: null);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }
        
        [Fact]
        public void TryCreate_WithEmptyNewLine_ShouldReturnFalse()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();
            
            // Act
            bool actual = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader _,
                newLine: "");

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate_WithEmptyNewLine_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();
            
            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader actual,
                newLine: "");

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void TryCreate_WithEmptyNewLine_ShouldSetExceptionArgument()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception actual, out CsvStreamReader _,
                newLine: "");

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }
        
        [Fact]
        public void TryCreate_WithTooLongNewLine_ShouldReturnFalse()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();
            
            // Act
            bool actual = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader _,
                newLine: "\r\n\n");

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate_WithTooLongNewLine_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();
            
            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader actual,
                newLine: "\r\n\n");

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void TryCreate_WithTooLongNewLine_ShouldSetExceptionArgument()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception actual, out CsvStreamReader _,
                newLine: "\r\n\n");

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }
        
        [Fact]
        public void TryCreate_WithDoubleQuoteNewLine_ShouldReturnFalse()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();
            
            // Act
            bool actual = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader _,
                newLine: "\r\"");

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate_WithDoubleQuoteNewLine_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();
            
            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader actual,
                newLine: "\r\"");

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void TryCreate_WithDoubleQuoteNewLine_ShouldSetExceptionArgument()
        {
            // Arrange
            using MemoryStream stream = GetDefaultStream();

            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception actual, out CsvStreamReader _,
                newLine: "\r\"");

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }

        [Fact]
        public void Create2_WithValidArguments_ShouldReturnInstance()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            var actual = CsvStreamReader.Create(stream);

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
                _ = CsvStreamReader.Create((TextReader) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>("stream", TestCode);
        }

        [Fact]
        public void Create2_WithInvalidSeparator_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = CsvStreamReader.Create((TextReader) null, separator: '\n');
            }

            // Assert
            Assert.Throws<ArgumentException>("separator", TestCode);
        }
        
        [Fact]
        public void Create2_WithNullNewLine_ShouldThrowException()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            void TestCode()
            {
                _ = CsvStreamReader.Create(stream, newLine:null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>("newLine", TestCode);
        }
        
        [Fact]
        public void Create2_WithEmptyNewLine_ShouldThrowException()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            void TestCode()
            {
                _ = CsvStreamReader.Create(stream, newLine:"");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }

        [Fact]
        public void Create2_WithTooLongNewLine_ShouldThrowException()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            void TestCode()
            {
                _ = CsvStreamReader.Create(stream, newLine:"\r\n\n");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }
        
        [Fact]
        public void Create2_WithDoubleQuoteNewLine_ShouldThrowException()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            void TestCode()
            {
                _ = CsvStreamReader.Create(stream, newLine:"\r\"");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }

        [Fact]
        public void Create3_WithValidArguments_ShouldReturnInstance()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            var actual = CsvStreamReader.Create(stream, out Exception _);

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void Create3_WithNullStream_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            _ = CsvStreamReader.Create((TextReader) null, out Exception actual);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }

        [Fact]
        public void Create3_WithInvalidSeparator_ShouldSetExceptionArgument()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();
            
            // Act
            _ = CsvStreamReader.Create(stream, out Exception actual, separator: '\n');

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }
        
        [Fact]
        public void Create3_WithNullNewLine_ShouldThrowException()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            _ = CsvStreamReader.Create(stream, out Exception actual, newLine: null);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }
        
        [Fact]
        public void Create3_WithEmptyNewLine_ShouldThrowException()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            _ = CsvStreamReader.Create(stream, out Exception actual, newLine: "");

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }

        [Fact]
        public void Create3_WithTooLongNewLine_ShouldThrowException()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            _ = CsvStreamReader.Create(stream, out Exception actual, newLine: "\r\n\n");

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }
        
        [Fact]
        public void Create3_WithDoubleQuoteNewLine_ShouldThrowException()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            _ = CsvStreamReader.Create(stream, out Exception actual, newLine: "\r\"");

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }

        [Fact]
        public void TryCreate1_WithValidArguments_ShouldReturnTrue()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            bool actual = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader _);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void TryCreate1_WithValidArguments_ShouldSetInstanceArgument()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader actual);

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void TryCreate1_WithValidArguments_ShouldSetExceptionArgumentToNull()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception actual, out CsvStreamReader _);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate1_WithNullStream_ShouldReturnFalse()
        {
            // Arrange
            // Act
            bool actual = CsvStreamReader.TryCreate((TextReader) null, out Exception _, out CsvStreamReader _);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate1_WithNullStream_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamReader.TryCreate((TextReader) null, out Exception _, out CsvStreamReader actual);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate1_WithNullStream_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            bool _ = CsvStreamReader.TryCreate((TextReader) null, out Exception actual, out CsvStreamReader _);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }

        [Fact]
        public void TryCreate1_WithInvalidSeparator_ShouldReturnFalse()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();
            
            // Act
            bool actual = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader _,
                separator: '\n');

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate1_WithInvalidSeparator_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();
            
            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader actual,
                separator: '\n');

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate1_WithInvalidSeparator_ShouldSetExceptionArgument()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();
            
            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception actual, out CsvStreamReader _,
                separator: '\n');

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }
        
        [Fact]
        public void TryCreate1_WithNullNewLine_ShouldReturnFalse()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();
            
            // Act
            bool actual = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader _,
                newLine: null);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate1_WithNullNewLine_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();
            
            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader actual,
                newLine: null);

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void TryCreate1_WithNullNewLine_ShouldSetExceptionArgument()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception actual, out CsvStreamReader _,
                newLine: null);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }
        
        [Fact]
        public void TryCreate1_WithEmptyNewLine_ShouldReturnFalse()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();
            
            // Act
            bool actual = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader _,
                newLine: "");

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate1_WithEmptyNewLine_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();
            
            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader actual,
                newLine: "");

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void TryCreate1_WithEmptyNewLine_ShouldSetExceptionArgument()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception actual, out CsvStreamReader _,
                newLine: "");

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }
        
        [Fact]
        public void TryCreate1_WithTooLongNewLine_ShouldReturnFalse()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();
            
            // Act
            bool actual = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader _,
                newLine: "\r\n\n");

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate1_WithTooLongNewLine_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();
            
            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader actual,
                newLine: "\r\n\n");

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void TryCreate1_WithTooLongNewLine_ShouldSetExceptionArgument()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception actual, out CsvStreamReader _,
                newLine: "\r\n\n");

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }
        
        [Fact]
        public void TryCreate1_WithDoubleQuoteNewLine_ShouldReturnFalse()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();
            
            // Act
            bool actual = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader _,
                newLine: "\r\"");

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate1_WithDoubleQuoteNewLine_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();
            
            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception _, out CsvStreamReader actual,
                newLine: "\r\"");

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void TryCreate1_WithDoubleQuoteNewLine_ShouldSetExceptionArgument()
        {
            // Arrange
            using StreamReader stream = GetDefaultStreamReader();

            // Act
            bool _ = CsvStreamReader.TryCreate(stream, out Exception actual, out CsvStreamReader _,
                newLine: "\r\"");

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }

        [Fact]
        public void Create4_WithNullString_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = CsvStreamReader.Create((string) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>("path", TestCode);
        }

        [Fact]
        public void Create4_WithInvalidSeparator_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = CsvStreamReader.Create((string) null, separator: '\n');
            }

            // Assert
            Assert.Throws<ArgumentException>("separator", TestCode);
        }
        
        [Fact]
        public void Create4_WithNullNewLine_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = CsvStreamReader.Create((string) null, newLine:null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>("newLine", TestCode);
        }
        
        [Fact]
        public void Create4_WithEmptyNewLine_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = CsvStreamReader.Create((string) null, newLine:"");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }

        [Fact]
        public void Create4_WithTooLongNewLine_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = CsvStreamReader.Create((string) null, newLine:"\r\n\n");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }
        
        [Fact]
        public void Create4_WithDoubleQuoteNewLine_ShouldThrowException()
        {
            // Arrange
            // Act
            void TestCode()
            {
                _ = CsvStreamReader.Create((string) null, newLine:"\r\"");
            }

            // Assert
            Assert.Throws<ArgumentException>("newLine", TestCode);
        }

        [Fact]
        public void Create4_WithNullString_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            _ = CsvStreamReader.Create((string) null, out Exception actual);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }

        [Fact]
        public void Create4_WithInvalidSeparator_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            _ = CsvStreamReader.Create((string) null, out Exception actual, separator: '\n');

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }

        [Fact]
        public void TryCreate2_WithNullString_ShouldReturnFalse()
        {
            // Arrange
            // Act
            bool actual = CsvStreamReader.TryCreate((string) null, out Exception _, out CsvStreamReader _);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate2_WithNullString_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamReader.TryCreate((string) null, out Exception _, out CsvStreamReader actual);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate2_WithNullString_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            bool _ = CsvStreamReader.TryCreate((string) null, out Exception actual, out CsvStreamReader _);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }

        [Fact]
        public void TryCreate2_WithInvalidSeparator_ShouldReturnFalse()
        {
            // Arrange
            // Act
            bool actual = CsvStreamReader.TryCreate((string) null, out Exception _, out CsvStreamReader _,
                separator: '\n');

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate2_WithInvalidSeparator_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamReader.TryCreate((string) null, out Exception _, out CsvStreamReader actual,
                separator: '\n');

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void TryCreate2_WithInvalidSeparator_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            bool _ = CsvStreamReader.TryCreate((string) null, out Exception actual, out CsvStreamReader _,
                separator: '\n');

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }
        
        [Fact]
        public void TryCreate2_WithNullNewLine_ShouldReturnFalse()
        {
            // Arrange
            // Act
            bool actual = CsvStreamReader.TryCreate((string) null, out Exception _, out CsvStreamReader _,
                newLine: null);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate2_WithNullNewLine_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamReader.TryCreate((string) null, out Exception _, out CsvStreamReader actual,
                newLine: null);

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void TryCreate2_WithNullNewLine_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            bool _ = CsvStreamReader.TryCreate((string) null, out Exception actual, out CsvStreamReader _,
                newLine: null);

            // Assert
            Assert.IsType<ArgumentNullException>(actual);
        }
        
        [Fact]
        public void TryCreate2_WithEmptyNewLine_ShouldReturnFalse()
        {
            // Arrange
            // Act
            bool actual = CsvStreamReader.TryCreate((string) null, out Exception _, out CsvStreamReader _,
                newLine: "");

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate2_WithEmptyNewLine_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamReader.TryCreate((string) null, out Exception _, out CsvStreamReader actual,
                newLine: "");

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void TryCreate2_WithEmptyNewLine_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            bool _ = CsvStreamReader.TryCreate((string) null, out Exception actual, out CsvStreamReader _,
                newLine: "");

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }
        
        [Fact]
        public void TryCreate2_WithTooLongNewLine_ShouldReturnFalse()
        {
            // Arrange
            // Act
            bool actual = CsvStreamReader.TryCreate((string) null, out Exception _, out CsvStreamReader _,
                newLine: "\r\n\n");

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate2_WithTooLongNewLine_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamReader.TryCreate((string) null, out Exception _, out CsvStreamReader actual,
                newLine: "\r\n\n");

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void TryCreate2_WithTooLongNewLine_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            bool _ = CsvStreamReader.TryCreate((string) null, out Exception actual, out CsvStreamReader _,
                newLine: "\r\n\n");

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }
        
        [Fact]
        public void TryCreate2_WithDoubleQuoteNewLine_ShouldReturnFalse()
        {
            // Arrange
            // Act
            bool actual = CsvStreamReader.TryCreate((string) null, out Exception _, out CsvStreamReader _,
                newLine: "\r\"");

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryCreate2_WithDoubleQuoteNewLine_ShouldSetInstanceArgumentToNull()
        {
            // Arrange
            // Act
            bool _ = CsvStreamReader.TryCreate((string) null, out Exception _, out CsvStreamReader actual,
                newLine: "\r\"");

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void TryCreate2_WithDoubleQuoteNewLine_ShouldSetExceptionArgument()
        {
            // Arrange
            // Act
            bool _ = CsvStreamReader.TryCreate((string) null, out Exception actual, out CsvStreamReader _,
                newLine: "\r\"");

            // Assert
            Assert.IsType<ArgumentException>(actual);
        }
        
        [Fact]
        public void RowsRead_WithNothingRead_ShouldReturnZero()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance();

            // Act

            // Assert
            Assert.Equal(0, csvStream.RowsRead);
        }

        [Fact]
        public void RowsRead_WithFieldsReadButNotFinishRow_ShouldReturnZero()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance("Field1,Field2\r\nField3,Field4");

            // Act
            csvStream.ReadField();
            csvStream.ReadField();

            // Assert
            Assert.Equal(0, csvStream.RowsRead);
        }
        
        [Fact]
        public void RowsRead_WithFieldsReadAndFinishedRow_ShouldReturnOne()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance("Field1,Field2\r\nField3,Field4");

            // Act
            csvStream.ReadField();
            csvStream.ReadField();
            csvStream.ReadField();

            // Assert
            Assert.Equal(1, csvStream.RowsRead);
        }

        [Fact]
        public void FormatProvider_WithNullValue_ShouldSetToThreadsCurrentCulture()
        {
            IFormatProvider expected = Thread.CurrentThread.CurrentCulture;
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance();
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
            using CsvStreamReader csvStream = GetDefaultInstance();

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
            using CsvStreamReader csvStream = GetDefaultInstance();
            csvStream.FormatProvider = _spanishCulture;

            // Act
            csvStream.FormatProvider = _englishUsCulture;

            IFormatProvider actual = csvStream.FormatProvider;

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

        private static CsvStreamReader GetDefaultInstance()
        {
            return new CsvStreamReader(GetDefaultStream(),
                formatProvider: _englishUsCulture);
        }

        private static CsvStreamReader GetDefaultInstance(string csvContent)
        {
            return new CsvStreamReader(GetDefaultStream(csvContent),
                formatProvider: _englishUsCulture);
        }

        private static StreamReader GetDefaultStreamReader()
        {
            return new StreamReader(GetDefaultStream());
        }

        private static StreamReader GetDefaultStreamReader(string csvContent)
        {
            return new StreamReader(GetDefaultStream(csvContent));
        }

        private static StreamReader GetDefaultStreamReader(string csvContent, out MemoryStream underlyingStream)
        {
            underlyingStream = GetDefaultStream(csvContent);
            return new StreamReader(underlyingStream);
        }

        private static MemoryStream GetDefaultStream(string csvContent)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(csvContent));
        }

        private static MemoryStream GetDefaultStream()
        {
            return new MemoryStream();
        }
    }
}
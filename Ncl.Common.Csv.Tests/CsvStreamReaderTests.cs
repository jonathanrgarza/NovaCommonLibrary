using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ncl.Common.Csv.Tests
{
    public class CsvStreamReaderTests
    {
        private const string ValidFieldBase = "field";
        private const string ValidField = "field1";
        private const string ValidFieldWithNewLine = "field1\r\n";
        private const string ValidFieldsTwoRows = "field1,field2\r\nfield3,field4";
        
        private const string EscapedField = "\"field1,\"\" more\"";
        private const string EscapedFieldExpected = "field1,\" more";
        
        private const string EscapedFieldWithNewLine = "\"field1,\"\" more\"\r\n";
        private const string EscapedFieldWithNewLineExpected = "field1,\" more";
        
        private const string EscapedFieldsTwoRows = 
            "\"field1,\"\" more\",\"field2,\"\" more\"\r\n\"field3,\"\" more\",\"field4,\"\" more\"";
        private const string EscapedFieldTwoRowsExpected = 
            "field1,\" more,field2,\" more\r\nfield3,\" more,field4,\" more";

        private const string DefaultCsvContent = ValidFieldsTwoRows;

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
        public void EndOfStream_WithNoReadsAndEOF_ShouldReturnTrue()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance();

            // Act
            bool actual = csvStream.EndOfStream;

            // Assert
            Assert.True(actual);
        }
        
        [Fact]
        public void EndOfStream_WithNoReadsAndNotEOF_ShouldReturnFalse()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidField);

            // Act
            bool actual = csvStream.EndOfStream;

            // Assert
            Assert.False(actual);
        }
        
        [Fact]
        public void EndOfStream_WithOneReadAndNewLineEnding_ShouldReturnTrue()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
            csvStream.ReadField();
            
            // Act
            bool actual = csvStream.EndOfStream;

            // Assert
            Assert.True(actual);
        }
        
        [Fact]
        public void EndOfStream_WithOneReadAndEOF_ShouldReturnTrue()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidField);
            csvStream.ReadField();
            
            // Act
            bool actual = csvStream.EndOfStream;

            // Assert
            Assert.True(actual);
        }
        
        [Fact]
        public void EndOfStream_WithDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            CsvStreamReader csvStream = GetDefaultInstance(ValidField);
            csvStream.Dispose();
            
            // Act
            void TestCode()
            {
                _ = csvStream.EndOfStream;
            }

            // Assert
            Assert.Throws<ObjectDisposedException>(TestCode);
        }
        
        [Fact]
        public void FirstRowRead_WithNothingRead_ShouldReturnFalse()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidField);

            // Act
            bool actual = csvStream.FirstRowRead;

            // Assert
            Assert.False(actual);
        }
        
        [Fact]
        public void FirstRowRead_WithReadButNotFinishFirstRow_ShouldReturnFalse()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            csvStream.ReadField();

            // Act
            bool actual = csvStream.FirstRowRead;

            // Assert
            Assert.False(actual);
        }
        
        [Fact]
        public void FirstRowRead_WithFirstRowRead_ShouldReturnTrue()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            csvStream.ReadField();
            csvStream.ReadField();

            // Act
            bool actual = csvStream.FirstRowRead;

            // Assert
            Assert.True(actual);
        }
        
        [Fact]
        public void FirstRowRead_WithFirstRowReadPlusAdditionalRead_ShouldReturnTrue()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            csvStream.ReadField();
            csvStream.ReadField();
            csvStream.ReadField();
            
            // Act
            bool actual = csvStream.FirstRowRead;

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void RowsRead_WithNothingRead_ShouldReturnZero()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance();

            // Act
            long actual = csvStream.RowsRead;

            // Assert
            Assert.Equal(0, actual);
        }

        [Fact]
        public void RowsRead_WithFieldsReadButNotFinishRow_ShouldReturnZero()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance("Field1,Field2\r\nField3,Field4");
            csvStream.ReadField();

            // Act
            long actual = csvStream.RowsRead;

            // Assert
            Assert.Equal(0, actual);
        }
        
        [Fact]
        public void RowsRead_WithFieldsReadAndFinishedRow_ShouldReturnOne()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance("Field1,Field2\r\nField3,Field4");
            csvStream.ReadField();
            csvStream.ReadField();
            csvStream.ReadField();
            
            // Act
            long actual = csvStream.RowsRead;

            // Assert
            Assert.Equal(1, actual);
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
        
        [Fact]
        public void FieldsRead_WithNoReads_ShouldReturnZero()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance();

            // Act
            long actual = csvStream.FieldsRead;

            // Assert
            Assert.Equal(0, actual);
        }
        
        [Fact]
        public void FieldsRead_WithOneRead_ShouldReturnOne()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            csvStream.ReadField();

            // Act
            long actual = csvStream.FieldsRead;

            // Assert
            Assert.Equal(1, actual);
        }
        
        [Fact]
        public void FieldsRead_WithReadsPastRowEnd_ShouldReturnTwo()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            csvStream.ReadField();
            csvStream.ReadField();

            // Act
            long actual = csvStream.FieldsRead;

            // Assert
            Assert.Equal(2, actual);
        }
        
        [Fact]
        public void FieldPosition_WithNoReads_ShouldReturnZero()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance();

            // Act
            long actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(0, actual);
        }
        
        [Fact]
        public void FieldPosition_WithOneRead_ShouldReturnOne()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            csvStream.ReadField();

            // Act
            long actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(1, actual);
        }
        
        [Fact]
        public void FieldPosition_WithReadsPastRowEnd_ShouldReturnZero()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            csvStream.ReadField();
            csvStream.ReadField();

            // Act
            long actual = csvStream.FieldPosition;

            // Assert
            Assert.Equal(0, actual);
        }
        
        [Fact]
        public void MaxFieldCount_WithNoReads_ShouldReturnZero()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance();

            // Act
            long actual = csvStream.MaxFieldCount;

            // Assert
            Assert.Equal(0, actual);
        }
        
        [Fact]
        public void MaxFieldCount_WithOneReadButRowNotDone_ShouldReturnOne()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            csvStream.ReadField();

            // Act
            long actual = csvStream.MaxFieldCount;

            // Assert
            Assert.Equal(0, actual);
        }
        
        [Fact]
        public void MaxFieldCount_WithReadsPastRowEnd_ShouldReturnTwo()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            csvStream.ReadField();
            csvStream.ReadField();

            // Act
            long actual = csvStream.MaxFieldCount;

            // Assert
            Assert.Equal(2, actual);
        }

        [Fact]
        public void ResetStreamPosition_WithOneFieldRead_ShouldMoveStreamPositionToBeginning()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            csvStream.ReadField();
            
            // Act
            csvStream.ResetStreamPosition();
            
            // Assert
            Assert.Equal(0, csvStream.BaseStream.Position);
            Assert.Equal(ValidField, csvStream.ReadField());
        }
        
        [Fact]
        public void ResetStreamPosition_WithOneFieldRead_ShouldResetProperties()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            csvStream.ReadField();
            
            // Act
            csvStream.ResetStreamPosition();
            
            // Assert
            Assert.Equal(0, csvStream.FieldPosition);
            Assert.Equal(0, csvStream.FieldsRead);
        }
        
        [Fact]
        public void ResetStreamPosition_WithUnSeekableStream_ShouldThrowNotSupportException()
        {
            // Arrange
            using var csvStream = new CsvStreamReader(new StringReader("field1,field2"));
            csvStream.ReadField();
            
            // Act
            void TestCode()
            {
                csvStream.ResetStreamPosition();
            }

            // Assert
            Assert.Throws<NotSupportedException>(TestCode);
        }
        
        [Fact]
        public void ResetStreamPosition_WithDisposedStream_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var csvStream = new CsvStreamReader(new StringReader("field1,field2"));
            csvStream.ReadField();
            csvStream.Dispose();
            
            // Act
            void TestCode()
            {
                csvStream.ResetStreamPosition();
            }

            // Assert
            Assert.Throws<ObjectDisposedException>(TestCode);
        }
        
        [Fact]
        public void ReadField_WithNoContent_ShouldReturnNull()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance();
            

            // Act
            string actual = csvStream.ReadField();

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void ReadField_WithContent_ShouldReturnField()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);

            // Act
            string actual = csvStream.ReadField();

            // Assert
            Assert.Equal(ValidField, actual);
        }
        
        [Fact]
        public void ReadField_WithEOF_ShouldReturnNull()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
            csvStream.ReadField();
            
            // Act
            string actual = csvStream.ReadField();

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void ReadField_WithEmptyFieldContent_ShouldReturnEmptyString()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(",field1");

            // Act
            string actual = csvStream.ReadField();

            // Assert
            Assert.Equal(string.Empty, actual);
        }
        
        [Fact]
        public void ReadField_WithEscapedFieldContent_ShouldReturnUnescapedField()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(EscapedFieldWithNewLine);

            // Act
            string actual = csvStream.ReadField();

            // Assert
            Assert.Equal(EscapedFieldWithNewLineExpected, actual);
        }
        
        [Fact]
        public void ReadField_WithEscapedFieldButTextAfterLastDoubleQuoteContent_ShouldReturnUnescapedField()
        {
            const string expected = "field1,test extra";
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance("\"field1,test\" extra,next field\r\n");

            // Act
            string actual = csvStream.ReadField();

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void ReadField_WithEscapedFieldButTextBeforeFirstDoubleQuoteContent_ShouldReturnUnescapedField()
        {
            const string expected = "extra field1,test";
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance("extra \"field1,test\",next field\r\n");

            // Act
            string actual = csvStream.ReadField();

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void ReadField_WithEscapedEmptyFieldContent_ShouldReturnEmptyString()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance("\"\"");

            // Act
            string actual = csvStream.ReadField();

            // Assert
            Assert.Equal(string.Empty, actual);
        }
        
        [Fact]
        public void ReadField_WithEmptyRowBetweenContent_ShouldReturnEmptyString()
        {
            const string expected = "";
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance("field1\r\n\r\nfield2");
            csvStream.ReadField();

            // Act
            string actual = csvStream.ReadField();

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void ReadField_WithStreamDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
            csvStream.Dispose();
            
            // Act
            void TestCode()
            {
                csvStream.ReadField();
            }

            // Assert
            Assert.Throws<ObjectDisposedException>(TestCode);
        }
        
        [Fact]
        public void ReadField_WithAsyncOperationRunning_ShouldThrowInvalidOperationException()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
            Task<string> task = csvStream.ReadFieldAsync();
            
            // Act
            void TestCode()
            {
                csvStream.ReadField();
            }

            // Assert
            Assert.Throws<InvalidOperationException>(TestCode);
        }
        
        [Fact]
        public void ReadFieldAndCheck_WithContentButNoNewLineEncountered_ShouldHaveNewLineEncounteredAsFalse()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);

            // Act
            FieldReadResult actual = csvStream.ReadFieldAndCheck();

            // Assert
            Assert.False(actual.NewLineEncountered);
        }
        
        [Fact]
        public void ReadFieldAndCheck_WithContentAndNewLineEncountered_ShouldHaveNewLineEncounteredAsTrue()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);

            // Act
            FieldReadResult actual = csvStream.ReadFieldAndCheck();

            // Assert
            Assert.True(actual.NewLineEncountered);
        }
        
        [Fact]
        public void ReadFieldAndCheck_WithContentAndAltNewLineEncountered_ShouldHaveNewLineEncounteredAsTrue()
        {
            // Arrange
            using var csvStream = new CsvStreamReader(GetDefaultStream("field1\n"), newLine:"\n");

            // Act
            FieldReadResult actual = csvStream.ReadFieldAndCheck();

            // Assert
            Assert.True(actual.NewLineEncountered);
        }
        
        [Fact]
        public void ReadFieldAndCheck_WithContentAndEOFEncountered_ShouldHaveNewLineEncounteredAsFalse()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidField);

            // Act
            FieldReadResult actual = csvStream.ReadFieldAndCheck();

            // Assert
            Assert.False(actual.NewLineEncountered);
        }
        
        [Fact]
        public async Task ReadFieldAsync_WithNoContent_ShouldReturnNull()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance();
            

            // Act
            string actual = await csvStream.ReadFieldAsync();

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public async Task ReadFieldAsync_WithContent_ShouldReturnField()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);

            // Act
            string actual = await csvStream.ReadFieldAsync();

            // Assert
            Assert.Equal(ValidField, actual);
        }
        
        [Fact]
        public async Task ReadFieldAsync_WithEOF_ShouldReturnNull()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
            await csvStream.ReadFieldAsync();
            
            // Act
            string actual = await csvStream.ReadFieldAsync();

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public async Task ReadFieldAsync_WithEmptyFieldContent_ShouldReturnEmptyString()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(",field1");

            // Act
            string actual = await csvStream.ReadFieldAsync();

            // Assert
            Assert.Equal(string.Empty, actual);
        }
        
        [Fact]
        public async Task ReadFieldAsync_WithEscapedFieldContent_ShouldReturnUnescapedField()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(EscapedFieldWithNewLine);

            // Act
            string actual = await csvStream.ReadFieldAsync();

            // Assert
            Assert.Equal(EscapedFieldWithNewLineExpected, actual);
        }
        
        [Fact]
        public async Task ReadFieldAsync_WithEscapedFieldButTextAfterLastDoubleQuoteContent_ShouldReturnUnescapedField()
        {
            const string expected = "field1,test extra";
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance("\"field1,test\" extra,next field\r\n");

            // Act
            string actual = await csvStream.ReadFieldAsync();

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public async Task ReadFieldAsync_WithEscapedFieldButTextBeforeFirstDoubleQuoteContent_ShouldReturnUnescapedField()
        {
            const string expected = "extra field1,test";
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance("extra \"field1,test\",next field\r\n");

            // Act
            string actual = await csvStream.ReadFieldAsync();

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public async Task ReadFieldAsync_WithEscapedEmptyFieldContent_ShouldReturnEmptyString()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance("\"\"");

            // Act
            string actual = await csvStream.ReadFieldAsync();

            // Assert
            Assert.Equal(string.Empty, actual);
        }
        
        [Fact]
        public async Task ReadFieldAsync_WithEmptyRowBetweenContent_ShouldReturnEmptyString()
        {
            const string expected = "";
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance("field1\r\n\r\nfield2");
            await csvStream.ReadFieldAsync();

            // Act
            string actual = await csvStream.ReadFieldAsync();

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public async Task ReadFieldAsync_WithStreamDisposed_ShouldThrowObjectDisposedException()
        {
            // Act
            async Task TestCode()
            {
                // Arrange
                using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
                csvStream.Dispose();
                
                await csvStream.ReadFieldAsync();
            }

            // Assert
            await Assert.ThrowsAsync<ObjectDisposedException>(TestCode);
        }
        
        [Fact]
        public async Task ReadFieldAsync_WithAsyncOperationRunning_ShouldThrowInvalidOperationException()
        {
            // Act
            async Task TestCode()
            {
                // Arrange
                using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
                Task<string> task = csvStream.ReadFieldAsync();
                
                await csvStream.ReadFieldAsync();
            }

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(TestCode);
        }
        
        [Fact]
        public async Task ReadFieldAndCheckAsync_WithContentButNoNewLineEncountered_ShouldHaveNewLineEncounteredAsFalse()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);

            // Act
            FieldReadResult actual = await csvStream.ReadFieldAndCheckAsync();

            // Assert
            Assert.False(actual.NewLineEncountered);
        }
        
        [Fact]
        public async Task ReadFieldAndCheckAsync_WithContentAndNewLineEncountered_ShouldHaveNewLineEncounteredAsTrue()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);

            // Act
            FieldReadResult actual = await csvStream.ReadFieldAndCheckAsync();

            // Assert
            Assert.True(actual.NewLineEncountered);
        }
        
        [Fact]
        public async Task ReadFieldAndCheckAsync_WithContentAndAltNewLineEncountered_ShouldHaveNewLineEncounteredAsTrue()
        {
            // Arrange
            using var csvStream = new CsvStreamReader(GetDefaultStream("field1\n"), newLine:"\n");

            // Act
            FieldReadResult actual = await csvStream.ReadFieldAndCheckAsync();

            // Assert
            Assert.True(actual.NewLineEncountered);
        }
        
        [Fact]
        public async Task ReadFieldAndCheckAsync_WithContentAndEOFEncountered_ShouldHaveNewLineEncounteredAsFalse()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidField);

            // Act
            FieldReadResult actual = await csvStream.ReadFieldAndCheckAsync();

            // Assert
            Assert.False(actual.NewLineEncountered);
        }

        [Fact]
        public void ReadRow_WithContent_ShouldReturnRow()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            
            // Act
            string[] actual = csvStream.ReadRow();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(2, actual.Length);
            Assert.Equal(ValidFieldBase + "1", actual[0]);
            Assert.Equal(ValidFieldBase + "2", actual[1]);
        }
        
        [Fact]
        public void ReadRow_WithContentAndEOF_ShouldReturnRow()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            csvStream.ReadRow();
            
            // Act
            string[] actual = csvStream.ReadRow();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(2, actual.Length);
            Assert.Equal(ValidFieldBase + "3", actual[0]);
            Assert.Equal(ValidFieldBase + "4", actual[1]);
        }
        
        [Fact]
        public void ReadRow_WithAtEOF_ShouldReturnNull()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
            csvStream.ReadRow();
            
            // Act
            string[] actual = csvStream.ReadRow();

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void ReadRow_WithNoNewLineAndAtEOF_ShouldReturnNull()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidField);
            csvStream.ReadRow();
            
            // Act
            string[] actual = csvStream.ReadRow();

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void ReadRow_WithEmptyRow_ShouldReturnEmptyField()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance("field1\r\n\r\nfield2");
            csvStream.ReadRow();
            
            // Act
            string[] actual = csvStream.ReadRow();

            // Assert
            Assert.NotNull(actual);
            Assert.Single(actual);
        }
        
        [Fact]
        public void ReadRow_WithStreamDisposed_ShouldThrowObjectDisposedException()
        {
            // Act
            void TestCode()
            {
                // Arrange
                using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
                csvStream.Dispose();
                
                csvStream.ReadRow();
            }

            // Assert
            Assert.Throws<ObjectDisposedException>(TestCode);
        }
        
        [Fact]
        public void ReadRow_WithAsyncOperationRunning_ShouldThrowInvalidOperationException()
        {
            // Act
            void TestCode()
            {
                // Arrange
                using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
                Task<string[]> task = csvStream.ReadRowAsync();
                
                csvStream.ReadRow();
            }

            // Assert
            Assert.Throws<InvalidOperationException>(TestCode);
        }
        
        [Fact]
        public async Task ReadRowAsync_WithContent_ShouldReturnRow()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            
            // Act
            string[] actual = await csvStream.ReadRowAsync();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(2, actual.Length);
            Assert.Equal(ValidFieldBase + "1", actual[0]);
            Assert.Equal(ValidFieldBase + "2", actual[1]);
        }
        
        [Fact]
        public async Task ReadRowAsync_WithContentAndEOF_ShouldReturnRow()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            await csvStream.ReadRowAsync();
            
            // Act
            string[] actual = await csvStream.ReadRowAsync();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(2, actual.Length);
            Assert.Equal(ValidFieldBase + "3", actual[0]);
            Assert.Equal(ValidFieldBase + "4", actual[1]);
        }
        
        [Fact]
        public async Task ReadRowAsync_WithAtEOF_ShouldReturnNull()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
            await csvStream.ReadRowAsync();
            
            // Act
            string[] actual = await csvStream.ReadRowAsync();

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public async Task ReadRowAsync_WithNoNewLineAndAtEOF_ShouldReturnNull()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidField);
            await csvStream.ReadRowAsync();
            
            // Act
            string[] actual = await csvStream.ReadRowAsync();

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public async Task ReadRowAsync_WithEmptyRow_ShouldReturnEmptyField()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance("field1\r\n\r\nfield2");
            await csvStream.ReadRowAsync();
            
            // Act
            string[] actual = await csvStream.ReadRowAsync();

            // Assert
            Assert.NotNull(actual);
            Assert.Single(actual);
        }
        
        [Fact]
        public async Task ReadRowAsync_WithStreamDisposed_ShouldThrowObjectDisposedException()
        {
            // Act
            async Task TestCode()
            {
                // Arrange
                using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
                csvStream.Dispose();
                
                await csvStream.ReadRowAsync();
            }

            // Assert
            await Assert.ThrowsAsync<ObjectDisposedException>(TestCode);
        }
        
        [Fact]
        public async Task ReadRowAsync_WithAsyncOperationRunning_ShouldThrowInvalidOperationException()
        {
            // Act
            async Task TestCode()
            {
                // Arrange
                using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
                Task<string[]> task = csvStream.ReadRowAsync();
                
                await csvStream.ReadRowAsync();
            }

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(TestCode);
        }
        
        [Fact]
        public void ReadToEnd_WithContent_ShouldReturnAllRowsAsMatrix()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            
            // Act
            string[][] actual = csvStream.ReadToEnd();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(2, actual.Length);
            Assert.Equal(2, actual[0].Length);
            Assert.Equal(2, actual[0].Length);
            Assert.Equal(ValidFieldBase + "1", actual[0][0]);
            Assert.Equal(ValidFieldBase + "2", actual[0][1]);
            Assert.Equal(ValidFieldBase + "3", actual[1][0]);
            Assert.Equal(ValidFieldBase + "4", actual[1][1]);
        }
        
        [Fact]
        public void ReadToEnd_WithContentAndEOF_ShouldReturnRemainingRowAsMatrix()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            csvStream.ReadRow();
            
            // Act
            string[][] actual = csvStream.ReadToEnd();

            // Assert
            Assert.NotNull(actual);
            Assert.Single(actual);
            Assert.Equal(2, actual[0].Length);
            Assert.Equal(ValidFieldBase + "3", actual[0][0]);
            Assert.Equal(ValidFieldBase + "4", actual[0][1]);
        }
        
        [Fact]
        public void ReadToEnd_WithAtEOF_ShouldReturnNull()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
            csvStream.ReadRow();
            
            // Act
            string[][] actual = csvStream.ReadToEnd();

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void ReadToEnd_WithNoNewLineAndAtEOF_ShouldReturnNull()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidField);
            csvStream.ReadRow();
            
            // Act
            string[][] actual = csvStream.ReadToEnd();

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public void ReadToEnd_WithEmptyRow_ShouldReturnAllContentIncludingEmptyRow()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance("field1\r\n\r\nfield2");

            // Act
            string[][] actual = csvStream.ReadToEnd();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(3, actual.Length);
            Assert.Single(actual[0]);
            Assert.Single(actual[1]);
            Assert.Single(actual[2]);
            Assert.Equal("", actual[1][0]);
        }
        
        [Fact]
        public void ReadToEnd_WithStreamDisposed_ShouldThrowObjectDisposedException()
        {
            // Act
            void TestCode()
            {
                // Arrange
                using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
                csvStream.Dispose();
                
                csvStream.ReadToEnd();
            }

            // Assert
            Assert.Throws<ObjectDisposedException>(TestCode);
        }
        
        [Fact]
        public void ReadToEnd_WithAsyncOperationRunning_ShouldThrowInvalidOperationException()
        {
            // Act
            void TestCode()
            {
                // Arrange
                using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
                Task<string[][]> task = csvStream.ReadToEndAsync();
                
                csvStream.ReadToEnd();
            }

            // Assert
            Assert.Throws<InvalidOperationException>(TestCode);
        }
        
        [Fact]
        public async Task ReadToEndAsync_WithContent_ShouldReturnAllRowsAsMatrix()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            
            // Act
            string[][] actual = await csvStream.ReadToEndAsync();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(2, actual.Length);
            Assert.Equal(2, actual[0].Length);
            Assert.Equal(2, actual[0].Length);
            Assert.Equal(ValidFieldBase + "1", actual[0][0]);
            Assert.Equal(ValidFieldBase + "2", actual[0][1]);
            Assert.Equal(ValidFieldBase + "3", actual[1][0]);
            Assert.Equal(ValidFieldBase + "4", actual[1][1]);
        }
        
        [Fact]
        public async Task ReadToEndAsync_WithContentAndEOF_ShouldReturnRemainingRowAsMatrix()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(DefaultCsvContent);
            await csvStream.ReadRowAsync();
            
            // Act
            string[][] actual = await csvStream.ReadToEndAsync();

            // Assert
            Assert.NotNull(actual);
            Assert.Single(actual);
            Assert.Equal(2, actual[0].Length);
            Assert.Equal(ValidFieldBase + "3", actual[0][0]);
            Assert.Equal(ValidFieldBase + "4", actual[0][1]);
        }
        
        [Fact]
        public async Task ReadToEndAsync_WithAtEOF_ShouldReturnNull()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
            await csvStream.ReadRowAsync();
            
            // Act
            string[][] actual = await csvStream.ReadToEndAsync();

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public async Task ReadToEndAsync_WithNoNewLineAndAtEOF_ShouldReturnNull()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance(ValidField);
            await csvStream.ReadRowAsync();
            
            // Act
            string[][] actual = await csvStream.ReadToEndAsync();

            // Assert
            Assert.Null(actual);
        }
        
        [Fact]
        public async Task ReadToEndAsync_WithEmptyRow_ShouldReturnAllContentIncludingEmptyRow()
        {
            // Arrange
            using CsvStreamReader csvStream = GetDefaultInstance("field1\r\n\r\nfield2");

            // Act
            string[][] actual = await csvStream.ReadToEndAsync();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(3, actual.Length);
            Assert.Single(actual[0]);
            Assert.Single(actual[1]);
            Assert.Single(actual[2]);
            Assert.Equal("", actual[1][0]);
        }
        
        [Fact]
        public async Task ReadToEndAsync_WithStreamDisposed_ShouldThrowObjectDisposedException()
        {
            // Act
            async Task TestCode()
            {
                // Arrange
                using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
                csvStream.Dispose();
                
                await csvStream.ReadToEndAsync();
            }

            // Assert
            await Assert.ThrowsAsync<ObjectDisposedException>(TestCode);
        }
        
        [Fact]
        public async Task ReadToEndAsync_WithAsyncOperationRunning_ShouldThrowInvalidOperationException()
        {
            // Act
            async Task TestCode()
            {
                // Arrange
                using CsvStreamReader csvStream = GetDefaultInstance(ValidFieldWithNewLine);
                Task<string[][]> task = csvStream.ReadToEndAsync();
                
                await csvStream.ReadToEndAsync();
            }

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(TestCode);
        }

        //Utility functions

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
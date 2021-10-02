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
    }
}
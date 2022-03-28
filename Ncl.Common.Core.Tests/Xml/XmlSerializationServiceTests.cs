using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Ncl.Common.Core.Utilities;
using Ncl.Common.Core.Xml;
using Xunit;

namespace Ncl.Common.Core.Tests.Xml
{
    public class XmlSerializationServiceTests
    {
        public const string DefaultIdValue = "test_id"; //Do not change value without updating XML string constants

        [Fact]
        public void WriteObject_WithDefaultInstance_ShouldWriteXmlContentToStream()
        {
            // Arrange
            var stream = new MemoryStream();
            XmlSerializationService instance = GetInstance();
            XmlDataContractStub dataInstance = GetDataContractDataInstance();

            // Act
            instance.WriteObject(stream, dataInstance);
            string actual = StringUtility.GetStringFromStream(stream);

            // Assert
            Assert.Equal(XmlDataContractStub.ExpectedDefaultXml, actual);
        }

        [Fact]
        public void WriteObject_WithNullStream_ShouldThrowArgumentNullException()
        {
            // Arrange
            XmlSerializationService instance = GetInstance();
            XmlDataContractStub dataInstance = GetDataContractDataInstance();

            void TestCode()
            {
                // Act
                instance.WriteObject((Stream) null, dataInstance);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(TestCode);
        }

        [Fact]
        public void WriteObject_WithNullValue_ShouldThrowArgumentNullException()
        {
            // Arrange
            var stream = new MemoryStream();
            XmlSerializationService instance = GetInstance();

            void TestCode()
            {
                // Act
                instance.WriteObject(stream, (object) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(TestCode);
        }

        [Fact]
        public void ReadObject_WithDefaultInstance_ShouldDeserializeObjectFromStream()
        {
            // Arrange
            Stream stream = GetDataContractDataXmlStream();
            XmlSerializationService instance = GetInstance();

            // Act
            var actual = instance.ReadObject<XmlDataContractStub>(stream);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(DefaultIdValue, actual.Id);
        }

        [Fact]
        public void ReadObject_WithNullStream_ShouldThrowArgumentNullException()
        {
            // Arrange
            XmlSerializationService instance = GetInstance();

            void TestCode()
            {
                // Act
                _ = instance.ReadObject<XmlDataContractStub>((Stream) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(TestCode);
        }

        [Fact]
        public void ReadObject_WithInvalidXmlStream_ShouldThrowSerializationException()
        {
            // Arrange
            Stream stream = GetInvalidDataContractDataXmlStream();
            XmlSerializationService instance = GetInstance();

            void TestCode()
            {
                // Act
                _ = instance.ReadObject<XmlDataContractStub>(stream);
            }

            // Assert
            Assert.Throws<SerializationException>(TestCode);
        }

        [Fact]
        public void TryWriteObject_WithDefaultInstance_ShouldWriteXmlContentToStream()
        {
            // Arrange
            var stream = new MemoryStream();
            XmlSerializationService instance = GetInstance();
            XmlDataContractStub dataInstance = GetDataContractDataInstance();

            // Act
            _ = instance.TryWriteObject(stream, dataInstance);
            string actual = StringUtility.GetStringFromStream(stream);

            // Assert
            Assert.Equal(XmlDataContractStub.ExpectedDefaultXml, actual);
        }

        [Fact]
        public void TryWriteObject_WithDefaultInstance_ShouldReturnTrue()
        {
            // Arrange
            var stream = new MemoryStream();
            XmlSerializationService instance = GetInstance();
            XmlDataContractStub dataInstance = GetDataContractDataInstance();

            // Act
            bool actual = instance.TryWriteObject(stream, dataInstance);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void TryWriteObject_WithNullStream_ShouldReturnFalse()
        {
            // Arrange
            XmlSerializationService instance = GetInstance();
            XmlDataContractStub dataInstance = GetDataContractDataInstance();

            // Act
            bool actual = instance.TryWriteObject((Stream) null, dataInstance);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryWriteObject_WithNullValue_ShouldThrowArgumentNullException()
        {
            // Arrange
            var stream = new MemoryStream();
            XmlSerializationService instance = GetInstance();

            // Act
            bool actual = instance.TryWriteObject(stream, (object) null);
            
            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryReadObject_WithDefaultInstance_ShouldDeserializeObjectFromStream()
        {
            // Arrange
            Stream stream = GetDataContractDataXmlStream();
            XmlSerializationService instance = GetInstance();

            // Act
            bool actual = instance.TryReadObject<XmlDataContractStub>(stream, out _);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void TryReadObject_WithNullStream_ShouldThrowArgumentNullException()
        {
            // Arrange
            XmlSerializationService instance = GetInstance();

            // Act
            bool actual = instance.TryReadObject<XmlDataContractStub>((Stream) null, out _);
            
            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryReadObject_WithInvalidXmlStream_ShouldThrowSerializationException()
        {
            // Arrange
            Stream stream = GetInvalidDataContractDataXmlStream();
            XmlSerializationService instance = GetInstance();

            // Act
            bool actual = instance.TryReadObject<XmlDataContractStub>(stream, out _);
            
            // Assert
            Assert.False(actual);
        }
        
        [Fact]
        public void Serialize_WithDefaultInstance_ShouldWriteXmlContentToStream()
        {
            // Arrange
            var stream = new MemoryStream();
            XmlSerializationService instance = GetInstance();
            XmlSerializerStub dataInstance = GetXmlSerializerDataInstance();

            // Act
            instance.Serialize(stream, dataInstance);
            string actual = StringUtility.GetStringFromStream(stream);

            // Assert
            Assert.Equal(XmlSerializerStub.ExpectedDefaultXml, actual);
        }

        [Fact]
        public void Serialize_WithNullStream_ShouldThrowArgumentNullException()
        {
            // Arrange
            XmlSerializationService instance = GetInstance();
            XmlSerializerStub dataInstance = GetXmlSerializerDataInstance();

            void TestCode()
            {
                // Act
                instance.Serialize((Stream) null, dataInstance);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(TestCode);
        }

        [Fact]
        public void Serialize_WithNullValue_ShouldThrowArgumentNullException()
        {
            // Arrange
            var stream = new MemoryStream();
            XmlSerializationService instance = GetInstance();

            void TestCode()
            {
                // Act
                instance.Serialize(stream, (object) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(TestCode);
        }

        [Fact]
        public void Deserialize_WithDefaultInstance_ShouldDeserializeObjectFromStream()
        {
            // Arrange
            Stream stream = GetXmlSerializerDataXmlStream();
            XmlSerializationService instance = GetInstance();

            // Act
            var actual = instance.Deserialize<XmlSerializerStub>(stream);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(DefaultIdValue, actual.Id);
        }

        [Fact]
        public void Deserialize_WithNullStream_ShouldThrowArgumentNullException()
        {
            // Arrange
            XmlSerializationService instance = GetInstance();

            void TestCode()
            {
                // Act
                _ = instance.Deserialize<XmlSerializerStub>((Stream) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(TestCode);
        }

        [Fact]
        public void Deserialize_WithInvalidXmlStream_ShouldThrowXmlException()
        {
            // Arrange
            Stream stream = GetInvalidXmlSerializerDataXmlStream();
            XmlSerializationService instance = GetInstance();

            void TestCode()
            {
                // Act
                _ = instance.Deserialize<XmlSerializerStub>(stream);
            }

            // Assert
            Assert.Throws<XmlException>(TestCode);
        }

        [Fact]
        public void Deserialize_WithInsecureSettings_ShouldThrowArgumentException()
        {
            // Arrange
            Stream stream = GetXmlSerializerDataXmlStream();
            var settings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Parse };
            var xmlReader = XmlReader.Create(stream, settings);
            XmlSerializationService instance = GetInstance();

            void TestCode()
            {
                // Act
                _ = instance.Deserialize<XmlSerializerStub>(xmlReader);
            }

            // Assert
            Assert.Throws<ArgumentException>(TestCode);
        }

        //////
        [Fact]
        public void TrySerialize_WithDefaultInstance_ShouldWriteXmlContentToStream()
        {
            // Arrange
            var stream = new MemoryStream();
            XmlSerializationService instance = GetInstance();
            XmlSerializerStub dataInstance = GetXmlSerializerDataInstance();

            // Act
            _ = instance.TrySerialize(stream, dataInstance);
            string actual = StringUtility.GetStringFromStream(stream);

            // Assert
            Assert.Equal(XmlSerializerStub.ExpectedDefaultXml, actual);
        }

        [Fact]
        public void TrySerialize_WithDefaultInstance_ShouldReturnTrue()
        {
            // Arrange
            var stream = new MemoryStream();
            XmlSerializationService instance = GetInstance();
            XmlSerializerStub dataInstance = GetXmlSerializerDataInstance();

            // Act
            bool actual = instance.TrySerialize(stream, dataInstance);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void TrySerialize_WithNullStream_ShouldReturnFalse()
        {
            // Arrange
            XmlSerializationService instance = GetInstance();
            XmlSerializerStub dataInstance = GetXmlSerializerDataInstance();

            //Act
            bool actual = instance.TrySerialize((Stream) null, dataInstance);
            
            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void TrySerialize_WithNullValue_ShouldReturnFalse()
        {
            // Arrange
            var stream = new MemoryStream();
            XmlSerializationService instance = GetInstance();

            //Act
            bool actual = instance.TrySerialize(stream, (object) null);

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryDeserialize_WithDefaultInstance_ShouldDeserializeObjectFromStream()
        {
            // Arrange
            Stream stream = GetXmlSerializerDataXmlStream();
            XmlSerializationService instance = GetInstance();

            // Act
            _ = instance.TryDeserialize(stream, out XmlSerializerStub actual);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(DefaultIdValue, actual.Id);
        }

        [Fact]
        public void TryDeserialize_WithDefaultInstance_ShouldReturnTrue()
        {
            // Arrange
            Stream stream = GetXmlSerializerDataXmlStream();
            XmlSerializationService instance = GetInstance();

            // Act
            bool actual = instance.TryDeserialize<XmlSerializerStub>(stream, out _);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void TryDeserialize_WithNullStream_ShouldReturnFalse()
        {
            // Arrange
            XmlSerializationService instance = GetInstance();

            // Act
            bool actual = instance.TryDeserialize<XmlSerializerStub>((Stream) null, out _);
            

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryDeserialize_WithInvalidXmlStream_ShouldReturnFalse()
        {
            // Arrange
            Stream stream = GetInvalidXmlSerializerDataXmlStream();
            XmlSerializationService instance = GetInstance();

            // Act
            bool actual = instance.TryDeserialize<XmlSerializerStub>(stream, out _);
            

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryDeserialize_WithInsecureSettings_ShouldReturnFalse()
        {
            // Arrange
            Stream stream = GetXmlSerializerDataXmlStream();
            var settings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Parse };
            var xmlReader = XmlReader.Create(stream, settings);
            XmlSerializationService instance = GetInstance();
            
            // Act
            bool actual = instance.TryDeserialize<XmlSerializerStub>(xmlReader, out _);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void SerializeToString_WithDefaultInstance_ShouldWriteXmlContentToStream()
        {
            // Arrange
            XmlSerializationService instance = GetInstance();
            XmlSerializerStub dataInstance = GetXmlSerializerDataInstance();

            // Act
            string actual = instance.SerializeToString(dataInstance);

            // Assert
            Assert.Equal(XmlSerializerStub.ExpectedDefaultXml, actual);
        }

        [Fact]
        public void SerializeToString_WithNullValue_ShouldThrowArgumentNullException()
        {
            // Arrange
            XmlSerializationService instance = GetInstance();

            void TestCode()
            {
                // Act
                instance.SerializeToString((object) null);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(TestCode);
        }

        [Fact]
        public void DeserializeFromString_WithDefaultString_ShouldDeserializeObjectFromStream()
        {
            // Arrange
            const string xmlString = XmlSerializerStub.ExpectedDefaultXml;
            XmlSerializationService instance = GetInstance();

            // Act
            var actual = instance.DeserializeFromString<XmlSerializerStub>(xmlString);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(DefaultIdValue, actual.Id);
        }

        [Fact]
        public void DeserializeFromString_WithInvalidXmlString_ShouldThrowXmlException()
        {
            // Arrange
            const string xmlString = XmlSerializerStub.InvalidXml;
            XmlSerializationService instance = GetInstance();

            void TestCode()
            {
                // Act
                _ = instance.DeserializeFromString<XmlSerializerStub>(xmlString);
            }

            // Assert
            Assert.Throws<XmlException>(TestCode);
        }

        [Fact]
        public void TrySerializeToString_WithDefaultInstance_ShouldWriteXmlContentToStream()
        {
            // Arrange
            XmlSerializationService instance = GetInstance();
            XmlSerializerStub dataInstance = GetXmlSerializerDataInstance();

            // Act
            _ = instance.TrySerializeToString(dataInstance, out string actual);

            // Assert
            Assert.Equal(XmlSerializerStub.ExpectedDefaultXml, actual);
        }

        [Fact]
        public void TrySerializeToString_WithDefaultInstance_ShouldReturnTrue()
        {
            // Arrange
            XmlSerializationService instance = GetInstance();
            XmlSerializerStub dataInstance = GetXmlSerializerDataInstance();

            // Act
            bool actual = instance.TrySerializeToString(dataInstance, out _);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void TrySerializeToString_WithNullValue_ShouldReturnFalse()
        {
            // Arrange
            XmlSerializationService instance = GetInstance();

            //Act
            bool actual = instance.TrySerializeToString((object) null, out _);

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void TryDeserializeFromString_WithDefaultString_ShouldDeserializeObjectFromStream()
        {
            // Arrange
            const string xmlString = XmlSerializerStub.ExpectedDefaultXml;
            XmlSerializationService instance = GetInstance();

            // Act
            _ = instance.TryDeserializeFromString(xmlString, out XmlSerializerStub actual);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(DefaultIdValue, actual.Id);
        }

        [Fact]
        public void TryDeserializeFromString_WithDefaultString_ShouldReturnTrue()
        {
            // Arrange
            const string xmlString = XmlSerializerStub.ExpectedDefaultXml;
            XmlSerializationService instance = GetInstance();

            // Act
            bool actual = instance.TryDeserializeFromString<XmlSerializerStub>(xmlString, out _);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void TryDeserializeFromString_WithInvalidXmlString_ShouldReturnFalse()
        {
            // Arrange
            const string xmlString = XmlSerializerStub.InvalidXml;
            XmlSerializationService instance = GetInstance();

            // Act
            bool actual = instance.TryDeserializeFromString<XmlSerializerStub>(xmlString, out _);

            // Assert
            Assert.False(actual);
        }

        //Utility functions
        public XmlSerializationService GetInstance()
        {
            return new XmlSerializationService();
        }

        public XmlDataContractStub GetDataContractDataInstance()
        {
            return new XmlDataContractStub();
        }

        public XmlSerializerStub GetXmlSerializerDataInstance()
        {
            return new XmlSerializerStub();
        }

        public Stream GetDataContractDataXmlStream()
        {
            return new MemoryStream(XmlDataContractStub.ExpectedDefaultXmlBytes);
        }

        public Stream GetXmlSerializerDataXmlStream()
        {
            return new MemoryStream(XmlSerializerStub.ExpectedDefaultXmlBytes);
        }

        public Stream GetInvalidDataContractDataXmlStream()
        {
            return new MemoryStream(XmlDataContractStub.InvalidXmlBytes);
        }

        public Stream GetInvalidXmlSerializerDataXmlStream()
        {
            return new MemoryStream(XmlSerializerStub.InvalidXmlBytes);
        }

        [DataContract]
        public class XmlDataContractStub
        {
            /// <summary>
            ///     The expected XML string from a default instance of this class.
            /// </summary>
            public const string ExpectedDefaultXml =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<XmlSerializationServiceTests.XmlDataContractStub xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.datacontract.org/2004/07/Ncl.Common.Core.Tests.Xml\">\r\n  <Id>test_id</Id>\r\n</XmlSerializationServiceTests.XmlDataContractStub>";

            /// <summary>
            ///     The invalid XML string of this class.
            /// </summary>
            public const string InvalidXml =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<XmlSerializationServiceTests.XmlDataContractStubsts xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.datacontract.org/2004/07/Ncl.Common.Core.Tests.Xml\">\r\n  <Id>test_id</Id>\r\n</XmlSerializationServiceTests.XmlDataContractStubsts>";

            /// <summary>
            ///     The expected XML string as UTF8 bytes. Do not modify.
            /// </summary>
            public static readonly byte[] ExpectedDefaultXmlBytes = Encoding.UTF8.GetBytes(ExpectedDefaultXml);

            /// <summary>
            ///     The invalid XML string as UTF8 bytes. Do not modify.
            /// </summary>
            public static readonly byte[] InvalidXmlBytes = Encoding.UTF8.GetBytes(InvalidXml);

            [DataMember] public string Id { get; set; } = DefaultIdValue;
        }

        [Serializable]
        public class XmlSerializerStub
        {
            /// <summary>
            ///     The expected XML string from a default instance of this class.
            /// </summary>
            public const string ExpectedDefaultXml =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<XmlSerializerStub xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <Id>test_id</Id>\r\n</XmlSerializerStub>";

            /// <summary>
            ///     The invalid XML string of this class.
            /// </summary>
            public const string InvalidXml =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<XmlSerializerStubss xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <Id>test_id</Id>\r\n</XmlSerializerStubss>";

            /// <summary>
            ///     The expected XML string as UTF8 bytes. Do not modify.
            /// </summary>
            public static readonly byte[] ExpectedDefaultXmlBytes = Encoding.UTF8.GetBytes(ExpectedDefaultXml);

            /// <summary>
            ///     The invalid XML string as UTF8 bytes. Do not modify.
            /// </summary>
            public static readonly byte[] InvalidXmlBytes = Encoding.UTF8.GetBytes(InvalidXml);

            public string Id { get; set; } = DefaultIdValue;
        }
    }
}
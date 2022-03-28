using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Ncl.Common.Core.Xml
{
    /// <summary>
    ///     The interface for a XML serialization service.
    /// </summary>
    public interface IXmlSerializationService
    {
        /// <summary>
        ///     Reads an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>The deserialized object read from the file.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined maximum length.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        ///     The specified path is invalid, (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     <paramref name="path" /> specified a directory.-or-
        ///     The caller does not have the required permission.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        ///     The file specified in <paramref name="path" /> was not found.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     <paramref name="path" /> is in an invalid format.
        /// </exception>
        /// <exception cref="IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of file.
        /// </exception>
        T ReadObject<T>(string path, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Reads an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>The deserialized object read from the file.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined maximum length.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        ///     The specified path is invalid, (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     <paramref name="path" /> specified a directory.-or-
        ///     The caller does not have the required permission.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        ///     The file specified in <paramref name="path" /> was not found.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     <paramref name="path" /> is in an invalid format.
        /// </exception>
        /// <exception cref="IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of file.
        /// </exception>
        T ReadObject<T>(string path, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Reads an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>The deserialized object read from the file.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined maximum length.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        ///     The specified path is invalid, (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     <paramref name="path" /> specified a directory.-or-
        ///     The caller does not have the required permission.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        ///     The file specified in <paramref name="path" /> was not found.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     <paramref name="path" /> is in an invalid format.
        /// </exception>
        /// <exception cref="IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of file.
        /// </exception>
        object ReadObject(string path, Type type, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Reads an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>The deserialized object read from the file.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined maximum length.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        ///     The specified path is invalid, (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     <paramref name="path" /> specified a directory.-or-
        ///     The caller does not have the required permission.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        ///     The file specified in <paramref name="path" /> was not found.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     <paramref name="path" /> is in an invalid format.
        /// </exception>
        /// <exception cref="IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of file.
        /// </exception>
        object ReadObject(string path, Type type, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Reads an object from a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>The deserialized object read from the stream.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The <paramref name="stream" /> does not support reading.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="stream" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the stream.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of file.
        /// </exception>
        T ReadObject<T>(Stream stream, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Reads an object from a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>The deserialized object read from the stream.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The <paramref name="stream" /> does not support reading.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="stream" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the stream.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of file.
        /// </exception>
        T ReadObject<T>(Stream stream, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Reads an object from a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>The deserialized object read from the stream.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The <paramref name="stream" /> does not support reading.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="stream" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the stream.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of file.
        /// </exception>
        object ReadObject(Stream stream, Type type, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Reads an object from a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>The deserialized object read from the stream.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The <paramref name="stream" /> does not support reading.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="stream" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the stream.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of file.
        /// </exception>
        object ReadObject(Stream stream, Type type, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Reads an object from a <see cref="TextReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>The deserialized object read from the text reader.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="reader" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="reader" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the text reader.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of file.
        /// </exception>
        T ReadObject<T>(TextReader reader, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Reads an object from a <see cref="TextReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>The deserialized object read from the text reader.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="reader" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="reader" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the text reader.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of file.
        /// </exception>
        T ReadObject<T>(TextReader reader, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Reads an object from a <see cref="TextReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>The deserialized object read from the text reader.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="reader" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="reader" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the text reader.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of file.
        /// </exception>
        object ReadObject(TextReader reader, Type type, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Reads an object from a <see cref="TextReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>The deserialized object read from the text reader.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="reader" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="reader" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the text reader.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of file.
        /// </exception>
        object ReadObject(TextReader reader, Type type, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Reads an object from a <see cref="XmlReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The XML reader to read from.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>The deserialized object read from the XML reader.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="reader" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An <see cref="XmlReader" /> method was called before a
        ///     previous asynchronous operation finished.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="reader" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the XML reader.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of file.
        /// </exception>
        T ReadObject<T>(XmlReader reader, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Reads an object from a <see cref="XmlReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The XML reader to read from.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>The deserialized object read from the XML reader.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="reader" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An <see cref="XmlReader" /> method was called before a
        ///     previous asynchronous operation finished.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="reader" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the XML reader.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of file.
        /// </exception>
        T ReadObject<T>(XmlReader reader, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Reads an object from a <see cref="XmlReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="reader">The XML reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>The deserialized object read from the XML reader.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="reader" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An <see cref="XmlReader" /> method was called before a
        ///     previous asynchronous operation finished.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="reader" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the XML reader.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of file.
        /// </exception>
        object ReadObject(XmlReader reader, Type type, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Reads an object from a <see cref="XmlReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="reader">The XML reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>The deserialized object read from the XML reader.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="reader" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An <see cref="XmlReader" /> method was called before a
        ///     previous asynchronous operation finished.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="reader" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the XML reader.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of file.
        /// </exception>
        object ReadObject(XmlReader reader, Type type, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="result">Out: The deserialized object read from the file.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject<T>(string path, out T result, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="result">Out: The deserialized object read from the file.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject<T>(string path, out T result,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="result">Out: The deserialized object read from the file.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject<T>(string path, out T result, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="result">Out: The deserialized object read from the file.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject<T>(string path, out T result, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the file.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject(string path, Type type, out object result,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the file.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject(string path, Type type, out object result,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the file.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject(string path, Type type, out object result, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the file.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject(string path, Type type, out object result, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="result">Out: The deserialized object read from the stream.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject<T>(Stream stream, out T result,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="result">Out: The deserialized object read from the stream.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject<T>(Stream stream, out T result,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="result">Out: The deserialized object read from the stream.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject<T>(Stream stream, out T result, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="result">Out: The deserialized object read from the stream.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject<T>(Stream stream, out T result, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the stream.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject(Stream stream, Type type, out object result, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the stream.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject(Stream stream, Type type, out object result,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the stream.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject(Stream stream, Type type, out object result, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the stream.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject(Stream stream, Type type, out object result, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="TextReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="result">Out: The deserialized object read from the text reader.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject<T>(TextReader reader, out T result, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="TextReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="result">Out: The deserialized object read from the text reader.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject<T>(TextReader reader, out T result, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="TextReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="result">Out: The deserialized object read from the text reader.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject<T>(TextReader reader, out T result, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="TextReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="result">Out: The deserialized object read from the text reader.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject<T>(TextReader reader, out T result, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="TextReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the text reader.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject(TextReader reader, Type type, out object result,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="TextReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the text reader.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject(TextReader reader, Type type, out object result,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="TextReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the text reader.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject(TextReader reader, Type type, out object result, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="TextReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the text reader.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject(TextReader reader, Type type, out object result, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="XmlReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The XML reader to read from.</param>
        /// <param name="result">Out: The deserialized object read from the XML reader.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject<T>(XmlReader reader, out T result, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="XmlReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The XML reader to read from.</param>
        /// <param name="result">Out: The deserialized object read from the XML reader.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject<T>(XmlReader reader, out T result, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="XmlReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The XML reader to read from.</param>
        /// <param name="result">Out: The deserialized object read from the XML reader.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject<T>(XmlReader reader, out T result, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="XmlReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The XML reader to read from.</param>
        /// <param name="result">Out: The deserialized object read from the XML reader.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject<T>(XmlReader reader, out T result, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="XmlReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="reader">The XML reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the XML reader.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject(XmlReader reader, Type type, out object result,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="XmlReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="reader">The XML reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the XML reader.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject(XmlReader reader, Type type, out object result,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="XmlReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="reader">The XML reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the XML reader.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject(XmlReader reader, Type type, out object result, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="XmlReader" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="reader">The XML reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the XML reader.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryReadObject(XmlReader reader, Type type, out object result, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="string" /> of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="xmlString">The string with XML content.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>The deserialized object read from the string.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="xmlString" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of content.
        /// </exception>
        T ReadObjectFromString<T>(string xmlString, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="string" /> of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="xmlString">The string with XML content.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>The deserialized object read from the string.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="xmlString" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of content.
        /// </exception>
        T ReadObjectFromString<T>(string xmlString, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="string" /> of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="xmlString">The string with XML content.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>The deserialized object read from the string.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="xmlString" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of content.
        /// </exception>
        object ReadObjectFromString(string xmlString, Type type, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="string" /> of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="xmlString">The string with XML content.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>The deserialized object read from the string.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="xmlString" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of content.
        /// </exception>
        object ReadObjectFromString(string xmlString, Type type, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="string" /> of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="xmlString">The string with XML content.</param>
        /// <param name="result">Out: The deserialized object read from the string.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="xmlString" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of content.
        /// </exception>
        bool TryReadObjectFromString<T>(string xmlString, out T result,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="string" /> of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="xmlString">The string with XML content.</param>
        /// <param name="result">Out: The deserialized object read from the string.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="xmlString" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of content.
        /// </exception>
        bool TryReadObjectFromString<T>(string xmlString, out T result,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="string" /> of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="xmlString">The string with XML content.</param>
        /// <param name="result">Out: The deserialized object read from the string.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="xmlString" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of content.
        /// </exception>
        bool TryReadObjectFromString<T>(string xmlString, out T result, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="string" /> of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="xmlString">The string with XML content.</param>
        /// <param name="result">Out: The deserialized object read from the string.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="xmlString" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of content.
        /// </exception>
        bool TryReadObjectFromString<T>(string xmlString, out T result, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="string" /> of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="xmlString">The string with XML content.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the string.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="xmlString" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of content.
        /// </exception>
        bool TryReadObjectFromString(string xmlString, Type type, out object result,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="string" /> of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="xmlString">The string with XML content.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the string.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="xmlString" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of content.
        /// </exception>
        bool TryReadObjectFromString(string xmlString, Type type, out object result,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to read an object from a <see cref="string" /> of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="xmlString">The string with XML content.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the string.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="xmlString" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of content.
        /// </exception>
        bool TryReadObjectFromString(string xmlString, Type type, out object result,
            out Exception exception, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to read an object from a <see cref="string" /> of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="xmlString">The string with XML content.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the string.</param>
        /// <param name="exception">Out: The exception that occurred when deserialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="xmlString" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="XmlException">
        ///     An XML related exception occurred, such as premature/unexpected end of content.
        /// </exception>
        bool TryReadObjectFromString(string xmlString, Type type, out object result,
            out Exception exception, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Writes an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path" /> is <see langword="null" />, empty or white space,
        ///     or contains one or more invalid characters.-or-
        ///     <paramref name="path" /> refers to a non-file device,
        ///     such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     <paramref name="path" /> refers to a non-file device,
        ///     such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        ///     The caller does not have the required permission.
        /// </exception>
        /// <exception cref="PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined maximum length.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        ///     The specified path is invalid, (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     <paramref name="path" /> specifies a file that is read-only.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the file.
        /// </exception>
        void WriteObject<T>(string path, T obj, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Writes an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path" /> is <see langword="null" />, empty or white space,
        ///     or contains one or more invalid characters.-or-
        ///     <paramref name="path" /> refers to a non-file device,
        ///     such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     <paramref name="path" /> refers to a non-file device,
        ///     such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        ///     The caller does not have the required permission.
        /// </exception>
        /// <exception cref="PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined maximum length.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        ///     The specified path is invalid, (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     <paramref name="path" /> specifies a file that is read-only.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the file.
        /// </exception>
        void WriteObject<T>(string path, T obj, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Writes an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path" /> is <see langword="null" />, empty or white space,
        ///     or contains one or more invalid characters.-or-
        ///     <paramref name="path" /> refers to a non-file device,
        ///     such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     <paramref name="path" /> refers to a non-file device,
        ///     such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        ///     The caller does not have the required permission.
        /// </exception>
        /// <exception cref="PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined maximum length.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        ///     The specified path is invalid, (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     <paramref name="path" /> specifies a file that is read-only.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the file.
        /// </exception>
        void WriteObject(string path, object obj, Type type,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Writes an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path" /> is <see langword="null" />, empty or white space,
        ///     or contains one or more invalid characters.-or-
        ///     <paramref name="path" /> refers to a non-file device,
        ///     such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     <paramref name="path" /> refers to a non-file device,
        ///     such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        ///     The caller does not have the required permission.
        /// </exception>
        /// <exception cref="PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined maximum length.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        ///     The specified path is invalid, (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     <paramref name="path" /> specifies a file that is read-only.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the file.
        /// </exception>
        void WriteObject(string path, object obj, Type type,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Writes an object to a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The <paramref name="stream" /> does not support writing.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="stream" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the stream.
        /// </exception>
        void WriteObject<T>(Stream stream, T obj, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Writes an object to a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The <paramref name="stream" /> does not support writing.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="stream" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the stream.
        /// </exception>
        void WriteObject<T>(Stream stream, T obj, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Writes an object to a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The <paramref name="stream" /> does not support writing.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="stream" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the stream.
        /// </exception>
        void WriteObject(Stream stream, object obj, Type type,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Writes an object to a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The <paramref name="stream" /> does not support writing.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="stream" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the stream.
        /// </exception>
        void WriteObject(Stream stream, object obj, Type type,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Writes an object to a <see cref="TextWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="writer" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the text writer.
        /// </exception>
        void WriteObject<T>(TextWriter writer, T obj, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Writes an object to a <see cref="TextWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="writer" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the text writer.
        /// </exception>
        void WriteObject<T>(TextWriter writer, T obj, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Writes an object to a <see cref="TextWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="writer" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the text writer.
        /// </exception>
        void WriteObject(TextWriter writer, object obj, Type type,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Writes an object to a <see cref="TextWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="writer" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the text writer.
        /// </exception>
        void WriteObject(TextWriter writer, object obj, Type type,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Writes an object to a <see cref="XmlWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="writer" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the XML writer.
        /// </exception>
        void WriteObject<T>(XmlWriter writer, T obj, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Writes an object to a <see cref="XmlWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="writer" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the XML writer.
        /// </exception>
        void WriteObject<T>(XmlWriter writer, T obj, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Writes an object to a <see cref="XmlWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="writer" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the XML writer.
        /// </exception>
        void WriteObject(XmlWriter writer, object obj, Type type,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Writes an object to a <see cref="XmlWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules.
        ///     For example, the <see cref="DataContractAttribute" /> attribute
        ///     has not been applied to the type.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="writer" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the XML writer.
        /// </exception>
        void WriteObject(XmlWriter writer, object obj, Type type,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject<T>(string path, T obj, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject<T>(string path, T obj, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject<T>(string path, T obj, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject<T>(string path, T obj, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject(string path, object obj, Type type,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject(string path, object obj, Type type,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject(string path, object obj, Type type, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject(string path, object obj, Type type, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject<T>(Stream stream, T obj, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject<T>(Stream stream, T obj, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject<T>(Stream stream, T obj, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject<T>(Stream stream, T obj, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject(Stream stream, object obj, Type type,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject(Stream stream, object obj, Type type,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject(Stream stream, object obj, Type type, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a <see cref="Stream" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject(Stream stream, object obj, Type type, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a <see cref="TextWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject<T>(TextWriter writer, T obj, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a <see cref="TextWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject<T>(TextWriter writer, T obj, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a <see cref="TextWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject<T>(TextWriter writer, T obj, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a <see cref="TextWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject<T>(TextWriter writer, T obj, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a <see cref="TextWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject(TextWriter writer, object obj, Type type,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a <see cref="TextWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject(TextWriter writer, object obj, Type type,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a <see cref="TextWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject(TextWriter writer, object obj, Type type, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a <see cref="TextWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject(TextWriter writer, object obj, Type type, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a <see cref="XmlWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject<T>(XmlWriter writer, T obj, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a <see cref="XmlWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject<T>(XmlWriter writer, T obj, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a <see cref="XmlWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject<T>(XmlWriter writer, T obj, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a <see cref="XmlWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject<T>(XmlWriter writer, T obj, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a <see cref="XmlWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject(XmlWriter writer, object obj, Type type,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a <see cref="XmlWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject(XmlWriter writer, object obj, Type type,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a <see cref="XmlWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject(XmlWriter writer, object obj, Type type, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a <see cref="XmlWriter" /> using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObject(XmlWriter writer, object obj, Type type, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Writes an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     The serialized object as an XML <see cref="string" />.
        /// </returns>
        string WriteObjectToString<T>(T obj, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Writes an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     The serialized object as an XML <see cref="string" />.
        /// </returns>
        string WriteObjectToString<T>(T obj, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Writes an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     The serialized object as an XML <see cref="string" />.
        /// </returns>
        string WriteObjectToString(object obj, Type type, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Writes an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     The serialized object as an XML <see cref="string" />.
        /// </returns>
        string WriteObjectToString(object obj, Type type, DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="xmlString">Out: The serialized object as an XML <see cref="string" />.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObjectToString<T>(T obj, out string xmlString, IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="xmlString">Out: The serialized object as an XML <see cref="string" />.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObjectToString<T>(T obj, out string xmlString,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="xmlString">Out: The serialized object as an XML <see cref="string" />.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObjectToString<T>(T obj, out string xmlString, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="xmlString">Out: The serialized object as an XML <see cref="string" />.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObjectToString<T>(T obj, out string xmlString, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="xmlString">Out: The serialized object as an XML <see cref="string" />.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObjectToString(object obj, Type type, out string xmlString,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="xmlString">Out: The serialized object as an XML <see cref="string" />.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObjectToString(object obj, Type type, out string xmlString,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Tries to write an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="xmlString">Out: The serialized object as an XML <see cref="string" />.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="knownTypes">
        ///     An <see cref="IEnumerable{T}" /> of <see cref="Type" /> that contains the known types
        ///     that may be present in the object graph.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObjectToString(object obj, Type type, out string xmlString, out Exception exception,
            IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Tries to write an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="DataContractSerializer" />.
        /// </summary>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="xmlString">Out: The serialized object as an XML <see cref="string" />.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="settings">
        ///     The serializer settings. Default: <see langword="null" /> for default settings.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryWriteObjectToString(object obj, Type type, out string xmlString, out Exception exception,
            DataContractSerializerSettings settings = null);

        /// <summary>
        ///     Deserializes an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <returns>The deserialized object read from the file.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined maximum length.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        ///     The specified path is invalid, (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     <paramref name="path" /> specified a directory.-or-
        ///     The caller does not have the required permission.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        ///     The file specified in <paramref name="path" /> was not found.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     <paramref name="path" /> is in an invalid format.
        /// </exception>
        /// <exception cref="IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during deserialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        T Deserialize<T>(string path);

        /// <summary>
        ///     Deserializes an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <returns>The deserialized object read from the file.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined maximum length.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        ///     The specified path is invalid, (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     <paramref name="path" /> specified a directory.-or-
        ///     The caller does not have the required permission.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        ///     The file specified in <paramref name="path" /> was not found.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     <paramref name="path" /> is in an invalid format.
        /// </exception>
        /// <exception cref="IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during deserialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        object Deserialize(string path, Type type);

        /// <summary>
        ///     Deserializes an object from a <see cref="Stream" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>The deserialized object read from the stream.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The <paramref name="stream" /> does not support reading.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="stream" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the stream.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during deserialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        T Deserialize<T>(Stream stream);

        /// <summary>
        ///     Deserializes an object from a <see cref="Stream" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <returns>The deserialized object read from the stream.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The <paramref name="stream" /> does not support reading.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="stream" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the stream.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during deserialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        object Deserialize(Stream stream, Type type);

        /// <summary>
        ///     Deserializes an object from a <see cref="TextReader" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The text reader to read from.</param>
        /// <returns>The deserialized object read from the text reader.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="reader" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="reader" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the text reader.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during deserialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        T Deserialize<T>(TextReader reader);

        /// <summary>
        ///     Deserializes an object from a <see cref="TextReader" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <returns>The deserialized object read from the text reader.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="reader" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="reader" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the text reader.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during deserialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        object Deserialize(TextReader reader, Type type);

        /// <summary>
        ///     Deserializes an object from a <see cref="XmlReader" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="skipSettingsCheck">
        ///     Should the settings security check be skipped. Default is <see langword="false" />.
        ///     Remark: The check is not perfect due to settings class limitation.
        /// </param>
        /// <returns>The deserialized object read from the XML reader.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="reader" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="reader" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the XML reader.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during deserialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        T Deserialize<T>(XmlReader reader, bool skipSettingsCheck = false);

        /// <summary>
        ///     Deserializes an object from a <see cref="XmlReader" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="skipSettingsCheck">
        ///     Should the settings security check be skipped. Default is <see langword="false" />.
        ///     Remark: The check is not perfect due to settings class limitation.
        /// </param>
        /// <returns>The deserialized object read from the XML reader.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="reader" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="reader" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while reading from the XML reader.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during deserialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        object Deserialize(XmlReader reader, Type type, bool skipSettingsCheck = false);

        /// <summary>
        ///     Tries to deserialize an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="result">Out: The deserialized object read from the file.</param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryDeserialize<T>(string path, out T result);

        /// <summary>
        ///     Tries to deserialize an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="result">Out: The deserialized object read from the file.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryDeserialize<T>(string path, out T result, out Exception exception);

        /// <summary>
        ///     Tries to deserialize an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the file.</param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryDeserialize(string path, Type type, out object result);

        /// <summary>
        ///     Tries to deserialize an object from a file, specified by the <paramref name="path" />,
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the file.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryDeserialize(string path, Type type, out object result, out Exception exception);

        /// <summary>
        ///     Tries to deserialize an object from a <see cref="Stream" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="result">Out: The deserialized object read from the stream.</param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryDeserialize<T>(Stream stream, out T result);

        /// <summary>
        ///     Tries to deserialize an object from a <see cref="Stream" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="result">Out: The deserialized object read from the stream.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryDeserialize<T>(Stream stream, out T result, out Exception exception);

        /// <summary>
        ///     Tries to deserialize an object from a <see cref="Stream" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the stream.</param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryDeserialize(Stream stream, Type type, out object result);

        /// <summary>
        ///     Tries to deserialize an object from a <see cref="Stream" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the stream.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryDeserialize(Stream stream, Type type, out object result, out Exception exception);

        /// <summary>
        ///     Tries to deserialize an object from a <see cref="TextReader" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="result">Out: The deserialized object read from the text reader.</param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryDeserialize<T>(TextReader reader, out T result);

        /// <summary>
        ///     Tries to deserialize an object from a <see cref="TextReader" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="result">Out: The deserialized object read from the text reader.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryDeserialize<T>(TextReader reader, out T result, out Exception exception);

        /// <summary>
        ///     Tries to deserialize an object from a <see cref="TextReader" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the text reader.</param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryDeserialize(TextReader reader, Type type, out object result);

        /// <summary>
        ///     Tries to deserialize an object from a <see cref="TextReader" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="reader">The text reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the text reader.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryDeserialize(TextReader reader, Type type, out object result, out Exception exception);

        /// <summary>
        ///     Tries to deserialize an object from a <see cref="XmlReader" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The XML reader to read from.</param>
        /// <param name="result">Out: The deserialized object read from the XML reader.</param>
        /// <param name="skipSettingsCheck">
        ///     Should the settings security check be skipped. Default is <see langword="false" />.
        ///     Remark: The check is not perfect due to settings class limitation.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryDeserialize<T>(XmlReader reader, out T result,
            bool skipSettingsCheck = false);

        /// <summary>
        ///     Tries to deserialize an object from a <see cref="XmlReader" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="reader">The XML reader to read from.</param>
        /// <param name="result">Out: The deserialized object read from the XML reader.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="skipSettingsCheck">
        ///     Should the settings security check be skipped. Default is <see langword="false" />.
        ///     Remark: The check is not perfect due to settings class limitation.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryDeserialize<T>(XmlReader reader, out T result, out Exception exception,
            bool skipSettingsCheck = false);

        /// <summary>
        ///     Tries to deserialize an object from a <see cref="XmlReader" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="reader">The XML reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the XML reader.</param>
        /// <param name="skipSettingsCheck">
        ///     Should the settings security check be skipped. Default is <see langword="false" />.
        ///     Remark: The check is not perfect due to settings class limitation.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryDeserialize(XmlReader reader, Type type, out object result,
            bool skipSettingsCheck = false);

        /// <summary>
        ///     Tries to deserialize an object from a <see cref="XmlReader" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="reader">The XML reader to read from.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the XML reader.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <param name="skipSettingsCheck">
        ///     Should the settings security check be skipped. Default is <see langword="false" />.
        ///     Remark: The check is not perfect due to settings class limitation.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the deserialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TryDeserialize(XmlReader reader, Type type, out object result, out Exception exception,
            bool skipSettingsCheck = false);

        /// <summary>
        ///     Deserializes an object from a <see cref="string" /> of XML content
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="xmlString">The string with XML content.</param>
        /// <returns>The deserialized object read from the string.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="xmlString" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during deserialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        T DeserializeFromString<T>(string xmlString);

        /// <summary>
        ///     Deserializes an object from a <see cref="string" /> of XML content
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="xmlString">The string with XML content.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <returns>The deserialized object read from the string.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="xmlString" /> is <see langword="null" />, empty or white space.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during deserialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        object DeserializeFromString(string xmlString, Type type);

        /// <summary>
        ///     Tries to deserialize an object from a <see cref="string" /> of XML content
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="xmlString">The string with XML content.</param>
        /// <param name="result">Out: The deserialized object read from the string.</param>
        bool TryDeserializeFromString<T>(string xmlString, out T result);

        /// <summary>
        ///     Tries to deserialize an object from a <see cref="string" /> of XML content
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized.</typeparam>
        /// <param name="xmlString">The string with XML content.</param>
        /// <param name="result">Out: The deserialized object read from the string.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        bool TryDeserializeFromString<T>(string xmlString, out T result, out Exception exception);

        /// <summary>
        ///     Tries to deserialize an object from a <see cref="string" /> of XML content
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="xmlString">The string with XML content.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the string.</param>
        bool TryDeserializeFromString(string xmlString, Type type, out object result);

        /// <summary>
        ///     Tries to deserialize an object from a <see cref="string" /> of XML content
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="xmlString">The string with XML content.</param>
        /// <param name="type">The type for the object being deserialized.</param>
        /// <param name="result">Out: The deserialized object read from the string.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        bool TryDeserializeFromString(string xmlString, Type type, out object result,
            out Exception exception);

        /// <summary>
        ///     Serializes an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path" /> is <see langword="null" />, empty or white space,
        ///     or contains one or more invalid characters.-or-
        ///     <paramref name="path" /> refers to a non-file device,
        ///     such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     <paramref name="path" /> refers to a non-file device,
        ///     such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        ///     The caller does not have the required permission.
        /// </exception>
        /// <exception cref="PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined maximum length.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        ///     The specified path is invalid, (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     <paramref name="path" /> specifies a file that is read-only.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the file.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during serialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        void Serialize<T>(string path, T obj);

        /// <summary>
        ///     Serializes an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path" /> is <see langword="null" />, empty or white space,
        ///     or contains one or more invalid characters.-or-
        ///     <paramref name="path" /> refers to a non-file device,
        ///     such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     <paramref name="path" /> refers to a non-file device,
        ///     such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        ///     The caller does not have the required permission.
        /// </exception>
        /// <exception cref="PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined maximum length.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        ///     The specified path is invalid, (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     <paramref name="path" /> specifies a file that is read-only.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the file.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during serialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        void Serialize(string path, object obj, Type type);

        /// <summary>
        ///     Serializes an object to a <see cref="Stream" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The <paramref name="stream" /> does not support writing.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="stream" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the stream.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during serialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        void Serialize<T>(Stream stream, T obj);

        /// <summary>
        ///     Serializes an object to a <see cref="Stream" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The <paramref name="stream" /> does not support writing.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="stream" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the stream.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during serialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        void Serialize(Stream stream, object obj, Type type);

        /// <summary>
        ///     Serializes an object to a <see cref="TextWriter" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="writer" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the text writer.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during serialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        void Serialize<T>(TextWriter writer, T obj);

        /// <summary>
        ///     Serializes an object to a <see cref="TextWriter" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="writer" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the text writer.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during serialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        void Serialize(TextWriter writer, object obj, Type type);

        /// <summary>
        ///     Serializes an object to a <see cref="XmlWriter" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The XML writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="writer" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the XML writer.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during serialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        void Serialize<T>(XmlWriter writer, T obj);

        /// <summary>
        ///     Serializes an object to a <see cref="XmlWriter" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="writer">The XML writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <paramref name="writer" /> was closed.
        /// </exception>
        /// <exception cref="IOException">
        ///     An I/O error occurred while writing to the XML writer.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during serialization.
        ///     The original exception is available using the InnerException property.
        /// </exception>
        void Serialize(XmlWriter writer, object obj, Type type);

        /// <summary>
        ///     Tries to serialize an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerialize<T>(string path, T obj);

        /// <summary>
        ///     Tries to serialize an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerialize<T>(string path, T obj, out Exception exception);

        /// <summary>
        ///     Tries to serialize an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerialize(string path, object obj, Type type);

        /// <summary>
        ///     Tries to serialize an object to a file, specified by the <paramref name="path" />,
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerialize(string path, object obj, Type type, out Exception exception);

        /// <summary>
        ///     Tries to serialize an object to a <see cref="Stream" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerialize<T>(Stream stream, T obj);

        /// <summary>
        ///     Tries to serialize an object to a <see cref="Stream" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerialize<T>(Stream stream, T obj, out Exception exception);

        /// <summary>
        ///     Tries to serialize an object to a <see cref="Stream" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerialize(Stream stream, object obj, Type type);

        /// <summary>
        ///     Tries to serialize an object to a <see cref="Stream" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerialize(Stream stream, object obj, Type type, out Exception exception);

        /// <summary>
        ///     Tries to serialize an object to a <see cref="TextWriter" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerialize<T>(TextWriter writer, T obj);

        /// <summary>
        ///     Tries to serialize an object to a <see cref="TextWriter" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerialize<T>(TextWriter writer, T obj, out Exception exception);

        /// <summary>
        ///     Tries to serialize an object to a <see cref="TextWriter" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerialize(TextWriter writer, object obj, Type type);

        /// <summary>
        ///     Tries to serialize an object to a <see cref="TextWriter" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerialize(TextWriter writer, object obj, Type type, out Exception exception);

        /// <summary>
        ///     Tries to serialize an object to a <see cref="XmlWriter" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The XML writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerialize<T>(XmlWriter writer, T obj);

        /// <summary>
        ///     Tries to serialize an object to a <see cref="XmlWriter" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="writer">The XML writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerialize<T>(XmlWriter writer, T obj, out Exception exception);

        /// <summary>
        ///     Tries to serialize an object to a <see cref="XmlWriter" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="writer">The XML writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerialize(XmlWriter writer, object obj, Type type);

        /// <summary>
        ///     Tries to serialize an object to a <see cref="XmlWriter" /> using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="writer">The XML writer to write to.</param>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerialize(XmlWriter writer, object obj, Type type, out Exception exception);

        /// <summary>
        ///     Serializes an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="obj">The object being serialized.</param>
        /// <returns>
        ///     The serialized object as an XML <see cref="string" />.
        /// </returns>
        string SerializeToString<T>(T obj);

        /// <summary>
        ///     Serializes an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <returns>
        ///     The serialized object as an XML <see cref="string" />.
        /// </returns>
        string SerializeToString(object obj, Type type);

        /// <summary>
        ///     Tries to serialize an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="xmlString">Out: The serialized object as an XML <see cref="string" />.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerializeToString<T>(T obj, out string xmlString);

        /// <summary>
        ///     Tries to serialize an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="xmlString">Out: The serialized object as an XML <see cref="string" />.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerializeToString<T>(T obj, out string xmlString, out Exception exception);

        /// <summary>
        ///     Tries to serialize an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="xmlString">Out: The serialized object as an XML <see cref="string" />.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerializeToString(object obj, Type type, out string xmlString);

        /// <summary>
        ///     Tries to serialize an object to a <see cref="string" /> (UTF-16) of XML content
        ///     using <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="obj">The object being serialized.</param>
        /// <param name="xmlString">Out: The serialized object as an XML <see cref="string" />.</param>
        /// <param name="type">The type for the object being serialized.</param>
        /// <param name="exception">Out: The exception that occurred when serialization failed.</param>
        /// <returns>
        ///     <see langword="true" /> if the serialization was successful,
        ///     otherwise, <see langword="false" />.
        /// </returns>
        bool TrySerializeToString(object obj, Type type, out string xmlString, out Exception exception);

        /// <summary>
        ///     Gets a secure instance of <see cref="XmlReaderSettings" />.
        /// </summary>
        /// <returns>An instance of <see cref="XmlReaderSettings" />.</returns>
        XmlReaderSettings GetSecureXmlReaderSettings();

        /// <summary>
        ///     Gets a instance of <see cref="XmlWriterSettings" />.
        /// </summary>
        /// <returns>An instance of <see cref="XmlWriterSettings" />.</returns>
        XmlWriterSettings GetXmlWriterSettings();
    }
}
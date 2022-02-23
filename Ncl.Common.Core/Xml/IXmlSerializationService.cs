using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Ncl.Common.Core.Xml
{
    /// <summary>
    ///     The interface for a XML serialization service.
    /// </summary>
    public interface IXmlSerializationService
    {
        T ReadObject<T>(string path,
            IEnumerable<Type> knownTypes);

        T ReadObject<T>(string path,
            DataContractSerializerSettings settings = null);

        object ReadObject(string path, Type type, IEnumerable<Type> knownTypes);

        object ReadObject(string path, Type type, DataContractSerializerSettings settings = null);

        T ReadObject<T>(Stream stream, IEnumerable<Type> knownTypes);

        T ReadObject<T>(Stream stream, DataContractSerializerSettings settings = null);

        object ReadObject(Stream stream, Type type, IEnumerable<Type> knownTypes);

        object ReadObject(Stream stream, Type type, DataContractSerializerSettings settings = null);

        T ReadObject<T>(TextReader reader, IEnumerable<Type> knownTypes);

        T ReadObject<T>(TextReader reader, DataContractSerializerSettings settings = null);

        object ReadObject(TextReader reader, Type type, IEnumerable<Type> knownTypes);

        object ReadObject(TextReader reader, Type type, DataContractSerializerSettings settings = null);

        T ReadObject<T>(XmlReader reader, IEnumerable<Type> knownTypes);

        T ReadObject<T>(XmlReader reader, DataContractSerializerSettings settings = null);

        object ReadObject(XmlReader reader, Type type, IEnumerable<Type> knownTypes);

        object ReadObject(XmlReader reader, Type type, DataContractSerializerSettings settings = null);

        bool TryReadObject<T>(string path, out T result,
            IEnumerable<Type> knownTypes);

        bool TryReadObject<T>(string path, out T result,
            DataContractSerializerSettings settings = null);

        bool TryReadObject<T>(string path, out T result, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryReadObject<T>(string path, out T result, out Exception exception,
            DataContractSerializerSettings settings = null);

        bool TryReadObject(string path, Type type, out object result,
            IEnumerable<Type> knownTypes);

        bool TryReadObject(string path, Type type, out object result,
            DataContractSerializerSettings settings = null);

        bool TryReadObject(string path, Type type, out object result, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryReadObject(string path, Type type, out object result, out Exception exception,
            DataContractSerializerSettings settings = null);

        bool TryReadObject<T>(Stream stream, out T result,
            IEnumerable<Type> knownTypes);

        bool TryReadObject<T>(Stream stream, out T result,
            DataContractSerializerSettings settings = null);

        bool TryReadObject<T>(Stream stream, out T result, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryReadObject<T>(Stream stream, out T result, out Exception exception,
            DataContractSerializerSettings settings = null);

        bool TryReadObject(Stream stream, Type type, out object result);

        bool TryReadObject(Stream stream, Type type, out object result, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryReadObject(Stream stream, Type type, out object result, out Exception exception,
            DataContractSerializerSettings settings = null);

        bool TryReadObject<T>(TextReader reader, out T result,
            IEnumerable<Type> knownTypes);

        bool TryReadObject<T>(TextReader reader, out T result,
            DataContractSerializerSettings settings = null);

        bool TryReadObject<T>(TextReader reader, out T result, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryReadObject<T>(TextReader reader, out T result, out Exception exception,
            DataContractSerializerSettings settings = null);

        bool TryReadObject(TextReader reader, Type type, out object result,
            IEnumerable<Type> knownTypes);

        bool TryReadObject(TextReader reader, Type type, out object result,
            DataContractSerializerSettings settings = null);

        bool TryReadObject(TextReader reader, Type type, out object result, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryReadObject(TextReader reader, Type type, out object result, out Exception exception,
            DataContractSerializerSettings settings = null);

        bool TryReadObject<T>(XmlReader reader, out T result,
            IEnumerable<Type> knownTypes);

        bool TryReadObject<T>(XmlReader reader, out T result,
            DataContractSerializerSettings settings = null);

        bool TryReadObject<T>(XmlReader reader, out T result, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryReadObject<T>(XmlReader reader, out T result, out Exception exception,
            DataContractSerializerSettings settings = null);

        bool TryReadObject(XmlReader reader, Type type, out object result,
            IEnumerable<Type> knownTypes);

        bool TryReadObject(XmlReader reader, Type type, out object result,
            DataContractSerializerSettings settings = null);

        bool TryReadObject(XmlReader reader, Type type, out object result, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryReadObject(XmlReader reader, Type type, out object result, out Exception exception,
            DataContractSerializerSettings settings = null);

        T ReadObjectFromString<T>(string xmlString,
            IEnumerable<Type> knownTypes);

        T ReadObjectFromString<T>(string xmlString,
            DataContractSerializerSettings settings = null);

        object ReadObjectFromString(string xmlString, Type type, IEnumerable<Type> knownTypes);
        object ReadObjectFromString(string xmlString, Type type, DataContractSerializerSettings settings = null);

        bool TryReadObjectFromString<T>(string xmlString, out T result,
            IEnumerable<Type> knownTypes);

        bool TryReadObjectFromString<T>(string xmlString, out T result,
            DataContractSerializerSettings settings = null);

        bool TryReadObjectFromString<T>(string xmlString, out T result, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryReadObjectFromString<T>(string xmlString, out T result, out Exception exception,
            DataContractSerializerSettings settings = null);

        bool TryReadObjectFromString(string xmlString, Type type, out object result,
            IEnumerable<Type> knownTypes);

        bool TryReadObjectFromString(string xmlString, Type type, out object result,
            DataContractSerializerSettings settings = null);

        bool TryReadObjectFromString(string xmlString, Type type, out object result,
            out Exception exception, IEnumerable<Type> knownTypes);

        bool TryReadObjectFromString(string xmlString, Type type, out object result,
            out Exception exception, DataContractSerializerSettings settings = null);

        void WriteObject<T>(string path, T obj,
            IEnumerable<Type> knownTypes);

        void WriteObject<T>(string path, T obj,
            DataContractSerializerSettings settings = null);

        void WriteObject(string path, object obj, Type type,
            IEnumerable<Type> knownTypes);

        void WriteObject(string path, object obj, Type type,
            DataContractSerializerSettings settings = null);

        void WriteObject<T>(Stream stream, T obj, IEnumerable<Type> knownTypes);

        void WriteObject<T>(Stream stream, T obj, DataContractSerializerSettings settings = null);

        void WriteObject(Stream stream, object obj, Type type,
            IEnumerable<Type> knownTypes);

        void WriteObject(Stream stream, object obj, Type type,
            DataContractSerializerSettings settings = null);

        void WriteObject<T>(TextWriter writer, T obj, IEnumerable<Type> knownTypes);
        void WriteObject<T>(TextWriter writer, T obj, DataContractSerializerSettings settings = null);

        void WriteObject(TextWriter writer, object obj, Type type,
            IEnumerable<Type> knownTypes);

        void WriteObject(TextWriter writer, object obj, Type type,
            DataContractSerializerSettings settings = null);

        void WriteObject<T>(XmlWriter writer, T obj, IEnumerable<Type> knownTypes);

        void WriteObject<T>(XmlWriter writer, T obj, DataContractSerializerSettings settings = null);

        void WriteObject(XmlWriter writer, object obj, Type type,
            IEnumerable<Type> knownTypes);

        void WriteObject(XmlWriter writer, object obj, Type type,
            DataContractSerializerSettings settings = null);

        bool TryWriteObject<T>(string path, T obj,
            IEnumerable<Type> knownTypes);

        bool TryWriteObject<T>(string path, T obj,
            DataContractSerializerSettings settings = null);

        bool TryWriteObject<T>(string path, T obj, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryWriteObject<T>(string path, T obj, out Exception exception,
            DataContractSerializerSettings settings = null);

        bool TryWriteObject(string path, object obj, Type type,
            IEnumerable<Type> knownTypes);

        bool TryWriteObject(string path, object obj, Type type,
            DataContractSerializerSettings settings = null);

        bool TryWriteObject(string path, object obj, Type type, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryWriteObject(string path, object obj, Type type, out Exception exception,
            DataContractSerializerSettings settings = null);

        bool TryWriteObject<T>(Stream stream, T obj, IEnumerable<Type> knownTypes);

        bool TryWriteObject<T>(Stream stream, T obj, DataContractSerializerSettings settings = null);

        bool TryWriteObject<T>(Stream stream, T obj, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryWriteObject<T>(Stream stream, T obj, out Exception exception,
            DataContractSerializerSettings settings = null);

        bool TryWriteObject(Stream stream, object obj, Type type,
            IEnumerable<Type> knownTypes);

        bool TryWriteObject(Stream stream, object obj, Type type,
            DataContractSerializerSettings settings = null);

        bool TryWriteObject(Stream stream, object obj, Type type, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryWriteObject(Stream stream, object obj, Type type, out Exception exception,
            DataContractSerializerSettings settings = null);

        bool TryWriteObject<T>(TextWriter writer, T obj, IEnumerable<Type> knownTypes);

        bool TryWriteObject<T>(TextWriter writer, T obj, DataContractSerializerSettings settings = null);

        bool TryWriteObject<T>(TextWriter writer, T obj, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryWriteObject<T>(TextWriter writer, T obj, out Exception exception,
            DataContractSerializerSettings settings = null);

        bool TryWriteObject(TextWriter writer, object obj, Type type,
            IEnumerable<Type> knownTypes);

        bool TryWriteObject(TextWriter writer, object obj, Type type,
            DataContractSerializerSettings settings = null);

        bool TryWriteObject(TextWriter writer, object obj, Type type, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryWriteObject(TextWriter writer, object obj, Type type, out Exception exception,
            DataContractSerializerSettings settings = null);

        bool TryWriteObject<T>(XmlWriter writer, T obj, IEnumerable<Type> knownTypes);

        bool TryWriteObject<T>(XmlWriter writer, T obj, DataContractSerializerSettings settings = null);

        bool TryWriteObject<T>(XmlWriter writer, T obj, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryWriteObject<T>(XmlWriter writer, T obj, out Exception exception,
            DataContractSerializerSettings settings = null);

        bool TryWriteObject(XmlWriter writer, object obj, Type type,
            IEnumerable<Type> knownTypes);

        bool TryWriteObject(XmlWriter writer, object obj, Type type,
            DataContractSerializerSettings settings = null);

        bool TryWriteObject(XmlWriter writer, object obj, Type type, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryWriteObject(XmlWriter writer, object obj, Type type, out Exception exception,
            DataContractSerializerSettings settings = null);

        string WriteObjectToString<T>(T obj, IEnumerable<Type> knownTypes);

        string WriteObjectToString<T>(T obj, DataContractSerializerSettings settings = null);

        string WriteObjectToString(object obj, Type type, IEnumerable<Type> knownTypes);

        string WriteObjectToString(object obj, Type type, DataContractSerializerSettings settings = null);

        bool TryWriteObjectToString<T>(T obj, out string xmlString, IEnumerable<Type> knownTypes);

        bool TryWriteObjectToString<T>(T obj, out string xmlString,
            DataContractSerializerSettings settings = null);

        bool TryWriteObjectToString<T>(T obj, out string xmlString, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryWriteObjectToString<T>(T obj, out string xmlString, out Exception exception,
            DataContractSerializerSettings settings = null);

        bool TryWriteObjectToString(object obj, Type type, out string xmlString,
            IEnumerable<Type> knownTypes);

        bool TryWriteObjectToString(object obj, Type type, out string xmlString,
            DataContractSerializerSettings settings = null);

        bool TryWriteObjectToString(object obj, Type type, out string xmlString, out Exception exception,
            IEnumerable<Type> knownTypes);

        bool TryWriteObjectToString(object obj, Type type, out string xmlString, out Exception exception,
            DataContractSerializerSettings settings = null);

        T Deserialize<T>(string path);

        object Deserialize(string path, Type type);

        T Deserialize<T>(Stream stream);

        object Deserialize(Stream stream, Type type);

        T Deserialize<T>(TextReader reader);

        object Deserialize(TextReader reader, Type type);

        T Deserialize<T>(XmlReader reader, bool skipSettingsCheck = false);

        object Deserialize(XmlReader reader, Type type, bool skipSettingsCheck = false);

        bool TryDeserialize<T>(string path, out T result);

        bool TryDeserialize<T>(string path, out T result, out Exception exception);

        bool TryDeserialize(string path, Type type, out object result);

        bool TryDeserialize(string path, Type type, out object result, out Exception exception);

        bool TryDeserialize<T>(Stream stream, out T result);

        bool TryDeserialize<T>(Stream stream, out T result, out Exception exception);

        bool TryDeserialize(Stream stream, Type type, out object result);

        bool TryDeserialize(Stream stream, Type type, out object result, out Exception exception);

        bool TryDeserialize<T>(TextReader reader, out T result);

        bool TryDeserialize<T>(TextReader reader, out T result, out Exception exception);

        bool TryDeserialize(TextReader reader, Type type, out object result);

        bool TryDeserialize(TextReader reader, Type type, out object result, out Exception exception);

        bool TryDeserialize<T>(XmlReader reader, out T result,
            bool skipSettingsCheck = false);

        bool TryDeserialize<T>(XmlReader reader, out T result, out Exception exception,
            bool skipSettingsCheck = false);

        bool TryDeserialize(XmlReader reader, Type type, out object result,
            bool skipSettingsCheck = false);

        bool TryDeserialize(XmlReader reader, Type type, out object result, out Exception exception,
            bool skipSettingsCheck = false);

        T DeserializeFromString<T>(string xmlString);

        object DeserializeFromString(string xmlString, Type type);

        bool TryDeserializeFromString<T>(string xmlString, out T result);

        bool TryDeserializeFromString<T>(string xmlString, out T result, out Exception exception);

        bool TryDeserializeFromString(string xmlString, Type type, out object result);

        bool TryDeserializeFromString(string xmlString, Type type, out object result,
            out Exception exception);

        void Serialize<T>(string path, T obj);

        void Serialize(string path, object obj, Type type);

        void Serialize<T>(Stream stream, T obj);

        void Serialize(Stream stream, object obj, Type type);

        void Serialize<T>(TextWriter writer, T obj);

        void Serialize(TextWriter writer, object obj, Type type);

        void Serialize<T>(XmlWriter writer, T obj);

        void Serialize(XmlWriter writer, object obj, Type type);

        bool TrySerialize<T>(string path, T obj);

        bool TrySerialize<T>(string path, T obj, out Exception exception);

        bool TrySerialize(string path, object obj, Type type);

        bool TrySerialize(string path, object obj, Type type, out Exception exception);

        bool TrySerialize<T>(Stream stream, T obj);

        bool TrySerialize<T>(Stream stream, T obj, out Exception exception);

        bool TrySerialize(Stream stream, object obj, Type type);

        bool TrySerialize(Stream stream, object obj, Type type, out Exception exception);

        bool TrySerialize<T>(TextWriter writer, T obj);

        bool TrySerialize<T>(TextWriter writer, T obj, out Exception exception);

        bool TrySerialize(TextWriter writer, object obj, Type type);

        bool TrySerialize(TextWriter writer, object obj, Type type, out Exception exception);

        bool TrySerialize<T>(XmlWriter writer, T obj);

        bool TrySerialize<T>(XmlWriter writer, T obj, out Exception exception);

        bool TrySerialize(XmlWriter writer, object obj, Type type);

        bool TrySerialize(XmlWriter writer, object obj, Type type, out Exception exception);

        string SerializeToString<T>(T obj);

        string SerializeToString(object obj, Type type);

        bool TrySerializeToString<T>(T obj, out string xmlString);

        bool TrySerializeToString<T>(T obj, out string xmlString, out Exception exception);

        bool TrySerializeToString(object obj, Type type, out string xmlString);

        bool TrySerializeToString(object obj, Type type, out string xmlString, out Exception exception);

        XmlReaderSettings GetSecureXmlReaderSettings();

        XmlWriterSettings GetXmlWriterSettings();
    }
}
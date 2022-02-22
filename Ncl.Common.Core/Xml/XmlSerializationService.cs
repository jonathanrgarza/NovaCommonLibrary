using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Ncl.Common.Core.Utilities;

namespace Ncl.Common.Core.Xml
{
    public class XmlSerializationService
    {
        public T Deserialize<T>(string path)
        {
            if (TryDeserialize(path, out T result, out Exception exception))
                return result;

            throw exception;
        }

        public bool TryDeserialize<T>(string path, out T result)
        {
            return TryDeserialize(path, out result, out Exception _);
        }

        public bool TryDeserialize<T>(string path, out T result, out Exception exception)
        {
            Type type = typeof(T);
            if (TryDeserialize(path, type, out object resultObj, out exception))
            {
                result = (T) resultObj;
                return true;
            }

            result = default;
            return false;
        }

        public object Deserialize(string path, Type type)
        {
            if (TryDeserialize(path, type, out object result, out Exception exception))
                return result;

            throw exception;
        }

        public bool TryDeserialize(string path, Type type, out object result)
        {
            return TryDeserialize(path, type, out result, out Exception _);
        }

        public bool TryDeserialize(string path, Type type, out object result, out Exception exception)
        {
            result = default;

            try
            {
                Guard.AgainstNullArgument(path, nameof(path));
            }
            catch (ArgumentNullException e)
            {
                exception = e;
                return false;
            }

            XmlReaderSettings settings = GetSecureXmlReaderSettings();
            using (var xmlReader = XmlReader.Create(path, settings))
            {
                if (!TryDeserialize(xmlReader, type, out object resultObj, out exception))
                    return false;

                result = resultObj;
                return true;
            }
        }

        public T Deserialize<T>(Stream stream)
        {
            if (TryDeserialize(stream, out T result, out Exception exception))
                return result;

            throw exception;
        }

        public bool TryDeserialize<T>(Stream stream, out T result)
        {
            return TryDeserialize(stream, out result, out Exception _);
        }

        public bool TryDeserialize<T>(Stream stream, out T result, out Exception exception)
        {
            Type type = typeof(T);
            if (TryDeserialize(stream, type, out object resultObj, out exception))
            {
                result = (T) resultObj;
                return true;
            }

            result = default;
            return false;
        }

        public object Deserialize(Stream stream, Type type)
        {
            if (TryDeserialize(stream, type, out object result, out Exception exception))
                return result;

            throw exception;
        }

        public bool TryDeserialize(Stream stream, Type type, out object result)
        {
            return TryDeserialize(stream, type, out result, out Exception _);
        }

        public bool TryDeserialize(Stream stream, Type type, out object result, out Exception exception)
        {
            result = default;

            try
            {
                Guard.AgainstNullArgument(stream, nameof(stream));
            }
            catch (ArgumentNullException e)
            {
                exception = e;
                return false;
            }

            XmlReaderSettings settings = GetSecureXmlReaderSettings();
            using (var xmlReader = XmlReader.Create(stream, settings))
            {
                if (!TryDeserialize(xmlReader, type, out object resultObj, out exception))
                    return false;

                result = resultObj;
                return true;
            }
        }

        public T Deserialize<T>(TextReader reader)
        {
            if (TryDeserialize(reader, out T result, out Exception exception))
                return result;

            throw exception;
        }

        public bool TryDeserialize<T>(TextReader reader, out T result)
        {
            return TryDeserialize(reader, out result, out Exception _);
        }

        public bool TryDeserialize<T>(TextReader reader, out T result, out Exception exception)
        {
            Type type = typeof(T);
            if (TryDeserialize(reader, type, out object resultObj, out exception))
            {
                result = (T) resultObj;
                return true;
            }

            result = default;
            return false;
        }

        public object Deserialize(TextReader reader, Type type)
        {
            if (TryDeserialize(reader, type, out object result, out Exception exception))
                return result;

            throw exception;
        }

        public bool TryDeserialize(TextReader reader, Type type, out object result)
        {
            return TryDeserialize(reader, type, out result, out Exception _);
        }

        public bool TryDeserialize(TextReader reader, Type type, out object result, out Exception exception)
        {
            result = default;

            try
            {
                Guard.AgainstNullArgument(reader, nameof(reader));
            }
            catch (ArgumentNullException e)
            {
                exception = e;
                return false;
            }

            XmlReaderSettings settings = GetSecureXmlReaderSettings();
            using (var xmlReader = XmlReader.Create(reader, settings))
            {
                if (!TryDeserialize(xmlReader, type, out object resultObj, out exception))
                    return false;

                result = resultObj;
                return true;
            }
        }

        public T Deserialize<T>(XmlReader reader, bool skipSettingsCheck = false)
        {
            Type type = typeof(T);
            if (TryDeserialize(reader, type, out object result, out Exception exception,
                    skipSettingsCheck))
                return (T) result;

            throw exception;
        }

        public bool TryDeserialize<T>(XmlReader reader, out T result,
            bool skipSettingsCheck = false)
        {
            Type type = typeof(T);
            if (TryDeserialize(reader, type, out object resultObj, out _,
                    skipSettingsCheck))
            {
                result = (T) resultObj;
                return true;
            }

            result = default;
            return false;
        }

        public bool TryDeserialize<T>(XmlReader reader, out T result, out Exception exception,
            bool skipSettingsCheck = false)
        {
            Type type = typeof(T);
            if (TryDeserialize(reader, type, out object resultObj, out exception,
                    skipSettingsCheck))
            {
                result = (T) resultObj;
                return true;
            }

            result = default;
            return false;
        }

        public object Deserialize(XmlReader reader, Type type, bool skipSettingsCheck = false)
        {
            if (TryDeserialize(reader, type, out object result, out Exception exception,
                    skipSettingsCheck))
                return result;

            throw exception;
        }

        public bool TryDeserialize(XmlReader reader, Type type, out object result,
            bool skipSettingsCheck = false)
        {
            return TryDeserialize(reader, type, out result, out Exception _, skipSettingsCheck);
        }

        public bool TryDeserialize(XmlReader reader, Type type, out object result, out Exception exception,
            bool skipSettingsCheck = false)
        {
            result = null;
            exception = null;
            try
            {
                Guard.AgainstNullArgument(reader, nameof(reader));
                Guard.AgainstNullArgument(type, nameof(type));

                if (!skipSettingsCheck)
                {
                    GuardAgainstInsecureSettings(reader.Settings);
                }

                var xmlSerializer = new XmlSerializer(type);

                if (!xmlSerializer.CanDeserialize(reader))
                {
                    throw new XmlException(
                        $"XML content can not be deserialized into the given type '{type.FullName}'");
                }

                result = xmlSerializer.Deserialize(reader);
                return true;
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public T DeserializeFromString<T>(string xmlString)
        {
            if (TryDeserializeFromString(xmlString, out T result, out Exception exception))
                return result;

            throw exception;
        }

        public bool TryDeserializeFromString<T>(string xmlString, out T result)
        {
            return TryDeserializeFromString(xmlString, out result, out Exception _);
        }

        public bool TryDeserializeFromString<T>(string xmlString, out T result, out Exception exception)
        {
            Type type = typeof(T);
            if (TryDeserializeFromString(xmlString, type, out object resultObj, out exception))
            {
                result = (T) resultObj;
                return true;
            }

            result = default;
            return false;
        }

        public object DeserializeFromString(string xmlString, Type type)
        {
            if (TryDeserializeFromString(xmlString, type, out object result, out Exception exception))
                return result;

            throw exception;
        }

        public bool TryDeserializeFromString(string xmlString, Type type, out object result)
        {
            return TryDeserializeFromString(xmlString, type, out result, out Exception _);
        }

        public bool TryDeserializeFromString(string xmlString, Type type, out object result, out Exception exception)
        {
            result = default;

            try
            {
                Guard.AgainstNullArgument(xmlString, nameof(xmlString));
            }
            catch (ArgumentNullException e)
            {
                exception = e;
                return false;
            }

            XmlReaderSettings settings = GetSecureXmlReaderSettings();

            using (var stringReader = new StringReader(xmlString))
            using (var xmlReader = XmlReader.Create(stringReader, settings))
            {
                if (!TryDeserialize(xmlReader, type, out object resultObj, out exception))
                    return false;

                result = resultObj;
                return true;
            }
        }

        public void Serialize<T>(string path, T obj)
        {
            if (TrySerialize(path, obj, out Exception exception))
                return;

            throw exception;
        }

        public bool TrySerialize<T>(string path, T obj)
        {
            return TrySerialize(path, obj, out Exception _);
        }

        public bool TrySerialize<T>(string path, T obj, out Exception exception)
        {
            Type type = typeof(T);
            //TODO: Revisit generic goes to Type function, maybe make generic implementation also
            return TrySerialize(path, obj, type, out exception);
        }

        public void Serialize(string path, object obj, Type type)
        {
            if (TrySerialize(path, obj, type, out Exception exception))
                return;

            throw exception;
        }

        public bool TrySerialize(string path, object obj, Type type)
        {
            return TrySerialize(path, obj, type, out Exception _);
        }

        public bool TrySerialize(string path, object obj, Type type, out Exception exception)
        {
            try
            {
                Guard.AgainstNullArgument(path, nameof(path));
            }
            catch (ArgumentNullException e)
            {
                exception = e;
                return false;
            }

            XmlWriterSettings settings = GetXmlWriterSettings();
            //TODO: Double check create, maybe use FileStream
            using (var xmlWriter = XmlWriter.Create(path, settings))
            {
                return TrySerialize(xmlWriter, obj, type, out exception);
            }
        }

        public void Serialize<T>(Stream stream, T obj)
        {
            if (TrySerialize(stream, obj, out Exception exception))
                return;

            throw exception;
        }

        public bool TrySerialize<T>(Stream stream, T obj)
        {
            return TrySerialize(stream, obj, out Exception _);
        }

        public bool TrySerialize<T>(Stream stream, T obj, out Exception exception)
        {
            Type type = typeof(T);
            return TrySerialize(stream, obj, type, out exception);
        }

        public void Serialize(Stream stream, object obj, Type type)
        {
            if (TrySerialize(stream, obj, type, out Exception exception))
                return;

            throw exception;
        }

        public bool TrySerialize(Stream stream, object obj, Type type)
        {
            return TrySerialize(stream, obj, type, out Exception _);
        }

        public bool TrySerialize(Stream stream, object obj, Type type, out Exception exception)
        {
            try
            {
                Guard.AgainstNullArgument(stream, nameof(stream));
            }
            catch (ArgumentNullException e)
            {
                exception = e;
                return false;
            }

            XmlWriterSettings settings = GetXmlWriterSettings();
            using (var xmlWriter = XmlWriter.Create(stream, settings))
            {
                return TrySerialize(xmlWriter, obj, type, out exception);
            }
        }

        public void Serialize<T>(TextWriter writer, T obj)
        {
            if (TrySerialize(writer, obj, out Exception exception))
                return;

            throw exception;
        }

        public bool TrySerialize<T>(TextWriter writer, T obj)
        {
            return TrySerialize(writer, obj, out Exception _);
        }

        public bool TrySerialize<T>(TextWriter writer, T obj, out Exception exception)
        {
            Type type = typeof(T);
            return TrySerialize(writer, obj, type, out exception);
        }

        public void Serialize(TextWriter writer, object obj, Type type)
        {
            if (TrySerialize(writer, obj, type, out Exception exception))
                return;

            throw exception;
        }

        public bool TrySerialize(TextWriter writer, object obj, Type type)
        {
            return TrySerialize(writer, obj, type, out Exception _);
        }

        public bool TrySerialize(TextWriter writer, object obj, Type type, out Exception exception)
        {
            try
            {
                Guard.AgainstNullArgument(writer, nameof(writer));
            }
            catch (ArgumentNullException e)
            {
                exception = e;
                return false;
            }

            XmlWriterSettings settings = GetXmlWriterSettings();
            using (var xmlWriter = XmlWriter.Create(writer, settings))
            {
                return TrySerialize(xmlWriter, obj, type, out exception);
            }
        }

        public void Serialize<T>(XmlWriter writer, T obj)
        {
            Type type = typeof(T);
            if (TrySerialize(writer, obj, type, out Exception exception))
                return;

            throw exception;
        }

        public bool TrySerialize<T>(XmlWriter writer, T obj)
        {
            Type type = typeof(T);
            return TrySerialize(writer, obj, type, out _);
        }

        public bool TrySerialize<T>(XmlWriter writer, T obj, out Exception exception)
        {
            Type type = typeof(T);
            return TrySerialize(writer, obj, type, out exception);
        }

        public void Serialize(XmlWriter writer, object obj, Type type)
        {
            if (TrySerialize(writer, obj, type, out Exception exception))
                return;

            throw exception;
        }

        public bool TrySerialize(XmlWriter writer, object obj, Type type)
        {
            return TrySerialize(writer, obj, type, out Exception _);
        }

        public bool TrySerialize(XmlWriter writer, object obj, Type type, out Exception exception)
        {
            exception = null;
            try
            {
                Guard.AgainstNullArgument(writer, nameof(writer));
                Guard.AgainstNullArgument(obj, nameof(obj));
                Guard.AgainstNullArgument(type, nameof(type));

                var xmlSerializer = new XmlSerializer(type);

                xmlSerializer.Serialize(writer, obj);
                return true;
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public string SerializeToString<T>(T obj)
        {
            if (TrySerializeToString(obj, out string xmlString, out Exception exception))
                return xmlString;

            throw exception;
        }

        public bool TrySerializeToString<T>(T obj, out string xmlString)
        {
            return TrySerializeToString(obj, out xmlString, out Exception _);
        }

        public bool TrySerializeToString<T>(T obj, out string xmlString, out Exception exception)
        {
            Type type = typeof(T);
            return TrySerializeToString(obj, type, out xmlString, out exception);
        }

        public string SerializeToString(object obj, Type type)
        {
            if (TrySerializeToString(obj, type, out string xmlString, out Exception exception))
                return xmlString;

            throw exception;
        }

        public bool TrySerializeToString(object obj, Type type, out string xmlString)
        {
            return TrySerializeToString(obj, type, out xmlString, out Exception _);
        }

        public bool TrySerializeToString(object obj, Type type, out string xmlString, out Exception exception)
        {
            xmlString = default;

            try
            {
                Guard.AgainstNullArgument(obj, nameof(obj));
            }
            catch (ArgumentNullException e)
            {
                exception = e;
                return false;
            }

            XmlWriterSettings settings = GetXmlWriterSettings();

            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
            {
                if (!TrySerialize(xmlWriter, obj, type, out exception))
                    return false;

                xmlString = stringWriter.ToString();
                return true;
            }
        }

        public XmlReaderSettings GetSecureXmlReaderSettings()
        {
            return new XmlReaderSettings
            {
                XmlResolver = null,
                DtdProcessing = DtdProcessing.Ignore
            };
        }

        public virtual XmlWriterSettings GetXmlWriterSettings()
        {
            return new XmlWriterSettings
            {
                Indent = true
            };
        }

        protected virtual void GuardAgainstInsecureSettings(XmlReaderSettings settings)
        {
            Guard.AgainstNullArgument(settings, nameof(settings));

            if (settings.DtdProcessing == DtdProcessing.Parse)
                throw new ArgumentException("XML reader settings are not properly secure");

            //XmlResolver is not accessible on a XmlReaderSettings so that can not be checked for null
        }
    }
}
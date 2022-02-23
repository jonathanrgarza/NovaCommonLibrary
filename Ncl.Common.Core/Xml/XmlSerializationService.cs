using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Ncl.Common.Core.Utilities;

namespace Ncl.Common.Core.Xml
{
    public class XmlSerializationService
    {
        public T ReadObject<T>(string path,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryReadObject(path, out T result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public T ReadObject<T>(string path,
            DataContractSerializerSettings settings = null)
        {
            if (TryReadObject(path, out T result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public bool TryReadObject<T>(string path, out T result,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObject(path, out result, out Exception _, settings);
        }

        public bool TryReadObject<T>(string path, out T result,
            DataContractSerializerSettings settings = null)
        {
            return TryReadObject(path, out result, out Exception _, settings);
        }

        public bool TryReadObject<T>(string path, out T result, out Exception exception,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObject(path, out result, out exception, settings);
        }

        public virtual bool TryReadObject<T>(string path, out T result, out Exception exception,
            DataContractSerializerSettings settings = null)
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

            try
            {
                //TODO: Double check create, maybe use FileStream
                XmlReaderSettings xmlSettings = GetSecureXmlReaderSettings();
                using (var xmlReader = XmlReader.Create(path, xmlSettings))
                {
                    return TryReadObject(xmlReader, out result, out exception, settings);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public object ReadObject(string path, Type type, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryReadObject(path, type, out object result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public object ReadObject(string path, Type type, DataContractSerializerSettings settings = null)
        {
            if (TryReadObject(path, type, out object result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public bool TryReadObject(string path, Type type, out object result,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObject(path, type, out result, out Exception _, settings);
        }

        public bool TryReadObject(string path, Type type, out object result,
            DataContractSerializerSettings settings = null)
        {
            return TryReadObject(path, type, out result, out Exception _, settings);
        }

        public virtual bool TryReadObject(string path, Type type, out object result, out Exception exception,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObject(path, type, out result, out exception, settings);
        }

        public virtual bool TryReadObject(string path, Type type, out object result, out Exception exception,
            DataContractSerializerSettings settings = null)
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

            try
            {
                //TODO: Double check create, maybe use FileStream
                XmlReaderSettings xmlSettings = GetSecureXmlReaderSettings();
                using (var xmlReader = XmlReader.Create(path, xmlSettings))
                {
                    if (!TryReadObject(xmlReader, type, out object resultObj, out exception, settings))
                        return false;

                    result = resultObj;
                    return true;
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public T ReadObject<T>(Stream stream, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryReadObject(stream, out T result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public T ReadObject<T>(Stream stream, DataContractSerializerSettings settings = null)
        {
            if (TryReadObject(stream, out T result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public bool TryReadObject<T>(Stream stream, out T result,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObject(stream, out result, out Exception _, settings);
        }

        public bool TryReadObject<T>(Stream stream, out T result,
            DataContractSerializerSettings settings = null)
        {
            return TryReadObject(stream, out result, out Exception _, settings);
        }

        public virtual bool TryReadObject<T>(Stream stream, out T result, out Exception exception,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObject(stream, out result, out exception, settings);
        }

        public virtual bool TryReadObject<T>(Stream stream, out T result, out Exception exception,
            DataContractSerializerSettings settings = null)
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

            try
            {
                XmlReaderSettings xmlSettings = GetSecureXmlReaderSettings();
                using (var xmlReader = XmlReader.Create(stream, xmlSettings))
                {
                    return TryReadObject(xmlReader, out result, out exception, settings);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public object ReadObject(Stream stream, Type type, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryReadObject(stream, type, out object result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public object ReadObject(Stream stream, Type type, DataContractSerializerSettings settings = null)
        {
            if (TryReadObject(stream, type, out object result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public bool TryReadObject(Stream stream, Type type, out object result)
        {
            return TryReadObject(stream, type, out result, out Exception _);
        }

        public bool TryReadObject(Stream stream, Type type, out object result, out Exception exception,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObject(stream, type, out result, out exception, settings);
        }

        public virtual bool TryReadObject(Stream stream, Type type, out object result, out Exception exception,
            DataContractSerializerSettings settings = null)
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

            try
            {
                XmlReaderSettings xmlSettings = GetSecureXmlReaderSettings();
                using (var xmlReader = XmlReader.Create(stream, xmlSettings))
                {
                    if (!TryReadObject(xmlReader, type, out object resultObj, out exception, settings))
                        return false;

                    result = resultObj;
                    return true;
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public T ReadObject<T>(TextReader reader, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryReadObject(reader, out T result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public T ReadObject<T>(TextReader reader, DataContractSerializerSettings settings = null)
        {
            if (TryReadObject(reader, out T result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public bool TryReadObject<T>(TextReader reader, out T result,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObject(reader, out result, out Exception _, settings);
        }

        public bool TryReadObject<T>(TextReader reader, out T result,
            DataContractSerializerSettings settings = null)
        {
            return TryReadObject(reader, out result, out Exception _, settings);
        }

        public bool TryReadObject<T>(TextReader reader, out T result, out Exception exception, 
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObject(reader, out result, out exception, settings);
        }

        public virtual bool TryReadObject<T>(TextReader reader, out T result, out Exception exception, 
            DataContractSerializerSettings settings = null)
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

            try
            {
                XmlReaderSettings xmlSettings = GetSecureXmlReaderSettings();
                using (var xmlReader = XmlReader.Create(reader, xmlSettings))
                {
                    return TryReadObject(xmlReader, out result, out exception, settings);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public object ReadObject(TextReader reader, Type type, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryReadObject(reader, type, out object result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public object ReadObject(TextReader reader, Type type, DataContractSerializerSettings settings = null)
        {
            if (TryReadObject(reader, type, out object result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public bool TryReadObject(TextReader reader, Type type, out object result, 
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObject(reader, type, out result, out Exception _, settings);
        }

        public bool TryReadObject(TextReader reader, Type type, out object result, 
            DataContractSerializerSettings settings = null)
        {
            return TryReadObject(reader, type, out result, out Exception _, settings);
        }

        public bool TryReadObject(TextReader reader, Type type, out object result, out Exception exception, 
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObject(reader, type, out result, out exception, settings);
        }

        public virtual bool TryReadObject(TextReader reader, Type type, out object result, out Exception exception, 
            DataContractSerializerSettings settings = null)
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

            try
            {
                XmlReaderSettings xmlSettings = GetSecureXmlReaderSettings();
                using (var xmlReader = XmlReader.Create(reader, xmlSettings))
                {
                    return TryReadObject(xmlReader, type, out result, out exception, settings);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public T ReadObject<T>(XmlReader reader, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryReadObject(reader, out T result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public T ReadObject<T>(XmlReader reader, DataContractSerializerSettings settings = null)
        {
            if (TryReadObject(reader, out T result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public bool TryReadObject<T>(XmlReader reader, out T result,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObject(reader, out result, out Exception _, settings);
        }

        public bool TryReadObject<T>(XmlReader reader, out T result,
            DataContractSerializerSettings settings = null)
        {
            return TryReadObject(reader, out result, out Exception _, settings);
        }

        public bool TryReadObject<T>(XmlReader reader, out T result, out Exception exception,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObject(reader, out result, out exception, settings);
        }

        public virtual bool TryReadObject<T>(XmlReader reader, out T result, out Exception exception,
            DataContractSerializerSettings settings = null)
        {
            result = default;
            exception = null;
            try
            {
                Guard.AgainstNullArgument(reader, nameof(reader));

                Type type = typeof(T);
                DataContractSerializer dataContractSerializer = settings == null ? 
                    new DataContractSerializer(type) : new DataContractSerializer(type, settings);
                

                result = (T)dataContractSerializer.ReadObject(reader);
                return true;
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public object ReadObject(XmlReader reader, Type type, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryReadObject(reader, type, out object result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public object ReadObject(XmlReader reader, Type type, DataContractSerializerSettings settings = null)
        {
            if (TryReadObject(reader, type, out object result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public bool TryReadObject(XmlReader reader, Type type, out object result,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObject(reader, type, out result, out Exception _, settings);
        }

        public bool TryReadObject(XmlReader reader, Type type, out object result,
            DataContractSerializerSettings settings = null)
        {
            return TryReadObject(reader, type, out result, out Exception _, settings);
        }

        public bool TryReadObject(XmlReader reader, Type type, out object result, out Exception exception,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObject(reader, type, out result, out exception, settings);
        }

        public virtual bool TryReadObject(XmlReader reader, Type type, out object result, out Exception exception,
            DataContractSerializerSettings settings = null)
        {
            result = null;
            exception = null;
            try
            {
                Guard.AgainstNullArgument(reader, nameof(reader));
                Guard.AgainstNullArgument(type, nameof(type));

                DataContractSerializer dataContractSerializer = settings == null ? 
                    new DataContractSerializer(type) : new DataContractSerializer(type, settings);
                

                result = dataContractSerializer.ReadObject(reader);
                return true;
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public T ReadObjectFromString<T>(string xmlString,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryReadObjectFromString(xmlString, out T result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public T ReadObjectFromString<T>(string xmlString,
            DataContractSerializerSettings settings = null)
        {
            if (TryReadObjectFromString(xmlString, out T result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public bool TryReadObjectFromString<T>(string xmlString, out T result, 
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObjectFromString(xmlString, out result, out Exception _, settings);
        }

        public bool TryReadObjectFromString<T>(string xmlString, out T result, 
            DataContractSerializerSettings settings = null)
        {
            return TryReadObjectFromString(xmlString, out result, out Exception _, settings);
        }

        public virtual bool TryReadObjectFromString<T>(string xmlString, out T result, out Exception exception,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObjectFromString(xmlString, out result, out exception, settings);
        }

        public virtual bool TryReadObjectFromString<T>(string xmlString, out T result, out Exception exception,
            DataContractSerializerSettings settings = null)
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

            try
            {
                XmlReaderSettings xmlSettings = GetSecureXmlReaderSettings();

                using (var stringReader = new StringReader(xmlString))
                using (var xmlReader = XmlReader.Create(stringReader, xmlSettings))
                {
                    return TryReadObject(xmlReader, out result, out exception, settings);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public object ReadObjectFromString(string xmlString, Type type, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryReadObjectFromString(xmlString, type, out object result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public object ReadObjectFromString(string xmlString, Type type, DataContractSerializerSettings settings = null)
        {
            if (TryReadObjectFromString(xmlString, type, out object result, out Exception exception, settings))
                return result;

            throw exception;
        }

        public bool TryReadObjectFromString(string xmlString, Type type, out object result, 
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObjectFromString(xmlString, type, out result, out Exception _, settings);
        }

        public bool TryReadObjectFromString(string xmlString, Type type, out object result, 
            DataContractSerializerSettings settings = null)
        {
            return TryReadObjectFromString(xmlString, type, out result, out Exception _, settings);
        }

        public bool TryReadObjectFromString(string xmlString, Type type, out object result,
            out Exception exception, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryReadObjectFromString(xmlString, type, out result, out exception, settings);
        }

        public virtual bool TryReadObjectFromString(string xmlString, Type type, out object result, out Exception exception,
            DataContractSerializerSettings settings = null)
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

            try
            {
                XmlReaderSettings xmlSettings = GetSecureXmlReaderSettings();

                using (var stringReader = new StringReader(xmlString))
                using (var xmlReader = XmlReader.Create(stringReader, xmlSettings))
                {
                    if (!TryReadObject(xmlReader, type, out object resultObj, out exception, settings))
                        return false;

                    result = resultObj;
                    return true;
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public void WriteObject<T>(string path, T obj,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryWriteObject(path, obj, out Exception exception, settings))
                return;

            throw exception;
        }

        public void WriteObject<T>(string path, T obj,
            DataContractSerializerSettings settings = null)
        {
            if (TryWriteObject(path, obj, out Exception exception, settings))
                return;

            throw exception;
        }

        public bool TryWriteObject<T>(string path, T obj,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObject(path, obj, out Exception _, settings);
        }

        public bool TryWriteObject<T>(string path, T obj,
            DataContractSerializerSettings settings = null)
        {
            return TryWriteObject(path, obj, out Exception _, settings);
        }

        public bool TryWriteObject<T>(string path, T obj, out Exception exception,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObject(path, obj, out exception, settings);
        }

        public virtual bool TryWriteObject<T>(string path, T obj, out Exception exception,
            DataContractSerializerSettings settings = null)
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

            try
            {
                XmlWriterSettings xmlSettings = GetXmlWriterSettings();
                //TODO: Double check create, maybe use FileStream
                using (var xmlWriter = XmlWriter.Create(path, xmlSettings))
                {
                    return TryWriteObject(xmlWriter, obj, out exception, settings);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public void WriteObject(string path, object obj, Type type,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryWriteObject(path, obj, type, out Exception exception, settings))
                return;

            throw exception;
        }

        public void WriteObject(string path, object obj, Type type,
            DataContractSerializerSettings settings = null)
        {
            if (TryWriteObject(path, obj, type, out Exception exception, settings))
                return;

            throw exception;
        }

        public bool TryWriteObject(string path, object obj, Type type,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObject(path, obj, type, out Exception _, settings);
        }

        public bool TryWriteObject(string path, object obj, Type type,
            DataContractSerializerSettings settings = null)
        {
            return TryWriteObject(path, obj, type, out Exception _, settings);
        }

        public bool TryWriteObject(string path, object obj, Type type, out Exception exception,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObject(path, obj, type, out exception, settings);
        }

        public virtual bool TryWriteObject(string path, object obj, Type type, out Exception exception,
            DataContractSerializerSettings settings = null)
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

            try
            {
                XmlWriterSettings xmlSettings = GetXmlWriterSettings();
                //TODO: Double check create, maybe use FileStream
                using (var xmlWriter = XmlWriter.Create(path, xmlSettings))
                {
                    return TryWriteObject(xmlWriter, obj, type, out exception, settings);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public void WriteObject<T>(Stream stream, T obj, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryWriteObject(stream, obj, out Exception exception, settings))
                return;

            throw exception;
        }

        public void WriteObject<T>(Stream stream, T obj, DataContractSerializerSettings settings = null)
        {
            if (TryWriteObject(stream, obj, out Exception exception, settings))
                return;

            throw exception;
        }

        public bool TryWriteObject<T>(Stream stream, T obj, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObject(stream, obj, out Exception _, settings);
        }

        public bool TryWriteObject<T>(Stream stream, T obj, DataContractSerializerSettings settings = null)
        {
            return TryWriteObject(stream, obj, out Exception _, settings);
        }

        public bool TryWriteObject<T>(Stream stream, T obj, out Exception exception,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObject(stream, obj, out exception, settings);
        }

        public virtual bool TryWriteObject<T>(Stream stream, T obj, out Exception exception,
            DataContractSerializerSettings settings = null)
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

            try
            {
                XmlWriterSettings xmlSettings = GetXmlWriterSettings();
                using (var xmlWriter = XmlWriter.Create(stream, xmlSettings))
                {
                    return TryWriteObject(xmlWriter, obj, out exception, settings);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public void WriteObject(Stream stream, object obj, Type type, 
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryWriteObject(stream, obj, type, out Exception exception, settings))
                return;

            throw exception;
        }

        public void WriteObject(Stream stream, object obj, Type type, 
            DataContractSerializerSettings settings = null)
        {
            if (TryWriteObject(stream, obj, type, out Exception exception, settings))
                return;

            throw exception;
        }

        public bool TryWriteObject(Stream stream, object obj, Type type,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObject(stream, obj, type, out Exception _, settings);
        }

        public bool TryWriteObject(Stream stream, object obj, Type type,
            DataContractSerializerSettings settings = null)
        {
            return TryWriteObject(stream, obj, type, out Exception _, settings);
        }

        public bool TryWriteObject(Stream stream, object obj, Type type, out Exception exception,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObject(stream, obj, type, out exception, settings);
        }

        public virtual bool TryWriteObject(Stream stream, object obj, Type type, out Exception exception,
            DataContractSerializerSettings settings = null)
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

            try
            {
                XmlWriterSettings xmlSettings = GetXmlWriterSettings();
                using (var xmlWriter = XmlWriter.Create(stream, xmlSettings))
                {
                    return TryWriteObject(xmlWriter, obj, type, out exception, settings);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public void WriteObject<T>(TextWriter writer, T obj, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryWriteObject(writer, obj, out Exception exception, settings))
                return;

            throw exception;
        }

        public void WriteObject<T>(TextWriter writer, T obj, DataContractSerializerSettings settings = null)
        {
            if (TryWriteObject(writer, obj, out Exception exception, settings))
                return;

            throw exception;
        }

        public bool TryWriteObject<T>(TextWriter writer, T obj, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObject(writer, obj, out Exception _, settings);
        }

        public bool TryWriteObject<T>(TextWriter writer, T obj, DataContractSerializerSettings settings = null)
        {
            return TryWriteObject(writer, obj, out Exception _, settings);
        }

        public bool TryWriteObject<T>(TextWriter writer, T obj, out Exception exception,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObject(writer, obj, out exception, settings);
        }

        public virtual bool TryWriteObject<T>(TextWriter writer, T obj, out Exception exception,
            DataContractSerializerSettings settings = null)
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

            try
            {
                XmlWriterSettings xmlSettings = GetXmlWriterSettings();
                using (var xmlWriter = XmlWriter.Create(writer, xmlSettings))
                {
                    return TryWriteObject(xmlWriter, obj, out exception, settings);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public void WriteObject(TextWriter writer, object obj, Type type,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryWriteObject(writer, obj, type, out Exception exception, settings))
                return;

            throw exception;
        }

        public void WriteObject(TextWriter writer, object obj, Type type,
            DataContractSerializerSettings settings = null)
        {
            if (TryWriteObject(writer, obj, type, out Exception exception, settings))
                return;

            throw exception;
        }

        public bool TryWriteObject(TextWriter writer, object obj, Type type,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObject(writer, obj, type, out Exception _, settings);
        }

        public bool TryWriteObject(TextWriter writer, object obj, Type type,
            DataContractSerializerSettings settings = null)
        {
            return TryWriteObject(writer, obj, type, out Exception _, settings);
        }

        public bool TryWriteObject(TextWriter writer, object obj, Type type, out Exception exception,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObject(writer, obj, type, out exception, settings);
        }

        public virtual bool TryWriteObject(TextWriter writer, object obj, Type type, out Exception exception,
            DataContractSerializerSettings settings = null)
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

            try
            {
                XmlWriterSettings xmlSettings = GetXmlWriterSettings();
                using (var xmlWriter = XmlWriter.Create(writer, xmlSettings))
                {
                    return TryWriteObject(xmlWriter, obj, type, out exception, settings);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public void WriteObject<T>(XmlWriter writer, T obj, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryWriteObject(writer, obj, out Exception exception, settings))
                return;

            throw exception;
        }

        public void WriteObject<T>(XmlWriter writer, T obj, DataContractSerializerSettings settings = null)
        {
            if (TryWriteObject(writer, obj, out Exception exception, settings))
                return;

            throw exception;
        }

        public bool TryWriteObject<T>(XmlWriter writer, T obj, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObject(writer, obj, out _, settings);
        }

        public bool TryWriteObject<T>(XmlWriter writer, T obj, DataContractSerializerSettings settings = null)
        {
            return TryWriteObject(writer, obj, out _, settings);
        }

        public bool TryWriteObject<T>(XmlWriter writer, T obj, out Exception exception,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObject(writer, obj, out exception, settings);
        }

        public virtual bool TryWriteObject<T>(XmlWriter writer, T obj, out Exception exception,
            DataContractSerializerSettings settings = null)
        {
            exception = null;
            try
            {
                Guard.AgainstNullArgument(writer, nameof(writer));
                Guard.AgainstNullArgument(obj, nameof(obj));
                
                Type type = typeof(T);

                DataContractSerializer dataContractSerializer = settings == null ? 
                    new DataContractSerializer(type) : new DataContractSerializer(type, settings);

                dataContractSerializer.WriteObject(writer, obj);
                return true;
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public void WriteObject(XmlWriter writer, object obj, Type type,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryWriteObject(writer, obj, type, out Exception exception, settings))
                return;

            throw exception;
        }

        public void WriteObject(XmlWriter writer, object obj, Type type,
            DataContractSerializerSettings settings = null)
        {
            if (TryWriteObject(writer, obj, type, out Exception exception, settings))
                return;

            throw exception;
        }

        public bool TryWriteObject(XmlWriter writer, object obj, Type type,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObject(writer, obj, type, out Exception _, settings);
        }

        public bool TryWriteObject(XmlWriter writer, object obj, Type type,
            DataContractSerializerSettings settings = null)
        {
            return TryWriteObject(writer, obj, type, out Exception _, settings);
        }

        public bool TryWriteObject(XmlWriter writer, object obj, Type type, out Exception exception,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObject(writer, obj, type, out exception, settings);
        }

        public virtual bool TryWriteObject(XmlWriter writer, object obj, Type type, out Exception exception,
            DataContractSerializerSettings settings = null)
        {
            exception = null;
            try
            {
                Guard.AgainstNullArgument(writer, nameof(writer));
                Guard.AgainstNullArgument(obj, nameof(obj));
                Guard.AgainstNullArgument(type, nameof(type));

                DataContractSerializer dataContractSerializer = settings == null ? 
                    new DataContractSerializer(type) : new DataContractSerializer(type, settings);

                dataContractSerializer.WriteObject(writer, obj);
                return true;
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public string WriteObjectToString<T>(T obj, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryWriteObjectToString(obj, out string xmlString, out Exception exception, settings))
                return xmlString;

            throw exception;
        }

        public string WriteObjectToString<T>(T obj, DataContractSerializerSettings settings = null)
        {
            if (TryWriteObjectToString(obj, out string xmlString, out Exception exception, settings))
                return xmlString;

            throw exception;
        }

        public bool TryWriteObjectToString<T>(T obj, out string xmlString, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObjectToString(obj, out xmlString, out Exception _, settings);
        }

        public bool TryWriteObjectToString<T>(T obj, out string xmlString, DataContractSerializerSettings settings = null)
        {
            return TryWriteObjectToString(obj, out xmlString, out Exception _, settings);
        }

        public bool TryWriteObjectToString<T>(T obj, out string xmlString, out Exception exception,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObjectToString(obj, out xmlString, out exception, settings);
        }

        public virtual bool TryWriteObjectToString<T>(T obj, out string xmlString, out Exception exception,
            DataContractSerializerSettings settings = null)
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

            try
            {
                XmlWriterSettings xmlSettings = GetXmlWriterSettings();

                using (var stringWriter = new StringWriter())
                using (var xmlWriter = XmlWriter.Create(stringWriter, xmlSettings))
                {
                    if (!TryWriteObject(xmlWriter, obj, out exception, settings))
                        return false;

                    xmlString = stringWriter.ToString();
                    return true;
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public string WriteObjectToString(object obj, Type type, IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            if (TryWriteObjectToString(obj, type, out string xmlString, out Exception exception, settings))
                return xmlString;

            throw exception;
        }

        public string WriteObjectToString(object obj, Type type, DataContractSerializerSettings settings = null)
        {
            if (TryWriteObjectToString(obj, type, out string xmlString, out Exception exception, settings))
                return xmlString;

            throw exception;
        }

        public bool TryWriteObjectToString(object obj, Type type, out string xmlString,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObjectToString(obj, type, out xmlString, out Exception _, settings);
        }

        public bool TryWriteObjectToString(object obj, Type type, out string xmlString,
            DataContractSerializerSettings settings = null)
        {
            return TryWriteObjectToString(obj, type, out xmlString, out Exception _, settings);
        }

        public bool TryWriteObjectToString(object obj, Type type, out string xmlString, out Exception exception,
            IEnumerable<Type> knownTypes)
        {
            DataContractSerializerSettings settings = GetDefaultDataContractSettings(knownTypes);
            return TryWriteObjectToString(obj, type, out xmlString, out exception, settings);
        }

        public virtual bool TryWriteObjectToString(object obj, Type type, out string xmlString, out Exception exception,
            DataContractSerializerSettings settings = null)
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

            try
            {
                XmlWriterSettings xmlSettings = GetXmlWriterSettings();

                using (var stringWriter = new StringWriter())
                using (var xmlWriter = XmlWriter.Create(stringWriter, xmlSettings))
                {
                    if (!TryWriteObject(xmlWriter, obj, type, out exception, settings))
                        return false;

                    xmlString = stringWriter.ToString();
                    return true;
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

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

        public virtual bool TryDeserialize<T>(string path, out T result, out Exception exception)
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

            try
            {
                //TODO: Double check create, maybe use FileStream
                XmlReaderSettings xmlSettings = GetSecureXmlReaderSettings();
                using (var xmlReader = XmlReader.Create(path, xmlSettings))
                {
                    return TryDeserialize(xmlReader, out result, out exception);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
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

        public virtual bool TryDeserialize(string path, Type type, out object result, out Exception exception)
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

            try
            {
                //TODO: Double check create, maybe use FileStream
                XmlReaderSettings xmlSettings = GetSecureXmlReaderSettings();
                using (var xmlReader = XmlReader.Create(path, xmlSettings))
                {
                    if (!TryDeserialize(xmlReader, type, out object resultObj, out exception))
                        return false;

                    result = resultObj;
                    return true;
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
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

        public virtual bool TryDeserialize<T>(Stream stream, out T result, out Exception exception)
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

            try
            {
                XmlReaderSettings xmlSettings = GetSecureXmlReaderSettings();
                using (var xmlReader = XmlReader.Create(stream, xmlSettings))
                {
                    return TryDeserialize(xmlReader, out result, out exception);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
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

        public virtual bool TryDeserialize(Stream stream, Type type, out object result, out Exception exception)
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

            try
            {
                XmlReaderSettings xmlSettings = GetSecureXmlReaderSettings();
                using (var xmlReader = XmlReader.Create(stream, xmlSettings))
                {
                    if (!TryDeserialize(xmlReader, type, out object resultObj, out exception))
                        return false;

                    result = resultObj;
                    return true;
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
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

        public virtual bool TryDeserialize<T>(TextReader reader, out T result, out Exception exception)
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

            try
            {
                XmlReaderSettings xmlSettings = GetSecureXmlReaderSettings();
                using (var xmlReader = XmlReader.Create(reader, xmlSettings))
                {
                    return TryDeserialize(xmlReader, out result, out exception);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
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

        public virtual bool TryDeserialize(TextReader reader, Type type, out object result, out Exception exception)
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

            try
            {
                XmlReaderSettings xmlSettings = GetSecureXmlReaderSettings();
                using (var xmlReader = XmlReader.Create(reader, xmlSettings))
                {
                    if (!TryDeserialize(xmlReader, type, out object resultObj, out exception))
                        return false;

                    result = resultObj;
                    return true;
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public T Deserialize<T>(XmlReader reader, bool skipSettingsCheck = false)
        {
            if (TryDeserialize(reader, out T result, out Exception exception,
                    skipSettingsCheck))
                return result;

            throw exception;
        }

        public bool TryDeserialize<T>(XmlReader reader, out T result,
            bool skipSettingsCheck = false)
        {
            return TryDeserialize(reader, out result, out Exception _, skipSettingsCheck);
        }

        public virtual bool TryDeserialize<T>(XmlReader reader, out T result, out Exception exception,
            bool skipSettingsCheck = false)
        {
            result = default;
            exception = null;
            try
            {
                Guard.AgainstNullArgument(reader, nameof(reader));

                if (!skipSettingsCheck)
                {
                    GuardAgainstInsecureSettings(reader.Settings);
                }

                Type type = typeof(T);

                var xmlSerializer = new XmlSerializer(type);

                if (!xmlSerializer.CanDeserialize(reader))
                {
                    throw new XmlException(
                        $"XML content can not be deserialized into the given type '{type.FullName}'");
                }

                result = (T) xmlSerializer.Deserialize(reader);
                return true;
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
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

        public virtual bool TryDeserialize(XmlReader reader, Type type, out object result, out Exception exception,
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

        public virtual bool TryDeserializeFromString<T>(string xmlString, out T result, out Exception exception)
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

            try
            {
                XmlReaderSettings xmlSettings = GetSecureXmlReaderSettings();

                using (var stringReader = new StringReader(xmlString))
                using (var xmlReader = XmlReader.Create(stringReader, xmlSettings))
                {
                    return TryDeserialize(xmlReader, out result, out exception);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
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

        public virtual bool TryDeserializeFromString(string xmlString, Type type, out object result, out Exception exception)
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

            try
            {
                XmlReaderSettings xmlSettings = GetSecureXmlReaderSettings();

                using (var stringReader = new StringReader(xmlString))
                using (var xmlReader = XmlReader.Create(stringReader, xmlSettings))
                {
                    if (!TryDeserialize(xmlReader, type, out object resultObj, out exception))
                        return false;

                    result = resultObj;
                    return true;
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
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

        public virtual bool TrySerialize<T>(string path, T obj, out Exception exception)
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

            try
            {
                XmlWriterSettings xmlSettings = GetXmlWriterSettings();
                //TODO: Double check create, maybe use FileStream
                using (var xmlWriter = XmlWriter.Create(path, xmlSettings))
                {
                    return TrySerialize(xmlWriter, obj, out exception);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
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

        public virtual bool TrySerialize(string path, object obj, Type type, out Exception exception)
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

            try
            {
                XmlWriterSettings xmlSettings = GetXmlWriterSettings();
                //TODO: Double check create, maybe use FileStream
                using (var xmlWriter = XmlWriter.Create(path, xmlSettings))
                {
                    return TrySerialize(xmlWriter, obj, type, out exception);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
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

        public virtual bool TrySerialize<T>(Stream stream, T obj, out Exception exception)
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

            try
            {
                XmlWriterSettings xmlSettings = GetXmlWriterSettings();
                using (var xmlWriter = XmlWriter.Create(stream, xmlSettings))
                {
                    return TrySerialize(xmlWriter, obj, out exception);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
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

        public virtual bool TrySerialize(Stream stream, object obj, Type type, out Exception exception)
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

            try
            {
                XmlWriterSettings xmlSettings = GetXmlWriterSettings();
                using (var xmlWriter = XmlWriter.Create(stream, xmlSettings))
                {
                    return TrySerialize(xmlWriter, obj, type, out exception);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
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

        public virtual bool TrySerialize<T>(TextWriter writer, T obj, out Exception exception)
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

            try
            {
                XmlWriterSettings xmlSettings = GetXmlWriterSettings();
                using (var xmlWriter = XmlWriter.Create(writer, xmlSettings))
                {
                    return TrySerialize(xmlWriter, obj, out exception);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
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

        public virtual bool TrySerialize(TextWriter writer, object obj, Type type, out Exception exception)
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

            try
            {
                XmlWriterSettings xmlSettings = GetXmlWriterSettings();
                using (var xmlWriter = XmlWriter.Create(writer, xmlSettings))
                {
                    return TrySerialize(xmlWriter, obj, type, out exception);
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public void Serialize<T>(XmlWriter writer, T obj)
        {
            if (TrySerialize(writer, obj, out Exception exception))
                return;

            throw exception;
        }

        public bool TrySerialize<T>(XmlWriter writer, T obj)
        {
            return TrySerialize(writer, obj, out _);
        }

        public virtual bool TrySerialize<T>(XmlWriter writer, T obj, out Exception exception)
        {
            exception = null;
            try
            {
                Guard.AgainstNullArgument(writer, nameof(writer));
                Guard.AgainstNullArgument(obj, nameof(obj));

                Type type = typeof(T);

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

        public virtual bool TrySerialize(XmlWriter writer, object obj, Type type, out Exception exception)
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

        public virtual bool TrySerializeToString<T>(T obj, out string xmlString, out Exception exception)
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

            try
            {
                XmlWriterSettings xmlSettings = GetXmlWriterSettings();

                using (var stringWriter = new StringWriter())
                using (var xmlWriter = XmlWriter.Create(stringWriter, xmlSettings))
                {
                    if (!TrySerialize(xmlWriter, obj, out exception))
                        return false;

                    xmlString = stringWriter.ToString();
                    return true;
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
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

        public virtual bool TrySerializeToString(object obj, Type type, out string xmlString, out Exception exception)
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

            try
            {
                XmlWriterSettings xmlSettings = GetXmlWriterSettings();

                using (var stringWriter = new StringWriter())
                using (var xmlWriter = XmlWriter.Create(stringWriter, xmlSettings))
                {
                    if (!TrySerialize(xmlWriter, obj, type, out exception))
                        return false;

                    xmlString = stringWriter.ToString();
                    return true;
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
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

        protected virtual DataContractSerializerSettings GetDefaultDataContractSettings(IEnumerable<Type> knownTypes)
        {
            if (knownTypes == null)
                return null;

            return new DataContractSerializerSettings
            {
                KnownTypes = knownTypes
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
using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Haxor.Util
{
    public class XmlSerialization
    {
        public static void Serialize(object toBeSerialized, string fileName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlSerializer serializer = new XmlSerializer(toBeSerialized.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, toBeSerialized);
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.Save(fileName);
            }
        }

        public static string SerializeToString(object toBeSerialized)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlSerializer serializer = new XmlSerializer(toBeSerialized.GetType());
            using (MemoryStream stream = new MemoryStream())
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                serializer.Serialize(stream, toBeSerialized);
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }

        public static T Deserialize<T>(string fileName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            return DeserializeFromString<T>(xmlDocument.OuterXml);
        }

        public static T DeserializeFromString<T>(string xmlString)
        {
            T deserializedObject;

            using (StringReader read = new StringReader(xmlString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (XmlReader reader = new XmlTextReader(read))
                {
                    deserializedObject = (T)serializer.Deserialize(reader);
                    reader.Close();
                }

                read.Close();
            }
            return deserializedObject;
        }
    }
}
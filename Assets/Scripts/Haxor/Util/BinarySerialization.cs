using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Haxor.Util
{
    public static class BinarySerialization
    {
        public static void Serialize(object toBeSerialized, string fileName)
        {
            Stream stream = File.Open(fileName, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, toBeSerialized);
            stream.Close();
        }

        public static T Deserialize<T>(string fileName)
        {
            using (Stream stream = File.Open(fileName, FileMode.Open))
            {
                return (T) new BinaryFormatter().Deserialize(stream);
            }
        }
    }
}

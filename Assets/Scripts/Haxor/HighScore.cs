using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System;
using System.Linq;
using Haxor.Util;

namespace Haxor
{
    [Serializable]
    public class Highscore : List<KeyValuePair<string, int>>
	{
        [XmlIgnore]
        [NonSerialized]
        private const string FILENAME = "highscore.dat";

        [XmlIgnore]
        [NonSerialized]
        private const int MAX_LENGTH = 10;

        public new void Add(KeyValuePair<string, int> item)
        {
            base.Add(item);
            this.Sort((a, b) => { return b.Value.CompareTo(a.Value); });
            if (base.Count > MAX_LENGTH)
            {
                base.RemoveAt(base.Count - 1);
            }
            Save(this);
        }

        public static void Save(Highscore highscore)
        {
            try
            {
                BinarySerialization.Serialize(highscore, FILENAME);
            }
            catch
            {
            }
        }

        public static Highscore Load()
        {
            try
            {
                return BinarySerialization.Deserialize<Highscore>(FILENAME);
            }
            catch
            {
                return new Highscore();
            }
        }
    }
}
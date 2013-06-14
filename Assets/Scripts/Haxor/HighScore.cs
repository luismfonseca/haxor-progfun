using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System;
using Assets.Scripts.Haxor.Util;

namespace Haxor
{
    [Serializable]	
	public class HighScore : List<KeyValuePair<string, int>>
	{
        [XmlIgnore]
        private const string FILENAME = "HighScore.dat";

        public static void Save(HighScore highScore)
        {
            BinarySerialization.Serialize(highScore, FILENAME);
        }

        public static HighScore Load()
        {
	            return BinarySerialization.Deserialize<HighScore>(FILENAME);
        }
	}
	
}
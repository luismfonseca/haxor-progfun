using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System;
using System.Linq;
using Haxor.Util;

namespace Haxor
{
    /// <summary>
    /// Class Highscore its a serializable list of KeyValuePair
    /// so it can load and save the highscores, the list
    /// contais the scores of the best players, it is used by the
    /// Highscore contrller to load the game
    /// </summary>
    [Serializable]
    public class Highscore : List<KeyValuePair<string, int>>
	{
        [XmlIgnore]
        [NonSerialized]
        private const string FILENAME = "highscore.dat"; /// name of the highscore file data

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

        /// <summary>
        /// saves the highscore to FILENAME
        /// </summary>
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

        /// <summary>
        /// loads the highscore from FILENAME
        /// </summary>
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
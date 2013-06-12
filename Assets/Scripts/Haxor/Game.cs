using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Haxor.Util;
using System.Xml.Serialization;
using Assets.Scripts.Haxor.Util;

namespace Haxor
{
    [Serializable]
    public class Game
    {
        [XmlIgnore]
        private const string GAME_FILENAME = "userSavedGame.dat";

        public List<Level> Levels;

        public Level CurrentLevel;

        public int PlayerScore;

        public static Game NewGame()
        {
            Game newGame = new Game();

            newGame.Levels = new List<Level>();
            newGame.Levels.Add(new Level0());

            newGame.CurrentLevel = newGame.Levels[0];
            return newGame;
        }

        public static void Save(Game Game)
        {
            BinarySerialization.Serialize(Game, GAME_FILENAME);
        }

        public static Game Load()
        {
            return BinarySerialization.Deserialize<Game>(GAME_FILENAME);
        }
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Haxor.Util;

namespace Haxor
{
    [Serializable]
    public class Game
    {
        private const string GAME_FILENAME = "userSavedGame.xml";

        public List<Level> Levels;

        public int PlayerScore;

        public static Game NewGame()
        {
            Game newGame = new Game();

            newGame.Levels = new List<Level>();
            newGame.Levels.Add(new Level0());
            return newGame;
        }

        public static void Save(Game Game)
        {
            XmlSerialization.Serialize(Game, GAME_FILENAME);
        }

        public static Game Load()
        {
            return XmlSerialization.Deserialize<Game>(GAME_FILENAME);
        }
    }
}
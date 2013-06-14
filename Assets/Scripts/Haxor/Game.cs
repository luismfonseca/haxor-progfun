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

        public int CurrentLevelNumber;
        public static int PlayerScore = 0;

        public static float EvaluateProgress(List<Line> original, List<Line> toBeValidated)
        {
            float correctCount = 0;
            for (int i = 0; i < original.Count && i < toBeValidated.Count; i++)
            {
                if (original[i] != toBeValidated[i])
                {
                    break;
                }
                correctCount++;
            }
            return correctCount / ((float) original.Count);
        }

        public static Game NewGame()
        {
            Game newGame = new Game();

            //newGame.Levels = new List<Level>();
            //newGame.Levels.Add(new Level1());
            //newGame.Levels.Add(new Level2());

            //newGame.CurrentLevel = newGame.Levels[Math.Min(CurrentLevelNumber, newGame.Levels.Count) - 1];
            return newGame;
        }

        public void init() {
            switch (CurrentLevelNumber)
            {
                case 1: CurrentLevel = new Level1(); break;
                case 2: CurrentLevel = new Level2(); break;
                case 10: CurrentLevel = new Level10(); break;
            }
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
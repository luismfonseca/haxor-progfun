using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Haxor.Util;
using System.Xml.Serialization;
using Assets.Scripts.Haxor.Util;
using System.IO;

namespace Haxor
{
    [Serializable]
    public class Game
    {
        [XmlIgnore]
        [NonSerialized]
        private const string GAME_FILENAME = "userSavedGame.dat";

        public int PlayerScore = 0;
        
        public List<Level> Levels;
        public int CurrentLevelNumber;
        public Level CurrentLevel
        {
            get
            {
                return Levels[CurrentLevelNumber];
            }
        }

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

            newGame.Levels = new List<Level>() {
                    new Level0(), new Level1(), new Level2(), new Level3(), new Level4() };

            newGame.CurrentLevelNumber = 0;
            return newGame;
        }

        public static void Save(Game Game)
        {
            BinarySerialization.Serialize(Game, GAME_FILENAME);
        }

        public static Game Load()
        {
            try
            {
                var game = BinarySerialization.Deserialize<Game>(GAME_FILENAME);

                // Make sure PlayerSolution is empty
                foreach (var level in game.Levels)
                {
                    level.PlayerSolution = new PlayerSolution();
                }

                return game;
            }
            catch
            {
                return null;
            }
        }

        public static void DeleteSavedGame()
        {
            try
            {
                File.Delete(GAME_FILENAME);
            }
            catch
            {
            }
        }
    }
}
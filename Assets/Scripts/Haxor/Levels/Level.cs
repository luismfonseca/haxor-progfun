using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Assets.Scripts.Haxor.Commands;

namespace Haxor
{
    [Serializable]
    public abstract class Level
    {
        public virtual Command[] GetCommands()
        {
            return null;
        }

        [XmlIgnore]
        internal static Pattern[] Patterns { get; set; }

        public PlayerSolution PlayerSolution { get; set; }

        public List<Line> Lines { get; set; }

        public int MaximumLines { get; set; }

        public string nextLevelScene { get; set; }

        public float Progress
        {
            get
            {
                return PlayerSolution.Count / MaximumLines;
            }
        }

        public abstract bool DisplayCharacter { get; }

        public Level()
        {
            Lines = new List<Line>();
            PlayerSolution = new PlayerSolution();
        }

        public void GenerateGuideline()
        {
            while (Lines.Count < MaximumLines)
            {
                var pattern = Patterns[UnityEngine.Random.Range(0, Patterns.Length)];

                foreach (var line in pattern)
                {
                    Lines.Add(line);
                }
            }

            MaximumLines = Lines.Count; // A longer pattern might have to Line.Count > MaximumLines
        }
    }
}
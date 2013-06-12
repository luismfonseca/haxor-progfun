using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Haxor
{
    [Serializable]
    public class Level
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

        public float Progress
        {
            get
            {
                return PlayerSolution.Count / MaximumLines;
            }
        }

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

    [Serializable]
    public class Level0 : Level
    {
        public Level0()
        {
            MaximumLines = 50;

            Patterns = new Pattern[7] { new Pattern(), new Pattern(), new Pattern(), new Pattern(), new Pattern(), new Pattern(), new Pattern() };
            // Some are repeated to increase their odds
            Patterns[0].Add(new Line() { Color = LineColor.Black });
            Patterns[1].Add(new Line() { Color = LineColor.Black });
            Patterns[2].Add(new Line() { Color = LineColor.Red });
            Patterns[3].Add(new Line() { Color = LineColor.Red });
            Patterns[4].Add(new Line() { Color = LineColor.Blue });
            Patterns[5].Add(new Line() { Color = LineColor.Blue });
            Patterns[6].Add(new Line() { Color = LineColor.Transparent });

            GenerateGuideline();
        }

        public override Command[] GetCommands()
        {
            return CommandLevel1.Commands;
        }
    }
}
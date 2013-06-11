using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Haxor
{
    [Serializable]
    public abstract class Level
    {
        [XmlIgnore]
        internal static Command[] Commands;

        [XmlIgnore]
        internal static Pattern[] Patterns;

        public UserSolution UserSolution;

        public List<Line> Lines;

        public int MaximumLines;

        public float Progress
        {
            get
            {
                return UserSolution.Count / MaximumLines;
            }
        }
    }

    [Serializable]
    public class Level0 : Level
    {
        public Level0()
        {
            MaximumLines = 50;

            Commands = new Command[]
            {
                new Command() {
                        Name = "Go",
                        CommandComponent = CommandComponent.FindCommandComponentByName("Go")
                },
                new Command() {
                        Name = "Skip",
                        CommandComponent = CommandComponent.FindCommandComponentByName("Skip")
                }
            };

            Patterns = new Pattern[7]; // Some are repeated to increase their odds
            Patterns[0].Add(new Line() { Color = LineColor.Black });
            Patterns[1].Add(new Line() { Color = LineColor.Red });
            Patterns[2].Add(new Line() { Color = LineColor.Blue });
            Patterns[3].Add(new Line() { Color = LineColor.Black });
            Patterns[4].Add(new Line() { Color = LineColor.Red });
            Patterns[5].Add(new Line() { Color = LineColor.Blue });
            Patterns[6].Add(new Line() { Color = LineColor.Transparent });
        }
    }
}
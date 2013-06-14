using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Assets.Scripts.Haxor.Commands;

namespace Haxor
{
    [Serializable]
    public class Level2 : Level
    {
        public override bool DisplayCharacter
        {
            get
            {
                return false;
            }
        }
        public Level2()
        {
            nextLevelScene = "mainScene";
            MaximumLines = 6;

            Patterns = new Pattern[11] { new Pattern(), new Pattern(), new Pattern(), new Pattern(), new Pattern(),
                new Pattern(), new Pattern(),new Pattern(), new Pattern(), new Pattern(), new Pattern() };
            // Some are repeated to increase their odds
            Patterns[0].Add(new Line() { LineColor = LineColor.Black });
            Patterns[1].Add(new Line() { LineColor = LineColor.Black });
            Patterns[2].Add(new Line() { LineColor = LineColor.Red });
            Patterns[3].Add(new Line() { LineColor = LineColor.Red });
            Patterns[4].Add(new Line() { LineColor = LineColor.Blue });
            Patterns[5].Add(new Line() { LineColor = LineColor.Blue });
            Patterns[6].Add(new Line() { LineColor = LineColor.Transparent });
            Patterns[7].Add(new Line() { LineColor = LineColor.Black });
            Patterns[8].Add(new Line() { LineColor = LineColor.Black });
            Patterns[9].Add(new Line() { LineColor = LineColor.Black });
            Patterns[10].Add(new Line() { LineColor = LineColor.Black });

            GenerateGuideline();
        }

        public override Command[] GetCommands()
        {
            return CommandLevel2.Commands;
        }
    }
}
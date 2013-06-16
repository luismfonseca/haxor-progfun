using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Assets.Scripts.Haxor.Commands;

namespace Haxor
{
    [Serializable]
    public class Level4 : Level
    {
        public override bool DisplayCharacter
        {
            get
            { 
                return true;
            }
        }

        public Level4()
        {
            MaximumLines = 100;

            Patterns = new Pattern[] { new Pattern(), new Pattern(), new Pattern(), new Pattern(), new Pattern(), new Pattern(), new Pattern() };

            // Some are repeated to increase their odds
            Patterns[0].Add(new Line() { LineColor = LineColor.Black });
            Patterns[1].Add(new Line() { LineColor = LineColor.Black });
            Patterns[2].Add(new Line() { LineColor = LineColor.Red });
            Patterns[3].Add(new Line() { LineColor = LineColor.Red });
            Patterns[4].Add(new Line() { LineColor = LineColor.Blue });
            Patterns[5].Add(new Line() { LineColor = LineColor.Blue });

            Patterns[6].Add(new Line() { LineColor = LineColor.Black });
            Patterns[6].Add(new Line() { LineColor = LineColor.Yellow });
            Patterns[6].Add(new Line() { LineColor = LineColor.Transparent });
            Patterns[6].Add(new Line() { LineColor = LineColor.Transparent });
            Patterns[6].Add(new Line() { LineColor = LineColor.Yellow });
            Patterns[6].Add(new Line() { LineColor = LineColor.Black });

            GenerateGuideline();
        }

        public override Command[] GetCommands()
        {
            return CommandLevel4.Commands;
        }
    }
}
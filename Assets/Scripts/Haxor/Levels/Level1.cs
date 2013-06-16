using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Assets.Scripts.Haxor.Commands;

namespace Haxor
{
    [Serializable]
    public class Level1 : Level
    {
        public override bool DisplayCharacter
        {
            get
            {
                return false;
            }
        }

        public Level1()
        {
            MaximumLines = 10;

            // Some are repeated to increase their odds
            Patterns = new Pattern[] {
                new Pattern() { new Line() { LineColor = LineColor.Black } },
                new Pattern() { new Line() { LineColor = LineColor.Black } },
                new Pattern() { new Line() { LineColor = LineColor.Transparent } } };

            GenerateGuideline();
        }

        public override Command[] GetCommands()
        {
            return CommandLevel1.Commands;
        }
    }
}
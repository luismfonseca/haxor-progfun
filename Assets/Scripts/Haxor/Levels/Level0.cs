using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Assets.Scripts.Haxor.Commands;

namespace Haxor
{
    [Serializable]
    public class Level0 : Level
    {
        public override bool DisplayCharacter
        {
            get
            {
                return false;
            }
        }

        public Level0()
        {
            MaximumLines = 6;

            Patterns = new Pattern[] { new Pattern() };
            Patterns[0].Add(new Line() { LineColor = LineColor.Black });

            GenerateGuideline();
        }

        public override Command[] GetCommands()
        {
            return CommandLevel0.Commands;
        }
    }
}
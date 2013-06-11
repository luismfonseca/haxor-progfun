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
        [XmlIgnore]
        internal static Command[] Commands;

        [XmlIgnore]
        internal static Pattern[] Patterns;

        public UserSolution UserSolution;

        public List<Line> Lines;
    }

    [Serializable]
    public class Level0 : Level
    {
        public Level0()
        {
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
        }
    }
}
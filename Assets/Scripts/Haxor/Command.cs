using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;

namespace Haxor
{
    [Serializable]
    public abstract class Command
    {
        [XmlIgnore]
        public CommandComponent CommandComponent;

        public string Name;

        public abstract Action GetAction();
    }

    public class CommandLevel1 : Command
    {
        public static Command[] Commands = new Command[]
        {
            new CommandLevel1() {
                    Name = "Go",
                    CommandComponent = CommandComponent.FindCommandComponentByName("Go") // FIXME
            },
            new CommandLevel1() {
                    Name = "Skip",
                    CommandComponent = CommandComponent.FindCommandComponentByName("Skip") // FIXME
            }
        };

        public override Action GetAction()
        {
            switch (Name)
            {
                case "Skip":
                    return () =>
                    {
                        GameController.Find().Game.CurrentLevel.Lines.Add(new Line() { Color = LineColor.Transparent });
                    };
                case "Go":
                    return () =>
                    {
                        GameController.Find().Game.CurrentLevel.Lines.Add(new Line());
                    };
                case "Black":
                    return () =>
                    {
                        LineColor.CurrentColor = LineColor.Black;
                    };
                case "Blue":
                    return () =>
                    {
                        LineColor.CurrentColor = LineColor.Blue;
                    };
                case "Green":
                    return () =>
                    {
                        LineColor.CurrentColor = LineColor.Green;
                    };
            }
            throw new Exception("Command Action undefined.");
        }
    }
}
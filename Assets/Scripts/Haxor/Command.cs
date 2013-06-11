using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;

namespace Haxor
{
    [Serializable]
    public class Command
    {
        [XmlIgnore]
        public CommandComponent CommandComponent;

        public string Name;

        [XmlIgnore]
        public Action Action
        {
            get
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
}
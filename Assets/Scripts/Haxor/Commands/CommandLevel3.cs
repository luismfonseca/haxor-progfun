using Haxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Haxor.Commands
{
    [Serializable]
    public class CommandLevel3 : Command
    {
        public static Command[] Commands = new Command[]
        {
            new CommandLevel3() { Name = "Go" },
            new CommandLevel3() { Name = "Skip" },
            new ChangeColor()
        };

        public override Action<IHandleCommand> GetAction()
        {
            switch (Name)
            {
                case "Skip":
                    return (handler) =>
                    {
                        handler.SkipLine();
                    };
                case "Go":
                    return (handler) =>
                    {
                        handler.AddLine(new Line());
                    };
                default:
                    throw new Exception("Command Action undefined.");
            }
        }
    }

    [Serializable]
    public class ChangeColor : CommandLevel3
    {
        public static LineColor[] Colors = new LineColor[]
        {
            LineColor.Black,
            LineColor.Red,
            LineColor.Blue
        };

        public int SelectionIndex;

        public ChangeColor()
        {
            Name = "Change Color";
            SelectionIndex = 0;
        }

        public override Action<IHandleCommand> GetAction()
        {
            return (handler) =>
            {
                handler.ChangeCurrentColor(Colors[SelectionIndex]);
            };
        }

        //public override object Clone()
        //{
        //    return new ChangeColor() { Name = Name };
        //}
    }
}

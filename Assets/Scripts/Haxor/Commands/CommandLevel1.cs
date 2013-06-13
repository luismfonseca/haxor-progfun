using Haxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Haxor.Commands
{
    [Serializable]
    public class CommandLevel1 : Command
    {
        public static Command[] Commands = new Command[]
        {
            new CommandLevel1() { Name = "Go" },
            new CommandLevel1() { Name = "Skip" },
            new CommandLevel1() { Name = "Black" },
            new CommandLevel1() { Name = "Blue" },
            new CommandLevel1() { Name = "Red" }
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
                case "Black":
                    return (handler) =>
                    {
                        handler.ChangeCurrentColor(LineColor.Black);
                    };
                case "Green":
                    return (handler) =>
                    {
                        handler.ChangeCurrentColor(LineColor.Green);
                    };
                case "Red":
                    return (handler) =>
                    {
                        handler.ChangeCurrentColor(LineColor.Red);
                    };
                default:
                    throw new Exception("Command Action undefined.");
            }
        }

        public override object Clone()
        {
            return new CommandLevel1() { Name = Name };
        }
    }
}
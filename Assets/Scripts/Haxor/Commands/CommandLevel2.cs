using Haxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Haxor.Commands
{
    [Serializable]
    public class CommandLevel2 : Command
    {
        public static Command[] Commands = new Command[]
        {
            new CommandLevel2() { Name = "Go" },
            new CommandLevel2() { Name = "Skip" },
            new CommandLevel2() { Name = "Black" },
            new CommandLevel2() { Name = "Blue" },
            new CommandLevel2() { Name = "Red" }
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
                case "Blue":
                    return (handler) =>
                    {
                        handler.ChangeCurrentColor(LineColor.Blue);
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
    }
}
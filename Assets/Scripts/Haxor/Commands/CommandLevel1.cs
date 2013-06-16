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
        public static Command[] Commands =
        {
            new CommandLevel1() { Name = "Go" },
            new CommandLevel1() { Name = "Skip" }
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
}
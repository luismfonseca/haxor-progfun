using Haxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Haxor.Commands
{
    [Serializable]
    public class CommandLevel0 : Command
    {
        public static Command[] Commands = new Command[]
        {
            new CommandLevel0() { Name = "Go" },
        };

        public override Action<IHandleCommand> GetAction()
        {
            switch (Name)
            {
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
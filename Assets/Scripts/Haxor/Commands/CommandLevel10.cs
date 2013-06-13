using Haxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Haxor.Commands
{
    [Serializable]
    public class CommandLevel10 : Command
    {
        public static Command[] Commands = new Command[]
        {
            new CommandLevel10() { Name = "Move" },
        };

        public override Action<IHandleCommand> GetAction()
        {
            switch (Name)
            {
                case "Move":
                    return (handler) =>
                    {
                        handler.Move();
                    };
                default:
                    throw new Exception("Command Action undefined.");
            }
        }

        public override object Clone()
        {
            return new CommandLevel10() { Name = Name };
        }
    }

}

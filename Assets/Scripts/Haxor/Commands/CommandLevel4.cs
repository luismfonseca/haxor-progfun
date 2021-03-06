﻿using Haxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Haxor.Commands
{
    [Serializable]
    public class CommandLevel4 : Command
    {
        public static Command[] Commands = new Command[]
        {
            new CommandLevel4() { Name = "Move" },
            new CommandLevel4() { Name = "Jump" },
            new Repeat()
        };

        public override Action<IHandleCommand> GetAction()
        {
            return (handler) =>
            {
                handler.PlayerAction(this);
            };
        }
    }

    [Serializable]
    public class Repeat : Command
    {
        public int Times;
        public List<Command> Commands;

        public Repeat()
        {
            Name = "Repeat";
            Times = 5;
            Commands = new List<Command>();
        }

        public override Action<IHandleCommand> GetAction()
        {
            return (handler) =>
            {
                for (int i = 0; i < Times; i++)
                {
                    foreach (var command in Commands)
                    {
                        command.GetAction()(handler);
                    }
                }
            };
        }
    }
}

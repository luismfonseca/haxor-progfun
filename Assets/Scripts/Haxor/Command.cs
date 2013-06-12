using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using Assets.Scripts.Haxor;
using System.Runtime.Serialization;

namespace Haxor
{
    [Serializable]
    public abstract class Command
    {
        public string Name;

        public abstract Action<IHandleCommand> GetAction();

    }

    [Serializable]
    public class CommandLevel1 : Command
    {
        public static Command[] Commands = new Command[]
        {
            new CommandLevel1() { Name = "Go" },
            new CommandLevel1() { Name = "Skip" },
            new CommandLevel1() { Name = "Black" },
            new CommandLevel1() { Name = "Blue" },
            new CommandLevel1() { Name = "Green" }
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
                case "Green":
                    return (handler) =>
                    {
                        handler.ChangeCurrentColor(LineColor.Green);
                    };
            }
            throw new Exception("Command Action undefined.");
        }
    }
}
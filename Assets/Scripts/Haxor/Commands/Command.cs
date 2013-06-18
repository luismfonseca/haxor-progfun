using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using Assets.Scripts.Haxor;
using System.Runtime.Serialization;

namespace Haxor
{
    /// <summary>
    /// the commands of the game, each level has its own
    /// file with their commands named with the following template
    /// "CommandLevel<n>.cs" where <n> is the level number
    /// </summary>
    
    [Serializable]
    public abstract class Command : ICloneable
    {
        public string Name;

        public abstract Action<IHandleCommand> GetAction();

        public virtual object Clone()
        {
            Command command = Activator.CreateInstance(this.GetType()) as Command;
            command.Name = Name;
            return command;
        }
    }
}
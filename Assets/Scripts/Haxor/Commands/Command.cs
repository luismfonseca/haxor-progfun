using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using Assets.Scripts.Haxor;
using System.Runtime.Serialization;

namespace Haxor
{
    [Serializable]
    public abstract class Command : ICloneable
    {
        public string Name;

        public abstract Action<IHandleCommand> GetAction();

        public abstract object Clone();
    }
}
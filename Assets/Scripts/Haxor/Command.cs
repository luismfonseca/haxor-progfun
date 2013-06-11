using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;

namespace Haxor
{
    [Serializable]
    public class Command
    {
        [XmlIgnore]
        public CommandComponent CommandComponent;

        public string Name;
    }
}
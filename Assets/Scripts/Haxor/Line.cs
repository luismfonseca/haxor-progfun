using UnityEngine;
using System;
using System.Collections;
using System.Xml.Serialization;

namespace Haxor
{
    [Serializable]
    public class Line
    {
        public Color Color;

        public LineOrientation Orientation = LineOrientation.Angle0;
    }

    [Serializable]
    public enum LineOrientation
    {
        Angle0,
        Angle30,
        Angle45,
        Angle60,
        Angle90
    }
}
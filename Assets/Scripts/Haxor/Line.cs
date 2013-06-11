using UnityEngine;
using System;
using System.Collections;
using System.Xml.Serialization;

namespace Haxor
{
    [Serializable]
    public class Line
    {
        public Color Color = LineColor.CurrentColor;

        public LineOrientation Orientation = LineOrientation.Angle0;
    }

    public class LineColor
    {
        public static readonly Color Black = Color.black;
        public static readonly Color Red = Color.red;
        public static readonly Color Blue = Color.blue;
        public static readonly Color Green = Color.green;
        public static readonly Color Transparent = new Color(0f, 0f, 0f, 1f);
        public static Color CurrentColor = Black;
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
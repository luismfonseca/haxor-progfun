using UnityEngine;
using System;
using System.Collections;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Haxor
{
    [Serializable]
    public class Line
    {
        public LineColor LineColor;

        public LineOrientation Orientation = LineOrientation.Angle0;
    }

    [Serializable]
    public class LineColor : ISerializable
    {
        public static readonly LineColor Black = new LineColor(Color.black);
        public static readonly LineColor Red = new LineColor(Color.red);
        public static readonly LineColor Blue = new LineColor(Color.blue);
        public static readonly LineColor Green = new LineColor(Color.green);
        public static readonly LineColor Transparent = new LineColor(new Color(0f, 0f, 0f, 0f));
        
        public Color Color;

        public LineColor()
        {
        }

        public LineColor(Color color)
        {
            this.Color = color;
        }

        public LineColor(SerializationInfo info, StreamingContext ctxt)
        {
            Color.r = (float)info.GetValue("Color.r", typeof(float));
            Color.g = (float)info.GetValue("Color.g", typeof(float));
            Color.b = (float)info.GetValue("Color.b", typeof(float));
            Color.a = (float)info.GetValue("Color.a", typeof(float));
        }
 
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Color.r", Color.r);
            info.AddValue("Color.g", Color.g);
            info.AddValue("Color.b", Color.b);
            info.AddValue("Color.a", Color.a);
        }
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
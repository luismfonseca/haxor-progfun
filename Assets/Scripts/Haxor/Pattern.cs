using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Haxor
{
    /// <summary>
    /// a serializable list of lines for saving/loading the game
    /// </summary>
    [Serializable]
    public class Pattern : List<Line>
    {
    }
}
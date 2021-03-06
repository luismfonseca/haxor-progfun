﻿using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using Assets.Scripts.Haxor.Commands;

namespace Haxor
{

    /// <summary>
    /// the levels of the game, it saves a list of lines of the solution,
    /// the number of lines of the level is no larger than the MaximumLines variable,
    /// each level has its own named with the following template
    /// "Level<n>.cs" where <n> is the level number
    /// </summary>

    [Serializable]
    public abstract class Level
    {
        public virtual Command[] GetCommands()
        {
            return null;
        }

        [XmlIgnore]
        [NonSerialized]
        internal static Pattern[] Patterns;

        [XmlIgnore] // TODO: Maybe support saving PlayerSolution in the future
        [NonSerialized]
        public PlayerSolution PlayerSolution = new PlayerSolution();

        public List<Line> Lines { get; set; }

        public int MaximumLines { get; set; }

        public float Progress
        {
            get
            {
                return PlayerSolution.Count / MaximumLines;
            }
        }

        public abstract bool DisplayCharacter { get; }

        public Level()
        {
            Lines = new List<Line>();
            PlayerSolution = new PlayerSolution();
        }

        public void GenerateGuideline()
        {
            while (Lines.Count < MaximumLines)
            {
                var pattern = Patterns[UnityEngine.Random.Range(0, Patterns.Length)];

                foreach (var line in pattern)
                {
                    Lines.Add(line);
                }
            }
            if (Lines.Last().LineColor == LineColor.Transparent)
            {
                Lines.Add(new Line() { LineColor = LineColor.Black });
            }

            MaximumLines = Lines.Count; // A longer pattern might have to Line.Count > MaximumLines
        }
    }
}
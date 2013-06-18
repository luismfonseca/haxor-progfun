using Haxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Haxor
{
    /// <summary>
    /// Command Handlers, they're going to be used when the
    /// player "plays" the solution
    /// </summary>
    public interface IHandleCommand
    {
        void AddLine(Line line);

        void SkipLine();

        void ChangeCurrentColor(LineColor color);

        // TODO Change angle, and whatnot

        void PlayerAction(Command command);
    }
}

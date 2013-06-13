using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class ButtonsController
    {
        private static readonly float OFFSET_X = 0;
        private static readonly float OFFSET_Y = 8;
        private static readonly float WIDTH = 4;

        private GameController gameController;

        public ButtonsController(GameController gameController)
        {
            this.gameController = gameController;
        }

        public void InstantiateCommandButtons()
        {
            var commands = gameController.Game.CurrentLevel.GetCommands();
            int index = 0;
            foreach (var command in commands)
            {
                var button = OT.CreateSprite(Prototype.CommandButton);
                button.gameObject.name = command.Name;
                button.position = new Vector2(OFFSET_X + WIDTH * index, OFFSET_Y);
                button.GetComponent<GuiButton>().command = command;
                ++index;
            }
        }
    }
}

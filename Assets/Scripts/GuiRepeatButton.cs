using UnityEngine;
using System.Collections;
using Haxor;
using Assets.Scripts.Haxor.Commands;
using System.Collections.Generic;

public class GuiRepeatButton : GuiButton
{
    private List<GuiButton> buttonList;


    void Awake()
    {
        buttonList = new List<GuiButton>();
    }

    public override float Height
    {
        get
        {
            return 2.5f + 1.5f * (buttonList.Count);
        }
    }

    public override float ButtonHeight
    {
        get
        {
            return 1.5f;
        }
    }

    public override bool AddCommand(GuiButton obj)
    {
        // Check if it needs to be added to obj.
        if (gameObject.transform.position.y < obj.gameObject.transform.position.y + Height)
        {
            // Check if any child wants this
            
            // Add as our child
            var repeatCommand = (Repeat)command;
            float objPositionY = obj.gameObject.transform.position.y;

            var index = buttonList.FindIndex(guiButton => guiButton.gameObject.transform.position.y < objPositionY);
            if (index == -1)
            {
                index = buttonList.Count;
            }
            obj.transform.parent = transform;
            obj.index = index;
            buttonList.Insert(index, obj);
            repeatCommand.Commands.Insert(index, obj.command);
            updatePositions();
            return true;
        }
        return false;
    }

    public override void RemoveCommand(GuiButton obj)
    {
        obj.transform.parent = null;
        ((Repeat)command).Commands.RemoveAt(obj.index);
        buttonList.RemoveAt(obj.index);
        updatePositions();
    }

    public void updatePositions()
    {
        int index = 0;
        var positionOffsetX = transform.position.x + 1f;
        var positionOffsetY = transform.position.y - 1.5f;
        foreach (var button in buttonList)
        {
            button.gameObject.transform.position =
                    new Vector3(positionOffsetX, positionOffsetY, -2);
            button.index = index;
            positionOffsetY -= (button.Height);
            ++index;
        }
    }
}

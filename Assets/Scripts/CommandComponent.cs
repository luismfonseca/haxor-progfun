using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using Haxor;

public class CommandComponent : MonoBehaviour
{
    public OTSprite Sprite;

    public static CommandComponent FindCommandComponentByName(string name)
    {
        return GameController.Find().CommandComponents.FirstOrDefault(commandComponent => commandComponent.name == name);
    }
}

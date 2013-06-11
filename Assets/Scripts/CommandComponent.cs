using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class CommandComponent : MonoBehaviour {

    public OTSprite Sprite;

    public Action Action; // TODO: Should be a script?


    public static CommandComponent FindCommandComponentByName(string name)
    {
        return GameController.Find().CommandComponents.FirstOrDefault(commandComponent => commandComponent.name == name);
    }
}

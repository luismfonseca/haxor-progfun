using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ControlPanel : MonoBehaviour {
	
	private OTSprite OTComponent;
	private int displayOffset;

	void Start() {
		OT.debug = true;
		OTComponent = this.GetComponent<OTSprite>();
		OTComponent.registerInput = true;
		OTComponent.onReceiveDrop += (owner) => {
            Debug.Log("Panel: I received a drop from : " + owner.gameObject.name);
            ControlPanel.AddCommand(OTComponent.dropTarget.gameObject.GetComponent<GuiButton>());
		};
		displayOffset = 0;
	}
	
	void Update() {
	
	}
	
	public static void AddCommand(GuiButton obj)
	{
		// Position the object correctly
		//obj.gameObject.transform.position = new Vector3(0.5f, 4.7f - 2f * lastIndex, -2);
	}

    public static void RemoveCommand(GuiButton obj)
    {
    }
	
	public static int GetCommandsCount() {
        return GameController.Find().Game.CurrentLevel.PlayerSolution.Count;
	}
}

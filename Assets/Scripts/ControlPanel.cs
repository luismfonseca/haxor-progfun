using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ControlPanel : MonoBehaviour {
	
	private OTSprite OTComponent;
	public static List<CommandsLevel1> Commands;
	private int displayOffset;

	void Start() {
		OT.debug = true;
		OTComponent = this.GetComponent<OTSprite>();
		OTComponent.registerInput = true;
		OTComponent.onReceiveDrop += (owner) => {
			Debug.Log("Panel: I received a drop from : " + owner.gameObject.name);
		};
		Commands = new List<CommandsLevel1>();
		displayOffset = 0;
	}
	
	void Update() {
	
	}
	
	public static void AddCommand(GuiButton obj)
	{
		int lastIndex = Commands.Count;
		Commands.Add((CommandsLevel1) Enum.Parse(typeof(CommandsLevel1), obj.name));
		
		// Position the object correctly
		obj.gameObject.transform.position = new Vector3(0.5f, 4.7f - 2f * lastIndex, -2);
	}
	
	public static int GetCommandsCount() {
		return Commands.Count;
	}
}

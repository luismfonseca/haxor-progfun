using UnityEngine;
using System.Collections;

public class GuiButton : MonoBehaviour {
	
	public GUIStyle style;
	private OTSprite OTComponent;
	public string name;
	
	// Use this for initialization
	void Start() {
		this.name = this.gameObject.name;
		OTComponent = this.GetComponent<OTSprite>();
		OTComponent.onDragStart += (owner) => {
			this.gameObject.name = "Command-" + ControlPanel.GetCommandsCount();
			var newObject = OT.CreateObject(this.name);
			newObject.gameObject.name = this.name;
			OTComponent.onDragStart = null; // Remove this action
		};

		OTComponent.onDragEnd += (owner) => {
			ControlPanel.AddCommand(this);
 		};
	}
	
	// Update is called once per frame 
	void Update() {
	}
	
	void OnGUI() {
		
        Vector3 screenPos = OT.inputCameras[0].WorldToScreenPoint(this.gameObject.transform.position);
        GUI.Label(
				new Rect(
						screenPos.x,
						Screen.height - screenPos.y,
						this.gameObject.transform.localScale.x, 
						this.gameObject.transform.localScale.y),
				this.name,
				style
		);
		
	}
}

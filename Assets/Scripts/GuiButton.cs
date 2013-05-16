using UnityEngine;
using System.Collections;

public class GuiButton : MonoBehaviour {
	
	public GUIStyle style;
	private OTSprite OTComponent;

	// Use this for initialization
	void Start() {
		OT.debug = true;
		OTComponent = this.GetComponent<OTSprite>();
		OTComponent.onDragStart += (owner) => {
			var newObject = OT.CreateObject(this.gameObject.name);
		};

		OTComponent.onDragEnd += (owner) => {
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
				this.gameObject.name,
				style
		);
		
	}
}

using UnityEngine;
using System.Collections;
using Haxor;

public class GuiButton : MonoBehaviour {

    public static readonly float HEIGHT = 2f;
    public static readonly float WIDTH = 2f;

	public GUIStyle style;
	private OTSprite OTComponent;
	public string name;
    public Command command;
    public int index;
	
	// Use this for initialization
	void Start() {
		this.name = this.gameObject.name;
		OTComponent = this.GetComponent<OTSprite>();
        OTComponent.size = new Vector2(WIDTH, HEIGHT);
		OTComponent.onDragStart += (owner) => {
			this.gameObject.name = "Command-" + ControlPanel.GetCommandsCount();
			var newObject = OT.CreateSprite(this.name);
			newObject.gameObject.name = this.name;
            newObject.GetComponent<GuiButton>().command = this.command;
            OTComponent.onDragStart = (component) =>
            {
                ControlPanel.RemoveCommand(this);
            }; // Remove this action
		};

        OTComponent.onDragEnd += (owner) => {
            if (OTComponent.dropTarget == null)
            {
                this.gameObject.SetActive(false);
                //OT.RemoveObject(OTComponent);
            }
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

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

    private bool isPlacedInCommandsPanel;

	void Start()
    {

		this.name = this.gameObject.name;
		OTComponent = this.GetComponent<OTSprite>();
        OTComponent.size = new Vector2(WIDTH, HEIGHT);
		OTComponent.onDragStart += (owner) => {
			this.gameObject.name = "Command-" + ControlPanel.GetCommandsCount();
			var newObject = OT.CreateSprite(this.name);
			newObject.gameObject.name = this.name;
            newObject.GetComponent<GuiButton>().command = this.command.Clone() as Command;
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
	
	void OnGUI() {
        float ypos = this.gameObject.transform.position.y;
        if (ypos > 6 || ypos < 6)
        {
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

    public void setAsPlacedInCommandPanel(bool placed)
    {
        isPlacedInCommandsPanel = placed;
        if (placed)
        {
            switch (command.Name)
            {
                case "Change Color":
                    var tinyButton = OT.CreateSprite(Prototype.SelectionBoxTiny);
                    tinyButton.transform.parent = this.transform;
                    tinyButton.position = new Vector2(2f, 0f);
                    tinyButton.tintColor = Color.black;
                    tinyButton.GetComponent<GuiSelectionButton>().IsSelected = true;
                    tinyButton.GetComponent<GuiSelectionButton>().index = 0;
                    tinyButton = OT.CreateSprite(Prototype.SelectionBoxTiny);
                    tinyButton.transform.parent = this.transform;
                    tinyButton.position = new Vector2(3f, 0f);
                    tinyButton.tintColor = Color.red;
                    tinyButton.GetComponent<GuiSelectionButton>().index = 1;
                    tinyButton = OT.CreateSprite(Prototype.SelectionBoxTiny);
                    tinyButton.transform.parent = this.transform;
                    tinyButton.position = new Vector2(4f, 0f);
                    tinyButton.tintColor = Color.blue;
                    tinyButton.GetComponent<GuiSelectionButton>().index = 2;
                    break;
                default:
                    break;
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                GameObject.DestroyObject(child.gameObject);
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using Haxor;
using System;

public class GuiButton : MonoBehaviour
{
    public static readonly float WIDTH = 2f;

	public GUIStyle style;
	private OTSprite OTComponent;
	public string name;
    public Command command;
    public int index;

    protected bool isPlacedInCommandsPanel;

    public virtual float Height
    {
        get
        {
            return 2f;
        }
    }

	void Start()
    {
		this.name = this.gameObject.name;
		OTComponent = this.GetComponent<OTSprite>();
        OTComponent.size = new Vector2(WIDTH, 2f);
		OTComponent.onDragStart += (owner) => {
			this.gameObject.name = "Command-" + ControlPanel.GetCommandsCount();
			var newObject = OT.CreateSprite(this.name);
			newObject.gameObject.name = this.name;
            newObject.GetComponent<GuiButton>().command = this.command.Clone() as Command;
            OTComponent.onDragStart = (component) =>
            {
                GameController.Find().controlPanel.RemoveCommand(this);
            }; // Remove this action
		};

        OTComponent.onDragEnd += (owner) => {
            if (OTComponent.dropTarget == null)
            {
                this.gameObject.SetActive(false);
                //OT.RemoveObject(OTComponent);
            }
        };
        OTComponent.onReceiveDrop += (owner) =>
        {
            if (AddCommand(OTComponent.dropTarget.gameObject.GetComponent<GuiButton>()) == false)
            {
                OTComponent.dropTarget.gameObject.SetActive(false);
            }
        };
	}

    void Update()
    {
        OTComponent.draggable = !isPlacedInCommandsPanel
                                || transform.position.y < 6f && transform.position.y > -6f;
    }

	void OnGUI()
    {
        if (OTComponent.draggable)
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

    public virtual void setAsPlacedInCommandPanel(bool placed)
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
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public virtual bool AddCommand(GuiButton obj)
    {
        return false;
    }

    public virtual void RemoveCommand(GuiButton obj)
    {
        throw new Exception("Should not be called.");
    }
}

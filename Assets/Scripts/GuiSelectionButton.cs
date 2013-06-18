using UnityEngine;
using System.Collections;
using System.Linq;
using Haxor;
using Assets.Scripts.Haxor.Commands;

/// <summary>
/// Selection button, it is used to select from a list of button
/// it is used by the Change Color Button
/// </summary>
public class GuiSelectionButton : MonoBehaviour {

    public static readonly float HEIGHTRATIO = GuiButton.WIDTH/1.5f;
    public static readonly float HEIGHT = 0.25f * HEIGHTRATIO;
    public static readonly float WIDTH = 0.25f;

	public GUIStyle style;
	private OTSprite OTComponent;
    public int index;

    private bool isSelected;
    public bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            foreach (Transform child in transform.parent.transform)
            {
                child.GetComponent<GuiSelectionButton>().isSelected = false;
                child.GetComponent<OTSprite>().size = new Vector2(WIDTH, HEIGHT);
            }
            isSelected = value;
            if (isSelected && OTComponent != null)
            {
                OTComponent.size = new Vector2(WIDTH * 2f, HEIGHT * 2f);
                ((ChangeColor)transform.parent.GetComponent<GuiButton>().command).SelectionIndex = index;
            }
        }
    }

	void Start()
    {
		this.name = this.gameObject.name;
		OTComponent = this.GetComponent<OTSprite>();
        if (isSelected)
        {
            OTComponent.size = new Vector2(WIDTH * 2f, HEIGHT * 2f);
        }
        else
        {
            OTComponent.size = new Vector2(WIDTH, HEIGHT);
        }
        OTComponent.onInput = (owner) =>
        {
            if (Input.GetMouseButtonDown(0))
            {
                IsSelected = true;
            }
        };
        transform.Translate(new Vector3(0f, 0f, 2f));
	}
}

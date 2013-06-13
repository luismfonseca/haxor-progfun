using UnityEngine;
using System.Collections;
using System.Linq;
using Haxor;
using Assets.Scripts.Haxor.Commands;

public class GuiSelectionButton : MonoBehaviour {

    public static readonly float HEIGHT = 0.25f;
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

    void Update()
    {
        // Fix orthelo default z position.
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
    }
}

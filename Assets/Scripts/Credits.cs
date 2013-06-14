using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
	
	public GUIStyle style = new GUIStyle();
	// Use this for initialization
	void Start () {
	
	}
	void OnGUI() {
		int width = Screen.width;
		int height = Screen.height;
		
		Rect labelRect = new Rect (100, (height/2-150), width, (height/4));
		Rect labelRect2 = new Rect (100, (height/2-75), width, (height/4));
		Rect labelRect3 = new Rect (200, (height/2), width, (height/4));
		Rect labelRect4 = new Rect (200, (height/2+75), width, (height/4));
		Rect labelRect5 = new Rect (200, (height/2+150), width, (height/4));
		style.fontSize = width/25;
		
		GUI.Label(labelRect, "This game has been made using Unity", style);
		GUI.Label(labelRect2, "By:", style);
		GUI.Label(labelRect3, "Omar", style);
		GUI.Label(labelRect4, "Luis", style);
		GUI.Label(labelRect5, "Witold", style);
	}
	// Update is called once per frame
	void Update () {
	
	}
}

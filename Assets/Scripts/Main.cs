using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	
	public GUIStyle style = new GUIStyle();
	// Use this for initialization
	void Start () {
		
	}
	
	    void OnGUI() {
     
		int width = Screen.width;
		int height = Screen.height;
		Rect buttonRectplay = new Rect ((width/2-60), (height/2)+60, 140, 30);
		Rect buttonRectload = new Rect ((width/2-60), (height/2)+100, 140, 30);
		Rect buttonRectscore = new Rect ((width/2-60), (height/2)+140, 140, 30);
		Rect buttonRectcredits = new Rect ((width/2-60), (height/2)+180, 140, 30);
		Rect labelRect = new Rect (width/2-100, (height/8-70), width, (height/4));
		Rect labelRect2 = new Rect ((width/2-50), (height/8), width, (height/4));
		style.fontSize = width/12;
		
		GUI.Label(labelRect, "Haxor", style);
		GUI.Label(labelRect2, "progfun", style);
		
		 if (GUI.Button(buttonRectplay, "START GAME")) {
             Application.LoadLevel("SceneStoryLevel1");
	    }
		if (GUI.Button(buttonRectload, "LOAD GAME")) {

	    }
		if (GUI.Button(buttonRectscore, "HIGHEST SCORES")) {
			 Application.LoadLevel("Score");

	    }
		if (GUI.Button(buttonRectcredits, "CREDITS")) {
			 Application.LoadLevel("Credits");

	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using Assets.Scripts.Haxor.Util;

public class ScoreScene : MonoBehaviour {
	
	public GUIStyle style = new GUIStyle();
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI () 
	{	
		
		int width = Screen.width;
		int height = Screen.height;
		Rect labelRect = new Rect (width/2-width / 8-40, 10, width, (height/4));
		style.fontSize = width/25;
		
		GUI.Label(labelRect, "10 BEST PLAYERS", style);
        for(int i=0;i<10;i++)
		{
		 	GUILayout.BeginArea ( new Rect( width/2-width / 8, 100+(i*30), width / 4, height / 4 ) );
         	GUILayout.Box ( i.ToString() );
			GUILayout.EndArea ();
         	
		}
	
     }
	
	// Update is called once per frame
	void Update () {
	
	}
}

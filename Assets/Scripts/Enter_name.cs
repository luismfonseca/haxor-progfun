using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System;
using Assets.Scripts.Haxor.Util;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using Haxor;

public class Enter_name : MonoBehaviour {
	
	HighScore HighScore;
	public List<HighScore> highScores;
	public GUIStyle style;
	int score;
	int i;
	bool test=false;
	public string username="";
	// Use this for initialization
	void Start () {
		HighScore= new HighScore();
		using (var fileStream = new FileStream(@"scores.dat", FileMode.Open, FileAccess.Read))
		{
		    var formatter = new BinaryFormatter();
		    highScores = (List<HighScore>) formatter.Deserialize(fileStream);
		}
		score =3000;
		i=0;
		foreach(var scores in highScores)
		{
		    Console.WriteLine(String.Format("{0}: {1} points", scores.PlayerName, scores.Score));
			if(i<10)
			{
				if(score>scores.Score)
					test=true;
				else
					test=false;
			}	
			i++;
		}
		if(test==false && i==9) 
			Application.LoadLevel("Score");
	}
	
	void OnGUI()
    {
		int width = Screen.width;
		int height = Screen.height;
		GUI.Label( new Rect((width/2-60), (height/2)-40, width, (height/4) ), "Please enter your name" );
		username = GUI.TextArea (new Rect ((width/2-60), (height/2), 140, 30 ), username);
		Rect buttonRectsave = new Rect ((width/2-60), (height/2)+40, 140, 30);		
		Rect buttonRectmenu = new Rect ((width/2-60), (height/2)+80, 140, 30);
		if (GUI.Button(buttonRectsave, "Save")) {
			if(test==true || i!=9)
			{
				HighScore a = new HighScore ();
				a.PlayerName=username;
				a.Score=score;
				highScores.Add(a);
							
				using (var fileStream = new FileStream(@"scores.dat", FileMode.Create, FileAccess.Write))
				{
				    var formatter = new BinaryFormatter();
				    formatter.Serialize(fileStream, highScores);
				}
				Application.LoadLevel("Score");
	
			}
			 
		}
		if(GUI.Button(buttonRectmenu, "Menu")) {
	    	Application.LoadLevel("Main");
	   	}
	}
	// Update is called once per frame
	void Update () {
	
	}
}

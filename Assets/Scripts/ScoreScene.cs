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


public class ScoreScene : MonoBehaviour {
	HighScore HighScore;
	public List<HighScore> highScores;
	public GUIStyle style;
	
	void Start () {
	HighScore= new HighScore();

	highScores = new List<HighScore>()
	{
	    new HighScore { PlayerName = "Helen", Score = 1000 },
	    new HighScore { PlayerName = "Christophe", Score = 2000 },
	    new HighScore { PlayerName = "Ruben", Score = 3000 },
	    new HighScore { PlayerName = "John", Score = 4000 },
	    new HighScore { PlayerName = "The Last Starfighter", Score = 5000 }
	};
		
		using (var fileStream = new FileStream(@"scores.dat", FileMode.Create, FileAccess.Write))
		{
		    var formatter = new BinaryFormatter();
		    formatter.Serialize(fileStream, highScores);
		}
		
		using (var fileStream = new FileStream(@"scores.dat", FileMode.Open, FileAccess.Read))
		{
		    var formatter = new BinaryFormatter();
		    highScores = (List<HighScore>) formatter.Deserialize(fileStream);
		}
		
		highScores.Sort();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnGUI ()
	{
	
		
		
		
		int width = Screen.width;
		int height = Screen.height;
		Rect labelRect = new Rect (width/2-width / 8-40, 10, width, (height/4));
		style.fontSize = width/25;
	
		GUI.Label(labelRect, "10 BEST PLAYERS", style);
       // for(int i=0;i<10;i++)
	//	{
			//GUILayout.BeginArea ( new Rect( width/2-width / 8, 100+(i*30), width / 4, height / 4 ) );
         	//GUILayout.Box ( i.ToString() );
			//GUILayout.EndArea ();
         	
	//	}
		int i=0;
		foreach(var score in highScores)
		{
		    Console.WriteLine(String.Format("{0}: {1} points", score.PlayerName, score.Score));
			if(i<10)
			{
				GUILayout.BeginArea ( new Rect( width/2-width / 8, 100+(i*30), width / 4, height / 4 ) );
	         	GUILayout.Box (score.PlayerName+ ": "+score.Score);
				GUILayout.EndArea ();
			}	
			i++;
		}
		
	}
	
}




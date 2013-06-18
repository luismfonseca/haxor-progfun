using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System;
using Haxor.Util;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using Haxor;

/// <summary>
/// Highscore controller, it starts when the scene changes to the HighScore
/// from the Main Menu, it loads the highscores and the interface
/// </summary>
public class HighscoreController : MonoBehaviour
{
    private static readonly int TITLE_START_Y = 163;
    private static readonly int FORM_CELL_HEIGHT = 50;
    private static readonly int CONTENT_START_Y = 214;

	public GUISkin Skin;

    private Highscore highscore;

	void Start()
    {
        highscore = Highscore.Load();
	}
	
	public void OnGUI ()
	{
        GUI.skin = Skin;

        GUILayout.BeginArea(new Rect(0, TITLE_START_Y, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label("TOP PLAYERS", Skin.customStyles[SkinStyle.Title]);
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(0, CONTENT_START_Y, Screen.width, Screen.height));
            foreach (var score in highscore)
            {
                GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    GUILayout.Button(score.Key);
                    GUILayout.Label(score.Value.ToString(), Skin.customStyles[SkinStyle.Score]);
                    GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(80, CONTENT_START_Y, 200, Screen.height));
            if (GUILayout.Button("Menu", Skin.customStyles[SkinStyle.ButtonMainMenu]))
            {
                Application.LoadLevel("MainMenu");
            }
        GUILayout.EndArea();
	}
}
using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    private static readonly int TITLE_START_Y = 163;
    public static readonly int FORM_CELL_HEIGHT = 50;
    public static readonly int FORM_CELL_WIDTH = 380;
    public static readonly int CONTENT_START_Y = 214;

    public GUISkin Skin;

    void OnGUI()
    {
        GUI.skin = Skin;
        int CONTENT_START_X = Screen.width / 2 - 190;

        GUILayout.BeginVertical();
        GUI.Label(new Rect(CONTENT_START_X, TITLE_START_Y, FORM_CELL_WIDTH, FORM_CELL_HEIGHT), "MENU", Skin.customStyles[0]);


        if (GUI.Button(new Rect(CONTENT_START_X, CONTENT_START_Y, FORM_CELL_WIDTH, FORM_CELL_HEIGHT), "START GAME"))
        {
            Application.LoadLevel("SceneStoryLevel1");
        }

        if (GUI.Button(new Rect(CONTENT_START_X, CONTENT_START_Y + FORM_CELL_HEIGHT + 5, FORM_CELL_WIDTH, FORM_CELL_HEIGHT), "LOAD GAME"))
        {
            // TODO
        }

        if (GUI.Button(new Rect(CONTENT_START_X, CONTENT_START_Y + 2 * (FORM_CELL_HEIGHT + 5), FORM_CELL_WIDTH, FORM_CELL_HEIGHT), "HIGHSCORES"))
        {
            Application.LoadLevel("Score");
        }

        if (GUI.Button(new Rect(CONTENT_START_X, CONTENT_START_Y + 3 * (FORM_CELL_HEIGHT + 5), FORM_CELL_WIDTH, FORM_CELL_HEIGHT), "CREDITS"))
        {
            Application.LoadLevel("Credits");
        }

        GUILayout.EndVertical();
    }
}
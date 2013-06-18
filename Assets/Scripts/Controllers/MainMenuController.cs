using UnityEngine;
using System.Collections;
using Haxor;

/// <summary>
/// Controller for the main Menu, this is where the buttons
/// are initialized
/// </summary>
public class MainMenuController : MonoBehaviour
{
    private static readonly int TITLE_START_Y = 163;
    private static readonly int FORM_CELL_HEIGHT = 50;
    private static readonly int FORM_CELL_WIDTH = 380;
    private static readonly int CONTENT_START_Y = 214;

    public GUISkin Skin;
    private bool menuStartingGame = false;
    private string playerName = string.Empty;

    void OnGUI()
    {
        GUI.skin = Skin;

        if (menuStartingGame)
        {
            GUIStartingGame();
        }
        else
        {
            GUIMainMenu();
        }
    }

    private void GUIStartingGame()
    {
        int CONTENT_START_X = Screen.width / 2 - 190;

        GUILayout.BeginVertical();
        GUI.Label(new Rect(CONTENT_START_X, TITLE_START_Y, FORM_CELL_WIDTH, FORM_CELL_HEIGHT), "YOUR NAME", Skin.customStyles[0]);

        playerName = GUI.TextField(new Rect(CONTENT_START_X, CONTENT_START_Y, FORM_CELL_WIDTH, FORM_CELL_HEIGHT), playerName);

        if (GUI.Button(new Rect(CONTENT_START_X, CONTENT_START_Y + FORM_CELL_HEIGHT + 5, FORM_CELL_WIDTH, FORM_CELL_HEIGHT), "START"))
        {
            Game.DeleteSavedGame();
            var game = Game.NewGame();
            game.PlayerName = playerName;
            Game.Save(game);
            menuStartingGame = true;
            Application.LoadLevel("Story");
        }

        GUILayout.EndVertical();

        GUILayout.BeginArea(new Rect(80, CONTENT_START_Y, 200, Screen.height));
            if (GUILayout.Button("Menu", Skin.customStyles[SkinStyle.ButtonMainMenu]))
            {
                menuStartingGame = false;
            }
        GUILayout.EndArea();
    }

    private void GUIMainMenu()
    {
        int CONTENT_START_X = Screen.width / 2 - 190;

        GUILayout.BeginVertical();
        GUI.Label(new Rect(CONTENT_START_X, TITLE_START_Y, FORM_CELL_WIDTH, FORM_CELL_HEIGHT), "MENU", Skin.customStyles[0]);


        if (GUI.Button(new Rect(CONTENT_START_X, CONTENT_START_Y, FORM_CELL_WIDTH, FORM_CELL_HEIGHT), "START GAME"))
        {
            Game.DeleteSavedGame();
            menuStartingGame = true;
        }

        if (GUI.Button(new Rect(CONTENT_START_X, CONTENT_START_Y + FORM_CELL_HEIGHT + 5, FORM_CELL_WIDTH, FORM_CELL_HEIGHT), "LOAD GAME"))
        {
            if (Game.Load() == null)
            {
                menuStartingGame = true;
            }
            else
            {
                Application.LoadLevel("Story");
            }
        }

        if (GUI.Button(new Rect(CONTENT_START_X, CONTENT_START_Y + 2 * (FORM_CELL_HEIGHT + 5), FORM_CELL_WIDTH, FORM_CELL_HEIGHT), "HIGHSCORE"))
        {
            Application.LoadLevel("Highscore");
        }

        if (GUI.Button(new Rect(CONTENT_START_X, CONTENT_START_Y + 3 * (FORM_CELL_HEIGHT + 5), FORM_CELL_WIDTH, FORM_CELL_HEIGHT), "CREDITS"))
        {
            Application.LoadLevel("Credits");
        }

        if (GUI.Button(new Rect(CONTENT_START_X, CONTENT_START_Y + 4 * (FORM_CELL_HEIGHT + 5), FORM_CELL_WIDTH, FORM_CELL_HEIGHT), "EXIT"))
        {
            Application.Quit();
        }

        GUILayout.EndVertical();
    }
}
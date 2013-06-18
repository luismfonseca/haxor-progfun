using UnityEngine;
using System.Collections;
using Haxor;
using Assets.Scripts.Controllers;


/// <summary>
/// Common controller for the game, it initializes the game level
/// initiates the command buttons, the menu, the stickman position, etc
/// it also creates the menu
/// </summary>
public class GameController : MonoBehaviour
{
    public GUISkin Skin;
    internal Game Game;

    internal LinesPanel linesPanel; 
    internal ControlPanel controlPanel;

    private ButtonsController buttonsController;
    private PlayerController playerController;
    private StoryController storyController;

    private static bool EscapePressing = false;
    private static bool showMenu = false;
    private Texture2D tex;

    void Awake()
    {
        storyController = StoryController.Find();
        playerController = PlayerController.Find();
        buttonsController = new ButtonsController(this);
        linesPanel = GameObject.FindObjectOfType(typeof(LinesPanel)) as LinesPanel;
        controlPanel = GameObject.FindObjectOfType(typeof(ControlPanel)) as ControlPanel;
        Game = storyController.game;

        GameObject.FindGameObjectWithTag(Tag.Player).SetActive(Game.CurrentLevel.DisplayCharacter);
    }

    void Start()
    {
        buttonsController.InstantiateCommandButtons();
        tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, new Color(0, 0, 0, 0.5f));
        tex.Apply();
    }

    void OnGUI()
    {
        GUI.skin = Skin;

        if (Input.GetKey(KeyCode.Return))
        {
            if(!EscapePressing){
                showMenu = !showMenu;
                Debug.Log(showMenu);
            }
            EscapePressing = true;
        } else {
            EscapePressing = false;
        }

        if (showMenu == true)
        {

            GUILayout.BeginArea(new Rect(Screen.width * 0.3f, Screen.height * 0.3f, Screen.width * 0.4f, Screen.height * 0.4f), tex);
            if (GUILayout.Button("Return To Main Menu"))
            {
                showMenu = false;
                Application.LoadLevel("MainMenu");
            }
            if (GUILayout.Button("Exit Game"))
            {
                Application.Quit();
            }
            GUILayout.EndArea();
        }
        
        
        GUILayout.BeginVertical();
            if (GUILayout.Button("Play"))
            {
                Game.Save(Game);
                if (Game.CurrentLevel.DisplayCharacter)
                {
                    playerController.Stop();
                    linesPanel.Play(Game.CurrentLevel.PlayerSolution);
                    playerController.Play();
                }
                else
                {
                    linesPanel.Play(Game.CurrentLevel.PlayerSolution);
                }
            }
            GUILayout.Label("Score: " + Game.PlayerScore);
        GUILayout.EndVertical();
    }

    public static GameController Find()
    {
        return GameObject.Find("GameController").GetComponent<GameController>();
    }
}
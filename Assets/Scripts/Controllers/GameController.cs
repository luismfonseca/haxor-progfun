using UnityEngine;
using System.Collections;
using Haxor;
using Assets.Scripts.Controllers;

public class GameController : MonoBehaviour
{
    public Game Game;

    public GameObject CommandComponent;

    public LinesPanel linesPanel;
    public ControlPanel controlPanel;

    private ButtonsController buttonsController;
    private PlayerController playerController;

    void Awake()
    {
        Game.init();
        //Game = Game.NewGame();
        //Game = Game.Load();
        linesPanel = GameObject.FindObjectOfType(typeof(LinesPanel)) as LinesPanel;
        controlPanel = GameObject.FindObjectOfType(typeof(ControlPanel)) as ControlPanel;
        buttonsController = new ButtonsController(this);
        if (Game.CurrentLevelNumber > 9)
        {
            playerController = PlayerController.Find();
        }
      
    }

    void Start()
    {
        buttonsController.InstantiateCommandButtons();
        OnLevelWasLoaded(0);
    }
	
    void OnLevelWasLoaded(int level)
    {
        var playerGameObject = GameObject.FindGameObjectWithTag(Tag.Player);
        playerGameObject.SetActive(Game.CurrentLevel.DisplayCharacter);
    }

    void OnGUI()
    {
        if (GUILayout.Button("Save Game"))
        {
            Game.Save(Game);
        }
        if (GUILayout.Button("Load Game"))
        {
            Game = Game.Load();
        }
        if (GUILayout.Button("Play"))
        {
            if (Game.CurrentLevelNumber > 9)
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
    }

    public static GameController Find()
    {
        return GameObject.Find("GameController").GetComponent<GameController>();
    }
}
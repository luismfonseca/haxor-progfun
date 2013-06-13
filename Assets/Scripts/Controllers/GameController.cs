using UnityEngine;
using System.Collections;
using Haxor;
using Assets.Scripts.Controllers;

public class GameController : MonoBehaviour
{
    public Game Game;

    public GameObject CommandComponent;

    public LinesPanel linesPanel;

    private ButtonsController buttonsController;

    void Awake()
    {
        Game = Game.NewGame();
        //Game = Game.Load();
        linesPanel = GameObject.FindObjectOfType(typeof(LinesPanel)) as LinesPanel;
        buttonsController = new ButtonsController(this);
    }

    void Start()
    {
        buttonsController.InstantiateCommandButtons();
    }
	
	void Update()
    {
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
            linesPanel.Play(Game.CurrentLevel.PlayerSolution);
        }
    }

    public static GameController Find()
    {
        return GameObject.Find("GameController").GetComponent<GameController>();
    }
}
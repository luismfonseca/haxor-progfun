using UnityEngine;
using System.Collections;
using Haxor;
using Assets.Scripts.Controllers;

public class GameController : MonoBehaviour
{
    public Game Game;

    public GameObject CommandComponent;

    private LinesPanel linesPanel;

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
        //for (int i = 0; i < Game.CurrentLevel.GetCommands().Length; i++)
        //{
            //TODO: Add panel buttons
        //}
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
            Game.CurrentLevel.PlayerSolution.Add(CommandLevel1.Commands[0]);
            Game.CurrentLevel.PlayerSolution.Add(CommandLevel1.Commands[0]);
            Game.CurrentLevel.PlayerSolution.Add(CommandLevel1.Commands[1]);
            Game.CurrentLevel.PlayerSolution.Add(CommandLevel1.Commands[1]);
            Game.CurrentLevel.PlayerSolution.Add(CommandLevel1.Commands[3]);
            Game.CurrentLevel.PlayerSolution.Add(CommandLevel1.Commands[0]);
            Game.CurrentLevel.PlayerSolution.Add(CommandLevel1.Commands[0]);
            Game.CurrentLevel.PlayerSolution.Add(CommandLevel1.Commands[1]);
            linesPanel.Play(Game.CurrentLevel.PlayerSolution);
        }
    }

    public static GameController Find()
    {
        return GameObject.Find("GameController").GetComponent<GameController>();
    }
}
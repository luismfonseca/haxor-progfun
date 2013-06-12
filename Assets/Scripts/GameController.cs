using UnityEngine;
using System.Collections;
using Haxor;

public class GameController : MonoBehaviour
{
    public Game Game;

    public CommandComponent[] CommandComponents;

    void Awake()
    {
        Game = new Game();
        //Game = Game.Load();

    }

    void Start()
    {
        for (int i = 0; i < Game.CurrentLevel.GetCommands().Length; i++)
        {
            //TODO: Add panel buttons
        }
    }
	
	void Update()
    {
	}

    public static GameController Find()
    {
        return GameObject.Find("GameController").GetComponent<GameController>();
    }
}
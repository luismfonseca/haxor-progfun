using UnityEngine;
using System.Collections;
using Haxor;

public class GameController : MonoBehaviour
{
    public Game Game;

    public CommandComponent[] CommandComponents;

	void Start()
    {
        //Game = Game.Load();
        Game = new Game();
	}
	
	void Update()
    {
	    
	}

    public static GameController Find()
    {
        return GameObject.Find("GameController").GetComponent<GameController>();
    }
}
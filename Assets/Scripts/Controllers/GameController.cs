using UnityEngine;
using System.Collections;
using Haxor;
using Assets.Scripts.Controllers;

public class GameController : MonoBehaviour
{
    public GUISkin Skin;
    internal Game Game;

    internal LinesPanel linesPanel;
    internal ControlPanel controlPanel;

    private ButtonsController buttonsController;
    private PlayerController playerController;
    private StoryController storyController;

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
    }

    void OnGUI()
    {
        GUI.skin = Skin;
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
    }

    public static GameController Find()
    {
        return GameObject.Find("GameController").GetComponent<GameController>();
    }
}
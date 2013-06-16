﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Haxor;

public class ControlPanel : MonoBehaviour {

    public List<GuiButton> buttonList;

	private OTSprite OTComponent;
	private int displayOffset;
    private Vector2 scrollPosition;
    private Texture2D progressBarTexture;

    private static GameController gameController;
    private LinesPanel linesPanel;
    private static ControlPanel controlPanel;
    private Camera mainCamera;
    private Vector2 relativeSize;
    private Vector2 topleft;
    private Vector2 bottomRigth;
    private Rect ViewportRect;
    private Vector2 screenSize;

    private static float fadeInFromLevelTime;
    private static float toNextLevel;
    private static readonly float fadeTime = 1.5f;
    private static Texture2D fadeOutTexture;
    
    void Awake()
    {
        linesPanel = GameObject.FindObjectOfType(typeof(LinesPanel)) as LinesPanel;
        gameController = GameController.Find();
        buttonList = new List<GuiButton>();
        controlPanel = this;
        mainCamera = Camera.main;

        UpdateViewportRect();
    }

	void Start()
    {
		OT.debug = true;
		OTComponent = this.GetComponent<OTSprite>();
		OTComponent.registerInput = true;
		OTComponent.onReceiveDrop += (owner) => {
            gameController.Game.PlayerScore -= 10;
            Debug.Log("Panel: I received a drop from : " + owner.gameObject.name);
            AddCommand(OTComponent.dropTarget.gameObject.GetComponent<GuiButton>());
		};
		displayOffset = 0;

        progressBarTexture = new Texture2D(1, 1);
        progressBarTexture.SetPixel(0, 0, new Color(0, 255, 0));
        progressBarTexture.wrapMode = TextureWrapMode.Repeat;
        progressBarTexture.Apply();

        //toNextLevel = -1;
        //fadeInFromLevelTime = 0;
        //fadeOutTexture = new Texture2D(1, 1);
        //fadeOutTexture.SetPixel(0, 0, new Color(1, 1, 1));
        //fadeOutTexture.wrapMode = TextureWrapMode.Repeat;
        //fadeOutTexture.Apply();
	}
	
	void Update()
    {
        if (screenSize.x != Screen.width || screenSize.y != Screen.height)
        {
            UpdateViewportRect();
        }
	}

    void UpdateViewportRect()
    {
        topleft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        bottomRigth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        ViewportRect = new Rect(topleft.x, topleft.y, bottomRigth.x - topleft.x, bottomRigth.y - topleft.y);
        screenSize = new Vector2(Screen.width, Screen.height);
    }

    public void AddCommand(GuiButton obj)
	{
        float objPositionY = obj.gameObject.transform.position.y;

        var index = buttonList.FindIndex(guiButton => guiButton.gameObject.transform.position.y < objPositionY);
        if (index == -1)
        {
            index = buttonList.Count;
        }

        // Pass the event to the previous container to see if he can handle it
        bool handled = false;
        if (index != 0)
        {
            handled = buttonList[index - 1].AddCommand(obj);
        }
        if (handled == false)
        {
            obj.index = index;
            buttonList.Insert(index, obj);
            gameController.Game.CurrentLevel.PlayerSolution.Insert(index, obj.command);
        }
        obj.setAsPlacedInCommandPanel(true);
        updatePositions();
	}

    public void RemoveCommand(GuiButton obj)
    {
        if (obj.transform.parent != null)
        {
            (obj.transform.parent.gameObject.GetComponent<GuiButton>()).RemoveCommand(obj);
        }
        else
        {
            gameController.Game.CurrentLevel.PlayerSolution.RemoveAt(obj.index);
            controlPanel.buttonList.RemoveAt(obj.index);
            controlPanel.updatePositions();
        }
        updatePositions();
        obj.setAsPlacedInCommandPanel(false);
    }

    /// <summary>
    /// update the commands positions relative to the scrollbar position
    /// </summary>
    public void updatePositions()
    {
        float relativeHeight = (Screen.height / ViewportRect.height);
        int index = 0;
        var positionOffset = 4.7f - 1f + (scrollPosition.y / relativeHeight);
        foreach (var button in buttonList)
        {
            button.gameObject.transform.position =
                    new Vector3(0.5f, positionOffset, -2);
            button.index = index;
            positionOffset -= (button.Height);
            ++index;
        }
    }

	public static int GetCommandsCount()
    {
        return gameController.Game.CurrentLevel.PlayerSolution.Count;
	}

    void OnGUI()
    {
        GUI.skin.label.fontStyle = FontStyle.Bold;
        GUI.skin.label.normal.textColor = new Color(0,0,0);
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.skin.box.normal.background = progressBarTexture;

        GUIStyle style = new GUIStyle();
        int width = Screen.width, height = Screen.height;
        Rect position = new Rect(width * 0.49f, height * 0.2f, height * 0.74f, height * 0.6f);
        GUILayout.BeginArea(position);
        GUILayout.FlexibleSpace();
        Vector2 lastScrollPosition = scrollPosition;
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(height * 0.74f), GUILayout.Height(height * 0.6f));
        if (scrollPosition != lastScrollPosition)
        {
            updatePositions();
        }

        float totalHeight = controlPanel.buttonList.Aggregate(0f, (heightSum, button) => { return heightSum + button.Height; }) + 2f;

        GUILayout.Space(totalHeight * (Screen.height / ViewportRect.height));
        GUILayout.EndScrollView();
        GUILayout.EndArea();

        drawProgressBar();

 	    //fadeInFromLevelTime += Time.deltaTime;
        //if (fadeInFromLevelTime < fadeTime)
        //{
        //    GUI.color = new Color(1, 1, 1, fadeTime - fadeInFromLevelTime);
        //    GUI.depth = -100;
        //    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
        //}
        //else if (toNextLevel > -1)
        //{
        //    toNextLevel += Time.deltaTime;
        //    if (toNextLevel >= 1)
        //    {
        //        toNextLevel = 1;
        //        Application.LoadLevel(gameController.Game.CurrentLevel.nextLevelScene);
        //    }
        //
        //    GUI.color = new Color(1, 1, 1, toNextLevel);
        //    GUI.depth = -100;
        //    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
        //}
    }

    /// <summary>
    /// Draw the progressbar of the level
    /// </summary>
    private void drawProgressBar()
    {
        int width = Screen.width, height = Screen.height;

        string currentLevelString = "Level " + (gameController.Game.CurrentLevelNumber + 1);
        string nextLevelString = "Level " + ( gameController.Game.CurrentLevelNumber + 2 );

        float progress = Game.EvaluateProgress(gameController.Game.CurrentLevel.Lines, linesPanel.PlayerRunResultLines);

        float labelHorizontalMargin = 0f;
        float labelWidth = width * 0.1f;

        float progressBarYposition = height * 0.73f;
        float progressBarHeight = height * 0.06f;
        float progressBarHorizontalMargin = labelHorizontalMargin + labelWidth;

        float GameViewportWidth = width * 0.451f;

        GUI.Label(new Rect(
            labelHorizontalMargin,
            progressBarYposition,
            labelWidth,
            progressBarHeight
          ), currentLevelString);
        GUI.Box(new Rect(
            progressBarHorizontalMargin,
            progressBarYposition,
            (GameViewportWidth - progressBarHorizontalMargin * 2) * progress,
            progressBarHeight), progressBarTexture);

        GUI.Label(new Rect(
            GameViewportWidth - progressBarHorizontalMargin,
            progressBarYposition,
            labelWidth,
            progressBarHeight
          ), nextLevelString);
    }
}

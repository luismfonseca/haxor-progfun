using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Haxor;

public class ControlPanel : MonoBehaviour {
	
	private OTSprite OTComponent;
	private int displayOffset;
    private Vector2 scrollPosition;
    private int lastCount;
    private Texture2D progressBarTexture;

    private static GameController gameController;
    private LinesPanel linesPanel;
    private static ControlPanel controlPanel;
    private List<GuiButton> buttonList;

    void Awake()
    {
        linesPanel = GameObject.FindObjectOfType(typeof(LinesPanel)) as LinesPanel;
        gameController = GameController.Find();
        buttonList = new List<GuiButton>();
        controlPanel = this;
    }

	void Start() {
		OT.debug = true;
		OTComponent = this.GetComponent<OTSprite>();
		OTComponent.registerInput = true;
		OTComponent.onReceiveDrop += (owner) => {
            Debug.Log("Panel: I received a drop from : " + owner.gameObject.name);
            AddCommand(OTComponent.dropTarget.gameObject.GetComponent<GuiButton>());
		};
		displayOffset = 0;

        progressBarTexture = new Texture2D(1, 1);
        progressBarTexture.SetPixel(0, 0, new Color(0, 255, 0));
        progressBarTexture.wrapMode = TextureWrapMode.Repeat;
        progressBarTexture.Apply();

	}
	
	void Update()
    {
	    
	}

    public void AddCommand(GuiButton obj)
	{
        float objYPosition = obj.gameObject.transform.position.y;
        int index = Math.Min(buttonList.Count, (int)((objYPosition - 4.7f) / -1f));
        //Debug.Log(objYPosition);

        // TODO: insert depending on scrollPosition
        buttonList.Insert(Math.Max(0,index), obj);
        gameController.Game.CurrentLevel.PlayerSolution.Add(obj.command);
		// Position the object correctly
        //obj.GetComponent<OTSprite>().position = new Vector2(0.5f, 4.7f - 2f * GetCommandsCount());
        //obj.gameObject.transform.position = new Vector3(0.5f, 4.7f - 2f * GetCommandsCount() - 1, -2);
        updatePositions();
       
	}

    /// <summary>
    /// update the commands positions relative to the scrollbar position
    /// </summary>
    public void updatePositions(){
        int index = 0;
        foreach(var button in buttonList){
            button.gameObject.transform.position = new Vector3(0.5f, 4.7f + scrollPosition.y / 20f - 2f * index - 1, -2);
            ++index;
        }
    }

    public static void RemoveCommand(GuiButton obj)
    {
        gameController.Game.CurrentLevel.PlayerSolution.Remove(obj.command);
        controlPanel.buttonList.Remove(obj);
        controlPanel.updatePositions();

    }
	
	public static int GetCommandsCount() {
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
        int count = GetCommandsCount();
        if (lastCount != count)
        {
            Debug.Log(count);
            lastCount = count;
        }
        GUILayout.Label("start !!!");

        GUILayout.Space(50f * count);
        GUILayout.Label("end !!!");
        GUILayout.EndScrollView();
        GUILayout.EndArea();

        drawProgressBar();

    }

    /// <summary>
    /// draw the progressbar of the level
    /// </summary>
    private void drawProgressBar()
    {
        int width = Screen.width, height = Screen.height;

        int currentLevel = 1;
        string currentLevelString = "Level" + currentLevel;
        string nextLevelString = "Level" + ( currentLevel + 1 );

        // omar needs to change this to the real one which someone he knows is responsible for calculating it in the first place.
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

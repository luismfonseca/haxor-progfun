using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ControlPanel : MonoBehaviour {
	
	private OTSprite OTComponent;
	private int displayOffset;
    private Vector2 scrollPosition;
    private int lastCount;
    private Texture2D progressBarTexture;
    private float progress = 0.0f;
	void Start() {
		OT.debug = true;
		OTComponent = this.GetComponent<OTSprite>();
		OTComponent.registerInput = true;
		OTComponent.onReceiveDrop += (owner) => {
            Debug.Log("Panel: I received a drop from : " + owner.gameObject.name);
            ControlPanel.AddCommand(OTComponent.dropTarget.gameObject.GetComponent<GuiButton>());
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
	
	public static void AddCommand(GuiButton obj)
	{
		// Position the object correctly
		//obj.gameObject.transform.position = new Vector3(0.5f, 4.7f - 2f * lastIndex, -2);
	}

    public static void RemoveCommand(GuiButton obj)
    {
    }
	
	public static int GetCommandsCount() {
        return GameController.Find().Game.CurrentLevel.PlayerSolution.Count;
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
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(height * 0.74f), GUILayout.Height(height * 0.6f));
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

    /*!
     * draw the progressbar of the level
     */
    private void drawProgressBar()
    {
        int width = Screen.width, height = Screen.height;

        int currentLevel = 1;
        string currentLevelString = "Level" + currentLevel;
        string nextLevelString = "Level" + ( currentLevel + 1 );

        //we need to change this to the real one
        progress += 0.001f;
        progress %= 1;

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

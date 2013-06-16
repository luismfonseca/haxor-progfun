using UnityEngine;
using System.Collections;
using Haxor;

public class StoryController : MonoBehaviour
{
    public float SceneTimeForLevel = 5f;
    public GUISkin Skin;

    internal Game game;
    private bool fadingToNextLevel = false;
    private bool displayStory = true;

    public void Advance()
    {
        StartCoroutine(FadeToNextLevel());
    }

    private IEnumerator FadeToNextLevel()
    {
        fadingToNextLevel = true;
        yield return new WaitForSeconds(1f);
        fadingToNextLevel = false;
        displayStory = true;
        game.CurrentLevelNumber++;
        Game.Save(game);
        Application.LoadLevel("Story");
    }

    // Make it a persistent object
    void Awake()
    {
        DontDestroyOnLoad(transform.root.gameObject);
        DontDestroyOnLoad(this);
        game = Game.Load();
        if (game == null)
        {
            game = Game.NewGame();
        }
    }

    void Update()
    {
        if (displayStory && Time.timeSinceLevelLoad >= SceneTimeForLevel)
        {
            Application.LoadLevel("Level");
            displayStory = false;
        }
    }

    void OnGUI()
    {
        GUI.skin = Skin;
        GUI.skin.label.normal.textColor = Color.black;
        if (fadingToNextLevel)
        {
            GUI.skin.label.normal.textColor = new Color(0, 0, 0, Mathf.Lerp(0, 1, Time.deltaTime));
        }
        else if (displayStory)
        {
            if (Time.timeSinceLevelLoad < 1)
            {
                GUI.skin.label.normal.textColor = new Color(0, 0, 0, Time.timeSinceLevelLoad);
            }
            else if (Time.timeSinceLevelLoad > SceneTimeForLevel - 1)
            {
                GUI.skin.label.normal.textColor = new Color(0, 0, 0, SceneTimeForLevel - Time.timeSinceLevelLoad);
            }
        }
        if (fadingToNextLevel || displayStory)
        {
            float tenthWidth = Screen.width * 0.1f;
            float tenthHeight = Screen.height * 0.1f;
            GUI.Label(new Rect(3 * tenthWidth, 3 * tenthHeight, 4 * tenthWidth, 4 * tenthHeight), StoryLine.Plot[game.CurrentLevelNumber]);
        }
    }

    public static StoryController Find()
    {
        var controller = GameObject.Find("Story Controller").GetComponent<StoryController>();
        if (controller == null)
        {
            Debug.LogError("GameController could not be found. Are you starting the game from the Story scene?");
        }
        return controller;
    }
}
using UnityEngine;
using System.Collections;

public class StoryLevel2 : MonoBehaviour {

    private float sceneTime;
    private float sceneTimeForLevel = 5f;

	// Use this for initialization
	void Start () {
        sceneTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        sceneTime += Time.deltaTime;
        if (sceneTime >= sceneTimeForLevel)
        {
            Application.LoadLevel("Level2");
        }
	}

    void OnGUI()
    {
        if (sceneTime < 1)
        {
            GUI.skin.label.normal.textColor = new Color(0, 0, 0, sceneTime);
        }
        else if (sceneTime > 4)
        {
            GUI.skin.label.normal.textColor = new Color(0, 0, 0, sceneTimeForLevel - sceneTime);
        }
        else
        {
            GUI.skin.label.normal.textColor = new Color(0, 0, 0, 1);
        }
        float tenthWidth = Screen.width * 0.1f;
        float tenthHeight = Screen.height * 0.1f;


        GUI.Label(new Rect( 3*tenthWidth, 3*tenthHeight, 4*tenthWidth, 4*tenthHeight ),"And then the colors joined together");
    }


}

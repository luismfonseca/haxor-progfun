using UnityEngine;
using System.Collections;

/* Splash Screen and Starter Menu
 * 
 */
public class StartGameMenu : MonoBehaviour
{
    public GUIAsset titleScreen_Splash;
    public GUIAsset titleScreen_Menu;

    public Texture2D[] splashes;
    private int splashID = 0;
    private GUIDrawTexture textureControl;

    public float splashForSeconds;
    private float splashCounter = 0;
    private bool showSplash = true;

    public float menuMoveSpeed = 4f;
    private Rect menuEndPosition;

    private GUIButton buttonNew, buttonLoad, buttonQuit;

    void Start()
    {
        // We copy the GUIAssets to prevent permenant changes to the assets.
        titleScreen_Splash = titleScreen_Splash.Copy();
        titleScreen_Menu = titleScreen_Menu.Copy();

        // Get the GUIDrawTexture control using the Find<T> method.
        textureControl = titleScreen_Splash.Find<GUIDrawTexture>("Texture");
        // Set the first splash image.
        textureControl.image = splashes[0];

        // Get the buttons using Find<T>.
        buttonNew = titleScreen_Menu.Find<GUIButton>("New Button");
        buttonLoad = titleScreen_Menu.Find<GUIButton>("Load Button");
        buttonQuit = titleScreen_Menu.Find<GUIButton>("Quit Button");

        // Prepare to display the splash screen.
        splashCounter = 0;
        splashID = 0;
        showSplash = true;

        // Prepare the menu to move from below the screen to the center.
        menuEndPosition = titleScreen_Menu.position;
        titleScreen_Menu.position.x = menuEndPosition.x = (Screen.width - titleScreen_Menu.position.width) / 2;
        titleScreen_Menu.position.y = Screen.height;
        menuEndPosition.y = (Screen.height - titleScreen_Menu.position.height) / 2;

        // Set the splash screen to the center.
        titleScreen_Splash.position.x = (Screen.width - titleScreen_Splash.position.width) / 2;
        titleScreen_Splash.position.y = (Screen.height - titleScreen_Splash.position.height) / 2;
    }

    void Update()
    {
        // If we are still showing the splash screen
        if (showSplash)
        {
            // Increase the timer.
            splashCounter += Time.deltaTime;

            // If the timer reaches the splash time, or if the player pressed any key,
            if (splashCounter >= splashForSeconds || Input.anyKeyDown)
            {
                // reset the counter.
                splashCounter = 0;
                // Move to the next splash image.
                splashID++;
                // If there are no more splash images,
                if (splashID >= splashes.Length)
                {
                    // Stop showing the splash screen.
                    showSplash = false;
                }
                // Otherwise,
                else
                {
                    // set the splash to the new image.
                    textureControl.image = splashes[splashID];
                }
            }
        }
        // Otherwise,
        else
        {
            // If the menu is not at the target position,
            if (titleScreen_Menu.position != menuEndPosition)
            {
                // Move it towards the target position.
                titleScreen_Menu.position.y = Mathf.Lerp(titleScreen_Menu.position.y, menuEndPosition.y, Time.deltaTime * menuMoveSpeed);
            }
        }
    }

    void OnGUI()
    {
        // If the splash screen is showing,
        if (showSplash)
            // draw the splash screen.
            titleScreen_Splash.OnGUI();
        // Otherwise,
        else
        {
            // draw the menu.
            titleScreen_Menu.OnGUI();

            // Here you would check to see which button was pressed, and react.
            if (buttonNew.pressed)
                Debug.Log("Starting a new game...");
            else if (buttonLoad.pressed)
                Debug.Log("Accessing Load Game Screen...");
            else if (buttonQuit.pressed)
                Application.Quit();
        }
    }
}
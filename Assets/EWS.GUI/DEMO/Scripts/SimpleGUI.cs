//= = = = = = = = = = = = = = = =//
//   Simple GUI  Example Script  //
//- - - - - - - - - - - - - - - -//
// This is one example of how to //
// to use a single GUIAsset in   //
// a MonoBehavior.               //
//= = = = = = = = = = = = = = = =//
using UnityEngine;

[AddComponentMenu("GUI Assets/Simple GUI")]
public class SimpleGUI : MonoBehaviour
{
    // This script will accept any
    // kind of GUIAsset. Drag/Drop
    // the GUIAsset asset into
    // the inspector.
    public GUIAsset gui;

    void Start()
    {
        // If there is no GUIAsset, exit the method.
        if (!gui)
            return;

        // We copy the GUIAsset
        // because saved assets
        // are persistant. This
        // means that any change
        // to the original GUIAsset
        // will permenantly change
        // the GUIAsset's .asset file.
        gui = gui.Copy();
        // When we copy the GUIAsset,
        // we do not have to worry
        // about damaging the asset
        // in-game. We can experiment
        // with the GUIAsset without
        // affecting the original.
    }

    void OnDestroy()
    {
        // If there is no GUIAsset, exit the method.
        if (!gui)
            return;

        // We destroy the gui
        // because it is a copy.
        Destroy(gui);
    }

    void Update()
    {
        // If you decide to make a GUIAsset that can be updated,
        // call gui.Update() here. :D
    }

    void OnGUI()
    {
        // If there is no GUIAsset, exit the method.
        if (!gui)
            return;

        // Finally, call the GUIAsset's OnGUI method
        // to display and handle the GUI Components.
        gui.OnGUI();

        // Down here is where you would check the components
        // for changes or values, and react to them.

        //foreach (GUIComponent component in gui)
        //    GUILayout.Label(component.name);

        // You can access GUIComponents in a GUIAsset in three ways.
        // 1. Use a Foreach Loop, as shown above.
        // 2. Use the GUIAsset like an array: gui[index]
        // 3. Use the Find or Find<T> method to search for a GUIComponent with a specific name.
    }
}
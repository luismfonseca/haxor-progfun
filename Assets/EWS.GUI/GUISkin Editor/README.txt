#=======================================#
#	EWS.GUISkinEditor					#
#		By Eddy @ EWS					#
#=======================================#
# Support: eddy.ews@gmail.com			#
#=======================================#

Version History:

1.2		Fixed a bug where deleting all custom styles
		resulted in an IndexOutOfBounds or NullReference
		exception(s).
		Removed the "Clear Skin" button.
		Added the ability to copy a particular style
		from a particular skin.
1.1		Added Undo Support.
1.0		No known bugs.
		Massive cosmetic changes to original GUISkin Inspector.
		Addition of Preview tool to see changes in real-time.
		Addition of GUIStyle Content Size Calculator.

Instructions:

GUISkin Editor is a rewrite of the GUISkin Inspector to make it cleaner in appearance,
and adds some functionality to ease the creation of GUISkins. This is only a small part
of a larger project to make Unity even more User-friendly than it already is.

With this package installed, simply select a GUISkin (or create a new one), and select it.
The GUISkin will appear in the Inspector, as usual, but with a few major differences.

In the original GUISkin Inspector, the GUIStyles were presented as a list of fouldouts
that each contained a list of GUIState foldouts, which finally contained the values to edit.
Some values were also exposed to edit inside the GUIStyle foldouts, and settings for
the entire skin were in a Settings foldout.

In the EWS.GUISkin Editor, you are presented with a toolbar and popup set to Settings, and a handful
of buttons that allow you to copy other GUI Skins. These settings are the GUISkin Settings. The
buttons allow you to Copy a GUI Skin, Copy the in-game default Skin, Copy the Editor/Inspector Skin,
Copy the Scene Editor Skin, or Clear the Skin data, leaving all fields blank or set to default values.

From the toolbar popup you can select a GUIStyle to edit, or select Custom to access the
Custom Styles Editor. If you select a GUIStyle to edit, you will begin (by default) in
the GUIStyle's Settings panel. A new popup will appear next to the GUIStyle Popup.
This new popup is the GUIState Popup. You can use it to change which state you
are editing, or to access the GUIStyle Settings or the GUIStyle Size Calculator.

The GUIStyle Size Calculator will display the size of a GUIContent.
You can input text and an image and see how these values change the size
of the control using this style.

The Custom Styles Editor introduces two new toolbars. The first toolbar contains a Custom Style Popup,
a New Button, which adds a new Custom Style, and a Delete Button, which deletes the selected style.
The second toolbar has a Disabled Search Bar and a Search Toggle Button. If the Search Toggle Button
is ON, the Search Bar becomes enabled, and the Custom Styles appear beneath it as a List of Toggles.
If you type into the Search Bar, only the Custom Styles whose names contain the search text
will be displayed, allowing you to find specific styles quickly if you have a long list of them.

Most panels will display a Preview. GUIStyle panels will display a preview, even Custom Styles.
When examining a Custom Style Preview, you can select from a small set of Controls to preview
the style. This popup will appear on the Preview Title Bar. If you are viewing the Preview from the
GUISkin Settings panel, several controls will be drawn together to give an overall feel for the skin.

Thank you for purchasing! If you need any help, or have any suggestions, please contact me at eddy.ews@gmail.com.

Thank you,
Eddy @ EWS
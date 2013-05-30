EWS GUIEditor Lite 1.0 README

CONTENTS

I.		Intro
II.		Known Bugs
III.	The GUI Editor
IV.		Contact

=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

I. Intro

The EWS GUI Editor is a Unity3d Editor Extension designed to make the creation of GUI systems
easier and faster. The GUI Assets created with the GUI Editor can be used in games, Editors,
EditorWindows, and anywhere else UnityGUI can be used.

This is just the Demo/Free version of the GUI Editor. A more powerful version can be found on the Asset Store.
The complete GUI Editor has more Standard Components, allows you to generate class files in C# and UnityScript,
and allows you to write new components, GUIAssets, and Code Generators. That version is updated faster,
and will recieve more components, including Editor-only components, in the future.

II. Known Bugs

Sometimes GUITextField and GUITextArea controls will become focused in the Canvas when you click on them.
To unfocus them, select a text field in the inspector or the Grid Size field in the Canvas toolbar
and press Enter.

When GUIAssets are added to the Canvas for editing, sometimes a pause will occur during which you
cannot select any assets. Select the assets through the Toolbox and everything should
return to normal.

III. The GUI Editor

The GUI Editor is split into folders inside the EWS.GUI folder. The table below explains the purpose of
each folder.

FOLDER								REQUIRED	DESCRIPTION
EWS.GUI/Core						Yes			This is the GUI Editor. DO NOT DELETE!
EWS.GUI/GUISkin Editor				No			This is the free EWS GUISkin Editor, with some minor updates.
												See the README in the GUISkin Editor folder for more information.

The GUI Editor interface is split into two Editor Windows; the Canvas and the Toolbox. You will edit components
in the Canvas, and create and manage components and GUIAssets in the Toolbox.

The Canvas consists of a Toolbar and the Canvas Area. You can drag a GUIAsset from the Project Panel to the Canvas Area
to open the GUIAsset for editing. You can open multiple GUIAssets at one time, or drag GUIAssets one-by-one into the
Canvas Area. To close a GUI Asset, hold ALT, right-click (or command-click) in the GUIAsset, and select Close GUIAsset.
You can also drag an exported component into an open GUIAsset to add the component to the GUIAsset.

You can right/command-click on a selected control to quickly access common actions, such as exporting/copying, sending
back/forward/etc, and anchoring. Right/command-click on GUIAssets while holding ALT to access some similar functions
for GUIAssets.

You can move and resize controls and GUIAssets using the mouse. To move a control, select the Move tool,
then select the control to move and click-and-drag the control to the desired position. Hold CTRL to snap the control
to the GUIAsset grid. To move a GUIAsset, hold ALT, then click-and-drag the selected GUIAsset to move it. Hold CTRL to
snap the GUIAsset to the Canvas Grid.

To resize controls or GUIAssets, follow the steps above while the Scale tool is selected.

The Visual Edit Toolbar has several controls that appear when a component is Selected.

VISUAL EDIT TOOLBAR
	-Toggle Canvas Grid		- Toggles whether the Canvas Grid is displayed.
	-Toggle GUIAsset Grid	- Toggles whether the GUIAsset grid is displayed.
	-Control Name Textfield	- Change the name of the selected control.
	-Send To Back			- Send the component to the back (top) of the list.
	-Send Back				- Send the component backwards (up the list).
	-Send Forward			- Send the component forwards (down the list).
	-Send to Front			- Send the component to the front (bottom) of the list.
	-Alignment Buttons		- Used to align controls (this will NOT Anchor the controls).
	-Toggle Toolbar Mode	- Toggles the Toolbar Mode. GUI is Visual Edit Mode. Code is Code Generation Mode.

The Toolbox window is where you can add controls to the selected GUIAsset, and create new GUIAssets. To create a new GUIAsset, toggle the toolbar
button from "Components" to "Assets." By default, you should see a single button, called "Standard GUI." Select the folder where you want the GUIAsset
to be created, then click "Standrad GUI." Toggle back to "Components," drag the GUIAsset into the Canvas window, and now the Components will be enabled.
Simply select a component to add it to the GUIAsset.

The Components can be organized into groups. To see only the components in a particular group, open the popup in the toolbar (by default,
it should say "All"), and select the group you would like to see. To search for a component by its name, select the Search option, then begin
typing the name in the search field that appears. Only components whose names contain the search text will be displayed.
To cancel the search, click on the X icon in the search field.

The bottom part of the toolbox is where you will see the components in the selected GUIAsset. You can select these components,
move them up/down the list, copy, export, and delete them.

IV. Contact

You can contact me at eddy.ews@gmail.com, or through a Unity Forum PM sent to EddyEpic.
Occasionally check out the Unity forum thread EWS GUI Editor 3.0 for updates
or news related to the GUI Editor.

~Eddy @ EWS
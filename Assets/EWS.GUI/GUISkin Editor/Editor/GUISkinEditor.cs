using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(GUISkin))]
public class GUISkinEditor : Editor
{
    #region Enums

    public enum GUIStyleEditMode
    {
        Settings, Box, Button, Toggle, Label, ScrollView, TextArea, TextField,
        Window, HorizontalScrollBar, HorizontalSlider,
        VerticalScrollBar, VerticalSlider, Custom
    }

    public enum GUIStateEditMode
    {
        Settings, Normal, Active, Focused, Hover, OnNormal, OnActive, OnHover, OnFocused, CalcSize
    }

    #endregion
    #region Constants

    private const string KEY_STYLE = "EWS.GUISkinEditor.Style";
    private const string KEY_STATE = "EWS.GUISkinEditor.Style.State";
    private const string KEY_PREV_ID = "EWS.GUISkinEditor.Preview.ControlIndex";

    #endregion
    #region Fields

    private GUIStyleEditMode styleEditMode;
    private GUIStateEditMode stateEditMode;
    private Vector2 mainScroll = Vector2.zero;
    private Vector2 searchScroll = Vector2.zero;
    private string[] previewControlTypes = new string[] { "Box", "Button", "Label", "Toggle", "TextField", "TextArea", "Window", "ScrollView" };
    private int previewControlIndex;
    private Vector2 preview_ScrollView = Vector2.zero;
    private Vector2 preview_Sliders = Vector2.zero;
    private bool preview_Toggle = true;
    private string preview_Text = "Preview Text.";
    private GUISkin copySkin = null;
    private GUISkin copyStyleSkin = null;
    private GUIContent[] customStyles;
    private int customStyleIndex = -1;
    private string searchString = "";
    private bool searching = false;
    private GUIContent calcSizeContent = new GUIContent("", null, "");
    private bool foldBorder, foldMargin, foldOverflow, foldPadding, foldFont;
    private GUIStyle searchTextField = null;
    private GUIStyle searchClearButton = null;
    private GUIStyle previewToolbarPopup = null;
    private string findStyleName = "";
    private string copyStyleName = "";
    private bool showCopyStyle = false;

    #endregion
    #region Init/Close

    void OnEnable()
    {
        string styleMode = EditorPrefs.GetString(KEY_STYLE, "Settings");
        string stateMode = EditorPrefs.GetString(KEY_STATE, "Settings");

        styleEditMode = (GUIStyleEditMode)System.Enum.Parse(typeof(GUIStyleEditMode), styleMode);
        stateEditMode = (GUIStateEditMode)System.Enum.Parse(typeof(GUIStateEditMode), stateMode);
        previewControlIndex = EditorPrefs.GetInt(KEY_PREV_ID, 0);

        foldBorder = foldMargin = foldOverflow = foldPadding = foldFont = false;

        ResetCustomStyles();

        GUISkin editor = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector);
        findStyleName = "ToolbarSeachTextField";
        searchTextField = ArrayUtility.Find<GUIStyle>(editor.customStyles, MatchCustomStyle);
        findStyleName = "ToolbarSeachCancelButton";
        searchClearButton = ArrayUtility.Find<GUIStyle>(editor.customStyles, MatchCustomStyle);
        findStyleName = "ToolbarPopup";
        previewToolbarPopup = ArrayUtility.Find<GUIStyle>(editor.customStyles, MatchCustomStyle);
    }

    void OnDisable()
    {
        EditorPrefs.SetString(KEY_STYLE, styleEditMode.ToString());
        EditorPrefs.SetString(KEY_STATE, stateEditMode.ToString());
        EditorPrefs.SetInt(KEY_PREV_ID, previewControlIndex);
    }

    #endregion
    #region GUI
    #region Primary GUI

    public override void OnInspectorGUI()
    {
        Undo.SetSnapshotTarget(Target, Target.name);
        Undo.CreateSnapshot();
        Undo.ClearSnapshotTarget();

        EditorGUI.BeginChangeCheck();

        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
        styleEditMode = (GUIStyleEditMode)EditorGUILayout.EnumPopup(styleEditMode, EditorStyles.toolbarPopup);
        if (styleEditMode != GUIStyleEditMode.Settings)
            stateEditMode = (GUIStateEditMode)EditorGUILayout.EnumPopup(stateEditMode, EditorStyles.toolbarPopup, GUILayout.Width(96f));
        EditorGUILayout.EndHorizontal();

        if (GUI.changed)
            mainScroll = Vector2.zero;

        mainScroll = EditorGUILayout.BeginScrollView(mainScroll, false, false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.box);
        switch (styleEditMode)
        {
            case GUIStyleEditMode.Box:
                Target.box = StyleEditor(Target.box);
                break;
            case GUIStyleEditMode.Button:
                Target.button = StyleEditor(Target.button);
                break;
            case GUIStyleEditMode.Toggle:
                Target.toggle = StyleEditor(Target.toggle);
                break;
            case GUIStyleEditMode.Label:
                Target.label = StyleEditor(Target.label);
                break;
            case GUIStyleEditMode.ScrollView:
                Target.scrollView = StyleEditor(Target.scrollView);
                break;
            case GUIStyleEditMode.TextField:
                Target.textField = StyleEditor(Target.textField);
                break;
            case GUIStyleEditMode.TextArea:
                Target.textArea = StyleEditor(Target.textArea);
                break;
            case GUIStyleEditMode.Window:
                Target.window = StyleEditor(Target.window);
                break;
            case GUIStyleEditMode.HorizontalSlider:
                GUILayout.Label("Horizontal Slider", EditorStyles.boldLabel);
                Target.horizontalSlider = StyleEditor(Target.horizontalSlider);
                GUILayout.Label("Horizontal Slider Thumb", EditorStyles.boldLabel);
                Target.horizontalSliderThumb = StyleEditor(Target.horizontalSliderThumb);
                break;
            case GUIStyleEditMode.VerticalSlider:
                GUILayout.Label("Vertical Slider", EditorStyles.boldLabel);
                Target.verticalSlider = StyleEditor(Target.verticalSlider);
                GUILayout.Label("Vertical Slider Thumb", EditorStyles.boldLabel);
                Target.verticalSliderThumb = StyleEditor(Target.verticalSliderThumb);
                break;
            case GUIStyleEditMode.HorizontalScrollBar:
                GUILayout.Label("Horizontal Scrollbar", EditorStyles.boldLabel);
                Target.horizontalScrollbar = StyleEditor(Target.horizontalScrollbar);
                GUILayout.Label("Horizontal Scrollbar Left Button", EditorStyles.boldLabel);
                Target.horizontalScrollbarLeftButton = StyleEditor(Target.horizontalScrollbarLeftButton);
                GUILayout.Label("Horizontal Scrollbar Right Button", EditorStyles.boldLabel);
                Target.horizontalScrollbarRightButton = StyleEditor(Target.horizontalScrollbarRightButton);
                GUILayout.Label("Horizontal Scrollbar Thumb", EditorStyles.boldLabel);
                Target.horizontalScrollbarThumb = StyleEditor(Target.horizontalScrollbarThumb);
                break;
            case GUIStyleEditMode.VerticalScrollBar:
                GUILayout.Label("Vertical Scrollbar", EditorStyles.boldLabel);
                Target.verticalScrollbar = StyleEditor(Target.verticalScrollbar);
                GUILayout.Label("Vertical Scrollbar Up Button", EditorStyles.boldLabel);
                Target.verticalScrollbarUpButton = StyleEditor(Target.verticalScrollbarUpButton);
                GUILayout.Label("Vertical Scrollbar Down Button", EditorStyles.boldLabel);
                Target.verticalScrollbarDownButton = StyleEditor(Target.verticalScrollbarDownButton);
                GUILayout.Label("Vertical Scrollbar Thumb", EditorStyles.boldLabel);
                Target.verticalScrollbarThumb = StyleEditor(Target.verticalScrollbarThumb);
                break;
            case GUIStyleEditMode.Custom:
                CustomStyleEditor();
                break;
            case GUIStyleEditMode.Settings:
                SkinSettingsGUI();
                break;
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndScrollView();

        if (EditorGUI.EndChangeCheck())
        {
            Undo.SetSnapshotTarget(Target, Target.name);
            Undo.RegisterSnapshot();
        }
    }

    private void CustomStyleEditor()
    {
        #region Toolbar Top
        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

        GUI.enabled = Target.customStyles.Length > 0;
        customStyleIndex = EditorGUILayout.Popup(customStyleIndex, customStyles, EditorStyles.toolbarPopup);
        GUI.enabled = true;

        if (GUILayout.Button("New", EditorStyles.toolbarButton, GUILayout.Width(32f)))
        {
            var cStyles = Target.customStyles;
            GUIStyle style = new GUIStyle();
            style.name = "New Style " + Target.customStyles.Length;
            ArrayUtility.Add<GUIStyle>(ref cStyles, style);
            Target.customStyles = cStyles;
            ArrayUtility.Add<GUIContent>(ref customStyles, new GUIContent(Target.customStyles[Target.customStyles.Length - 1].name));
            customStyleIndex = Target.customStyles.Length - 1;
        }

        GUI.enabled = customStyleIndex > -1;
        {
            if (GUILayout.Button("Delete", EditorStyles.toolbarButton, GUILayout.Width(64f)))
            {
                if (customStyleIndex <= customStyles.Length)
                {
                    ArrayUtility.RemoveAt<GUIContent>(ref customStyles, customStyleIndex);
                    var cStyles = Target.customStyles;
                    ArrayUtility.RemoveAt<GUIStyle>(ref cStyles, customStyleIndex);
                    Target.customStyles = cStyles;
                    customStyleIndex = Mathf.Clamp(customStyleIndex - 1, 0, Target.customStyles.Length);
                }
            }
        }
        GUI.enabled = true;

        EditorGUILayout.EndHorizontal();
        #endregion
        #region Toolbar Search

        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

        GUI.enabled = searching;
        searchString = SearchField(searchString);
        GUI.enabled = true;

        EditorGUI.BeginChangeCheck();
        searching = GUILayout.Toggle(searching, "Search", EditorStyles.toolbarButton, GUILayout.Width(64f));
        if (EditorGUI.EndChangeCheck() && !searching)
            searchString = "";

        EditorGUILayout.EndHorizontal();

        if (searching)
        {
            searchScroll = GUILayout.BeginScrollView(searchScroll, false, true, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.box, GUILayout.Height(128f));
            for (int i = 0; i < customStyles.Length; i++)
            {
                if (customStyles[i].text.Contains(searchString))
                {
                    if (GUILayout.Toggle(i == customStyleIndex, customStyles[i].text))
                    {
                        customStyleIndex = i;
                    }
                }
            }
            GUILayout.EndScrollView();
        }

        #endregion

        if (customStyleIndex > -1 && customStyleIndex < Target.customStyles.Length)
        {
            Target.customStyles[customStyleIndex] = StyleEditor(Target.customStyles[customStyleIndex]);
            customStyles[customStyleIndex].text = Target.customStyles[customStyleIndex].name;
        }
    }

    private GUIStyle StyleEditor(GUIStyle style)
    {
        switch (stateEditMode)
        {
            case GUIStateEditMode.Normal:
                style.normal = StateEditor(style.normal);
                break;
            case GUIStateEditMode.Active:
                style.active = StateEditor(style.active);
                break;
            case GUIStateEditMode.Hover:
                style.hover = StateEditor(style.hover);
                break;
            case GUIStateEditMode.Focused:
                style.focused = StateEditor(style.focused);
                break;
            case GUIStateEditMode.OnNormal:
                style.onNormal = StateEditor(style.onNormal);
                break;
            case GUIStateEditMode.OnActive:
                style.onActive = StateEditor(style.onActive);
                break;
            case GUIStateEditMode.OnHover:
                style.onHover = StateEditor(style.onHover);
                break;
            case GUIStateEditMode.OnFocused:
                style.onFocused = StateEditor(style.onFocused);
                break;
            case GUIStateEditMode.Settings:
                style = StyleSettingsGUI(style);
                break;
            case GUIStateEditMode.CalcSize:
                CalcSize(style);
                break;
        }

        return style;
    }

    #endregion
    #region Settings GUI

    private void SkinSettingsGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Name");
        Target.name = EditorGUILayout.TextField(Target.name);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Hide Flags");
        Target.hideFlags = (HideFlags)EditorGUILayout.EnumPopup(Target.hideFlags);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Selection Color");
        Target.settings.selectionColor = EditorGUILayout.ColorField(Target.settings.selectionColor);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Cursor Color");
        Target.settings.cursorColor = EditorGUILayout.ColorField(Target.settings.cursorColor);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Cursor Flash Speed");
        Target.settings.cursorFlashSpeed = EditorGUILayout.FloatField(Target.settings.cursorFlashSpeed);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Double Click Selects Word");
        Target.settings.doubleClickSelectsWord = EditorGUILayout.Toggle(Target.settings.doubleClickSelectsWord);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Triple Click Selects Line");
        Target.settings.tripleClickSelectsLine = EditorGUILayout.Toggle(Target.settings.tripleClickSelectsLine);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Copy From");
        copySkin = (GUISkin)EditorGUILayout.ObjectField(copySkin, typeof(GUISkin), false);
        GUI.enabled = copySkin != null;
        if (GUILayout.Button("Copy", GUILayout.Width(48f)))
            CopySkin(copySkin);
        GUI.enabled = true;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Use Game Skin"))
        {
            if (EditorUtility.DisplayDialog("Warning!", "This action cannot be undone. Continue?", "Yes", "Cancel"))
            {
                CopySkin(EditorGUIUtility.GetBuiltinSkin(EditorSkin.Game));
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Use Editor Skin"))
        {
            if (EditorUtility.DisplayDialog("Warning!", "This action cannot be undone. Continue?", "Yes", "Cancel"))
            {
                CopySkin(EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector));
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Use Scene Skin"))
        {
            if (EditorUtility.DisplayDialog("Warning!", "This action cannot be undone. Continue?", "Yes", "Cancel"))
            {
                CopySkin(EditorGUIUtility.GetBuiltinSkin(EditorSkin.Scene));
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    private GUIStyle StyleSettingsGUI(GUIStyle style)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Name");
        style.name = EditorGUILayout.TextField(style.name);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical("Box");
        showCopyStyle = EditorGUILayout.Foldout(showCopyStyle, "Copy Style from Skin");
        if (showCopyStyle)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Copy From");
            copyStyleSkin = (GUISkin)EditorGUILayout.ObjectField(copyStyleSkin, typeof(GUISkin), false);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Style Name");
            copyStyleName = EditorGUILayout.TextField(copyStyleName);
            EditorGUILayout.EndHorizontal();
            GUI.enabled = copyStyleSkin && !string.IsNullOrEmpty(copyStyleName);
            if (GUILayout.Button("COPY"))
            {
                GUIStyle copyStyle = copyStyleSkin.FindStyle(copyStyleName);
                if (copyStyle != null)
                    style = new GUIStyle(copyStyle);
                else
                    Debug.LogWarning("ERROR : No style named " + copyStyleName + " in GUISkin " + copyStyleSkin.name + " could be found!");
                copyStyleSkin = null;
                copyStyleName = "";
            }
            GUI.enabled = true;
        }
        EditorGUILayout.EndVertical();

        #region EnumPopups

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Alignment");
        style.alignment = (TextAnchor)EditorGUILayout.EnumPopup(style.alignment);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Clipping");
        style.clipping = (TextClipping)EditorGUILayout.EnumPopup(style.clipping);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Image Position");
        style.imagePosition = (ImagePosition)EditorGUILayout.EnumPopup(style.imagePosition);
        EditorGUILayout.EndHorizontal();

        #endregion
        #region Simple

        style.contentOffset = EditorGUILayout.Vector2Field("Content Offset", style.contentOffset);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Fixed Width");
        style.fixedWidth = EditorGUILayout.FloatField(style.fixedWidth);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Fixed Height");
        style.fixedHeight = EditorGUILayout.FloatField(style.fixedHeight);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Stretch Width?");
        style.stretchWidth = EditorGUILayout.Toggle(style.stretchWidth);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Stretch Height?");
        style.stretchHeight = EditorGUILayout.Toggle(style.stretchHeight);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Word Wrap?");
        style.wordWrap = EditorGUILayout.Toggle(style.wordWrap);
        EditorGUILayout.EndHorizontal();

        #endregion
        #region Groups/Complex
        #region Font

        foldFont = EditorGUILayout.Foldout(foldFont, "Font");
        if (foldFont)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Font");
            style.font = (Font)EditorGUILayout.ObjectField(style.font, typeof(Font), false);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Font Size");
            style.fontSize = EditorGUILayout.IntField(style.fontSize);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Font Style");
            style.fontStyle = (FontStyle)EditorGUILayout.EnumPopup(style.fontStyle);
            EditorGUILayout.EndHorizontal();
        }

        #endregion
        #region Rect Offsets

        style.border = RectOffset(style.border, "Border", ref foldBorder);
        style.margin = RectOffset(style.margin, "Margin", ref foldMargin);
        style.overflow = RectOffset(style.overflow, "Overflow", ref foldOverflow);
        style.padding = RectOffset(style.padding, "Padding", ref foldPadding);
        
        #endregion
        #endregion

        return style;
    }

    #endregion
    #region Misc GUI

    private void CalcSize(GUIStyle style)
    {
        GUILayout.Label("Calculate Size of Content", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Text");
        calcSizeContent.text = EditorGUILayout.TextField(calcSizeContent.text);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Image");
        calcSizeContent.image = (Texture)EditorGUILayout.ObjectField(calcSizeContent.image, typeof(Texture), false, GUILayout.Width(64f), GUILayout.Height(64f));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Tooltip");
        calcSizeContent.tooltip = EditorGUILayout.TextField(calcSizeContent.tooltip);
        EditorGUILayout.EndHorizontal();

        Vector2 size = style.CalcSize(calcSizeContent);

        EditorGUILayout.PrefixLabel("Size");
        EditorGUI.indentLevel++;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Width:");
        GUILayout.Label(size.x.ToString());
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Height:");
        GUILayout.Label(size.y.ToString());
        EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel++;
    }

    private GUIStyleState StateEditor(GUIStyleState state)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Background Image");
        state.background = (Texture2D)EditorGUILayout.ObjectField(state.background, typeof(Texture2D), false, GUILayout.Width(64f), GUILayout.Height(64f));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Text Color");
        state.textColor = EditorGUILayout.ColorField(state.textColor);
        EditorGUILayout.EndHorizontal();

        return state;
    }

    private RectOffset RectOffset(RectOffset rectOff, string label, ref bool fold)
    {
        fold = EditorGUILayout.Foldout(fold, label);
        if (fold)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Top");
            rectOff.top = EditorGUILayout.IntField(rectOff.top);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Left");
            rectOff.left = EditorGUILayout.IntField(rectOff.left);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Bottom");
            rectOff.bottom = EditorGUILayout.IntField(rectOff.bottom);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Right");
            rectOff.right = EditorGUILayout.IntField(rectOff.right);
            EditorGUILayout.EndHorizontal();
        }

        return rectOff;
    }

    #endregion
    #region Preview GUI

    public override void OnPreviewGUI(Rect r, GUIStyle background)
    {
        GUI.skin = Target;

        if (background.name == "IN BigTitle Inner")
            GUI.Box(r, "GUI\nSkin");
        else
        {
            switch (styleEditMode)
            {
                case GUIStyleEditMode.Box:
                    GUI.Box(r, "Box");
                    break;
                case GUIStyleEditMode.Button:
                    GUI.Button(new Rect(r.x + 8f, r.y + 8f, 96f, 21f), "Button");
                    break;
                case GUIStyleEditMode.Toggle:
                    preview_Toggle = GUI.Toggle(new Rect(r.x + 4f, r.y + 4f, 96f, 21f), preview_Toggle, "Toggle " + (preview_Toggle ? "ON" : "OFF"));
                    break;
                case GUIStyleEditMode.Label:
                    GUI.Label(new Rect(r.x + 4f, r.y + 4f, 96f, 21f), "Label");
                    break;
                case GUIStyleEditMode.ScrollView:
                    GUI.Box(r, "Scroll View", Target.scrollView);
                    preview_ScrollView.x = GUI.HorizontalScrollbar(new Rect(r.x, r.yMax - 16f, r.xMax - 16f, 16f), preview_ScrollView.x, 1, 0, 100);
                    preview_ScrollView.y = GUI.VerticalScrollbar(new Rect(r.xMax - 16f, r.y, 16f, r.height - 16f), preview_ScrollView.y, 1, 0, 100);
                    break;
                case GUIStyleEditMode.TextField:
                    preview_Text = GUI.TextField(new Rect(r.x + 4f, r.y + 4f, 96f, 21f), preview_Text);
                    break;
                case GUIStyleEditMode.TextArea:
                    preview_Text = GUI.TextArea(new Rect(r.x + 4f, r.y + 4f, 96f, 64f), preview_Text);
                    break;
                case GUIStyleEditMode.Window:
                    GUI.Box(r, "Window", Target.window);
                    break;
                case GUIStyleEditMode.HorizontalSlider:
                    preview_Sliders.x = GUI.HorizontalSlider(new Rect(r.x, r.y + ((r.height / 2) - 8f), r.width, 16f), preview_Sliders.x, 0f, 100f);
                    break;
                case GUIStyleEditMode.VerticalSlider:
                    preview_Sliders.y = GUI.VerticalSlider(new Rect(r.x + ((r.width / 2f) - 8f), r.y, 16f, r.height), preview_Sliders.y, 0f, 100f);
                    break;
                case GUIStyleEditMode.HorizontalScrollBar:
                    preview_ScrollView.x = GUI.HorizontalScrollbar(new Rect(r.x, r.yMax - 16f, r.xMax - 16f, 16f), preview_ScrollView.x, 1f, 0f, 100f);
                    break;
                case GUIStyleEditMode.VerticalScrollBar:
                    preview_ScrollView.y = GUI.VerticalScrollbar(new Rect(r.xMax - 16f, r.y, 16f, r.height - 16f), preview_ScrollView.y, 1f, 0f, 100f);
                    break;
                case GUIStyleEditMode.Custom:
                    if (customStyleIndex < 0 || customStyleIndex >= Target.customStyles.Length)
                        break;
                    GUIStyle style = Target.customStyles[customStyleIndex];
                    switch (previewControlIndex)
                    {
                        case 0: // Box
                            GUI.Box(r, style.name, style);
                            break;
                        case 1: // Button
                            GUI.Button(new Rect(r.x + 4f, r.y + 4f, 96f, 21f), style.name, style);
                            break;
                        case 2: // Label
                            GUI.Label(new Rect(r.x + 4f, r.y + 4f, 96f, 21f), style.name, style);
                            break;
                        case 3:
                            preview_Toggle = GUI.Toggle(new Rect(r.x + 4f, r.y + 4f, 96f, 21f), preview_Toggle, style.name + " Toggle " + (preview_Toggle ? "ON" : "OFF"), style);
                            break;
                        case 4: // TextField
                            preview_Text = GUI.TextField(new Rect(r.x + 4f, r.y + 4f, 96f, 21f), preview_Text, style);
                            break;
                        case 5: // TextArea
                            preview_Text = GUI.TextArea(new Rect(r.x + 4f, r.y + 4f, 96f, 64f), preview_Text, style);
                            break;
                        case 6: // Window
                            GUI.Box(r, style.name, style);
                            break;
                        case 7: // ScrollView
                            GUI.Box(r, style.name, style);
                            preview_ScrollView.x = GUI.HorizontalScrollbar(new Rect(r.x, r.yMax - 16f, r.xMax - 16f, 16f), preview_ScrollView.x, 1, 0, 100);
                            preview_ScrollView.y = GUI.VerticalScrollbar(new Rect(r.xMax - 16f, r.y, 16f, r.height - 16f), preview_ScrollView.y, 1, 0, 100);
                            break;
                    }
                    break;
                case GUIStyleEditMode.Settings:
                    GUI.Box(r, Target.name);
                    GUI.Button(new Rect(r.x + 4f, r.y + 16f, 64f, 21f), "Button");
                    preview_Toggle = GUI.Toggle(new Rect(r.x + 4f, r.y + 38f, r.width - 48f, 21f), preview_Toggle, "Toggle " + (preview_Toggle ? "ON" : "OFF"));
                    preview_Text = GUI.TextField(new Rect(r.x + 4f, r.y + 60f, r.width - 48f, 21f), preview_Text);
                    preview_Text = GUI.TextArea(new Rect(r.x + 4f, r.y + 82f, r.width - 48f, 48f), preview_Text);
                    preview_Sliders.x = GUI.HorizontalSlider(new Rect(r.x + 4f, r.y + 130f, r.width - 48f, 12f), preview_Sliders.x, 0, 100);
                    preview_Sliders.y = GUI.VerticalSlider(new Rect(r.xMax - 16f, r.y + 16f, 12f, r.height - 18f), preview_Sliders.y, 0, 100);
                    preview_ScrollView.x = GUI.HorizontalScrollbar(new Rect(r.x + 4f, r.y + 152f, r.width - 48f, 16f), preview_ScrollView.x, 1, 0, 10);
                    preview_ScrollView.y = GUI.VerticalScrollbar(new Rect(r.width - 36f, r.y + 16f, 16f, r.height - 18f), preview_ScrollView.y, 1, 0, 10);
                    break;
            }
        }

        GUI.skin = null;
    }

    public override GUIContent GetPreviewTitle()
    {
        return new GUIContent(Target.name);
    }

    public override void OnPreviewSettings()
    {
        if (styleEditMode == GUIStyleEditMode.Custom)
            previewControlIndex = EditorGUILayout.Popup(previewControlIndex, previewControlTypes, previewToolbarPopup);
    }

    #endregion
    #endregion
    #region Methods

    private void CopySkin(GUISkin skin)
    {
        Target.box = skin.box;
        Target.button = skin.button;
        Target.customStyles = skin.customStyles;
        Target.font = skin.font;
        Target.horizontalScrollbar = skin.horizontalScrollbar;
        Target.horizontalScrollbarLeftButton = skin.horizontalScrollbarLeftButton;
        Target.horizontalScrollbarRightButton = skin.horizontalScrollbarRightButton;
        Target.horizontalScrollbarThumb = skin.horizontalScrollbarThumb;
        Target.horizontalSlider = skin.horizontalSlider;
        Target.horizontalSliderThumb = skin.horizontalSliderThumb;
        Target.label = skin.label;
        Target.scrollView = skin.scrollView;
        Target.textArea = skin.textArea;
        Target.textField = skin.textField;
        Target.toggle = skin.toggle;
        Target.verticalScrollbar = skin.verticalScrollbar;
        Target.verticalScrollbarDownButton = skin.verticalScrollbarDownButton;
        Target.verticalScrollbarThumb = skin.verticalScrollbarThumb;
        Target.verticalScrollbarUpButton = skin.verticalScrollbarUpButton;
        Target.verticalSlider = skin.verticalSlider;
        Target.verticalSliderThumb = skin.verticalSliderThumb;
        Target.window = skin.window;
        Target.settings.cursorColor = skin.settings.cursorColor;
        Target.settings.cursorFlashSpeed = skin.settings.cursorFlashSpeed;
        Target.settings.doubleClickSelectsWord = skin.settings.doubleClickSelectsWord;
        Target.settings.selectionColor = skin.settings.selectionColor;
        Target.settings.tripleClickSelectsLine = skin.settings.tripleClickSelectsLine;

        ResetCustomStyles();
    }

    private void ResetCustomStyles()
    {
        if (customStyles != null)
            ArrayUtility.Clear<GUIContent>(ref customStyles);

        if (Target.customStyles.Length == 0)
        {
            customStyles = new GUIContent[0];
            customStyleIndex = -1;
            return;
        }

        customStyles = new GUIContent[Target.customStyles.Length];
        for (int i = 0; i < customStyles.Length; i++)
        {
            string name = string.IsNullOrEmpty(Target.customStyles[i].name) ? "New Style " + i : Target.customStyles[i].name;

            Target.customStyles[i].name = name;
            customStyles[i] = new GUIContent(name);
        }
        
        customStyleIndex = 0;
    }

    private bool MatchCustomStyle(GUIStyle testStyle)
    {
        if (testStyle.name == findStyleName)
            return true;
        return false;
    }

    private string SearchField(string text)
    {
        EditorGUILayout.BeginHorizontal();
        text = EditorGUILayout.TextField(text, searchTextField);
        if (GUILayout.Button("", searchClearButton))
            text = "";
        EditorGUILayout.EndHorizontal();

        return text;
    }

    public override bool HasPreviewGUI()
    {
        return true;
    }

    #endregion
    #region Properties

    public GUISkin Target { get { return target as GUISkin; } }

    #endregion
}
using UnityEngine;
using System.Collections;

public class CreditsController : MonoBehaviour
{
    private static readonly int TITLE_START_Y = 163;
    public static readonly int FORM_CELL_HEIGHT = 50;
    public static readonly int CONTENT_START_Y = 214;

    private static readonly string LINK_LUIS = "https://sigarra.up.pt/feup/pt/fest_geral.cursos_list?pv_num_unico=201008868";
    private static readonly string LINK_OMAR = "https://sigarra.up.pt/feup/pt/fest_geral.cursos_list?pv_num_unico=200801640";
    private static readonly string LINK_WITEK = "https://sigarra.up.pt/feup/pt/vld_entidades_geral.entidade_pagina?pct_id=778512";

    public GUISkin Skin;

    void OnGUI()
    {
        GUI.skin = Skin;
        int CONTENT_START_X = Screen.width / 2 - 190;

        GUILayout.BeginArea(new Rect(0, TITLE_START_Y, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label("THIS GAME WAS MADE BY", Skin.customStyles[SkinStyle.Title]);
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(0, CONTENT_START_Y, Screen.width, Screen.height));

            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("LUÍS FONSECA"))
                {
                    Application.OpenURL(LINK_LUIS);
                }

                if (GUILayout.Button("OMAR CASTRO"))
                {
                    Application.OpenURL(LINK_OMAR);
                }

                if (GUILayout.Button("WITOLD ZGRABKA"))
                {
                    Application.OpenURL(LINK_WITEK);
                }
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

        GUILayout.EndArea();


        GUILayout.BeginArea(new Rect(0, CONTENT_START_Y + FORM_CELL_HEIGHT + 5, Screen.width, Screen.height));

            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label("Luís Fonseca is 22 years old and is currently a student at Faculdade de Engenharia da Universidade do Porto. \nHe likes music, programming, and games in general.", Skin.customStyles[SkinStyle.Credits]);
                GUILayout.Label("Omar Castro is 22 years old and is currently a student at Faculdade de Engenharia da Universidade do Porto. \nHe likes stuff.", Skin.customStyles[SkinStyle.Credits]);
                GUILayout.Label("Witold Zgrabka is 24 years old and is currently an erasmus student at Faculdade de Engenharia da Universidade do Porto. \nHe likes stuff.", Skin.customStyles[SkinStyle.Credits]);
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.Space(100);
            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("OK"))
                {
                    Application.LoadLevel("MainMenu");
                }
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
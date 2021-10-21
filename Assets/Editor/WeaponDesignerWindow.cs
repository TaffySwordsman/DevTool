using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponDesignerWindow : EditorWindow
{

    Texture2D baseTexture;
    Texture2D opticsTexture;
    Texture2D barrelTexture;
    Texture2D magTexture;

    Rect mainSection;
    Rect displaySection;

    [MenuItem("Window/Weapon Designer")]
    static void OpenWindow()
    {
        WeaponDesignerWindow window = (WeaponDesignerWindow)GetWindow(typeof(WeaponDesignerWindow));
        window.minSize = new UnityEngine.Vector2(400, 500);
        window.Show();
    }

    void OnEnable()
    {
        InitTextures();
    }

    void InitTextures()
    {
        baseTexture = Resources.Load<Texture2D>("Famas");
    }

    void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
    }

    void DrawLayouts()
    {
        mainSection.x = 0;
        mainSection.y = 0;
        mainSection.width = Screen.width;
        mainSection.height = Screen.height;

        displaySection.x = 20;
        displaySection.y = Screen.height * 0.66f + 20;
        displaySection.width = Screen.width - 50;
        displaySection.height = Screen.height * 0.33f - 50;

        EditorGUILayout.BeginVertical();
        EditorGUILayout.ObjectField(baseTexture, typeof(Texture2D), false);
        EditorGUILayout.ObjectField(opticsTexture, typeof(Texture2D), false);
        EditorGUILayout.EndVertical();

        GUI.DrawTexture(displaySection, baseTexture, ScaleMode.ScaleToFit);
    }

    void DrawHeader()
    {

    }

    Rect CenterImage(Texture2D image, Rect bounds)
    {
        return new Rect ((bounds.width / 2) - (image.width/2), (bounds.height / 2) - (image.height/2), image.width, image.height);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponDesignerWindow : EditorWindow
{
    Texture2D noWeapon, noAttachment, weapon;
    Texture2D baseTexture;
    Texture2D opticsTexture;
    Texture2D barrelTexture;
    Texture2D magTexture;

    Texture2D mainTex, dispTex, attachTex, weapTex;

    Rect mainSection;
    Rect displaySection;
    Rect attachmentSection;
    Rect weaponSection;

    Color mainColor = Color.yellow;
    Color displayColor = Color.blue;
    Color attachmentColor = Color.cyan;
    Color weaponColor = Color.magenta;

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
        mainTex = new Texture2D(1,1);
        mainTex.SetPixel(0, 0, mainColor);
        mainTex.Apply();

        dispTex = new Texture2D(1,1);
        dispTex.SetPixel(0, 0, displayColor);
        dispTex.Apply();

        attachTex = new Texture2D(1,1);
        attachTex.SetPixel(0, 0, weaponColor);
        attachTex.Apply();

        weapTex = new Texture2D(1,1);
        weapTex.SetPixel(0, 0, attachmentColor);
        weapTex.Apply();

        noWeapon = Resources.Load<Texture2D>("RifleNone");
        noAttachment = Resources.Load<Texture2D>("None");
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
        displaySection.y = Screen.height * 0.5f + 20;
        displaySection.width = Screen.width - 50;
        displaySection.height = Screen.height * 0.5f - 50;

        attachmentSection.x = displaySection.x;
        attachmentSection.y = displaySection.y;
        attachmentSection.width = displaySection.width;
        attachmentSection.height = displaySection.height * 0.33f;

        weaponSection.x = displaySection.x;
        weaponSection.y = displaySection.y + displaySection.height * 0.33f;
        weaponSection.width = displaySection.width;
        weaponSection.height = displaySection.height * 0.66f;

        // Used for debug
        GUI.DrawTexture(mainSection, mainTex);
        GUI.DrawTexture(displaySection, dispTex);
        GUI.DrawTexture(attachmentSection, attachTex);
        GUI.DrawTexture(weaponSection, weapTex);

        GUILayout.BeginArea(mainSection);
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space();
        
        baseTexture = (Texture2D)EditorGUILayout.ObjectField(baseTexture, typeof(Texture2D), false);
        // If "None" selected
        if(baseTexture == null)
            weapon = noWeapon;
        else
            weapon = baseTexture;

        EditorGUILayout.ObjectField(opticsTexture, typeof(Texture2D), false);
        EditorGUILayout.EndVertical();

        GUI.DrawTexture(weaponSection, weapon, ScaleMode.ScaleToFit);
        GUILayout.EndArea();
    }

    void DrawHeader()
    {

    }

    Rect CenterImage(Texture2D image, Rect bounds)
    {
        return new Rect ((bounds.width / 2) - (image.width/2), (bounds.height / 2) - (image.height/2), image.width, image.height);
    }
}

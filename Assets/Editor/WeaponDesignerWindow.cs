using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponDesignerWindow : EditorWindow
{
    Texture2D noWeapon, noAttachment;
    Texture2D baseTexture, opticsTexture, barrelTexture, magTexture;
    Texture2D mainTex, dispTex, attachTex, weapTex;

    WeaponBaseData weaponBase;
    BarrelData barrel;
    OpticsData optics;
    MagazineData magazine;

    string weaponName, barrelName, opticsName, magName;

    Rect mainSection;
    Rect displaySection;
    Rect attachmentSection;
    Rect weaponSection;
    Rect barrelSection, opticsSection, magSection;

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
        mainTex = new Texture2D(1, 1);
        mainTex.SetPixel(0, 0, mainColor);
        mainTex.Apply();

        dispTex = new Texture2D(1, 1);
        dispTex.SetPixel(0, 0, displayColor);
        dispTex.Apply();

        attachTex = new Texture2D(1, 1);
        attachTex.SetPixel(0, 0, weaponColor);
        attachTex.Apply();

        weapTex = new Texture2D(1, 1);
        weapTex.SetPixel(0, 0, attachmentColor);
        weapTex.Apply();

        noWeapon = Resources.Load<Texture2D>("RifleNone");
        noAttachment = Resources.Load<Texture2D>("None");
    }

    void OnGUI()
    {
        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;

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
        weaponSection.y = displaySection.y + displaySection.height * 0.33f + 30;
        weaponSection.width = displaySection.width;
        weaponSection.height = displaySection.height * 0.66f - 30;

        barrelSection.x = attachmentSection.x;
        barrelSection.y = attachmentSection.y;
        barrelSection.width = attachmentSection.width / 3;
        barrelSection.height = attachmentSection.height;

        opticsSection.x = attachmentSection.x + attachmentSection.width / 3;
        opticsSection.y = attachmentSection.y;
        opticsSection.width = attachmentSection.width / 3;
        opticsSection.height = attachmentSection.height;

        magSection.x = attachmentSection.x + attachmentSection.width / 3 * 2;
        magSection.y = attachmentSection.y;
        magSection.width = attachmentSection.width / 3;
        magSection.height = attachmentSection.height;

        // Used for debug
        // GUI.DrawTexture(mainSection, mainTex);
        // GUI.DrawTexture(displaySection, dispTex);
        // GUI.DrawTexture(attachmentSection, attachTex);
        // GUI.DrawTexture(weaponSection, weapTex);

        GUILayout.BeginArea(mainSection);
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space();

        weaponBase = (WeaponBaseData)EditorGUILayout.ObjectField("Weapon Base", weaponBase, typeof(WeaponBaseData), false);
        // If "None" selected
        if (weaponBase == null)
        {
            baseTexture = noWeapon;
            weaponName = "Empty";
        }
        else
        {
            baseTexture = weaponBase.image;
            weaponName = weaponBase.partName;
        }

        barrel = (BarrelData)EditorGUILayout.ObjectField("Barrel", barrel, typeof(BarrelData), false);
        // If "None" selected
        if (barrel == null)
        {
            barrelTexture = noAttachment;
            barrelName = "Empty";
        }
        else
        {
            barrelTexture = barrel.image;
            barrelName = barrel.partName;
        }

        optics = (OpticsData)EditorGUILayout.ObjectField("Optics", optics, typeof(OpticsData), false);
        // If "None" selected
        if (optics == null)
        {
            opticsTexture = noAttachment;
            opticsName = "Empty";
        }
        else
        {
            opticsTexture = optics.image;
            opticsName = optics.partName;
        }

        magazine = (MagazineData)EditorGUILayout.ObjectField("Magazine", magazine, typeof(MagazineData), false);
        // If "None" selected
        if (magazine == null)
        {
            magTexture = noAttachment;
            magName = "Empty";
        }
        else
        {
            magTexture = magazine.image;
            magName = magazine.partName;
        }

        EditorGUILayout.EndVertical();

        GUI.DrawTexture(barrelSection, barrelTexture, ScaleMode.ScaleToFit);
        GUI.Label(CenterTextAbove(barrelSection), barrelName);
        GUI.DrawTexture(opticsSection, opticsTexture, ScaleMode.ScaleToFit);
        GUI.Label(CenterTextAbove(opticsSection), opticsName);
        GUI.DrawTexture(magSection, magTexture, ScaleMode.ScaleToFit);
        GUI.Label(CenterTextAbove(magSection), magName);
        GUI.DrawTexture(weaponSection, baseTexture, ScaleMode.ScaleToFit);
        GUI.Label(CenterTextAbove(weaponSection), weaponName);
        GUILayout.EndArea();
    }

    void DrawHeader()
    {

    }

    Rect CenterTextAbove(Rect bounds)
    {
        // return new Rect ((bounds.width / 2) - (image.width/2), (bounds.height / 2) - (image.height/2), image.width, image.height);
        return new Rect(bounds.x, bounds.y - 20, bounds.width, 20);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class WeaponDesignerWindow : EditorWindow
{

    Texture2D noWeapon, noAttachment;
    Texture2D baseTexture, opticsTexture, barrelTexture, magTexture;
    Texture2D mainTex, dispTex, attachTex, weapTex;

    WeaponBaseData weaponBase;
    BarrelData barrel;
    OpticsData optics;
    MagazineData magazine;

    string weaponName = "New Weapon", baseName, barrelName, opticsName, magName;
    Vector2 scrollPos;

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

        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(displaySection.y - 30));

        EditorGUILayout.Space(10);
        GUILayout.Label("Weapon", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        DrawPartFields();

        EditorGUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        
        if (GUILayout.Button("New Base", GUILayout.Height(40)))
        {
            WeaponPartAsset.CreateWeaponBase();
        }
        if (GUILayout.Button("New Barrel", GUILayout.Height(40)))
        {
            WeaponPartAsset.CreateBarrel();
        }
        if (GUILayout.Button("New Optics", GUILayout.Height(40)))
        {
            WeaponPartAsset.CreateOptics();
        }
        if (GUILayout.Button("New Magazine", GUILayout.Height(40)))
        {
            WeaponPartAsset.CreateMagazine();
        }

        EditorGUILayout.EndHorizontal();

        WeaponBaseData[] instances = Resources.FindObjectsOfTypeAll<WeaponBaseData>();

        if (GUILayout.Button("Reset Weapon", GUILayout.Height(40)))
        {
            weaponName = "New Weapon";
            weaponBase = null;
            barrel = null;
            optics = null;
            magazine = null;
        }

        if (GUILayout.Button("Save Weapon As Prefab", GUILayout.Height(40)))
        {
            GameObject prefab = new GameObject(weaponName);
            Weapon prefabWeapon = prefab.AddComponent<Weapon>();
            GameObject newPrefab = prefabWeapon.CreatePrefab(weaponName, weaponBase, barrel, optics, magazine);
            string localPath = "Assets/Saved Weapons/" + weaponName + ".prefab";
            if (!Directory.Exists("Assets/Saved Weapons/"))
                Directory.CreateDirectory("Assets/Saved Weapons/");
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
            PrefabUtility.SaveAsPrefabAssetAndConnect(prefab, localPath, InteractionMode.UserAction);
            DestroyImmediate(newPrefab);
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

        GuiLine(3);
        EditorGUILayout.Space(10);

        GUI.DrawTexture(barrelSection, barrelTexture, ScaleMode.ScaleToFit);
        GUI.Label(CenterTextAbove(barrelSection), barrelName);
        GUI.DrawTexture(opticsSection, opticsTexture, ScaleMode.ScaleToFit);
        GUI.Label(CenterTextAbove(opticsSection), opticsName);
        GUI.DrawTexture(magSection, magTexture, ScaleMode.ScaleToFit);
        GUI.Label(CenterTextAbove(magSection), magName);
        GUI.DrawTexture(weaponSection, baseTexture, ScaleMode.ScaleToFit);
        GUI.Label(CenterTextAbove(weaponSection), baseName);
    }

    void DrawPartFields()
    {
        weaponName = EditorGUILayout.TextField("Name", weaponName);

        weaponBase = (WeaponBaseData)EditorGUILayout.ObjectField("Base", weaponBase, typeof(WeaponBaseData), false);
        // If "None" selected
        if (weaponBase == null)
        {
            baseTexture = noWeapon;
            baseName = "Empty";
        }
        else
        {
            baseTexture = weaponBase.image;
            baseName = weaponBase.partName;
            // weapon.weaponBase = weaponBase;
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
            // weapon.barrel = barrel;
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
            // weapon.optics = optics;
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
            // weapon.magazine = magazine;
        }
    }

    Rect CenterTextAbove(Rect bounds)
    {
        // return new Rect ((bounds.width / 2) - (image.width/2), (bounds.height / 2) - (image.height/2), image.width, image.height);
        return new Rect(bounds.x, bounds.y - 20, bounds.width, 20);
    }

    void GuiLine(int i_height = 1)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, i_height);
        rect.height = i_height;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
    }
}

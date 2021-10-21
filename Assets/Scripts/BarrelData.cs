using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrelData : ScriptableObject
{
    public Texture2D image;
    public string partName;
    [Range(-1, 1)]
    public float accuracyModifier;
    [Range(-1, 1)]
    public float stabilityModifier; 
}

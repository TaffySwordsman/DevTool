using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBaseData : ScriptableObject
{
    public Texture2D image;
    public string partName;
    public MagnificationEnums magnification;
    public float damage;
    [Range(0, 1)]
    public float accuracy;
    [Range(0, 1)]
    public float stability;
    public float reloadSpeed;
    public float fireRate;
    public float magSize;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponBaseData weaponBase;
    [SerializeField] BarrelData barrel;
    [SerializeField] OpticsData optics;
    [SerializeField] MagazineData magazine;
    [ReadOnly] public string partName;
    [ReadOnly] public MagnificationEnums magnification;
    [ReadOnly] public float damage;
    [ReadOnly, Range(0, 1)]
    public float accuracy;
    [ReadOnly, Range(0, 1)]
    public float stability;
    [ReadOnly] public float reloadSpeed;
    [ReadOnly] public float fireRate;
    [ReadOnly] public float magSize;
}

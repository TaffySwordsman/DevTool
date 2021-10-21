using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public WeaponBaseData weaponBase;
    [SerializeField] public BarrelData barrel;
    [SerializeField] public OpticsData optics;
    [SerializeField] public MagazineData magazine;
    public string weaponName;
    [ReadOnly] public MagnificationEnums magnification = MagnificationEnums.X1;
    [ReadOnly] public float damage = 1;
    [Range(0, 1),ReadOnly] public float accuracy = 1;
    [Range(0, 1), ReadOnly] public float stability = 1;
    [ReadOnly] public float reloadSpeed = 3;
    [ReadOnly] public float fireRate = 10;
    [ReadOnly] public float magSize = 10;

    public Weapon()
    {
        this.weaponBase = null;
        this.barrel = null;
        this.optics = null;
        this.magazine = null;
    }

    public Weapon(WeaponBaseData weaponBase = null, BarrelData barrel = null, OpticsData optics = null, MagazineData magazine = null)
    {
        this.weaponBase = weaponBase;
        this.barrel = barrel;
        this.optics = optics;
        this.magazine = magazine;

        if (weaponBase != null)
        {
            magnification = weaponBase.magnification;
            damage = weaponBase.damage;
            accuracy = weaponBase.accuracy;
            stability = weaponBase.stability;
            reloadSpeed = weaponBase.reloadSpeed;
            fireRate = weaponBase.fireRate;
            magSize = weaponBase.magSize;
        }

        if (barrel != null)
        {
            accuracy = Mathf.Clamp(accuracy + barrel.accuracyModifier, 0, 1);
            stability = Mathf.Clamp(stability + barrel.stabilityModifier, 0, 1);
        }

        if (optics != null)
        {
            magnification = optics.magnification;
        }

        if (magazine != null)
        {
            magSize = magazine.magSize;
            reloadSpeed = magazine.reloadSpeed;
        }
    }

    public GameObject CreatePrefab(string weaponName, WeaponBaseData weaponBase = null, BarrelData barrel = null, OpticsData optics = null, MagazineData magazine = null)
    {
        this.weaponBase = weaponBase;
        this.barrel = barrel;
        this.optics = optics;
        this.magazine = magazine;

        if (weaponBase != null)
        {
            magnification = weaponBase.magnification;
            damage = weaponBase.damage;
            accuracy = weaponBase.accuracy;
            stability = weaponBase.stability;
            reloadSpeed = weaponBase.reloadSpeed;
            fireRate = weaponBase.fireRate;
            magSize = weaponBase.magSize;
        }

        if (barrel != null)
        {
            accuracy = Mathf.Clamp(accuracy + barrel.accuracyModifier, 0, 1);
            stability = Mathf.Clamp(stability + barrel.stabilityModifier, 0, 1);
        }

        if (optics != null)
        {
            magnification = optics.magnification;
        }

        if (magazine != null)
        {
            magSize = magazine.magSize;
            reloadSpeed = magazine.reloadSpeed;
        }
        GameObject go = new GameObject(weaponName);
        // go.AddComponent<Weapon>();
        return go;
    }

    void OnValidate() {
        if (weaponBase != null)
        {
            magnification = weaponBase.magnification;
            damage = weaponBase.damage;
            accuracy = weaponBase.accuracy;
            stability = weaponBase.stability;
            reloadSpeed = weaponBase.reloadSpeed;
            fireRate = weaponBase.fireRate;
            magSize = weaponBase.magSize;
        }

        if (barrel != null)
        {
            accuracy = Mathf.Clamp(accuracy + barrel.accuracyModifier, 0, 1);
            stability = Mathf.Clamp(stability + barrel.stabilityModifier, 0, 1);
        }

        if (optics != null)
        {
            magnification = optics.magnification;
        }

        if (magazine != null)
        {
            magSize = magazine.magSize;
            reloadSpeed = magazine.reloadSpeed;
        }
    }


}

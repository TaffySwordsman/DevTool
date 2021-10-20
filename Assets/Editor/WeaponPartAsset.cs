using UnityEngine;
using UnityEditor;

public class WeaponPartAsset
{
    [MenuItem("Assets/Create/Modular Weapon Part/Weapon Base")]
	public static void CreateWeaponBase ()
	{
		ScriptableObjectUtility.CreateAsset<WeaponBaseData> ();
	}

    [MenuItem("Assets/Create/Modular Weapon Part/Optics")]
	public static void CreateOptics ()
	{
		ScriptableObjectUtility.CreateAsset<OpticsData> ();
	}

    [MenuItem("Assets/Create/Modular Weapon Part/Barrel")]
	public static void CreateBarrel ()
	{
		ScriptableObjectUtility.CreateAsset<BarrelData> ();
	}

    [MenuItem("Assets/Create/Modular Weapon Part/Magazine")]
	public static void CreateMagazine ()
	{
		ScriptableObjectUtility.CreateAsset<BarrelData> ();
	}

    [MenuItem("Assets/Create/Modular Weapon Part/Bullet")]
	public static void CreateBullet ()
	{
		ScriptableObjectUtility.CreateAsset<BulletData> ();
	}
}
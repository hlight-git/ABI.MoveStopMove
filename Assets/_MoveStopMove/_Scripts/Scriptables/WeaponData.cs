using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/Weapon Data")]
public class WeaponData : SingletonCollection<WeaponData, WeaponType, TmpWeaponItem>
{
    public WeaponType NextItem(WeaponType weaponType)
    {
        int index = values.FindIndex(q => q.type == weaponType);
        index = index + 1 >= values.Count ? 0 : index + 1;
        return values[index].type;
    }

    public WeaponType PrevItem(WeaponType weaponType)
    {
        int index = values.FindIndex(q => q.type == weaponType);
        index = index - 1 < 0 ? values.Count - 1 : index - 1;
        return values[index].type;
    }
}

[System.Serializable]
public class TmpWeaponItem
{
    public string name;
    public WeaponType type;
    public int cost;
    public int ads;
}
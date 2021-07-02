using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LaserWeaponFeatures", menuName = "ScriptableObjects/LaserWeaponFeatures", order = 1)]
//All laser weapon features
public class LaserWeaponFeatures : ScriptableObject
{
    public string weaponName;
    public float battery;
    public float range;
    public Material material;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerFeatures", menuName = "ScriptableObjects/PlayerFeatures", order = 1)]

//All player features
public class PlayerFeatures : ScriptableObject
{
    public float playerHealth;
    public Color color;
}

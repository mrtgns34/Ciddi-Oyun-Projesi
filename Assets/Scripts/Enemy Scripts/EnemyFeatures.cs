using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFeatures", menuName = "ScriptableObjects/EnemyFeatures", order = 1)]
//All enemy features
public class EnemyFeatures : ScriptableObject
{
    public float health;
    public float stoppingDistance;
    public Color color;
}

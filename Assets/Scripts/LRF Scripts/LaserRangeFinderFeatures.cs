using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LaserRangeFinderFeatures", menuName = "ScriptableObjects/LaserRangeFinderFeatures", order = 1)]
//All lrf features
public class LaserRangeFinderFeatures : ScriptableObject
{
    public string lrfName;
    public float viewAngle;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Report values
public class ReportManager : MonoBehaviour
{
    public static float playerHealth;
    public static int totalFire;
    public static int totalSuccessFire;
    public static float totalDamage;
    public static int totalKill;
    public static List<float> killDistances = new List<float>();
    public static float totalTime;
    public static void ClearData()
    {
        playerHealth = 0;
        totalFire = 0;
        totalSuccessFire = 0;
        totalDamage = 0;
        totalKill = 0;
        killDistances.Clear();
        totalTime = 0;
    }
}

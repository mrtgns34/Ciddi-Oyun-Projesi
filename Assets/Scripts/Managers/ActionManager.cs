using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All actions
public class ActionManager : MonoBehaviour
{
    public static ActionManager instance;
    public Action OnPlayerDead;
    public Action<float> OnEnemyDead;
    public Action OnAllEnemyDead;
    public Action OnBatteryDead;
    private void Awake()
    {
        instance = this;
    }
}

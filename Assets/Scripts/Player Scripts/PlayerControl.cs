using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    PlayerFeatures playerFeaturesObject;

    [SerializeField]
    LaserWeapon laserWeapon;
    [SerializeField]
    LaserRangeFinder laserRangeFinder;

    [SerializeField]
    LaserWeaponFeatures[] laserWeaponFeatures;
    [SerializeField]
    LaserRangeFinderFeatures[] laserRangeFinderFeatures;
    [SerializeField]
    PlayerFeatures[] playerFeatures;
    #region features
    private float playerHealth;
    private Color color;
    #endregion

    [SerializeField]
    TextMeshPro playerInfo;

    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    Material mainMaterial;

    ActionManager actionManager;

    float timer;
    float playerHealthFirstValue;

    #region uiElements
    [SerializeField]
    Text timeText;
    [SerializeField]
    Text killText;
    #endregion
    private void Start()
    {
        ReportManager.ClearData(); //Clear report data for every restart
        SetAllFeatures();
        SetFeatures();
        SetColor();
        actionManager = ActionManager.instance;
        playerHealthFirstValue = playerHealth;

        actionManager.OnAllEnemyDead += GameOver;
        actionManager.OnPlayerDead += GameOver;
        actionManager.OnEnemyDead += EnemyKill;
        actionManager.OnBatteryDead += GameOver;

        LockCursor();

    }
    private void GameOver()
    {
        SceneManager.LoadScene(2);
        ReportManager.totalTime = timer;
        ReportManager.playerHealth = playerHealth;
    }
    //Show kill count on ui and send kill distance to reportmanager
    private void EnemyKill(float distance)
    {
        ReportManager.totalKill += 1;
        killText.text = "KILL X "+ReportManager.totalKill;
        ReportManager.killDistances.Add(distance);
    }
    private void Update()
    {
        Timer();
        Rotation();
        SetPlayerInfo();
    }
    //Rotating with mouse movement
    private void Rotation()
    {
        transform.Rotate(0, (Input.GetAxis("Mouse X") * 100 * Time.deltaTime), 0, Space.World);
        cameraTransform.Rotate(-1*(Input.GetAxis("Mouse Y") * 100 * Time.deltaTime), 0, 0,Space.Self);
    }
    //Hit player
    public void Hit(float damage)
    {
        playerHealth -= damage;
    }
    //Set player info textmeshpro
    private void SetPlayerInfo()
    {
        playerInfo.text = PlayerPrefs.GetString("name") + "\n" + GetPlayerHealthTextColor();
    }
    //Calculate and show time
    private void Timer()
    {
        timer += Time.deltaTime;
        timeText.text = "TIME: " + (int)timer;
    }
    //Show player health with colored text
    private string GetPlayerHealthTextColor()
    {
        string playerHealthString = playerHealth.ToString();
        if (playerHealth < playerHealthFirstValue / 3)
        {
            playerHealthString = "<b><color=red>" + playerHealth + "</color></b>";
        }
        else if (playerHealth < playerHealthFirstValue / 2)
        {
            playerHealthString = "<b><color=orange>" + playerHealth + "</color></b>";
        }
        else
        {
            playerHealthString = "<b><color=green>" + playerHealth + "</color></b>";
        }
        return playerHealthString;
    }
    //Lock cursor
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    //Set features with scriptable object
    private void SetFeatures()
    {
        playerHealth = playerFeaturesObject.playerHealth;
        color = playerFeaturesObject.color;
    }
    //Set player color
    private void SetColor()
    {
        mainMaterial.color = color;
    }
    //Set features for laser weapon,player and lrf
    private void SetAllFeatures()
    {
        playerFeaturesObject = playerFeatures[PlayerPrefs.GetInt("character")];
        laserWeapon.GetComponent<LaserWeapon>().laserWeaponFeatures = laserWeaponFeatures[PlayerPrefs.GetInt("weapon")];
        laserRangeFinder.GetComponent<LaserRangeFinder>().laserRangeFinderFeatures = laserRangeFinderFeatures[PlayerPrefs.GetInt("equipment")];
    }
}

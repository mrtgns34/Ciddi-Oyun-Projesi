using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FinishSceneManager : MonoBehaviour
{
    [Header("Text Areas")]
    [SerializeField]
    Text totalDamage;
    [SerializeField]
    Text totalKill;
    [SerializeField]
    Text killDistances;
    [SerializeField]
    Text totalTime;
    [SerializeField]
    Text health;
    [SerializeField]
    Text totalFire;
    [SerializeField]
    Text totalSuccessFire;
    [SerializeField]
    Image weapon;

    [SerializeField]
    Sprite[] weaponSprites;
    //Show report values on ui
    void Awake()
    {
        UnLockCursor();
        totalDamage.text ="TOTAL DAMAGE: "+ Mathf.Abs(ReportManager.totalDamage);
        totalKill.text =ReportManager.totalKill+" x";
        killDistances.text = "AVERAGE KILL DISTANCE: " + AverageKillDistance();
        totalTime.text = "TOTAL TIME: " + ReportManager.totalTime;
        health.text = "PLAYER HEALTH: " + ReportManager.playerHealth;
        totalFire.text = "TOTAL FIRE: " + ReportManager.totalFire;
        totalSuccessFire.text = "TOTAL SUCCESS FIRE:" + ReportManager.totalSuccessFire;
        weapon.sprite = weaponSprites[PlayerPrefs.GetInt("weapon")];
    }
    //Calculate average kill distance with killdistances list
    private float AverageKillDistance()
    {
        if (ReportManager.killDistances.Count > 0)
        {
            float total = 0;
            foreach (float distance in ReportManager.killDistances)
            {
                total += distance;
            }
            return total / ReportManager.killDistances.Count;
        }
        else
        {
            return 0;
        }
    }
    //Menu buttons
    public void MenuButtons(int i)
    {
        switch (i)
        {
            case 0:
                SceneManager.LoadScene(0);
                break;
            case 1:
                SceneManager.LoadScene(1);
                break;
            case 2:
                Application.Quit();
                break;
        }
    }
    //unlock cursor
    private void UnLockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}

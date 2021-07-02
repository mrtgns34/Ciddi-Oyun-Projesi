using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LaserWeapon : MonoBehaviour
{
    [HideInInspector]
    public LaserWeaponFeatures laserWeaponFeatures;

    #region sounds
    AudioSource source;
    [SerializeField]
    AudioClip fireSound;
    [SerializeField]
    AudioSource rechargeSoundSource;
    #endregion

    #region uiElements
    [SerializeField]
    Image chargeStatus;
    [SerializeField]
    Image batteryStatus;
    [SerializeField]
    Image tempatureStatus;
    [SerializeField]
    Text tempatureText;
    #endregion

    #region features
    private float bc; //battery capacity
    private float r; //range
    private string weaponName;
    private Material material;
    private float cb; //current battery
    private float le; //loaded energy
    private float eld; //energy loading duration
    private float damage; //damage
    private float uer; //used energy for range
    private float tempature=20; //temparature
    private float cd; // cooling duration
    #endregion

    float eldTimer = 5;
    bool fireable;
    void Start()
    {
        SetFeatures();
        SetMaterial();
        SetAudioSource();
    }

    private void Update()
    {
        WeaponFireSystem();
        ShowBatteryStatus();
        ShowChargeStatus();
        ShowTempatureStatus();
        FireableControl();
    }
    #region Setters
    //Set features with sciptable object
    private void SetFeatures()
    {
        bc = laserWeaponFeatures.battery;
        cb = bc;
        r = laserWeaponFeatures.range;
        weaponName = laserWeaponFeatures.weaponName;
        material = laserWeaponFeatures.material;
    }
    //Set material
    private void SetMaterial()
    {
        GetComponent<SkinnedMeshRenderer>().material = material;
    }
    //Set audiosource
    private void SetAudioSource()
    {
        source = GetComponent<AudioSource>();
    }
    #endregion
    #region Systems
    //Calculate current battery and if current battery is zero, game over.
    private void WeaponBatterySystem()
    {
        cb = cb - le;
        if (cb <= 0)
        {
            ActionManager.instance.OnBatteryDead();
        }
    }
    //Hit the enemy with energy But energy decreases with distance.
    private void WeaponShootingSystem(Transform enemy,float le)
    {
        float range = Vector3.Distance(Camera.main.transform.position, enemy.transform.position);
        uer = le * Mathf.Log(20, range);
        damage = le - uer;
        ReportManager.totalDamage += damage;
        Debug.Log("Hit"+" damage:"+damage);
        enemy.GetComponent<Enemy>().HitEnemy(damage);
    }
    //Cooling system
    IEnumerator CoolingSystem()
    {
        cd = Mathf.Pow(2, tempature / 10) / Mathf.Pow(2, tempature / 20);
        float tempCD = cd;
        while (cd > 0)
        {
            cd -= 1 * Time.deltaTime;
            if ((int)tempature > 20)
            {
                tempature -= (70 / tempCD) * Time.deltaTime;
                fireable = false;
            }
            yield return null;
        }
    }
    //Fire and recharge system
    private void WeaponFireSystem()
    {
        if (fireable)
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * r, Color.red);
            if (Input.GetMouseButton(0))
            {
                if (eld < eldTimer)
                {
                    rechargeSoundSource.enabled = true;
                    eld += 1 * Time.deltaTime;
                    tempature += 10 * Time.deltaTime;
                    le = Mathf.Pow(2, eld) * 100;
                }
                else
                {
                    rechargeSoundSource.enabled = false;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                rechargeSoundSource.enabled = false;
                source.PlayOneShot(fireSound);
                ReportManager.totalFire += 1;
                eld = 0;
                WeaponBatterySystem();
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward)*r, out hit))
                {
                    if (hit.transform.root.CompareTag("Enemy"))
                    {
                        ReportManager.totalSuccessFire += 1;
                        WeaponShootingSystem(hit.transform.root, le);
                    }
                }
                StartCoroutine(CoolingSystem());
                le = 0;
            }
        }
    }
    #endregion
    //Show battery status on ui
    private void ShowBatteryStatus()
    {
        batteryStatus.fillAmount = cb/bc;
    }
    //Show charge status on ui
    private void ShowChargeStatus()
    {
        chargeStatus.fillAmount= eld/eldTimer;
    }
    //Show tempature status on ui
    private void ShowTempatureStatus()
    {
        tempatureStatus.fillAmount = tempature / 70;
        tempatureText.text = "Tempature: "+(int)tempature;
    }
    //It can be fired if the temperature drops to 20 degrees.
    private void FireableControl()
    {
        if ((int)tempature == 20)
        {
            StopAllCoroutines();
            fireable = true;
        }
    }
    
}

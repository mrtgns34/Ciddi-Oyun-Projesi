using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LaserRangeFinder : MonoBehaviour
{
    [HideInInspector]
    public LaserRangeFinderFeatures laserRangeFinderFeatures;

    [SerializeField]
    Camera viewCamera;
    [SerializeField]
    Transform radar;
    [SerializeField]
    GameObject enemyOnRadar;

    #region features
    private float viewAngle;
    private string lrfName;
    #endregion

    Dictionary<GameObject, GameObject> enemiesOnRadar = new Dictionary<GameObject, GameObject>();
   
    private void Start()
    {
        SetFeatures();
        SetViewAngle(viewAngle);
    }
    void Update()
    {
        RangeFinder();
        UpdateRadar();
        RotateRadarWithPlayer();
    }
    //Radar rotating with player
    private void RotateRadarWithPlayer()
    {
        radar.eulerAngles = new Vector3(0, 0, transform.root.eulerAngles.y);
    }
    //Set view angle on camera
    private void SetViewAngle(float viewAngle)
    {
        viewCamera.fieldOfView = viewAngle;
    }
    //Detects all rendered enemies.
    private void RangeFinder()
    {
        ClearRadar();
        Renderer[] allRenderers = FindObjectsOfType<Renderer>();
        foreach (Renderer renderer in allRenderers)
        {
            GameObject obj = renderer.transform.root.gameObject;
            if (InView(renderer))
            {
                if (obj.CompareTag("Enemy"))
                {
                    if (!enemiesOnRadar.ContainsKey(obj))
                    {
                        enemiesOnRadar.Add(obj, ShowInRadar());
                    }
                }
            }
        }
    }
    //Checks if it's rendering.
    private bool InView(Renderer renderer)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(viewCamera);
        return (GeometryUtility.TestPlanesAABB(planes, renderer.bounds)) ? true : false;
    }
    //Create a minimal object on radar for enemy.
    private GameObject ShowInRadar()
    {
        GameObject enemyOnRadarObject = Instantiate(enemyOnRadar, new Vector3(0, 0, 0), Quaternion.identity);
        enemyOnRadarObject.transform.parent = radar;
        enemyOnRadarObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        enemyOnRadarObject.transform.localScale = Vector3.one;
        return enemyOnRadarObject;
    }
    //Update radar objects with enemy position
    private void UpdateRadar()
    {
        foreach (var enemy in enemiesOnRadar)
        {
            if (enemy.Key != null)
            {
                enemy.Value.GetComponent<RectTransform>().anchoredPosition = new Vector3(350 * enemy.Key.transform.position.x / 100, 350 * enemy.Key.transform.position.z / 100, 0);
            }
            else
            {
                Destroy(enemy.Value);
            }
        }
    }
    //Clear radar
    private void ClearRadar()
    {
        foreach (var enemy in enemiesOnRadar)
        {
            Destroy(enemy.Value);
        }
        enemiesOnRadar.Clear();
     }
    //Set features with scriptable object
    private void SetFeatures()
    {
        viewAngle = laserRangeFinderFeatures.viewAngle;
        lrfName = laserRangeFinderFeatures.lrfName;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    [SerializeField]
    Transform[] enemySpawnPoints;
    [SerializeField]
    GameObject enemyModel;
    [SerializeField]
    EnemyFeatures[] enemyFeatures;

    List<GameObject> enemiesInScene = new List<GameObject>();
    void Awake()
    {
        SpawnEnemy();
    }
    //Spawn enemy with difficulty variable Difficulty affect spawn points, enemy object and speed.
    private void SpawnEnemy()
    {
        int difficulty = Mathf.Abs(PlayerPrefs.GetInt("difficulty")-2) + 1;
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            if (i % difficulty == 0)
            {
                GameObject enemy = Instantiate(enemyModel,enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)].position, Quaternion.identity);
                enemy.GetComponent<Enemy>().enemyFeatures = enemyFeatures[PlayerPrefs.GetInt("difficulty")];
                enemy.GetComponent<Enemy>().maxSpeed = 1 + difficulty/4;
                enemiesInScene.Add(enemy);
            }
        }
    }
    //If enemy count in scene is 0, game will be completed
    private void Update()
    {
        if (enemiesInScene.Count == 0)
        {
            ActionManager.instance.OnAllEnemyDead();
        }
    }
}

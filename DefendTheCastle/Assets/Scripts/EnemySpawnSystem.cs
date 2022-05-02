using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    public int startingLevel = 1;
    public int maxLevel;
    public float waitingTime;

    [Header("Enemy Prefabs")]
    public GameObject[] enemyPrefabs;
    private GameObject enemyParent;

    [Header("Paths For Spawnpoints")]
    private Transform spawnPoint;
    public PathSystem pathSystem;

    private void Start()
    {
        spawnPoint = pathSystem.wayPoints[0];
        enemyParent = GameObject.Find("Enemies");
    }

    public void SpawnEnemyWave()
    {
        StartCoroutine("WaitAndSpawnEnemy");
    }

    IEnumerator WaitAndSpawnEnemy()
    {
        for (int i = 0; i < 10 * GameController.levelCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(waitingTime * (1f / GameController.levelCount));
        }
    }

    private void SpawnEnemy()
    {
        GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(randomEnemyPrefab, spawnPoint.position, randomEnemyPrefab.transform.rotation, enemyParent.transform);
    }
}

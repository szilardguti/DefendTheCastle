using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    public int startingLevel = 1;
    public int maxLevel;
    public float waitingTime;
    private int enemyAmountForWave;
    public bool isWaveOver = false;

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

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    public void SpawnEnemyWave()
    {
        enemyAmountForWave = 10 * GameController.levelCount;
        StartCoroutine(WaitAndSpawnEnemy());
        StartCoroutine(CheckForFinishedWave());
    }

    IEnumerator WaitAndSpawnEnemy()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < enemyAmountForWave; i++)
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

    

    IEnumerator CheckForFinishedWave()
    {
        yield return new WaitForSeconds(1f);

        int killedEnemy = 0;
        if (enemyParent.transform.childCount != 0)
        {
            foreach (Transform enemy in enemyParent.transform)
            {
                if (enemy.GetComponent<EnemyController>().isDead && enemy.gameObject.activeSelf)
                {
                    killedEnemy++;
                }
            }
        }


        if(killedEnemy == enemyAmountForWave)
        {
            isWaveOver = true;
        }
        else
        {
            isWaveOver = false;
            StartCoroutine(CheckForFinishedWave());
        }
    }
}

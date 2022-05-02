using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static int levelCount = 1;

    public int goldAmount = 0;
    public int towerCost = 250;

    [Header("Spawning Props")]
    public EnemySpawnSystem enemySpawnSystem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            enemySpawnSystem.SpawnEnemyWave();
        }
    }

    public void AddGold(int gold)
    {
        goldAmount += gold;
    }

    public void RemoveGold()
    {
        goldAmount -= towerCost;
    }
}

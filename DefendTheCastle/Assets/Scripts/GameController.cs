using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static int levelCount = 1;

    public int goldAmount = 0;
    public int towerCost = 250;

    [Header("Spawning Props")]
    public EnemySpawnSystem enemySpawnSystem;

    [Header("UI components")]
    public TextMeshProUGUI goldText;

    private void Start()
    {
        UpadteGoldText();
        SpawnWave();
    }

    public void AddGold(int gold)
    {
        goldAmount += gold;
        UpadteGoldText();
    }

    public void RemoveGold()
    {
        goldAmount -= towerCost;
        UpadteGoldText();
    }

    private void UpadteGoldText()
    {
        goldText.SetText("Gold: {}", goldAmount);
    }

    public void SpawnWave()
    {
        enemySpawnSystem.SpawnEnemyWave();

        StartCoroutine(CheckIfWaveIsFinished());
    }

    IEnumerator CheckIfWaveIsFinished()
    {

        yield return new WaitForSeconds(5f);

        if (enemySpawnSystem.isWaveOver)
        {
            Debug.Log("Wave " + levelCount + "is finished");

            levelCount++;
            
            foreach(Transform enemy in GameObject.Find("Enemies").transform)
            {
                enemy.gameObject.SetActive(false);
            }

            SpawnWave();
            enemySpawnSystem.isWaveOver = false;
            yield return new WaitForSeconds(10f);
        }
        else
        {
            StartCoroutine(CheckIfWaveIsFinished());
        }
    }
}

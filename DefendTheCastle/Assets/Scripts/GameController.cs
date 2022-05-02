using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static int levelCount = 1;
    public static bool isGameWon = false;
    public static bool isGameLost = false;
    public static bool isGamePaused = false;

    public int goldAmount = 0;
    public int towerCost = 250;

    [Header("Spawning Props")]
    public EnemySpawnSystem enemySpawnSystem;

    [Header("UI components")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI levelText;
    public GameObject menuBackground;
    public TextMeshProUGUI menuText;

    [Header("Player Components for HP")]
    public Slider playerHPSlider;
    public TextMeshProUGUI playerHPText;
    private PlayerController player;


    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerHPSlider.maxValue = player.maxHealthPoints;
        UpdateHP();

        menuBackground.SetActive(false);
        UpdateGoldText();
        UpdateLevelText();


        SpawnWave();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;

            if (isGamePaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void AddGold(int gold)
    {
        goldAmount += gold;
        UpdateGoldText();
    }

    public void RemoveGold()
    {
        goldAmount -= towerCost;
        UpdateGoldText();
    }

    private void UpdateGoldText()
    {
        goldText.SetText("Gold: {}", goldAmount);
    }
    private void UpdateLevelText()
    {
        levelText.SetText("Level: {}", levelCount);
    }

    public void UpdateHP()
    {
        playerHPSlider.value = player.actualHealthPoints;
        playerHPText.SetText("{0}/{1}", player.actualHealthPoints, player.maxHealthPoints);
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

            if(levelCount > enemySpawnSystem.maxLevel)
            {
                isGameWon = true;
                GameOver();
            }
            
            foreach(Transform enemy in GameObject.Find("Enemies").transform)
            {
                enemy.gameObject.SetActive(false);
            }

            if (!isGameWon)
            {
                SpawnWave();
                UpdateLevelText();
                enemySpawnSystem.isWaveOver = false;
            }

            yield return new WaitForSeconds(10f);
        }
        else
        {
            StartCoroutine(CheckIfWaveIsFinished());
        }
    }

    public void GameOver()
    {
        StopAllCoroutines();
        enemySpawnSystem.StopSpawning();
        menuBackground.SetActive(true);

        if (isGameWon)
        {
            menuText.SetText("game won!");
        }
        else
        {
            menuText.SetText("game lost!");
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        menuBackground.SetActive(true);
        menuText.SetText("game paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        menuBackground.SetActive(false);
    }
}

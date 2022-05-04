using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseController : MonoBehaviour
{
    [Header("Game Stats")]
    public float baseMaxHealth = 2000;
    public float baseActualHealth;
    public float endOfLevelBonus = 150;

    public GameController gameController;

    private Animator animator;

    [Header("UI Components")]
    public Slider HPSlider;
    public TextMeshProUGUI HPText;


    private void Start()
    {
        animator = GetComponent<Animator>();
        baseActualHealth = baseMaxHealth;

        HPSlider.maxValue = baseMaxHealth;
        HPSlider.value = baseActualHealth;

        HPText.SetText("{0}/{1}", baseActualHealth, baseMaxHealth);
    }

    public void BaseAddHPEndOfWave()
    {
        baseActualHealth += endOfLevelBonus;

        UpdateUI();
    }

    public void BaseTakeDamage(float damage)
    {
        baseActualHealth -= damage;
        animator.SetTrigger("enemyReached");

        if (baseActualHealth < 0)
        {
            GameController.isGameLost = true;
            gameController.GameOver();
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        HPSlider.value = baseActualHealth;

        HPText.SetText("{0}/{1}", baseActualHealth, baseMaxHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();

            BaseTakeDamage(enemy.damage);

            enemy.speed = 0;
            enemy.isDead = true;
            enemy.transform.GetChild(0).gameObject.SetActive(false);
            enemy.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}

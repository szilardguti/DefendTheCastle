using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float speed;
    public bool isDead = false;
    public float baseHealthPoint;
    public float actualHealthPoint;
    public float damage;
    public int goldWorth;

    [Header("HP UI")]
    public Slider hpSlider;
    public Transform canvasTransform;

    [Header("Path Components")]
    private PathSystem pathSystem;
    private int pathDone = 0;
    private Transform goalPosition;

    [Header("Misc.")]
    public Animator animator;
    private GameController gameController;



    [Header("Sound components")]
    public AudioClip hitSound;
    public AudioClip deathSound;
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        pathSystem = FindObjectOfType<PathSystem>();
        gameController = FindObjectOfType<GameController>();
        audioSource = GetComponent<AudioSource>();


        goalPosition = pathSystem.wayPoints[0];

        actualHealthPoint = Random.Range(baseHealthPoint, baseHealthPoint * GameController.levelCount);
        damage = damage + 10 * GameController.levelCount;
        goldWorth = goldWorth + 20 * GameController.levelCount;

        hpSlider.maxValue = actualHealthPoint;
        hpSlider.value = actualHealthPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.isGameLost || GameController.isGameWon || GameController.isGamePaused)
        {
            return;
        }

        if (pathDone < pathSystem.pathCount)
        {
            transform.position = Vector3.MoveTowards(transform.position, goalPosition.position, speed * Time.deltaTime);
            transform.LookAt(goalPosition);

            if (Vector3.Distance(transform.position, goalPosition.position) < 0.25)
            {
                pathDone++;

                if(pathDone == pathSystem.pathCount)
                {
                    return;
                }

                goalPosition = pathSystem.wayPoints[pathDone];
            }
        }
    }

    private void LateUpdate()
    {
        canvasTransform.LookAt(transform.position + Camera.main.transform.forward);
    }


    public void EnemyHit(float damageDealt)
    {
        if (isDead)
            return;

        actualHealthPoint -= damageDealt;
        audioSource.PlayOneShot(hitSound, audioSource.volume);

        if (actualHealthPoint < 0)
        {
            actualHealthPoint = 0;
            isDead = true;
            speed = 0;

            gameController.AddGold(Random.Range(Mathf.FloorToInt(goldWorth * 0.9f), Mathf.FloorToInt(goldWorth * 1.1f)));

            this.GetComponent<CapsuleCollider>().enabled = false;
            this.GetComponent<SphereCollider>().enabled = false;
            this.transform.Find("Canvas").gameObject.SetActive(false);

            animator.SetTrigger("enemyDie");
            audioSource.PlayOneShot(deathSound, audioSource.volume);
        }

        hpSlider.value = actualHealthPoint;
    }
}

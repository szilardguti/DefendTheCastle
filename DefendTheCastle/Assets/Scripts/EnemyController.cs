using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float speed = 1.5f;
    public bool isDead = false;
    public float healthPoint = 100f;
    public float damage = 20;

    public Slider hpSlider;
    public Transform canvasTransform;

    public PathSystem pathSystem;
    private int pathDone = 0;
    private Transform goalPosition;

    private void Start()
    {
        goalPosition = pathSystem.wayPoints[0];

        hpSlider.maxValue = healthPoint;
        hpSlider.value = healthPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (pathDone < pathSystem.pathCount)
        {
            transform.position = Vector3.MoveTowards(transform.position, goalPosition.position, speed * Time.deltaTime);
            transform.LookAt(goalPosition);

            if (Vector3.Distance(transform.position, goalPosition.position) < 0.25)
            {
                pathDone++;
                goalPosition = pathSystem.wayPoints[pathDone];
            }
        }
    }

    private void LateUpdate()
    {
        canvasTransform.LookAt(transform.position + Camera.main.transform.forward);
    }


    public void enemyHit(float damageDealt)
    {
        if (isDead)
            return;

        healthPoint -= damageDealt;

        if (healthPoint < 0)
        {
            healthPoint = 0;
            isDead = true;
            speed = 0;
        }

        hpSlider.value = healthPoint;
    }
}

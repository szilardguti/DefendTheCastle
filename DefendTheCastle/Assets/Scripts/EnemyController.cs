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

    public Vector3 goalPosition;

    public Slider hpSlider;
    public Transform canvasTransform;


    private void Start()
    {
        goalPosition = new Vector3(-1f, 0.25f, -15f);
        
        hpSlider.maxValue = healthPoint;
        hpSlider.value = healthPoint;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, goalPosition, speed * Time.deltaTime);
        transform.LookAt(goalPosition);   
    }

    private void LateUpdate()
    {
        canvasTransform.LookAt(transform.position + Camera.main.transform.forward);
    }


    public void enemyHit(float damageDealt)
    {
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

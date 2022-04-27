using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    public float rotationSpeed = 10f;
    public float attackSpeed = 1.5f;
    public float damage = 15f;
    public float healthPoints = 100f;

    private BoxCollider hitRange;
    private CapsuleCollider capsuleBody;

    private void Start()
    {
        hitRange = GetComponent<BoxCollider>();
        capsuleBody = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        transform.Translate(0, 0, translation * Time.deltaTime * speed);
        transform.Rotate(0, rotation * Time.deltaTime * rotationSpeed, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                other.GetComponent<EnemyController>().enemyHit(damage);

                Debug.Log("Enemy: " + other.name + " hit!");
                StartCoroutine(PlayerAttackOnCooldown());
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit player");
            StartCoroutine(PlayerDizzy());
        }
    }


    IEnumerator PlayerAttackOnCooldown()
    {
        hitRange.enabled = false;
        yield return new WaitForSeconds(attackSpeed);
        hitRange.enabled = true;
    }

    IEnumerator PlayerDizzy()
    {
        float tempSpeed = speed;
        speed = 0;
        capsuleBody.enabled = false;

        yield return new WaitForSeconds(1);
        
        speed = tempSpeed;

        yield return new WaitForSeconds(1.5f);

        capsuleBody.enabled = true;
    }
}

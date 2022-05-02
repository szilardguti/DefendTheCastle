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
    private bool isPlayerDizzy = false;

    private BoxCollider hitRange;
    private CapsuleCollider capsuleBody;

    public GameObject turretPrefab;

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

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (CheckIfCanPlaceTurret())
            {
                Instantiate(turretPrefab, transform.position, Quaternion.identity);
            }
        }
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
            if (!isPlayerDizzy)
            {
                StartCoroutine(PlayerDizzy());
            }
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
        isPlayerDizzy = true;

        float tempSpeed = speed;
        speed = 0;
        capsuleBody.enabled = false;

        yield return new WaitForSeconds(1);
        
        speed = tempSpeed;

        yield return new WaitForSeconds(1.5f);

        capsuleBody.enabled = true;
        isPlayerDizzy = false;
    }

    private bool CheckIfCanPlaceTurret()
    {
        bool result = true;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Obstacle") || hitCollider.CompareTag("Road"))
            {
                result = false;
            }
        }

        return result;
    }
}

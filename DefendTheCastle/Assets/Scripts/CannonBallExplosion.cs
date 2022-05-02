using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallExplosion : MonoBehaviour
{

    public GameObject explosionPrefab;
    public GameObject cannonFireExplosion;

    private void Start()
    {
        Instantiate(cannonFireExplosion, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy") && hitCollider is SphereCollider)
            {
                hitCollider.GetComponent<EnemyController>().enemyHit(40f);
            }
        }
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}

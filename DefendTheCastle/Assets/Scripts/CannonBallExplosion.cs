using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallExplosion : MonoBehaviour
{
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

        Destroy(this.gameObject);
    }
}

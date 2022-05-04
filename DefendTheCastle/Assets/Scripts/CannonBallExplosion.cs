using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallExplosion : MonoBehaviour
{

    public GameObject explosionPrefab;
    public GameObject cannonFireExplosion;

    public float cannonDamage = 40f;

    private Transform particleParent;
    private List<EnemyController> enemiesHit;


    private void Start()
    {
        enemiesHit = new List<EnemyController>();
        particleParent = GameObject.Find("/Particles").transform;

        Instantiate(cannonFireExplosion, transform.position, Quaternion.identity, particleParent);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (var hitCollider in hitColliders)
        {
            
            if (hitCollider.CompareTag("Enemy") && hitCollider is SphereCollider)
            {
                
                EnemyController enemy = hitCollider.GetComponent<EnemyController>();
                if (!enemiesHit.Contains(enemy))
                {
                    enemy.EnemyHit(cannonDamage);
                    enemiesHit.Add(enemy);
                }
            }
        }
        Instantiate(explosionPrefab, transform.position, Quaternion.identity, particleParent);
        Destroy(this.gameObject);
    }
}

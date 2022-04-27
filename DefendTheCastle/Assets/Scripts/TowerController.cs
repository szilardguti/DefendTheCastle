using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [Tooltip("current target of this turret")]private Transform target;
    private Transform origoTransform;
    public bool hasTarget = false;

    private Vector3 previousTargetPos;
    private float targetVelocity;
    private Vector3 targetVelocityVec;
    private float targetTimeToFuturePos;

    public GameObject cannonBallPrefab;
    public float reloadSpeed = 2.5f;

    private void Start()
    {
        origoTransform = GameObject.Find("/Enemies").transform;
        target = origoTransform;
        StartCoroutine(CheckIfCouldShoot());
    }

    // Update is called once per frame
    void Update()
    {

        //calculate position
        var direction = target.position - transform.position; 
        //set y axis so it won't rotate
        direction.y = 0;
        //quaternion is rotation
        transform.Find("rotater").rotation = Quaternion.LookRotation(direction);


        // calculate velocity of the target without rigidbody
        targetVelocity = (target.position - previousTargetPos).magnitude * Time.deltaTime;
        targetVelocityVec = (target.position - previousTargetPos) * Time.deltaTime;
        targetTimeToFuturePos = Vector3.Distance(target.position, transform.position) / targetVelocity;

        previousTargetPos = target.position;

        if (!(target.gameObject.GetComponent<EnemyController>() is EnemyController))
        {
            return;
        }
        if (target.GetComponent<EnemyController>().isDead)
        {
            hasTarget = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!(other.gameObject.GetComponent<EnemyController>() is EnemyController))
        {
            return;
        }


        if (!hasTarget && !other.GetComponent<EnemyController>().isDead)
        {
            hasTarget = true;
            previousTargetPos = other.transform.position;
            target = other.transform;
        }
    }

    IEnumerator CheckIfCouldShoot()
    {
        if (hasTarget)
        {
            GameObject ballShot = Instantiate(cannonBallPrefab, transform.Find("rotater").Find("CannonBallSpawnPoint").position, transform.Find("rotater").rotation);

            ballShot.GetComponent<Rigidbody>().AddForce((target.position + (targetVelocityVec * 0.3f * targetTimeToFuturePos)) - transform.position, ForceMode.Impulse);
            yield return new WaitForSeconds(reloadSpeed);
        }
        yield return new WaitForEndOfFrame();
        StartCoroutine(CheckIfCouldShoot());
    }
}

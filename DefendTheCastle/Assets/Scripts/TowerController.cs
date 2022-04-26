using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public Transform target;

    private Vector3 previousTargetPos;
    private float targetVelocity;
    private Vector3 targetVelocityVec;
    private float targetTimeToFuturePos;

    public GameObject cannonBallPrefab;


    private void Start()
    {
        previousTargetPos = target.position;   
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


        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject ballShot = Instantiate(cannonBallPrefab, transform.Find("rotater").Find("CannonBallSpawnPoint").position, transform.Find("rotater").rotation);

            ballShot.GetComponent<Rigidbody>().AddForce( (target.position + (targetVelocityVec * 0.3f *targetTimeToFuturePos)) - transform.position, ForceMode.Impulse);
        }
    }
}

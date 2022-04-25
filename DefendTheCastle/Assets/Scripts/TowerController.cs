using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        //calculate position
        var direction = target.position - transform.position; 
        //set y axis so it won't rotate
        direction.y = 0;
        //quaternion is rotation
        transform.Find("rotater").rotation = Quaternion.LookRotation(direction);

    }
}

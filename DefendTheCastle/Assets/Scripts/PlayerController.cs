using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    public float rotationSpeed = 10f;

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        transform.Translate(0, 0, translation * Time.deltaTime * speed);
        transform.Rotate(0, rotation * Time.deltaTime * rotationSpeed, 0);
    }

}

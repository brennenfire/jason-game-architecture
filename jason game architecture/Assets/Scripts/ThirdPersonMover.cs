using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMover : MonoBehaviour
{
    [SerializeField] float turnSpeed = 1000;
    [SerializeField] float moveSpeed = 5f;
    new Rigidbody rigidbody;

    void Update()
    {
        var mouseMovement = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseMovement * Time.deltaTime * turnSpeed, 0);
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var velocity = new Vector3(horizontalInput, 0, vertical);
        velocity *= moveSpeed * Time.fixedDeltaTime;
        var offset = transform.rotation * velocity;
        rigidbody.MovePosition(transform.position + offset);
    }
}

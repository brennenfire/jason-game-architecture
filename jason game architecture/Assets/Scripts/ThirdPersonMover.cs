using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ThirdPersonMover : MonoBehaviour
{
    [SerializeField] float turnSpeed = 1000;
    [SerializeField] float moveSpeed = 5f;
    float mouseMovement;
    new Rigidbody rigidbody;
    Animator animator;
    
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        mouseMovement = Input.GetAxis("Mouse X");
    }

    void Update()
    {
        mouseMovement += Input.GetAxis("Mouse X");
    }

    void FixedUpdate()
    {
        transform.Rotate(0, mouseMovement * Time.deltaTime * turnSpeed, 0);
        mouseMovement = 0f;
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if(Input.GetKey(KeyCode.LeftShift))
        {
            vertical *= 2f;
        }

        var velocity = new Vector3(horizontal, 0, vertical);
        velocity *= moveSpeed * Time.fixedDeltaTime;
        var offset = transform.rotation * velocity;
        rigidbody.MovePosition(transform.position + offset);

        animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
        animator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
    }
}

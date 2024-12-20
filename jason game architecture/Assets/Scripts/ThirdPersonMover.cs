using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ThirdPersonMover : MonoBehaviour
{
    [SerializeField] float turnSpeed = 1000;
    [SerializeField] StatType moveSpeed;
    [SerializeField] StatType energy;

    float mouseMovement;
    StatsManager statsManager;
    new Rigidbody rigidbody;
    Animator animator;
    void Awake()
    {
        statsManager = GetComponent<StatsManager>();
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
        //if(ToggleablePanel.AnyVisible) 
        //{
        //    animator.SetFloat("Vertical", 0f, 0.1f, Time.deltaTime);
        //    animator.SetFloat("Horizontal", 0f, 0.1f, Time.deltaTime);
        //    return;
        //}

        transform.Rotate(0, mouseMovement * Time.deltaTime * turnSpeed, 0);

        mouseMovement = 0f;
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if(Input.GetKey(KeyCode.LeftShift))
        {
            vertical *= 2f;
        }

        var velocity = new Vector3(horizontal, 0, vertical);
        velocity *= statsManager.GetStatValue(moveSpeed) * Time.fixedDeltaTime;
        var offset = transform.rotation * velocity;

        statsManager.Modify(energy, -offset.magnitude);

        rigidbody.MovePosition(transform.position + offset);
        if (animator != null)
        {
            animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
            animator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
        }

        
    }
}

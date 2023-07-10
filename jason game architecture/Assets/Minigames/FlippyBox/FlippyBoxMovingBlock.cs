using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippyBoxMovingBlock : MonoBehaviour
{
    Rigidbody2D rigidbody;
    [SerializeField] float moveSpeed;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();    
    }

    void FixedUpdate()
    {
        Vector2 movement;
        movement = Vector2.left * moveSpeed * Time.deltaTime;
        rigidbody.position += movement;
        if(rigidbody.position.x <= -15f)
        {
            rigidbody.position += new Vector2(30f, 0f);
        }
    }
}

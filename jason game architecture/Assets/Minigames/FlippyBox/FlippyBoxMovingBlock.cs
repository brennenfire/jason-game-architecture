using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippyBoxMovingBlock : MonoBehaviour, IRestart
{
    [SerializeField] float moveSpeed;
    new Rigidbody2D rigidbody;
    Vector3 startingPosition;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();    
        startingPosition = transform.position;
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

    public void Restart()
    {
        transform.position = startingPosition;
    }
}

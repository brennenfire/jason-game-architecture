using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippyBoxMovingBlock : MonoBehaviour, IRestart
{
    new Rigidbody2D rigidbody;
    Vector3 startingPosition;

    float MoveSpeed => FlippyBoxMinigamePanel.Instance.CurrentSettings.MovingBlockSpeed;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();    
        startingPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector2 movement;
        movement = Vector2.left * MoveSpeed * Time.deltaTime;
        rigidbody.position += movement;
        if(transform.localPosition.x <= -15f)
        {
            rigidbody.position += new Vector2(30f, 0f);
        }
    }

    public void Restart()
    {
        transform.position = startingPosition;
    }
}

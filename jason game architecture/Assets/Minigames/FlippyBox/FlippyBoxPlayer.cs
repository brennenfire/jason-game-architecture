using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippyBoxPlayer : MonoBehaviour
{
    [SerializeField] Vector2 jumpVelocity;
    Rigidbody2D rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            rigidbody.velocity = jumpVelocity;
        }
    }
}

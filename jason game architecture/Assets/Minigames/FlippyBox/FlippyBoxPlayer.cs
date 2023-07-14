using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class FlippyBoxPlayer : MonoBehaviour, IRestart
{
    [SerializeField] float spinSpeed = 50f;

    Vector2 JumpVelocity => FlippyBoxMinigamePanel.Instance.CurrentSettings.JumpVelocity;
    float GrowTime => FlippyBoxMinigamePanel.Instance.CurrentSettings.GrowTime;

    new Rigidbody2D rigidbody;
    Vector3 startingPosition;
    Quaternion startingRotation;
    float elapsed;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        startingRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            rigidbody.velocity = JumpVelocity;
        }

        transform.Rotate(0f, 0f, Time.deltaTime * spinSpeed);

        elapsed += Time.deltaTime;
        float size = Mathf.Lerp(1f, 2f, elapsed / GrowTime);
        transform.localScale = new Vector3(size, size, size);
    }

    public void Restart()
    {
        transform.position = startingPosition;
        transform.rotation = startingRotation;
        elapsed = 0f;
        transform.localScale = Vector3.one;
        GetComponent<SpriteRenderer>().color = FlippyBoxMinigamePanel.Instance.CurrentSettings.PlayerColor;
    }
}

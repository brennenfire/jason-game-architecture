using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippyBoxWinCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            FindObjectOfType<FlippyBoxMinigamePanel>().Win();
        }
    }
}

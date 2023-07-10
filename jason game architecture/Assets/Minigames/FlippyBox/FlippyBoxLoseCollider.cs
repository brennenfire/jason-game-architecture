using UnityEngine;

public class FlippyBoxLoseCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Lost");
        }
    }
}

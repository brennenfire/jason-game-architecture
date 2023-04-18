using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGiver : MonoBehaviour
{
    [SerializeField] GameEvent doorEvent;

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<ThirdPersonMover>();
        if (player != null)
        {
            doorEvent.Invoke();
        }
    }
}

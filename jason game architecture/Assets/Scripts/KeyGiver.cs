using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGiver : MonoBehaviour
{
    [SerializeField] GameEvent doorEvent;
    bool activated;

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<ThirdPersonMover>();
        if (player != null && activated)
        {
            doorEvent.Invoke();
        }
    }

    public void Activate()
    {
        activated = true;
    }
}

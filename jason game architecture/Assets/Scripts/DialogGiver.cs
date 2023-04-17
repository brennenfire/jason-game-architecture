using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogGiver : MonoBehaviour
{
    [SerializeField] TextAsset dialog;
    [SerializeField] Canvas canvas;

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<ThirdPersonMover>();
        if (player != null)
        {
            FindObjectOfType<DialogController>().StartDialog(dialog);
            transform.LookAt(player.transform.position);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlagTriggerArea : MonoBehaviour
{
    [SerializeField] BoolGameFlag gameFlag;

    private void OnTriggerEnter(Collider other)
    {
        gameFlag.Set(true);
    }
}

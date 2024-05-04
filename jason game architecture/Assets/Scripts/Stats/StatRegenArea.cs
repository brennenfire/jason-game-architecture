using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatRegenArea : MonoBehaviour
{
    [SerializeField] StatType stat;
    [SerializeField] float amountPerSecond = 1f;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        var amount = Time.deltaTime * amountPerSecond;
        StatsManager.Instance.Modify(stat, amount);
    }
}

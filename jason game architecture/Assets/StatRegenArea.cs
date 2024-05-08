using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatRegenArea : MonoBehaviour
{
    [SerializeField] StatType energyStat;
    [SerializeField] float amountPerSecond = 2f;

    void OnTriggerStay(Collider other)
    {
        if(!other.CompareTag("Player"))
        {
            return;
        }

        var amount = Time.deltaTime * amountPerSecond;
        StatsManager.Instance.Modify(energyStat, amount);
    }
}

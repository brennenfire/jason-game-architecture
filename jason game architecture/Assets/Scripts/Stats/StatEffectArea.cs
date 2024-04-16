using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatEffectArea : MonoBehaviour
{
    [SerializeField] Stat stat;
    [SerializeField] int amount;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") == false)
        {
            return;
        }

        StatsManager.Instance.Modify(stat, amount);
    }

    void OnTriggerExit(Collider other)
    {
        StatsManager.Instance.Modify(stat, -amount);
    }
}

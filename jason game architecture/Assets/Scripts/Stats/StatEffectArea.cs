using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatEffectArea : MonoBehaviour
{
    [SerializeField] List<StatMod> statMods; 

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") == false)
        {
            return;
        }

        StatsManager.Instance.AddStatMods(statMods);
    }

    void OnTriggerExit(Collider other)
    {
        StatsManager.Instance.RemoveStatMods(statMods);
    }
}

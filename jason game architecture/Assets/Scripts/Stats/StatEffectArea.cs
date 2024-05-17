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

        var statsManager = other.GetComponent<StatsManager>();
        if(statsManager != null) 
        {
            statsManager.AddStatMods(statMods);
        }
    }

    void OnTriggerExit(Collider other)
    {
        var statsManager = other.GetComponent<StatsManager>();
        if(statsManager != null) 
        {
            statsManager.RemoveStatMods(statMods);
        }
    }
}

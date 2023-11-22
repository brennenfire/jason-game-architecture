using System;
using UnityEngine;

public class InteractableStatCondition : MonoBehaviour, IMet
{
    [SerializeField] int requiredStatValue;
    [SerializeField] Stat requiredStat;

    public bool Met()
    {
        int statValue = StatsManager.Instance.GetStatValue(requiredStat);
        return statValue >= requiredStatValue;
    }
}

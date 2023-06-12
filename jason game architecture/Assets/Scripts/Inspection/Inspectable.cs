using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inspectable : MonoBehaviour
{
    public static event Action<bool> InspectablesInRangeChanged;
    static HashSet<Inspectable> inspectablesInRange = new HashSet<Inspectable>();

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inspectablesInRange.Add(this);
            InspectablesInRangeChanged?.Invoke(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inspectablesInRange.Remove(this);
            InspectablesInRangeChanged?.Invoke(inspectablesInRange.Any());
        }
    }
}

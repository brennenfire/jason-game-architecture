using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inspectable : MonoBehaviour
{
    public static event Action<bool> InspectablesInRangeChanged;

    static HashSet<Inspectable> inspectablesInRange = new HashSet<Inspectable>();
    
    float timeInspected;
    [SerializeField] float timeToInspect = 3f;

    public static IReadOnlyCollection<Inspectable> InspectablesInRange => inspectablesInRange;

    public float InspectionProgress => timeInspected / timeToInspect;

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

    public void Inspect()
    {
        timeInspected += Time.deltaTime;
        if(timeInspected >= timeToInspect) 
        {
            CompleteInspection();
        }
    }

    void CompleteInspection()
    {
        gameObject.SetActive(false);
        inspectablesInRange.Remove(this);
        InspectablesInRangeChanged?.Invoke(inspectablesInRange.Any());
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inspectable : MonoBehaviour
{
    public static event Action<bool> InspectablesInRangeChanged;

    static HashSet<Inspectable> inspectablesInRange = new HashSet<Inspectable>();
    
    [SerializeField] float timeToInspect = 3f;
    [SerializeField] UnityEvent OnInspectionCompleted;

    InspectableData data;

    public static IReadOnlyCollection<Inspectable> InspectablesInRange => inspectablesInRange;

    public float InspectionProgress => data.TimeInspected / timeToInspect;

    public bool WasFullyInspected => InspectionProgress >= 1;
    
    public void Bind(InspectableData inspectableData)
    {
        data = inspectableData;
        if(data.TimeInspected> timeToInspect) 
        {
            CompleteInspection();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && WasFullyInspected == false)
        {
            inspectablesInRange.Add(this);
            InspectablesInRangeChanged?.Invoke(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (inspectablesInRange.Remove(this))
            {
                InspectablesInRangeChanged?.Invoke(inspectablesInRange.Any());
            }
        }
    }

    public void Inspect()
    {
        if(WasFullyInspected)
        {
            return;
        }

        data.TimeInspected += Time.deltaTime;
        if(data.TimeInspected >= timeToInspect) 
        {
            CompleteInspection();
        }
    }

    void CompleteInspection()
    {
        inspectablesInRange.Remove(this);
        InspectablesInRangeChanged?.Invoke(inspectablesInRange.Any());
        OnInspectionCompleted?.Invoke();
    }
}

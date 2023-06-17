using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inspectable : MonoBehaviour
{
    public static event Action<bool> InspectablesInRangeChanged;

    static HashSet<Inspectable> inspectablesInRange = new HashSet<Inspectable>();
    
    float timeInspected;
    [SerializeField] float timeToInspect = 3f;
    [SerializeField] UnityEvent OnInspectionCompleted;

    public static IReadOnlyCollection<Inspectable> InspectablesInRange => inspectablesInRange;

    public float InspectionProgress => timeInspected / timeToInspect;

    public bool WasFullyInspected { get; private set; }

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
        timeInspected += Time.deltaTime;
        if(timeInspected >= timeToInspect) 
        {
            CompleteInspection();
        }
    }

    void CompleteInspection()
    {
        WasFullyInspected = true;
        inspectablesInRange.Remove(this);
        InspectablesInRangeChanged?.Invoke(inspectablesInRange.Any());
        OnInspectionCompleted?.Invoke();
    }
}

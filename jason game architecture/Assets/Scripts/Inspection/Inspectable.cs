using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inspectable : MonoBehaviour
{
    public static event Action<bool> InspectablesInRangeChanged;
    public static event Action<Inspectable, string> AnyInspectionComplete;

    static HashSet<Inspectable> inspectablesInRange = new HashSet<Inspectable>();

    [SerializeField] float timeToInspect = 3f;
    [SerializeField, TextArea] string completedInspectionText;
    [SerializeField] UnityEvent OnInspectionCompleted;
    
    InspectableData data;
    IMet[] allConditions;

    public static IReadOnlyCollection<Inspectable> InspectablesInRange => inspectablesInRange;

    public float InspectionProgress => data?.TimeInspected ?? 0f / timeToInspect;

    public bool WasFullyInspected => InspectionProgress >= 1;

    public bool MeetsConditions()
    {
        foreach(var condition in allConditions) 
        {
            if(condition.Met() == false)
            {
                return false;
            }

        }

        return true;
    }

    void Awake()
    {
        allConditions = GetComponents<IMet>();    
    }

    public void Bind(InspectableData inspectableData)
    {
        data = inspectableData;
        if(WasFullyInspected) 
        {
            CompleteInspection();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && WasFullyInspected == false && MeetsConditions() == true)
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
        if(WasFullyInspected) 
        {
            CompleteInspection();
        }
    }

    void CompleteInspection()
    {
        inspectablesInRange.Remove(this);
        InspectablesInRangeChanged?.Invoke(inspectablesInRange.Any());
        OnInspectionCompleted?.Invoke();
        AnyInspectionComplete?.Invoke(this, completedInspectionText);
    }
}

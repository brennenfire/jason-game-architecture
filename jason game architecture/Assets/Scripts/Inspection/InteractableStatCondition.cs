using System;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractableStatCondition : MonoBehaviour, IMet
{
    [SerializeField] int requiredStatValue;
    [SerializeField] Stat requiredStat;
    [SerializeField] bool skillupOnInteractionComplete = true;
    Interactable interactable;
    
    void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.InteractionCompleted += HandleInteractionCompleted;
    }

    void OnDestroy()
    {
        interactable.InteractionCompleted -= HandleInteractionCompleted;
    }

    private void HandleInteractionCompleted()
    {
        if(skillupOnInteractionComplete)
        {
            StatsManager.Instance.Modify(requiredStat, 1);
        }
    }

    public bool Met()
    {
        int statValue = StatsManager.Instance.GetStatValue(requiredStat);
        return statValue >= requiredStatValue;
    }
}

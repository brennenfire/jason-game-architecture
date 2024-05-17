using System;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractableStatCondition : MonoBehaviour, IMet
{
    [SerializeField] int requiredStatValue;
    [SerializeField] StatType requiredStat;
    [SerializeField] bool skillupOnInteractionComplete = true;
    Interactable interactable;

    public string NotMetMessage { get; private set; }

    public string MetMessage { get; private set; }

    void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.InteractionCompleted += HandleInteractionCompleted;
        NotMetMessage = $"<color=red>{requiredStat.name} ({requiredStatValue})</color>";
        MetMessage = $"<color=green>{requiredStat.name} ({requiredStatValue})</color>";
    }

    void OnDestroy()
    {
        interactable.InteractionCompleted -= HandleInteractionCompleted;
    }

    void HandleInteractionCompleted()
    {
        if(skillupOnInteractionComplete)
        {
            //StatsManager.Instance.Modify(requiredStat, 1);
        }
    }

    public bool Met()
    {
        return true;
        //var statValue = StatsManager.Instance.GetStatValue(requiredStat);
        //return statValue >= requiredStatValue;
    }
}

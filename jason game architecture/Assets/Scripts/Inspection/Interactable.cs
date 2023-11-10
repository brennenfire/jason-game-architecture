using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public static event Action<bool> InteractablesInRangeChanged;
    public static event Action<Interactable, string> AnyInteractionComplete;

    static HashSet<Interactable> interactablesInRange = new HashSet<Interactable>();

    [SerializeField] InteractionType interactionType;
    [SerializeField] float timeToInteract = 3f;
    [SerializeField] UnityEvent OnInteractionCompleted;
    [SerializeField] bool requireMinigame = false;
    [SerializeField] MinigameSettings minigameSettings;

    InteractableData data;
    IMet[] allConditions;

    public InteractionType InteractionType => interactionType;

    //public KeyCode Hotkey => interactionType.Hotkey;

    public static IReadOnlyCollection<Interactable> InteractablesInRange => interactablesInRange;

    public float InteractionProgress => data?.TimeInteracted ?? 0f / timeToInteract;

    public bool WasFullyInteracted => InteractionProgress >= 1;
     
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

    IEnumerator Start()
    {
        yield return null;
        if(data == null)
        {
            InteractionManager.Bind(this);
        }
    }

    public void Bind(InteractableData interactableData)
    {
        data = interactableData;
        if(WasFullyInteracted) 
        {
            RestoreInteractionText();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && WasFullyInteracted == false && MeetsConditions() == true)
        {
            interactablesInRange.Add(this);
            InteractablesInRangeChanged?.Invoke(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactablesInRange.Remove(this))
            {
                InteractablesInRangeChanged?.Invoke(interactablesInRange.Any());
            }
        }
    }

    public void Interact()
    {
        if(WasFullyInteracted)
        {
            return;
        }

        data.TimeInteracted += Time.deltaTime;

        if(WasFullyInteracted) 
        {
            if (requireMinigame)
            {
                interactablesInRange.Remove(this);
                InteractablesInRangeChanged?.Invoke(interactablesInRange.Any());
                MinigameManager.Instance.StartMinigame(minigameSettings ,HandleMinigameCompleted);
            }
            else
            {
                CompleteInteraction();
            }
        }
    }

    void HandleMinigameCompleted(MinigameResult result)
    {
        if(result == MinigameResult.Won)
        {
            CompleteInteraction();
        }
        else if(result == MinigameResult.Lost) 
        {
            interactablesInRange.Add(this);
            InteractablesInRangeChanged?.Invoke(interactablesInRange.Any());
            data.TimeInteracted = 0f;
        }
    }

    void CompleteInteraction()
    {
        interactablesInRange.Remove(this);
        InteractablesInRangeChanged?.Invoke(interactablesInRange.Any());
        OnInteractionCompleted?.Invoke();
        AnyInteractionComplete?.Invoke(this, interactionType.CompletedInteraction);
    }

    void RestoreInteractionText()
    {
        interactablesInRange.Remove(this);
        InteractablesInRangeChanged?.Invoke(interactablesInRange.Any());
        OnInteractionCompleted?.Invoke();
    }
}

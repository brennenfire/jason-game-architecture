using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    static Interactable currentInteractable;
    static List<InteractableData> localDatas;

    public static float InteractionProgress => currentInteractable?.InteractionProgress ?? 0f;
    public static bool Interacting { get; private set; }

    public event Action<Interactable> CurrentInteractableChanged;

    void Awake()
    {
        Interactable.InteractablesInRangeChanged += HandleInteractablesInRangeChanged;    
    }

    void OnDestroy()
    {
        Interactable.InteractablesInRangeChanged -= HandleInteractablesInRangeChanged;
    }

    void HandleInteractablesInRangeChanged(bool obj)
    {
        var nearest = Interactable.InteractablesInRange.
            OrderBy(t => Vector3.Distance(t.transform.position, transform.position)).
            FirstOrDefault();

        currentInteractable = nearest;
        CurrentInteractableChanged.Invoke(currentInteractable);
    }

    void Update()
    {
        if(currentInteractable != null && Input.GetKey(currentInteractable.InteractionType.Hotkey))
        {
            currentInteractable.Interact();
            Interacting = true;
        }
        else
        {
            Interacting = false;
        }
    }

    public static void Bind(List<InteractableData> datas)
    {
        localDatas = datas;
        var allInteractables = GameObject.FindObjectsOfType<Interactable>(true);
        foreach(var interactable in allInteractables)
        {
            Bind(interactable);
        }
    }

    public static void Bind(Interactable interactable)
    {
        var data = localDatas.FirstOrDefault(t => t.Name == interactable.name);
        if (data == null)
        {
            data = new InteractableData() { Name = interactable.name };
            localDatas.Add(data);
        }
        interactable.Bind(data);
    }
}

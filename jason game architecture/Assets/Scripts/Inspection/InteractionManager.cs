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
    public static bool Interaction => currentInteractable != null && currentInteractable.WasFullyInteracted == false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable = Interactable.InteractablesInRange.FirstOrDefault();
        }
        if(Input.GetKey(KeyCode.E) && currentInteractable != null) 
        {
            currentInteractable.Interact();
        }
        else
        {
            currentInteractable = null;
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

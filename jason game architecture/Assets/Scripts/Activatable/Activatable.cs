using System;
using UnityEngine;
using UnityEngine.Events;

public class Activatable : MonoBehaviour
{
    [SerializeField] UnityEvent onActivated;
    [SerializeField] UnityEvent onDeactivated;

    bool activated;

    [ContextMenu("Toggle")]
    public void Toggle()
    {
        activated = !activated;
        if(activated) 
        {
            onActivated.Invoke();
        }
        else
        {
            onDeactivated.Invoke();
        }
    }

    internal float CompareTag(object activatableTag)
    {
        throw new NotImplementedException();
    }
}

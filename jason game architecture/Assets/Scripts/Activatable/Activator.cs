using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField] ActivatorMode mode;

    [SerializeField] float radius = 10f;
    [SerializeField] string activatableTag;

    public void Activate()
    {
        var activatables = FindObjectsOfType<Activatable>()
            .Where(t => t.CompareTag(activatableTag));

        switch (mode)
        {
            case ActivatorMode.Nearest:
                activatables = activatables
                .OrderBy(t => Vector3.Distance(t.transform.position, transform.position))
                .Take(1);
                break;
            case ActivatorMode.All:
                break;
            case ActivatorMode.AllInRadius:
                activatables = activatables
                .Where(t => Vector3.Distance(t.transform.position, transform.position) <= radius);
                break;
            default:
                break;
        }

        foreach (var activatable in activatables)
        {
            activatable.Toggle();
        }
    }
}

public enum ActivatorMode
{
    Nearest,
    All,
    AllInRadius
}
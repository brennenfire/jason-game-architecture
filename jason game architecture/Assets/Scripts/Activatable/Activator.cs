using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField] float radius = 10f;
    [SerializeField] string activatableTag;

    public void Activate()
    {
        var allActivatablesMatchingTag = FindObjectsOfType<Activatable>()
            .Where(t => t.CompareTag(activatableTag));

        var allActivatablesInRange = allActivatablesMatchingTag
            .Where(t => Vector3.Distance(t.transform.position, transform.position) <= radius);
        foreach (var activatable in allActivatablesInRange)
        {
            activatable.Toggle();
        }
    }
}

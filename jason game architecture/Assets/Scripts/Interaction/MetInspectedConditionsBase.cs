﻿using UnityEngine;

internal class MetInspectedConditionsBase : MonoBehaviour, IMet
{
    [SerializeField] Interactable requiredInspectable;

    public string NotMetMessage { get; }
    public string MetMessage { get; }

    public bool Met()
    {
        return requiredInspectable.WasFullyInteracted;
    }

    void OnDrawGizmos()
    {
        if (requiredInspectable != null)
        {
            Gizmos.color = Met() ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, requiredInspectable.transform.position);
        }
    }
}
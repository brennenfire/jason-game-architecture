using System;
using UnityEngine;

internal class MetInspectedConditions : MonoBehaviour
{
    [SerializeField] Inspectable requiredInspectable;
    internal bool Met()
    {
        return requiredInspectable.WasFullyInspected;
    }
}
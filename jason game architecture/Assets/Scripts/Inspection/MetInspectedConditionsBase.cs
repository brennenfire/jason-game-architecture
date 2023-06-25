using UnityEngine;

internal class MetInspectedConditionsBase : MonoBehaviour, IMet
{
    [SerializeField] Inspectable requiredInspectable;

    public bool Met()
    {
        return requiredInspectable.WasFullyInspected;
    }
}
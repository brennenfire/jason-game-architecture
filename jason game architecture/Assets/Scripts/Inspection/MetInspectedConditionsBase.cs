using UnityEngine;

internal class MetInspectedConditionsBase : MonoBehaviour, IMet
{
    [SerializeField] Inspectable requiredInspectable;

    public bool Met()
    {
        return requiredInspectable.WasFullyInspected;
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
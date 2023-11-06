using UnityEngine;

internal class MetInspectedConditionsBase : MonoBehaviour, IMet
{
    [SerializeField] Interactable requiredInspectable;

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
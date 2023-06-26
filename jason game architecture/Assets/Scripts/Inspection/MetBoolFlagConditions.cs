using UnityEngine;

internal class MetBoolFlagConditions : MonoBehaviour, IMet
{
    [SerializeField] BoolGameFlag requiredFlag;

    public bool Met()
    {
        return requiredFlag.Value;
    }
}

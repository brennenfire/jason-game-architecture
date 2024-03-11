using UnityEngine;

internal class MetBoolFlagConditions : MonoBehaviour, IMet
{
    [SerializeField] BoolGameFlag requiredFlag;

    public string NotMetMessage => $"<color=red>{requiredFlag.name} </color>";

    public bool Met()
    {
        return requiredFlag.Value;
    }
}

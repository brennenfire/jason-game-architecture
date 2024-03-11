using UnityEngine;

public class MetDecimalFlagCondition : MonoBehaviour, IMet
{
    [SerializeField] DecimalGameFlag requiredFlag;
    [SerializeField] decimal requiredValue;

    public string NotMetMessage => $"<color=red>{requiredFlag.name} ({requiredValue})</color>";

    public bool Met()
    {
        return requiredFlag.Value >= requiredValue;
    }
}
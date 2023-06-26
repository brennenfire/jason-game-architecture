using UnityEngine;

public class MetDecimalFlagCondition : MonoBehaviour, IMet
{
    [SerializeField] DecimalGameFlag requiredFlag;
    [SerializeField] decimal requiredValue;

    public bool Met()
    {
        return requiredFlag.Value >= requiredValue;
    }
}
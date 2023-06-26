using UnityEngine;

public class MetIntFlagCondition : MonoBehaviour, IMet
{
    [SerializeField] IntGameFlag requiredFlag;
    [SerializeField] int requiredValue;

    public bool Met()
    {
        return requiredFlag.Value >= requiredValue;
    }

}
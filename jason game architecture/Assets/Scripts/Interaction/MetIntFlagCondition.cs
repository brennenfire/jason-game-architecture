using UnityEngine;

public class MetIntFlagCondition : MonoBehaviour, IMet
{
    [SerializeField] IntGameFlag requiredFlag;
    [SerializeField] int requiredValue;

    public string NotMetMessage => $"<color=red>{requiredFlag.name}</color>";
    public string MetMessage { get; }

    public bool Met()
    {
        return requiredFlag.Value >= requiredValue;
    }

}
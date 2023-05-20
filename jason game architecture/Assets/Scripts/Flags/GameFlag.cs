using System;
using UnityEngine;
using Unity.VisualScripting;

public abstract class GameFlag : ScriptableObject
{
    public event Action Changed;

    protected void SendChanged() => Changed?.Invoke();
}

public abstract class GameFlag<T> : GameFlag
{
    public T Value { get; protected set; }

    void OnEnable()
    {
        Value = default;    
    }

    void OnDisable()
    {
        Value = default;
    }

    public void Set(T value)
    {
        Value = value;
        SendChanged();
    }
}

[Serializable]
public class GameFlagData
{
    public string Name;
    public string Value;
}
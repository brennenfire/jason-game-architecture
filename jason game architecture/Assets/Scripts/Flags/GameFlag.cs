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
}
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Counted Int Game Flag")]
public class IntGameFlag : ScriptableObject
{
    //public static event Action AnyChanged;
    public event Action Changed;

    public int Value { get; private set; }

    void OnEnable()
    {
        Value = default;
    }

    private void OnDisable()
    {
        Value = default;
    }

    public void Modify(int value)
    {
        Value += value;
        Changed?.Invoke();
    }
}

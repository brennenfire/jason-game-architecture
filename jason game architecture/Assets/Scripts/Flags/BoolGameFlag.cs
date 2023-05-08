using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bool Game Flag")]
public class BoolGameFlag : ScriptableObject
{
    //public static event Action AnyChanged;
    public event Action Changed;

    public bool Value { get; private set; }

    void OnEnable()
    {
        Value = default;
    }

    private void OnDisable()
    {
        Value = default;
    }

    public void Set(bool value)
    {
        Value = value;
        //AnyChanged?.Invoke();
        Changed?.Invoke();
    }
}

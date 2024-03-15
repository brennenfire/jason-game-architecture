using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueKey : MonoBehaviour
{
    public string Key;

    void OnValidate()
    {
        if(string.IsNullOrWhiteSpace(Key))
        {
            Key = Guid.NewGuid().ToString();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueKey : MonoBehaviour
{
    static Dictionary<string, UniqueKey> usedIs = new Dictionary<string, UniqueKey>();

    public string Id;

    void OnValidate()
    {
        if(string.IsNullOrWhiteSpace(Id))
        {
            Id = Guid.NewGuid().ToString();
        }
        while(usedIs.TryGetValue(Id, out var usedKey))
        {
            if(usedKey == this) 
            {
                return;
            }
            Id = Guid.NewGuid().ToString();
        }
        usedIs.Add(Id, this);
    }
}

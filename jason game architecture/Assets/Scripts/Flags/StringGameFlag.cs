using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Flag/String Game Flag")]
public class StringGameFlag : GameFlag<string>
{
    protected override void SetFromData(string value)
    {
        Set(value); 
    }
}
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Flag/String Game Flag")]
public class StringGameFlag : GameFlag<string>
{
    public void Modify(string value)
    {
        Value = value;
        SendChanged();
    }
}
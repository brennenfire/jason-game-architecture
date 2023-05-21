using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Flag/Decimal Game Flag")]
public class DecimalGameFlag : GameFlag<decimal>
{
    protected override void SetFromData(string value)
    {
        if(decimal.TryParse(value, out var decimalValue))
        {
            Set(decimalValue);
        }
    }
}
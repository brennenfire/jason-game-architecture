using UnityEngine;

[CreateAssetMenu(menuName = "Decimal Game Flag")]
public class DecimalGameFlag : GameFlag<decimal>
{
    public void Modify(decimal value)
    {
        Value += value;
        SendChanged();
    }
}
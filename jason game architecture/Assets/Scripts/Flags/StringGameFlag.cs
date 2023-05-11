using UnityEngine;

[CreateAssetMenu(menuName = "Counted Int Game Flag")]
public class StringGameFlag : GameFlag<string>
{
    public void Modify(string value)
    {
        Value = value;
        SendChanged();
    }
}
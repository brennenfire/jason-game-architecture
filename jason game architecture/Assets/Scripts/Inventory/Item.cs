using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public Sprite Icon;
    public string Name;

    [Multiline(3)]
    public string Description;
}

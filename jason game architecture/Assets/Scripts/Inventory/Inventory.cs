using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> Items = new List<Item>();
    [SerializeField] Item debugItem;

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    [ContextMenu(nameof(AddDebugItem))]
    void AddDebugItem()
    {
        AddItem(debugItem);
    }
}

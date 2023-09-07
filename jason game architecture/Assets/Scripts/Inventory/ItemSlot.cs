using System;
using System.Diagnostics;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public event Action Changed;

    public Item Item;
    SlotData slotDataLocal;

    public bool IsEmpty => Item == null;

    public void SetItem(Item item)
    {
        var previousItem = Item;
        Item = item;
        slotDataLocal.ItemName = item?.name ?? string.Empty;
        
        if (previousItem != item)
        {
            Changed?.Invoke();
        }
    }

    public void Bind(SlotData slotData)
    {
        slotDataLocal = slotData;
        var item = Resources.Load<Item>("Items/" + slotDataLocal.ItemName);
        SetItem(item);
    }
}


[Serializable]
public class SlotData
{
    public string SlotName;
    public string ItemName;
}
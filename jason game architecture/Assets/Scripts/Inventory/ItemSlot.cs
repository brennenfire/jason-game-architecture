using System;
using System.Diagnostics;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public Item Item;
    SlotData slotDataLocal;

    public bool isEmpty => Item == null;

    public void SetItem(Item item)
    {
        Item = item;
        slotDataLocal.ItemName = item?.name ?? string.Empty;
    }

    public void Bind(SlotData slotData)
    {
        slotDataLocal = slotData;
        UnityEngine.Debug.LogError($"Attempted to load item {slotDataLocal.ItemName}");
        //SetItem();
    }
}


[Serializable]
public class SlotData
{
    public string SlotName;
    public string ItemName;
}
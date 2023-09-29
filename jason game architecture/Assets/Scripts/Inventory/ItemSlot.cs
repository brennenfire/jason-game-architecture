using System;
using System.Diagnostics;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public event Action Changed;

    public Item Item;
    public SlotData slotDataLocal;

    public bool IsEmpty => Item == null;

    public bool HasStackSpaceAvailable => slotDataLocal.StackCount < Item.MaxStackSize;

    public void SetItem(Item item)
    {
        var previousItem = Item;
        Item = item;
        slotDataLocal.ItemName = item?.name ?? string.Empty;
        slotDataLocal.StackCount = 1;
        
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

    public void Swap(ItemSlot slotToSwap)
    {
        var itemOtherSlot = slotToSwap.Item;
        slotToSwap.SetItem(Item);
        SetItem(itemOtherSlot);
    }

    public void RemoveItem()
    {
        SetItem(null);
    }

    internal void ModifyStack(int amount)
    {
        slotDataLocal.StackCount += amount;
        Changed?.Invoke();
    }
}


[Serializable]
public class SlotData
{
    public string SlotName;
    public string ItemName;
    public int StackCount;
}
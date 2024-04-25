using System;
using System.Diagnostics;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public event Action<Item, Item> Changed;

    public readonly EquipmentSlotType EquipmentSlotType;
    public Item Item;
    SlotData slotDataLocal;

    public ItemSlot()
    {

    }

    public ItemSlot(EquipmentSlotType equipmentSlotType)
    {
        EquipmentSlotType = equipmentSlotType;
    }

    public bool IsEmpty => Item == null;

    public bool HasStackSpaceAvailable => slotDataLocal.StackCount < Item.MaxStackSize;

    public int StackCount => slotDataLocal.StackCount;

    public int AvailableStackSpace => Item != null ? Item.MaxStackSize - slotDataLocal.StackCount : 0;

    public void SetItem(Item item, int stackCount = 1)
    {
        var previousItem = Item; 
        Item = item;
        slotDataLocal.ItemName = item?.name ?? string.Empty;
        slotDataLocal.StackCount = stackCount;

        Changed?.Invoke(Item, previousItem);
        
    }

    public void Bind(SlotData slotData)
    {
        var previousItem = Item;
        slotDataLocal = slotData;
        var item = Resources.Load<Item>("Items/" + slotDataLocal.ItemName);
        Item = item;
        Changed?.Invoke(Item, previousItem);
    }

    public void Swap(ItemSlot slotToSwap)
    {
        var itemOtherSlot = slotToSwap.Item;
        int stackCountInOtherSlot = slotToSwap.StackCount;
        slotToSwap.SetItem(Item, StackCount);
        SetItem(itemOtherSlot, stackCountInOtherSlot);
    }

    public void RemoveItem()
    {
        SetItem(null);
    }

    internal void ModifyStack(int amount)
    {
        slotDataLocal.StackCount += amount;
        if (slotDataLocal.StackCount <= 0)
        {
            SetItem(null);
        }
        else
        {
            Changed?.Invoke(Item, Item);
        }
    }

    public bool CanHold(Item item)
    {
        if(item == null) 
            return true;

        if(EquipmentSlotType != null && item.EquipmentSlotType != EquipmentSlotType)
        {
            return false;
        }

        return true;
    }
}


[Serializable]
public class SlotData
{
    public string SlotName;
    public string ItemName;
    public int StackCount;
}
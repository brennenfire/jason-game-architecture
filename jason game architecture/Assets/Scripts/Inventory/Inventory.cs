using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    const int GENERAL_SIZE = 9;
    const int CRAFTING_SIZE = 9;

    public ItemSlot[] GeneralSlots = new ItemSlot[GENERAL_SIZE];
    public ItemSlot[] CraftingSlots = new ItemSlot[CRAFTING_SIZE];
    public List<ItemSlot> OverflowSlots = new List<ItemSlot>();   

    [SerializeField] Item debugItem;
    List<SlotData> localSlotDatas;

    public static Inventory Instance { get; private set; }
    public ItemSlot TopOverflowSlot => OverflowSlots?.FirstOrDefault();

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < GENERAL_SIZE; i++)
        {
            GeneralSlots[i] = new ItemSlot();
        }
        for (int i = 0; i < CRAFTING_SIZE; i++)
        {
            CraftingSlots[i] = new ItemSlot();
        }
    }

    public void AddItem(Item item, InventoryType preferredInventoryType = InventoryType.General)
    {
        var preferredSlots = preferredInventoryType == InventoryType.General ? GeneralSlots : CraftingSlots;
        var backupSlots = preferredInventoryType == InventoryType.General ? CraftingSlots : GeneralSlots;


        if (AddItemToSlots(item, preferredSlots))
        {
            return;
        }

        if (AddItemToSlots(item, backupSlots))
        {
            return;
        }

        if (AddItemToSlots(item, OverflowSlots))
        {
            CreateOverflowSlot();
        }

        else
        {
            Debug.LogError($"unable to add {item}");
        }

        /*
        if (firstAvailableSlot == null)
        {
            firstAvailableSlot = OverflowSlots.Last();
            CreateOverflowSlot();
        }

        if (firstAvailableSlot != null)
        {
            firstAvailableSlot.SetItem(item);
        }
        else
        {
            Debug.LogError($"unable to add {item}");
        }
        */
    }

    bool AddItemToSlots(Item item, IEnumerable<ItemSlot> slots)
    {
        var stackableSlot = slots.FirstOrDefault(t => t.Item == item && t.HasStackSpaceAvailable);
        if(stackableSlot != null)
        {
            stackableSlot.ModifyStack(1);
            return true;
        }

        var slot = slots.FirstOrDefault(t => t.IsEmpty);
        if (slot != null)
        {
            slot.SetItem(item);
            return true;
        }

        return false;
    }

    [ContextMenu(nameof(AddDebugItem))]
    void AddDebugItem()
    {
        AddItem(debugItem);
    }

    [ContextMenu(nameof(MoveItemsRight))]
    void MoveItemsRight()
    {
        var lastItemSlot = GeneralSlots.Last().Item;
        for (int i = GENERAL_SIZE - 1; i > 0; i--)
        {
            GeneralSlots[i].SetItem(GeneralSlots[i - 1].Item);
        }

        GeneralSlots.First().SetItem(lastItemSlot);
    }

    public void Bind(List<SlotData> slotDatas)
    {
        localSlotDatas = slotDatas;
        CreateOverflowSlot();
        TopOverflowSlot.Changed += () =>
        {
            if (TopOverflowSlot.IsEmpty && OverflowSlots.Any(t => t.IsEmpty == false))
            {
                MoveOverflowItemsUp();
            }
        };

        for (int i = 0; i < GeneralSlots.Length; i++)
        {
            var slot = GeneralSlots[i];
            var slotData = slotDatas.FirstOrDefault(t => t.SlotName == "General" + i);
            if (slotData == null)
            {
                slotData = new SlotData() { SlotName = "General" + i };
                slotDatas.Add(slotData);
            }

            slot.Bind(slotData);
        }

        for (int i = 0; i < CraftingSlots.Length; i++)
        {
            var slot = CraftingSlots[i];
            var slotData = slotDatas.FirstOrDefault(t => t.SlotName == "Crafting" + i);
            if (slotData == null)
            {
                slotData = new SlotData() { SlotName = "Crafting" + i };
                slotDatas.Add(slotData);
            }

            slot.Bind(slotData);
        }
    }

    void CreateOverflowSlot()
    {
        var overflowSlot = new ItemSlot();
        var overflowSlotData = new SlotData { SlotName = "Overflow" + OverflowSlots.Count };
        localSlotDatas.Add(overflowSlotData);
        overflowSlot.Bind(overflowSlotData);
        OverflowSlots.Add(overflowSlot);
    }

    public void ClearCraftingSlots()
    {
        foreach (var slot in CraftingSlots)
        {
            slot.RemoveItem();
        }
    }

    void MoveOverflowItemsUp()
    {
        for (int i = 0; i < OverflowSlots.Count - 1; i++)
        {
            var item = OverflowSlots[i + 1].Item;
            OverflowSlots[i].SetItem(item);
        }
        OverflowSlots.Last().RemoveItem();
    }

    public void Swap(ItemSlot sourceSlot, ItemSlot focusedSlot)
    {
        if (focusedSlot == TopOverflowSlot)
        {
            Debug.LogError("overflow slot cant :P");
        }
        else if (focusedSlot != null &&
                focusedSlot.IsEmpty &&
                Input.GetKey(KeyCode.C) &&
                sourceSlot.StackCount > 1)
        {
            focusedSlot.SetItem(sourceSlot.Item);
            sourceSlot.ModifyStack(-1);
        }
        else if (focusedSlot != null && focusedSlot.Item == sourceSlot.Item && focusedSlot.HasStackSpaceAvailable)
        {
            int numberToMove = Mathf.Min(focusedSlot.AvailableStackSpace, sourceSlot.StackCount);
            if(Input.GetKey(KeyCode.C) && numberToMove > 1) 
            {
                numberToMove = 1;
            }
            focusedSlot.ModifyStack(numberToMove);
            sourceSlot.ModifyStack(-numberToMove);
        }
        else
        {
            sourceSlot.Swap(focusedSlot);
        }
    }
}

public enum InventoryType
{
    General,
    Crafting
}
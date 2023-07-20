using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    const int GENERAL_SIZE = 9;

    public ItemSlot[] GeneralInventory = new ItemSlot[GENERAL_SIZE]; 

    [SerializeField] Item debugItem;

    void Awake()
    {
        for (int i = 0; i < 9; i++)
        {
            GeneralInventory[i] = new ItemSlot();
        }    
    }

    public void AddItem(Item item)
    {
        var firstAvailableSlot = GeneralInventory.FirstOrDefault(t => t.isEmpty);
        firstAvailableSlot.SetItem(item);
    }

    [ContextMenu(nameof(AddDebugItem))]
    void AddDebugItem()
    {
        AddItem(debugItem);
    }

    [ContextMenu(nameof(MoveItemsRight))]
    void MoveItemsRight()
    {
        var lastItemSlot = GeneralInventory.Last().Item;
        for (int i = GENERAL_SIZE - 1; i > 0; i--)
        {
            GeneralInventory[i].SetItem(GeneralInventory[i - 1].Item);
        }

        GeneralInventory.First().SetItem(lastItemSlot);
    }
    
}

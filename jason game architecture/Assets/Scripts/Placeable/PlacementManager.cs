using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public ItemSlot itemSlotLocal;

    public static PlacementManager Instance { get; private set; }

    public void BeginPlacement(ItemSlot itemSlot)
    {
        if(itemSlot == null || itemSlot.Item == null || itemSlot.Item.PlaceablePrefab == null)
        {
            Debug.LogError("unable to place");
            return;
        }

        itemSlotLocal = itemSlot;
        Debug.Log($"started placing {itemSlotLocal.Item}");

        var placeable = Instantiate(itemSlot.Item.PlaceablePrefab);
        placeable.transform.SetParent(transform);
    }

    void Awake()
    {
        Instance = this;        
    }
}

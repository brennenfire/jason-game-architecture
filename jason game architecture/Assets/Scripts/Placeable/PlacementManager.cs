using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;

    public ItemSlot itemSlotLocal;
    GameObject placeable;
    [SerializeField] float rotateSpeed = 500f;

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

        placeable = Instantiate(itemSlot.Item.PlaceablePrefab);
        placeable.transform.SetParent(transform);
    }

    void Awake()
    {
        Instance = this;        
    }

    void Update()
    {
        if(placeable == null)
        {
            return;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, float.MaxValue, layerMask, QueryTriggerInteraction.Ignore))
        {
            placeable.transform.position = hitInfo.point;
        }

        var rotation = Input.mouseScrollDelta.y * Time.deltaTime * rotateSpeed;
        placeable.transform.Rotate(0, rotation, 0);
    }
}

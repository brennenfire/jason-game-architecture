using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;

    public ItemSlot itemSlotLocal;
    Placeable placeable;

    [SerializeField] float rotateSpeed = 500f;
    [SerializeField] List<Placeable> allPlaceables;

    List<PlaceableData> localPlaceableDatas;
    

    public static PlacementManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

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
        var uniqueKey = placeable.GetComponent<UniqueKey>();
        uniqueKey.GenerateRuntimePlacedKey();
        placeable.transform.SetParent(transform);
    }

    void Update()
    {
        if(placeable == null)
        {
            return;
        }

        var rotation = Input.mouseScrollDelta.y * Time.deltaTime * rotateSpeed;
        placeable.transform.Rotate(0, rotation, 0);

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, float.MaxValue, layerMask, QueryTriggerInteraction.Ignore))
        {
            placeable.SetPosition(hitInfo.point);
            if(Input.GetMouseButtonDown(0) && placeable.IsPlacementValid)
            {
                FinishPlacement();
            }
        }
    }

    void FinishPlacement()
    {
        var uniqueKey = placeable.GetComponent<UniqueKey>();
        
        localPlaceableDatas.Add(new PlaceableData()
        {
            PlaceablePrefab = itemSlotLocal.Item.PlaceablePrefab.name,
            Position = placeable.transform.position,
            Rotation = placeable.transform.rotation,
            Key = uniqueKey.Id
        });

        placeable.Place();
        placeable = null;
        itemSlotLocal.RemoveItem();
        itemSlotLocal = null;
    }

    public void Bind(List<PlaceableData> placeablesDatas)
    {
        localPlaceableDatas = placeablesDatas;

        foreach (var placeableData in localPlaceableDatas)
        {
            var prefab = allPlaceables.FirstOrDefault(t => t.name == placeableData.PlaceablePrefab);
            if(prefab != null)
            {
                var placeable = Instantiate(prefab, placeableData.Position, placeableData.Rotation);
                if (placeable != null)
                {
                    placeable.Place();
                    placeable.GetComponent<UniqueKey>().Id = placeableData.Key;
                }
            }
            else
            {
                Debug.LogError($"unable respawn placeable item {placeableData.PlaceablePrefab}");
            }
        }
    }
}

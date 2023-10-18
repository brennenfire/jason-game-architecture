using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;

    public ItemSlot itemSlotLocal;
    GameObject placeable;

    [SerializeField] float rotateSpeed = 500f;
    [SerializeField] List<GameObject> allPlaceables;

    List<PlaceableData> localPlaceableDatas;
    

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

        var rotation = Input.mouseScrollDelta.y * Time.deltaTime * rotateSpeed;
        placeable.transform.Rotate(0, rotation, 0);

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, float.MaxValue, layerMask, QueryTriggerInteraction.Ignore))
        {
            placeable.transform.position = hitInfo.point;
            if(Input.GetMouseButtonDown(0))
            {
                FinishPlacement();
            }
        }
    }

    void FinishPlacement()
    {
        localPlaceableDatas.Add(new PlaceableData()
        {
            PlaceablePrefab = itemSlotLocal.Item.PlaceablePrefab.name,
            Position = placeable.transform.position,
            Rotation = placeable.transform.rotation
        });
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
                Instantiate(prefab, placeableData.Position, placeableData.Rotation);
            }
            else
            {
                Debug.LogError($"unable respawn placeable item {placeableData.PlaceablePrefab}");
            }
        }
    }
}

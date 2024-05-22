using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player ActivePlayer { get; private set; }

    [SerializeField] Inventory inventory;
    [SerializeField] Transform shoulders;
    [SerializeField] ThirdPersonMover mover;
    StatsManager statsManager;

    public Inventory Inventory => Inventory;

    public Transform Shoulders => shoulders;

    public StatsManager StatsManager => statsManager;

    void OnValidate()
    {
        mover = GetComponent<ThirdPersonMover>();
        statsManager = GetComponent<StatsManager>();
        inventory = GetComponent<Inventory>();
    }

    public void Bind(PlayerData playerData)
    {
        GetComponent<StatsManager>().Bind(playerData.StatDatas);
    }

    public void ToggleActive(bool state)
    {
        mover.enabled = state;
        if (state == true)
        {
            ActivePlayer = this;
            FindObjectOfType<StatsPanel>().Bind(statsManager);
            FindObjectOfType<CraftingPanel>().Bind(inventory);
            FindObjectOfType<EquipmentPanel>().Bind(inventory);
            FindObjectOfType<InventoryPanel>().Bind(inventory);
        }
    }
}

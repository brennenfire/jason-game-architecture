using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player ActivePlayer { get; private set; }   

    [SerializeField] Transform shoulders;
    [SerializeField] ThirdPersonMover mover;
    StatsManager statsManager;

    public Transform Shoulders => shoulders;

    public StatsManager StatsManager => statsManager;

    void OnValidate()
    {
        mover = GetComponent<ThirdPersonMover>();
        statsManager = GetComponent<StatsManager>();
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
            StatsPanel.FindObjectOfType<StatsPanel>().Bind(statsManager);
        }
    }
}

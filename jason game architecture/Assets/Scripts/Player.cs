using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform shoulders;
    public Transform Shoulders => shoulders;

    public void Bind(PlayerData playerData)
    {
        GetComponent<StatsManager>().Bind(playerData.StatDatas);
    }
}

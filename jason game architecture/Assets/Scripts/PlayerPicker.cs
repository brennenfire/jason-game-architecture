using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPicker : MonoBehaviour
{
    [SerializeField] List<Player> players;
    [SerializeField] KeyCode swapKey = KeyCode.Tab;
    [SerializeField] CinemachineVirtualCamera camera;
    int playerIndex;

    public void Initialize()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].ToggleActive(i == 0);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(swapKey))
        {
            ActivateNextPlayer();
        }
    }

    void ActivateNextPlayer()
    {
        players[playerIndex].ToggleActive(false);

        playerIndex++;
        if(playerIndex >= players.Count) 
        {
            playerIndex = 0;
        }
        var playerToActivate = players[playerIndex];
        playerToActivate.ToggleActive(true);
        camera.Follow = playerToActivate.Shoulders;
    }
}

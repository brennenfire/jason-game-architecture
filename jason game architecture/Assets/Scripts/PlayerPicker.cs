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

    void Update()
    {
        if(Input.GetKeyDown(swapKey))
        {
            ActivateNextPlayer();
        }
    }

    void ActivateNextPlayer()
    {
        players[playerIndex].GetComponent<ThirdPersonMover>().enabled = false;

        playerIndex++;
        if(playerIndex >= players.Count) 
        {
            playerIndex = 0;
        }
        var playerToActivate = players[playerIndex];
        playerToActivate.GetComponent<ThirdPersonMover>().enabled = true;
        camera.Follow = playerToActivate.Shoulders;
    }
}

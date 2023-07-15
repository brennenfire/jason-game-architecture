using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    Action completeInspectionLocal;

    public static MinigameManager Instance {get; private set;}

   
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y)) 
        {
            completeInspectionLocal?.Invoke();
            completeInspectionLocal = null;
        }
    }

    public void StartMinigame(MinigameSettings settings, Action<MinigameResult> completeInspection)
    {
        if (settings is FlippyBoxSettings flippyBoxSettings)
        {
            FlippyBoxMinigamePanel.Instance.StartMinigame(flippyBoxSettings, completeInspection);
        }
        else if(settings is WinLoseMinigameSettings winLoseSettings)
        {
            WinLoseMinigamePanel.Instance.StartMinigame(completeInspection);
        }
    }

}

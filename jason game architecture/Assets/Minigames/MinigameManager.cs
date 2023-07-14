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

    public void StartMinigame(FlippyBoxSettings settings, Action<MinigameResult> completeInspection)
    {
        FlippyBoxMinigamePanel.Instance.StartMinigame(settings, completeInspection);
    }

    internal void StartMinigame(object minigameSettings, Action<MinigameResult> handleMinigameCompleted)
    {
        throw new NotImplementedException();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippyBoxMinigamePanel : MonoBehaviour
{
    Action<MinigameResult> completeInspectionLocal;

    public static FlippyBoxMinigamePanel Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void StartMinigame(Action<MinigameResult> completeInspection)
    {
        completeInspectionLocal = completeInspection;
        foreach(var restartable in GetComponentsInChildren<IRestart>()) 
        {
            restartable.Restart();
        }
        gameObject.SetActive(true);
    }

    public void Win()
    {
        completeInspectionLocal?.Invoke(MinigameResult.Won);
        completeInspectionLocal = null;
        gameObject.SetActive(false);
    }

    public void Lose()
    {
        completeInspectionLocal?.Invoke(MinigameResult.Lost);
        completeInspectionLocal = null;
        gameObject.SetActive(false);
    }
}


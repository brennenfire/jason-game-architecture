using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippyBoxMinigamePanel : MonoBehaviour
{
    [SerializeField] FlippyBoxSettings defaultSettings;

    Action<MinigameResult> completeInspectionLocal;

    public FlippyBoxSettings CurrentSettings { get; private set; }
    public static FlippyBoxMinigamePanel Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameObject.SetActive(false);
        if(transform.parent == null) 
        {
            StartMinigame(defaultSettings, (result) => Debug.Log(result));
        }
    }

    public void StartMinigame(FlippyBoxSettings settings , Action<MinigameResult> completeInspection)
    {
        CurrentSettings = settings ?? defaultSettings;
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


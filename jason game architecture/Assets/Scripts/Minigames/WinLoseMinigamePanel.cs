using System;
using UnityEngine;

public class WinLoseMinigamePanel : MonoBehaviour
{
    Action<MinigameResult> completeInspectionLocal;

    public static WinLoseMinigamePanel Instance {  get; private set; }

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

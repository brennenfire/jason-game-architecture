using System;
using UnityEngine;

public class WinLoseMinigamePanel : MonoBehaviour
{
    Action completeInspectionLocal;

    public static WinLoseMinigamePanel Instance {  get; private set; }

    void Awake()
    {
        Instance = this;    
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void StartMinigame(Action completeInspection)
    {
        completeInspectionLocal = completeInspection;
        gameObject.SetActive(true);
    }

    public void Win()
    {
        completeInspectionLocal?.Invoke();
        completeInspectionLocal = null;
        gameObject.SetActive(false);
    }

    public void Lose()
    {
        completeInspectionLocal = null;
        gameObject.SetActive(false);
    }
}
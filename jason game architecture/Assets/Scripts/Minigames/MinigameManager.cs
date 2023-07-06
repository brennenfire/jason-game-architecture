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

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y)) 
        {
            completeInspectionLocal?.Invoke();
            completeInspectionLocal = null;
        }
    }

    public void StartMinigame(Action completeInspection)
    {
        completeInspectionLocal = completeInspection;
    }

}

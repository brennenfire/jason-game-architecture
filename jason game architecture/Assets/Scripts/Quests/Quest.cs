using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Build.Reporting;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest")]
public class Quest : ScriptableObject
{
    public event Action Changed;

    [SerializeField] string displayName;
    [SerializeField] string description;

    [Tooltip("designer / programmer notes, not visible to player")]
    [SerializeField] string notes;

    [SerializeField] Sprite sprite;

    public List<Step> steps;

    int currentStepIndex;

    public string Description => description;
    public string DisplayName => displayName;
    public Sprite Sprite => sprite;

    public Step CurrentStep => steps[currentStepIndex];

    void OnEnable()
    {
        currentStepIndex = 0;
        foreach(var step in steps)
        {
            foreach(var objective in step.Objectives) 
            {
                if(objective.GameFlag != null)
                {
                    objective.GameFlag.Changed += HandleFlagChanged;
                }
            }
        }
    }

    void HandleFlagChanged()
    {
        TryProgress();
        Changed?.Invoke();
    }

    internal void TryProgress()
    {
        var currentStep = GetCurrentStep();
        if(currentStep.HasAllObjectivesCompleted())
        {
            currentStepIndex++;
            Changed?.Invoke();
        }
    }

    Step GetCurrentStep()
    {
        return steps[currentStepIndex];
    }
}


[Serializable]
public class Step
{
    [SerializeField] string instructions;
    public string Instructions => instructions;
    public List<Objective> Objectives;

    internal bool HasAllObjectivesCompleted()
    {
        return Objectives.TrueForAll(t => t.IsCompleted);
    }
}

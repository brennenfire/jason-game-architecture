using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest")]
public class Quest : ScriptableObject
{
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

    internal void TryProgress()
    {
        var currentStep = GetCurrentStep();
        if(currentStep.HasAllObjectivesCompleted())
        {
            currentStepIndex++;

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

[Serializable]
public class Objective
{
    [SerializeField] ObjectiveType objectiveType;

    public bool IsCompleted { get; internal set; }

    public enum ObjectiveType
    {
        Flag,
        Item,
        Kill
    }

    public override string ToString()
    {
        return objectiveType.ToString();
    }
}
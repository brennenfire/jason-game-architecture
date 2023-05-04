using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Build.Reporting;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest")]
public class Quest : ScriptableObject
{
    public event Action Progressed;

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
    }

    internal void TryProgress()
    {
        var currentStep = GetCurrentStep();
        if(currentStep.HasAllObjectivesCompleted())
        {
            currentStepIndex++;
            Progressed?.Invoke();
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
    [SerializeField] GameFlag gameFlag;

    public enum ObjectiveType
    {
        Flag,
        Item,
        Kill
    }

    public bool IsCompleted
    {
        get
        {
            switch(objectiveType) 
            {
                case ObjectiveType.Flag: return gameFlag.Value;
                default: return false;
            }
        }
    }

    public override string ToString()
    {
        switch (objectiveType)
        {
            case ObjectiveType.Flag: return gameFlag.name;
            default: return objectiveType.ToString();
        }
    }

    //public override string ToString() => objectiveType.ToString();
}
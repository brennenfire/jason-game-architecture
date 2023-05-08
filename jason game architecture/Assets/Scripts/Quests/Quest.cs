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
                if(objective.GameFlagBool != null)
                {
                    objective.GameFlagBool.Changed += HandleFlagChanged;
                }
                if(objective.GameFlagInt != null)
                {
                    objective.GameFlagInt.Changed += HandleFlagChanged;
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

[Serializable]
public class Objective
{
    [SerializeField] ObjectiveType objectiveType;
    [SerializeField] BoolGameFlag boolGameFlag;

    [Header("int game flags")]
    [SerializeField] IntGameFlag intGameFlag;

    [Tooltip("required amount for the counted int game flag")]
    [SerializeField] int required;

    public BoolGameFlag GameFlagBool => boolGameFlag;
    public IntGameFlag GameFlagInt => intGameFlag;

    public enum ObjectiveType
    {
        BoolFlag,
        CountedIntFlag,
        Item,
        Kill
    }

    public bool IsCompleted
    {
        get
        {
            switch(objectiveType) 
            {
                case ObjectiveType.BoolFlag: return boolGameFlag.Value;
                case ObjectiveType.CountedIntFlag: return intGameFlag.Value >= required;
                default: return false;
            }
        }
    }


    public override string ToString()
    {
        switch (objectiveType)
        {
            case ObjectiveType.BoolFlag: return boolGameFlag.name;
            case ObjectiveType.CountedIntFlag: return $"{intGameFlag.name} ({intGameFlag.Value} / {required})";
            default: return objectiveType.ToString();
        }
    }

    //public override string ToString() => objectiveType.ToString();
}
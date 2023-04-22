using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string description;

    [Tooltip("designer / programmer notes, not visible to player")]
    [SerializeField] string notes;
    
    public List<Step> steps;
}


[Serializable]
public class Step
{
    [SerializeField] string instructions;
    public List<Objective> objectives;
}

[Serializable]
public class Objective
{
    [SerializeField] ObjectiveType objectiveType;
    public enum ObjectiveType
    {
        Flag,
        Item,
        Kill
    }
}
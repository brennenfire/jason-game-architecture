using System;
using UnityEngine;

[Serializable]
public class Objective
{
    [SerializeField] ObjectiveType objectiveType;
    [SerializeField] GameFlag gameFlag;

    //[Header("int game flags")]
    //[SerializeField] IntGameFlag intGameFlag;

    [Tooltip("required amount for the counted int game flag")]
    [SerializeField] int required;

    public GameFlag GameFlag => gameFlag;
    //public IntGameFlag GameFlagInt => intGameFlag;

    public enum ObjectiveType
    {
        GameFlag,
        Item,
        Kill
    }

    public bool IsCompleted
    {
        get
        {
            switch(objectiveType) 
            {
                case ObjectiveType.GameFlag:
                    {
                        if (gameFlag is BoolGameFlag boolGameFlag)
                        {
                            return boolGameFlag.Value;
                        }
                        if (gameFlag is IntGameFlag intGameFlag)
                        {
                            return intGameFlag.Value >= required;
                        }
                        return false;
                    }
                default: return false;
            }
        }
    }


    public override string ToString()
    {
        switch (objectiveType)
        {
            case ObjectiveType.GameFlag:
                {
                    if (gameFlag is BoolGameFlag boolGameFlag)
                    {
                        return gameFlag.name;
                    }
                    if(gameFlag is IntGameFlag intGameFlag)
                    {
                        return $"{intGameFlag.name} ({intGameFlag.Value} / {required})";
                    }
                    return "Unknown objective type";
                }
            //case ObjectiveType.CountedIntFlag: return $"{intGameFlag.name} ({intGameFlag.Value} / {required})";
            default: return objectiveType.ToString();
        }
    }

    //public override string ToString() => objectiveType.ToString();
}
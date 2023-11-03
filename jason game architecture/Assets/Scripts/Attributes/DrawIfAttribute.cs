using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawIfAttribute : PropertyAttribute
{
    #region Fields

    public string comparedPropertyName { get; set; }

    public object comparedValue { get; set;}

    public DisablingType disablingType { get; set; } 
    
    public enum DisablingType
    {
        ReadOnly = 2,
        DontDraw = 3
    }

    #endregion

    public DrawIfAttribute(string comparedPropertyName, object comparedValue, DisablingType disabingType = DisablingType.DontDraw)
    {
        this.comparedPropertyName = comparedPropertyName;
        this.comparedValue = comparedValue;
        this.disablingType = disabingType;
    }
}

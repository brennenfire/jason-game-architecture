using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InspectionManager : MonoBehaviour
{
    static Inspectable currentInspectable;

    public static float InspectionProgress => currentInspectable?.InspectionProgress ?? 0f;
    public static bool Inspecting => currentInspectable != null && currentInspectable.isActiveAndEnabled; 

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            currentInspectable = Inspectable.InspectablesInRange.FirstOrDefault();
        }
        if(Input.GetKey(KeyCode.E) && currentInspectable != null) 
        {
            currentInspectable.Inspect();
        }
        else
        {
            currentInspectable = null;
        }
    }
}

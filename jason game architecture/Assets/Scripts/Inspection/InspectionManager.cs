using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InspectionManager : MonoBehaviour
{
    private Inspectable currentInspectable;

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
    }
}

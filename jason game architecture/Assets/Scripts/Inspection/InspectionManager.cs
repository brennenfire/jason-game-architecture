using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InspectionManager : MonoBehaviour
{
    static Inspectable currentInspectable;

    public static float InspectionProgress => currentInspectable?.InspectionProgress ?? 0f;
    public static bool Inspecting => currentInspectable != null && currentInspectable.WasFullyInspected == false;

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

    public static void Bind(List<InspectableData> datas)
    {
        var allInspectables = GameObject.FindObjectsOfType<Inspectable>(true);
        foreach(var inspectable in allInspectables) 
        {
            var data = datas.FirstOrDefault(t => t.Name == inspectable.name);
            if (data == null)
            {
                data = new InspectableData() { Name = inspectable.name };
                datas.Add(data);
            }
            inspectable.Bind(data);
        }
    }
}

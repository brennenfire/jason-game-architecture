using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] GameObject previewObject;
    [SerializeField] GameObject placedObject;
    [SerializeField] List<Renderer> tintedRenderers;
    [SerializeField] Color defaultColor;
    [SerializeField] Color invalidColor;
    InRangeOfPlayerValidator[] validators;

    public bool IsPlacementValid { get; private set; }

    void Awake()
    {
        validators = GetComponents<InRangeOfPlayerValidator>();    
    }

    public void Place()
    {
        previewObject.SetActive(false);
        placedObject.SetActive(true);
    }

    public void SetPosition(Vector3 point)
    {
        transform.position = point; 
        IsPlacementValid = true;

        foreach (var validator in validators)
        {
            if(validator.IsValid() == false)
            {
                IsPlacementValid = false;
                break;
            }
        }

        foreach (var renderer  in tintedRenderers)
        {
            renderer.material.color = IsPlacementValid ? defaultColor : invalidColor;    
        }
    }
}

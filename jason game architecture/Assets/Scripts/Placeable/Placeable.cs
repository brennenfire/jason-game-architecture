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

    public bool IsPlacementValid { get; private set; }

    public void Place()
    {
        previewObject.SetActive(false);
        placedObject.SetActive(true);
    }

    public void SetPosition(Vector3 point)
    {
        transform.position = point; 
        IsPlacementValid = true;

        if(Vector3.Distance(transform.position, FindObjectOfType<ThirdPersonMover>().transform.position) > 10f)
        {
            IsPlacementValid = false;
        }

        foreach (var renderer  in tintedRenderers)
        {
            renderer.material.color = IsPlacementValid ? defaultColor : invalidColor;    
        }
    }
}

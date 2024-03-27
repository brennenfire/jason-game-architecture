using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] GameObject previewObject;
    [SerializeField] GameObject placedObject;
    [SerializeField] List<Renderer> tintedRenderers;
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color invalidColor = Color.red;
    IValidatePlacement[] validators;

    public bool IsPlacementValid { get; private set; }

    void Awake()
    {
        validators = GetComponents<IValidatePlacement>();
        previewObject.SetActive(true);
        placedObject.SetActive(false);
    }

    public void Place()
    {
        previewObject.SetActive(false);
        placedObject.SetActive(true);
    }

    public void SetPositionAndValidate(Vector3 point, Quaternion orientation)
    {
        transform.SetPositionAndRotation(point, orientation);
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
            if (renderer != null)
            {
                renderer.material.color = IsPlacementValid ? defaultColor : invalidColor;
            }
            else
            {
                Debug.LogError("missing error on placeable", gameObject);
            }
        }
    }
}

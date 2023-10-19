using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] GameObject previewObject;
    [SerializeField] GameObject placedObject;

    public void Place()
    {
        previewObject.SetActive(false);
        placedObject.SetActive(true);
    }
}

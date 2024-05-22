using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentVisuals : MonoBehaviour
{
    public List<EquipmentVisual> Visuals;
    [SerializeField] Inventory inventory;

    void Start()
    {
        foreach (var visual in Visuals)
        {
            visual.DefaultMaterial = visual.SkinnedMeshRenderers.FirstOrDefault()?.material;
        }
        foreach (var slot in inventory.EquipmentSlots)
        {
            slot.Changed += (added, removed) => UpdateEquipmentVisual(slot);
            UpdateEquipmentVisual(slot);
        }    
    }

    void OnValidate()
    {
        inventory = GetComponent<Inventory>();    
    }

    void UpdateEquipmentVisual(ItemSlot slot)
    {
        foreach (var visual in Visuals.Where(t => t.EquipmentSlotType == slot.EquipmentSlotType))
        {
            foreach (var skinnedMeshRenderer in visual.SkinnedMeshRenderers)
            {
                skinnedMeshRenderer.material = slot.Item?.EquipMaterial ?? visual.DefaultMaterial;
            }

            if(visual.VisualModelRoot != null) 
            {
                for (int i = 0; i < visual.VisualModelRoot.childCount; i++)
                {
                    var model = visual.VisualModelRoot.GetChild(i);
                    model.gameObject.SetActive(model.name == slot.Item?.ModelName);
                }
            }

        }    
    }
}


[Serializable]

public class EquipmentVisual
{
    public EquipmentSlotType EquipmentSlotType;
    public List<SkinnedMeshRenderer> SkinnedMeshRenderers;
    public Material DefaultMaterial;
    public Transform VisualModelRoot;
}
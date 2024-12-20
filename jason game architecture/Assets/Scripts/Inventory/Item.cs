using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public List<EquipmentSlotType> EquipmentSlotTypes;
    public Material EquipMaterial;

    public Sprite Icon;
    public string Name;
    public string ModelName;

    [Multiline(3)]
    public string Description;

    public int MaxStackSize;

    public Placeable PlaceablePrefab;
    public List<StatMod> StatMods;

    [ContextMenu("Add 1")] public void Add1() => Inventory.Instance.AddItem(this);
   [ContextMenu("Add 5")] public void Add5() { for (int i = 0; i < 5; i++) { Inventory.Instance.AddItem(this); } }
   [ContextMenu("Add 10")] public void Add10() { for (int i = 0; i < 10; i++) { Inventory.Instance.AddItem(this); } }

}

using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelSlot : MonoBehaviour
{
    ItemSlot itemSlotLocal;
    [SerializeField] Image itemIconLocal;

    public void Bind(ItemSlot itemSlot)
    {
        itemSlotLocal = itemSlot;

        if (itemSlotLocal.Item != null)
        {
            itemIconLocal.sprite = itemSlotLocal.Item.Icon;
            itemIconLocal.enabled = true;
        }
        else
        {
            itemIconLocal.sprite = null;
            itemIconLocal.enabled = false;
        }
    }
}
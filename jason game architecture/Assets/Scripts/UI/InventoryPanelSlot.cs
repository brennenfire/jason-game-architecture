using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPanelSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    ItemSlot itemSlotLocal;
    [SerializeField] Image draggedItemIcon;
    [SerializeField] Image itemIconLocal;
    [SerializeField] Outline outline;
    [SerializeField] Color draggingColor = Color.gray;

    public void Bind(ItemSlot itemSlot)
    {
        itemSlotLocal = itemSlot;
        itemSlotLocal.Changed += UpdateIcon;
        UpdateIcon();
    }

    void UpdateIcon()
    {
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outline.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(itemSlotLocal.IsEmpty)
        {
            return;
        }

        itemIconLocal.color = draggingColor;
        draggedItemIcon.sprite = itemIconLocal.sprite;
        draggedItemIcon.enabled = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemIconLocal.color = Color.white;
        draggedItemIcon.sprite = null;
        draggedItemIcon.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        draggedItemIcon.transform.position = eventData.position;
    }
}
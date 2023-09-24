using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPanelSlot : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IBeginDragHandler,
    IEndDragHandler,
    IDragHandler,
    IPointerClickHandler
{
    public static InventoryPanelSlot Focused;

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
        Focused = this;
        outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(Focused == this)
        {
            Focused = null;
        }
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
        if (Focused == null && Input.GetKey(KeyCode.LeftShift))
        {
            itemSlotLocal.RemoveItem();
        }

        if (itemSlotLocal.IsEmpty == false && Focused != null)
        {
            itemSlotLocal.Swap(Focused.itemSlotLocal);
        }

        itemIconLocal.color = Color.white;
        draggedItemIcon.sprite = null;
        draggedItemIcon.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        draggedItemIcon.transform.position = eventData.position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.Tab) && itemSlotLocal.IsEmpty == false)
        {
            Inventory.Instance.AddItem(itemSlotLocal.Item);
        }
        else
        {
            ItemTooltipPanel.Instance.ShowItem(itemSlotLocal.Item);
        }
    }

    internal void Bind(object topOverflowSlot)
    {
        throw new NotImplementedException();
    }
}
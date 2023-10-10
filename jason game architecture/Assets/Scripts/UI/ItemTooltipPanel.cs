using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemTooltipPanel : MonoBehaviour, IPointerClickHandler
{
    public static ItemTooltipPanel Instance { get; private set; }

    [SerializeField] TMP_Text name;
    [SerializeField] TMP_Text description;
    [SerializeField] Image icon;
    [SerializeField] Button placeButton;

    CanvasGroup canvasGroup;

    void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        Toggle(false);  
    }

    public void ShowItem(Item item)
    {
        if (item == null)
        {
            Toggle(false);
        }
        else
        {
            Toggle(true);
            name.SetText(item.name);
            description.SetText(item.Description);
            icon.sprite = item.Icon;
            placeButton.gameObject.SetActive(item.PlaceablePrefab != null);
        }
    }

    void Toggle(bool visible)
    {
        canvasGroup.alpha = visible ? 1f : 0f;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle(false);
    }
}

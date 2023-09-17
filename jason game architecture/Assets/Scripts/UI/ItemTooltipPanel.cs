using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltipPanel : MonoBehaviour
{
    public static ItemTooltipPanel Instance { get; private set; }

    [SerializeField] TMP_Text name;
    [SerializeField] TMP_Text description;
    [SerializeField] Image icon;

    CanvasGroup canvasGroup;

    void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
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
        }
    }

    void Toggle(bool visible)
    {
        canvasGroup.alpha = visible ? 1f : 0f;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltipPanel : MonoBehaviour
{
    [SerializeField] TMP_Text name;
    [SerializeField] TMP_Text description;
    [SerializeField] Image icon;

    public static ItemTooltipPanel Instance { get; private set; }

    void Awake()
    {
        Instance = this;    
    }

    public void ShowItem(Item item)
    {
        name.SetText(item.name);
        description.SetText(item.Description);
        icon.sprite = item.Icon;
    }
}

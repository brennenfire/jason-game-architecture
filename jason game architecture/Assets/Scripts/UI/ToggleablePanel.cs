using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ToggleablePanel : MonoBehaviour
{
    CanvasGroup canvasGroup;
    static HashSet<ToggleablePanel> visiblePanels = new HashSet<ToggleablePanel>();

    public static bool AnyVisible => visiblePanels.Any();
    public bool IsVisible => canvasGroup.alpha > 0;

    [SerializeField] KeyCode Hotkey;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    void Update()
    {
        if(Input.GetKeyDown(Hotkey)) 
        {
            ToggleState();
        }
        else if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            Hide();
        }
    }

    void ToggleState()
    {
        if(IsVisible)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    protected void Show()
    {
        visiblePanels.Add(this);
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        visiblePanels.Remove(this);
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
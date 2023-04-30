using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ToggleablePanel : MonoBehaviour
{
    CanvasGroup canvasGroup;
    static HashSet<ToggleablePanel> visiblePanels = new HashSet<ToggleablePanel>();

    public static bool IsVisible => visiblePanels.Any();

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    protected void Show()
    {
        visiblePanels.Add(this);
        canvasGroup.alpha = 0.5f;
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